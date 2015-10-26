﻿using System;
using NUnit.Framework;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using XmlUnit.Xunit;

namespace MSTestAllureAdapter.Tests
{
    [TestFixture]
    public class GeneratedReportTests
    {
        string mTargetDir = "results";
        
        string mValidTrxFile = Path.Combine("trx", "sample.trx");
        
        private void DeleteTargetDir()
        {
            if (Directory.Exists(mTargetDir))
                Directory.Delete(mTargetDir, true);
        }

        [SetUp]
        public void SetUp()
        {
            DeleteTargetDir();
            Directory.CreateDirectory(mTargetDir);
        }

        [Test]
        public void GeneratedFilesHaveCorrectSchema()
        {
            RunReport(mValidTrxFile);
            
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreWhitespace = true;
            readerSettings.Schemas.Add(null, Path.Combine("xsd", "allure.xsd"));
            readerSettings.ValidationType = ValidationType.Schema;

            string[] files = Directory.GetFiles(mTargetDir, "*.xml");

            if (files.Length == 0)
            {
                Assert.Fail("No generated files were found.");
            }

            foreach (string file in files)
            {
                // XmlDocument.Load will also work but we don't want to load the entire XML to memory.
                XmlReader xmlReader = XmlReader.Create(file, readerSettings);
                while (xmlReader.Read());
            }
        }

        [Test]
        public void CompareExpectedXMLs()
        {
            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> actual = new Dictionary<string, string>();

            RunReport(mValidTrxFile);
            
            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
            xmlNamespaceManager.AddNamespace("prefix", "urn:model.allure.qatools.yandex.ru");

            DiffConfiguration diffConfiguration = new DiffConfiguration(String.Empty, false, WhitespaceHandling.None, true);
            
            FillCategoryToXmlMap("sample-output", expected);
            FillCategoryToXmlMap(mTargetDir, actual);

            if (expected.Keys.Count != actual.Keys.Count)
            {
                Assert.Fail("Expected {0} categories but found {1}.", expected.Keys.Count, actual.Keys.Count);
            }

            foreach (string category in actual.Keys)
            {
                if (!expected.ContainsKey(category))
                {
                    Assert.Fail("The category " + category + " was not expected.");
                }

                string expectedFile = expected[category];
                string actualFile = actual[category];

                string expectedFileText = File.ReadAllText(expectedFile);
                string actualFileText = File.ReadAllText(actualFile);

                XmlInput control = new XmlInput(expectedFileText);
                XmlInput test = new XmlInput(actualFileText);
                
                XmlDiff xmlDiff = new XmlDiff(control, test, diffConfiguration);
                    
                DiffResult diffResult = xmlDiff.Compare();
                if (!diffResult.Identical)
                {
                    string failureMessage = String.Format("The expected file {0} was different from the actual file {1}", expectedFile, actualFile);
                    failureMessage += Environment.NewLine;
                    failureMessage += "Expected XML: ";
                    failureMessage += expectedFileText;
                    failureMessage += Environment.NewLine;
                    failureMessage += "Actual XML: ";
                    failureMessage += actualFileText;
                    failureMessage += Environment.NewLine;
                    failureMessage += "Difference: ";
                    failureMessage += diffResult.Difference;

                    Assert.Fail(failureMessage);
                }
            }
        }

        private void FillCategoryToXmlMap(string source, IDictionary<string, string> map)
        {
            string[] files = Directory.GetFiles(source, "*.xml");

            foreach (string file in files)
            {
                string category = String.Empty;
                XDocument xDoc = XDocument.Load(file);
                XElement element = xDoc.Root.XPathSelectElement("./name");

                if (element != null)
                {
                    category = element.Value;
                }
                
                map[category] = file;
            }
        }
        
        private void RunReport(string trxPath)
        {
            AllureAdapter adapter = new AllureAdapter();
            
            ITestResultProvider testResultsProvider = new TRXParser();
            
            IEnumerable<MSTestResult> testResults = testResultsProvider.GetTestResults(trxPath);

            adapter.GenerateTestResults(testResults, mTargetDir);
        }
    }
}

