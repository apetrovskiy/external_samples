using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecWrap02
{
    [Binding]
    public  class GlobalHook
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public enum TestRequsites
        {
            SourceDomain,
            TargetDomain,
            DSAWorker,
            SourceOU,
            TargetUser
        };

        [BeforeScenario]
        public static void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
        [BeforeFeature]
        public static void BeforeFeture()
        {
            

            if (!FeatureContext.Current.ContainsKey("SourceDomain"))
                FeatureContext.Current.Add("SourceDomain", TestsPrerequisites.sdom);
            if (!FeatureContext.Current.ContainsKey("TargetDomain"))
                FeatureContext.Current.Add("TargetDomain", TestsPrerequisites.tdom);
            if (!FeatureContext.Current.ContainsKey("DSAWorker"))
                FeatureContext.Current.Add("DSAWorker", TestsPrerequisites.dsaWorker);
                       
        }
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestsPrerequisites.sdom.DeleteTestOU();
            TestsPrerequisites.tdom.DeleteTestOU();
           
        }

    }
}
