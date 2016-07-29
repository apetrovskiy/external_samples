﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SpecflowPlayground.RegexSamples
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("RegexSamples")]
    public partial class RegexSamplesFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "RegexSamples.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "RegexSamples", "\tIn order to show off Specflow\r\n\tAs a Specflow enthusiast\r\n\tI want to demo common" +
                    " regex for Specflow steps", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Limit argument options to enum values")]
        public virtual void LimitArgumentOptionsToEnumValues()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Limit argument options to enum values", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table1.AddRow(new string[] {
                        "Galaxy IV"});
            table1.AddRow(new string[] {
                        "iPhone"});
            table1.AddRow(new string[] {
                        "Windows Phone"});
            table1.AddRow(new string[] {
                        "Note"});
            table1.AddRow(new string[] {
                        "Kindle"});
#line 7
 testRunner.Given("the following devices", ((string)(null)), table1, "Given ");
#line 14
 testRunner.When("I sort by product name ascending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name",
                        "Index"});
            table2.AddRow(new string[] {
                        "Galaxy IV",
                        "1"});
            table2.AddRow(new string[] {
                        "iPhone",
                        "2"});
            table2.AddRow(new string[] {
                        "Kindle",
                        "3"});
            table2.AddRow(new string[] {
                        "Note",
                        "4"});
            table2.AddRow(new string[] {
                        "Windows Phone",
                        "5"});
#line 15
 testRunner.Then("they should be in the following order", ((string)(null)), table2, "Then ");
#line 22
 testRunner.When("I sort by product name descending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name",
                        "Index"});
            table3.AddRow(new string[] {
                        "Windows Phone",
                        "1"});
            table3.AddRow(new string[] {
                        "Note",
                        "2"});
            table3.AddRow(new string[] {
                        "Kindle",
                        "3"});
            table3.AddRow(new string[] {
                        "iPhone",
                        "4"});
            table3.AddRow(new string[] {
                        "Galaxy IV",
                        "5"});
#line 23
 testRunner.Then("they should be in the following order", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sort using a step argument transformation")]
        public virtual void SortUsingAStepArgumentTransformation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sort using a step argument transformation", ((string[])(null)));
#line 31
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table4.AddRow(new string[] {
                        "Galaxy IV"});
            table4.AddRow(new string[] {
                        "iPhone"});
            table4.AddRow(new string[] {
                        "Windows Phone"});
            table4.AddRow(new string[] {
                        "Note"});
            table4.AddRow(new string[] {
                        "Kindle"});
#line 32
 testRunner.Given("the following devices", ((string)(null)), table4, "Given ");
#line 39
 testRunner.When("I sort by product name from A-Z", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name",
                        "Index"});
            table5.AddRow(new string[] {
                        "Galaxy IV",
                        "1"});
            table5.AddRow(new string[] {
                        "iPhone",
                        "2"});
            table5.AddRow(new string[] {
                        "Kindle",
                        "3"});
            table5.AddRow(new string[] {
                        "Note",
                        "4"});
            table5.AddRow(new string[] {
                        "Windows Phone",
                        "5"});
#line 40
 testRunner.Then("they should be in the following order", ((string)(null)), table5, "Then ");
#line 47
 testRunner.When("I sort by product name from Z-A", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name",
                        "Index"});
            table6.AddRow(new string[] {
                        "Windows Phone",
                        "1"});
            table6.AddRow(new string[] {
                        "Note",
                        "2"});
            table6.AddRow(new string[] {
                        "Kindle",
                        "3"});
            table6.AddRow(new string[] {
                        "iPhone",
                        "4"});
            table6.AddRow(new string[] {
                        "Galaxy IV",
                        "5"});
#line 48
 testRunner.Then("they should be in the following order", ((string)(null)), table6, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Convert 1st, 2nd, etc to integral values")]
        public virtual void Convert1St2NdEtcToIntegralValues()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Convert 1st, 2nd, etc to integral values", ((string[])(null)));
#line 56
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table7.AddRow(new string[] {
                        "Galaxy IV"});
            table7.AddRow(new string[] {
                        "iPhone"});
            table7.AddRow(new string[] {
                        "Windows Phone"});
            table7.AddRow(new string[] {
                        "Note"});
            table7.AddRow(new string[] {
                        "Kindle"});
            table7.AddRow(new string[] {
                        "Blackberry Storm"});
            table7.AddRow(new string[] {
                        "iPad"});
            table7.AddRow(new string[] {
                        "Surface"});
            table7.AddRow(new string[] {
                        "Surface Pro"});
            table7.AddRow(new string[] {
                        "HTC One"});
#line 57
 testRunner.Given("the following devices", ((string)(null)), table7, "Given ");
#line 69
 testRunner.When("I remove the 10th item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 70
 testRunner.And("I remove the 4th item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And("I remove the 3rd item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("I remove the 2nd item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And("I remove the 1st item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table8.AddRow(new string[] {
                        "Kindle"});
            table8.AddRow(new string[] {
                        "Blackberry Storm"});
            table8.AddRow(new string[] {
                        "iPad"});
            table8.AddRow(new string[] {
                        "Surface"});
            table8.AddRow(new string[] {
                        "Surface Pro"});
#line 74
 testRunner.Then("the following devices remain", ((string)(null)), table8, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Support singular and plural wording")]
        public virtual void SupportSingularAndPluralWording()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Support singular and plural wording", ((string[])(null)));
#line 82
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table9.AddRow(new string[] {
                        "Galaxy IV"});
            table9.AddRow(new string[] {
                        "iPhone"});
#line 83
 testRunner.Given("the following devices", ((string)(null)), table9, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table10.AddRow(new string[] {
                        "Windows Phone"});
#line 87
 testRunner.And("the following device", ((string)(null)), table10, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Make \"I\" optional")]
        public virtual void MakeIOptional()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Make \"I\" optional", ((string[])(null)));
#line 91
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table11.AddRow(new string[] {
                        "Galaxy IV"});
            table11.AddRow(new string[] {
                        "iPhone"});
            table11.AddRow(new string[] {
                        "Windows Phone"});
            table11.AddRow(new string[] {
                        "Note"});
            table11.AddRow(new string[] {
                        "Kindle"});
            table11.AddRow(new string[] {
                        "Blackberry Storm"});
            table11.AddRow(new string[] {
                        "iPad"});
            table11.AddRow(new string[] {
                        "Surface"});
            table11.AddRow(new string[] {
                        "Surface Pro"});
            table11.AddRow(new string[] {
                        "HTC One"});
#line 92
 testRunner.Given("the following devices", ((string)(null)), table11, "Given ");
#line 104
 testRunner.When("I remove the 10th item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 105
 testRunner.And("remove the 4th item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
 testRunner.And("remove the 3rd item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
 testRunner.And("remove the 2nd item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 108
 testRunner.And("remove the 1st item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Product Name"});
            table12.AddRow(new string[] {
                        "Kindle"});
            table12.AddRow(new string[] {
                        "Blackberry Storm"});
            table12.AddRow(new string[] {
                        "iPad"});
            table12.AddRow(new string[] {
                        "Surface"});
            table12.AddRow(new string[] {
                        "Surface Pro"});
#line 109
 testRunner.Then("the following devices remain", ((string)(null)), table12, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
