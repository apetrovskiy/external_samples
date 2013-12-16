using System;
using TechTalk.SpecFlow;
using DMMAutoProject;
using LegacyQMM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecWrap02
{
    [Binding]
    public class MigrationToWindows2012Steps
    {
        public static object domainPair;
        public static DSAWorker dsaW;
        public static LegacyTest.Steps.LegacySteps_Checker check = new LegacyTest.Steps.LegacySteps_Checker();
        public static  DomainObject sUser;
        public static object _job;

        [Given(@"I have created user in any source Directory")]
        public void GivenIHaveCreatedUserInAnySourceDirectory()
        {

            DomainWorker sDom = FeatureContext.Current["SourceDomain"] as DomainWorker;
            DomainObject ou = sDom.CreateAdObjectEasy("specFlOU", DomainWorker.ObjectClass.OU);
            FeatureContext.Current.Add("SourceOU", ou);
            sUser = sDom.CreateAdObjectEasy("specFlU01", DomainWorker.ObjectClass.USER, ou);

        }
        
        [Given(@"I have Domain Pair in QMM with Windows 2012 as Target Domain Controller")]
        public void GivenIHaveDomainPairInQMMWithWindowsAsTargetDomainController()
        {
            dsaW=  FeatureContext.Current[GlobalHook.TestRequsites.DSAWorker.ToString()] as  LegacyQMM.DSAWorker;
            dsaW.OpenProject();
            var pair = dsaW.OpenDomainPair(FeatureContext.Current[GlobalHook.TestRequsites.SourceDomain.ToString()] as DomainWorker,
                FeatureContext.Current[GlobalHook.TestRequsites.TargetDomain.ToString()] as DomainWorker);
            Assert.IsNotNull(pair);
            domainPair = pair;
                        
        }     
        
        [When]
        public void WhenICreateNewMigrationSessionWith(Table table)
        {
            var job = dsaW.GetEmptySessionObject(domainPair);
            DomainObject targetCreateContainer = 
            (FeatureContext.Current[GlobalHook.TestRequsites.TargetDomain.ToString()] as DomainWorker).CreateAdObjectEasy("dsa_SimpleMigration_tgt_create", DomainWorker.ObjectClass.OU);
            DSAWorker.MigrationConfig mConfig = 
                new DSAWorker.MigrationConfig(FeatureContext.Current["SourceOU"] as DomainObject, targetCreateContainer);

            TableRow raw = table.Rows[0];
            
                if (raw[0] == "Password")
                    if (bool.Parse(raw["checked"]))
                    {
                        mConfig.pwdAction = AMMProjectLibLib.AmmEnumPasswordActions.pwdAct_sync;
                        mConfig.enableTarget = true;
                    }

            
            dsaW.FillSession_WithConfig(job, mConfig);
            dsaW.StartSession(job, domainPair);
            _job = job;
            
        }

        public static DomainObject tUser;
        [Then(@"the user appears in target directory with all supported attrubites")]
        public void ThenTheUserAppearsInTargetDirectoryWithAllSupportedAttrubites()
        {
            DomainWorker tdom = (FeatureContext.Current[GlobalHook.TestRequsites.TargetDomain.ToString()] as DomainWorker);
            DomainObject tuser = tdom.GetObjectByGuid(dsaW.GetMigratedObjectGuidBySourceGuid(sUser.NativeGuid));
            Result r = check.IS_TargetObjectCreatedByJob(sUser,tdom, _job);
            Assert.IsTrue(r.result, r.message);
            tUser=tuser;
            

        }
        
        [Then(@"The use able to loginin via LDAP with source password")]
        public void ThenTheUseAbleToLogininViaLDAPWithSourcePassword()
        {

            Result r = check.IS_PasswordCopied(tUser, "=1qwerty");
            Assert.IsTrue(r.result, r.message);
        }
    }
}
