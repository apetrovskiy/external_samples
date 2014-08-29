﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Runner.cs" company="NUnit.MultiCore Development Team">
//   NUnit.MultiCore Development Team
// </copyright>
// <summary>
//  Defines the Runner used to run the tests in a specified assembly and output 
//  the results as xml to a file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace NUNit.MultiCore.ConsoleTestRunner
{
    using System.Reflection;
	using NUnit.Core;

    // using NUnit.Core;
    using NUnit.MultiCore.TestRunner;
    using NUnit.Util;

    /// <summary>
    /// Runner used to run the tests in a specified assembly and output the results as 
    /// xml to a file.
    /// </summary>
    public class Runner
    {
        /// <summary>
        /// Run the tests in the specified assembly and output the results as xml to
        /// the specified file.
        /// </summary>
        /// <param name="fullAssemblyPath">The full assembly path.</param>
        /// <param name="outputXmlPath">The output xml path.</param>
        /// <returns>False if any tests failed or any errors occurred, otherwise true</returns>
        public bool RunTests(string fullAssemblyPath, string outputXmlPath)
        {
            var assemblyToTest = Assembly.LoadFrom(fullAssemblyPath);

            var results = new ParallelTestRunner().RunTestsInParallel(assemblyToTest);
            SaveXmlOutput(results, outputXmlPath);
        	return !results.Results.Cast<TestResult>().Any(x => x.IsFailure || x.IsError);
        }

        /// <summary>
        /// Saves the TestResult as xml to the specified path.
        /// </summary>
        /// <param name="result">The test result to be saved.</param>
        /// <param name="outputXmlPath">The output xml path.</param>
        private static void SaveXmlOutput(TestResult result, string outputXmlPath)
        {
            if (!string.IsNullOrEmpty(outputXmlPath))
            {
                var writer = new XmlResultWriter(outputXmlPath);
                writer.SaveTestResult(result);
            }
        }
    }
}