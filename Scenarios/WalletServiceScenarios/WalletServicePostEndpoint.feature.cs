﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace UserServiceTest.Scenarios.WalletServiceScenarios
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("WalletServicePostEndpoint")]
    public partial class WalletServicePostEndpointFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "WalletServicePostEndpoint.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Scenarios/WalletServiceScenarios", "WalletServicePostEndpoint", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change user balance - Ok")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        [NUnit.Framework.TestCaseAttribute("300", null)]
        [NUnit.Framework.TestCaseAttribute("9999999.99", null)]
        [NUnit.Framework.TestCaseAttribute("10000000", null)]
        public void ChangeUserBalance_Ok(string amount, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "RegisteredActiveUser"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("amount", amount);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change user balance - Ok", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
 testRunner.When(string.Format("Change user balance with amount \'{0}\'", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 7
 testRunner.Then("Change balance status code is \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate change user balance body - Body is Id in Guid format")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        [NUnit.Framework.TestCaseAttribute("0.01", null)]
        [NUnit.Framework.TestCaseAttribute("9999999.99", null)]
        [NUnit.Framework.TestCaseAttribute("10000000", null)]
        public void ValidateChangeUserBalanceBody_BodyIsIdInGuidFormat(string amount, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "RegisteredActiveUser"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("amount", amount);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate change user balance body - Body is Id in Guid format", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 15
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 16
 testRunner.When(string.Format("Change user balance with amount \'{0}\'", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 17
 testRunner.Then("Change balance body is in guid formate", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change balance of unexisted user - Internal server error")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        public void ChangeBalanceOfUnexistedUser_InternalServerError()
        {
            string[] tagsOfScenario = new string[] {
                    "RegisteredActiveUser"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change balance of unexisted user - Internal server error", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 25
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 26
 testRunner.Given("Unexisted user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 27
 testRunner.When("Change unexisted user balance with \'0.01\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 28
 testRunner.Then("Change balance status code is \'500\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 29
 testRunner.And("Change balance error message is \'not active user\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change balance with negative amount when balance is 0 - Internal Server Error")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        [NUnit.Framework.TestCaseAttribute("-0.01", null)]
        [NUnit.Framework.TestCaseAttribute("-10000000.01", null)]
        public void ChangeBalanceWithNegativeAmountWhenBalanceIs0_InternalServerError(string amount, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "RegisteredActiveUser"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("amount", amount);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change balance with negative amount when balance is 0 - Internal Server Error", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 32
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 33
 testRunner.When(string.Format("Change user balance with amount \'{0}\'", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 34
 testRunner.Then(string.Format("Change balance with negative amount error message is \'User have \'0\', you try to c" +
                            "harge \'{0}\'\'", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change balance with amount of zero - Internal Server Error")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        public void ChangeBalanceWithAmountOfZero_InternalServerError()
        {
            string[] tagsOfScenario = new string[] {
                    "RegisteredActiveUser"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change balance with amount of zero - Internal Server Error", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 43
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 44
 testRunner.When("Change user balance with amount \'0\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 45
 testRunner.Then("Change balance status code is \'500\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 46
 testRunner.And("Change balance error message is \'Amount cannot be \'0\'\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Balance plus amount bigger than 10m - Internal Server Error")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        public void BalancePlusAmountBiggerThan10M_InternalServerError()
        {
            string[] tagsOfScenario = new string[] {
                    "RegisteredActiveUser"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Balance plus amount bigger than 10m - Internal Server Error", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 49
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 50
 testRunner.Given("Change user balance with amount \'5000000\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 51
 testRunner.When("Change user balance with amount \'5000001\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 52
 testRunner.Then("Change balance status code is \'500\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 53
 testRunner.And("Change balance error message is \'After this charge balance could be \'10000001.0\'," +
                        " maximum user balance is \'10000000\'\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Balance + Amount smaller or equal to 10m - Ok")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        [NUnit.Framework.TestCaseAttribute("500000", "500000", null)]
        [NUnit.Framework.TestCaseAttribute("499999", "500000", null)]
        public void BalanceAmountSmallerOrEqualTo10M_Ok(string balance, string amount, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "RegisteredActiveUser"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("balance", balance);
            argumentsOfScenario.Add("amount", amount);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Balance + Amount smaller or equal to 10m - Ok", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 56
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 57
 testRunner.Given(string.Format("Change user balance with amount \'{0}\'", balance), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 58
 testRunner.When(string.Format("Change user balance with amount \'{0}\'", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 59
 testRunner.Then("Change balance status code is \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change balance with negative amount, which bigger than balance - Internal Server " +
            "Error")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        [NUnit.Framework.TestCaseAttribute("50", "-1000", null)]
        public void ChangeBalanceWithNegativeAmountWhichBiggerThanBalance_InternalServerError(string balance, string amount, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "RegisteredActiveUser"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("balance", balance);
            argumentsOfScenario.Add("amount", amount);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change balance with negative amount, which bigger than balance - Internal Server " +
                    "Error", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 67
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 68
 testRunner.Given(string.Format("Change user balance with amount \'{0}\'", balance), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 69
 testRunner.When(string.Format("Change user balance with amount \'{0}\'", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 70
 testRunner.Then("Change balance status code is \'500\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 71
 testRunner.And(string.Format("Change balance with negative amount error message is \'User have \'{0}.0\', you try " +
                            "to charge \'{1}.0\'\'", balance, amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wrong precision of amount - Internal Server Error")]
        [NUnit.Framework.CategoryAttribute("RegisteredActiveUser")]
        [NUnit.Framework.TestCaseAttribute("0.1", "0.001", null)]
        [NUnit.Framework.TestCaseAttribute("5.78", "6.325", null)]
        public void WrongPrecisionOfAmount_InternalServerError(string balance, string amount, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "RegisteredActiveUser"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("balance", balance);
            argumentsOfScenario.Add("amount", amount);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wrong precision of amount - Internal Server Error", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 78
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 79
 testRunner.Given(string.Format("Change user balance with amount \'{0}\'", balance), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 80
 testRunner.When(string.Format("Change user balance with amount \'{0}\'", amount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 81
 testRunner.Then("Change balance status code is \'500\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 82
 testRunner.And("Change balance error message is \'Amount value must have precision 2 numbers after" +
                        " dot\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
