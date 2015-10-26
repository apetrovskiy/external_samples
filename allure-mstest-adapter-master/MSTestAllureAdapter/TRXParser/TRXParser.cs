﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;

namespace MSTestAllureAdapter
{
    /// <summary>
    /// MSTest TRX parser.
    /// </summary>
    public class TRXParser : ITestResultProvider
    {
        // this namespace is required whenever using linq2xml on the trx.
        // for aesthetic reasons the naming convention was violated.
        private static readonly XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

        /// <summary>
        /// Parses the test results from the supplied trx file.
        /// </summary>
        /// <returns>The parsed test results.</returns>
        /// <param name="filePath">File path to the trx file.</param>
        public IEnumerable<MSTestResult> GetTestResults(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            IEnumerable<XElement> unitTests = doc.Descendants(ns + "UnitTest");

            IEnumerable<XElement> unitTestResults = doc.Descendants(ns + "UnitTestResult");

            Func<XElement, string> outerKeySelector = _ => _.Element(ns + "Execution").Attribute("id").Value;
            Func<XElement, string> innerKeySelector = _ => _.Attribute("executionId").Value;
            Func<XElement, XElement, MSTestResult> resultSelector = CreateMSTestResult;

            IEnumerable<MSTestResult> result = unitTests.Join<XElement, XElement, string, MSTestResult>(unitTestResults, outerKeySelector, innerKeySelector, resultSelector);
             
            // this will return the flat list of the tests with the inner tests.
            // here a test 'parent' that holds other tests will be discarded (such as the data driven tests).
            // result = result.EnumerateTestResults();

            return result;
        }

        private ErrorInfo ParseErrorInfo(XElement errorInfoXmlElement)
        {
            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
            xmlNamespaceManager.AddNamespace("prefix", ns.NamespaceName);

            errorInfoXmlElement = errorInfoXmlElement.Element(ns + "Output");
            
            XElement messageElement = errorInfoXmlElement.XPathSelectElement("prefix:ErrorInfo/prefix:Message", xmlNamespaceManager);
            
            string message = (messageElement != null) ? messageElement.Value : null;

            XElement stackTraceElement = errorInfoXmlElement.XPathSelectElement("prefix:ErrorInfo/prefix:StackTrace", xmlNamespaceManager);
            
            string stackTrace = (stackTraceElement != null) ? stackTraceElement.Value : null;

            XElement stdOutElement = errorInfoXmlElement.XPathSelectElement("prefix:StdOut", xmlNamespaceManager);
            
            string stdOut = (stdOutElement != null) ? stdOutElement.Value : null;

            return new ErrorInfo(message, stackTrace, stdOut);
        }

        private class UnitTestData
        {
            public string Name { get; set; }
            public string Owner { get; set; }
            public string Description { get; set; }
            public IEnumerable<string> Suits { get; set; }
             
        }
        
        private MSTestResult CreateMSTestResult(XElement unitTest, XElement unitTestResult)
        {
            UnitTestData unitTestData = new UnitTestData();
            unitTestData.Name = unitTest.GetSafeAttributeValue(ns + "TestMethod", "name");
            unitTestData.Owner = GetOwner(unitTest);
            unitTestData.Description = GetDescription(unitTest);
            unitTestData.Suits = (from testCategory in unitTest.Descendants(ns + "TestCategoryItem")
                select testCategory.GetSafeAttributeValue("TestCategory")).ToList<string>();
            
            return CreateMSTestResultInternal(unitTestData, unitTestResult);
        }
        
        private MSTestResult CreateMSTestResultInternal(UnitTestData unitTestData, XElement unitTestResult)
        {
            string dataRowInfo = unitTestResult.GetSafeAttributeValue("dataRowInfo");

            // in data driven tests this appends the input row number to the test name
            string unitTestName = unitTestData.Name;
            
            unitTestName += dataRowInfo;

            TestOutcome outcome = (TestOutcome)Enum.Parse(typeof(TestOutcome), unitTestResult.Attribute("outcome").Value);

            DateTime start = DateTime.Parse(unitTestResult.Attribute("startTime").Value);

            DateTime end = DateTime.Parse(unitTestResult.Attribute("endTime").Value);

            /*
            if (categories.Length == 0)
                categories = new string[]{ DEFAULT_CATEGORY };
            */

            IEnumerable<MSTestResult> innerTestResults = ParseInnerTestResults(unitTestData, unitTestResult);

            MSTestResult testResult = new MSTestResult(unitTestName, outcome, start, end, unitTestData.Suits, innerTestResults);

            bool containsInnerTestResults = unitTestResult.Element(ns + "InnerResults") == null;
            if ((outcome == TestOutcome.Error || outcome == TestOutcome.Failed) && containsInnerTestResults)
            {
                testResult.ErrorInfo = ParseErrorInfo(unitTestResult);
            }

            testResult.Owner = unitTestData.Owner;

            testResult.Description = unitTestData.Description;
            
            return testResult;
        }

        private IEnumerable<MSTestResult> ParseInnerTestResults(UnitTestData unitTestData, XElement unitTestResult)
        {
            IEnumerable<XElement> innerResultsElements = unitTestResult.Descendants(ns + "InnerResults");

            if (!innerResultsElements.Any())
                return null;

            // the schema for the trx states there can be multiple 'InnerResults' elements but
            // until we see it we take the first.
            // In the future if it will be required to handle multiple 'InnerResults' elements 
            // one can wrap the comming loop in another loop that loops over them.
            XElement innerResultsElement = innerResultsElements.FirstOrDefault<XElement>();

            IList<MSTestResult> result = new List<MSTestResult>();

            foreach (XElement innerUnitTestResult in innerResultsElement.Descendants(ns + "UnitTestResult"))
            {
                    result.Add(CreateMSTestResultInternal(unitTestData, innerUnitTestResult));
            }

            return result;
        }

        private string GetOwner(XElement unitTestElement)
        {
            string owner = null;

            XElement ownerElement = unitTestElement.Descendants(ns + "Owner").FirstOrDefault();

            if (ownerElement != null)
            {
                XAttribute ownerAttribute = ownerElement.Attribute("name");
                owner = ownerAttribute.Value;
            }

            return owner;
        }

        private string GetDescription(XElement unitTestElement)
        {
            string description = null;

            XElement descriptionElement = unitTestElement.Element(ns + "Description");

            if (descriptionElement != null)
            {
                description = descriptionElement.Value;
            }

            return description;
        }
    }
}

