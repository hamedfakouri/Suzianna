using TechTalk.SpecFlow;

namespace Suzianna.SpecflowPlugin.Tests.Integration
{
    [Binding]
    public class SuziannaBindings
    {
        private FeatureContext featureContext;
        private ScenarioContext scenarioContext;

        public SuziannaBindings( FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }
  

        [BeforeFeature(Order = int.MinValue)]
        public static void FirstBeforeFeature()
        {
            // start feature container in BindingInvoker
        }

        [AfterFeature(Order = int.MaxValue)]
        public static void LastAfterFeature()
        {
            // write feature container in BindingInvoker
        }

        [BeforeScenario(Order = int.MinValue)]
        public void FirstBeforeScenario()
        {
            //PluginHelper.StartTestContainer(featureContext, scenarioContext);
            //SuziannaHelper.StartTestCase(scenarioContainer.uuid, featureContext, scenarioContext);
        }

        [BeforeScenario(Order = int.MaxValue)]
        public void LastBeforeScenario()
        {
            // start scenario after last fixture and before the first step to have valid current step context in Suzianna storage
            //var scenarioContainer = PluginHelper.GetCurrentTestConainer(scenarioContext);
            //PluginHelper.StartTestCase(scenarioContainer.uuid, featureContext, scenarioContext);
        }

        [AfterScenario(Order = int.MaxValue)]
        public void FirstAfterScenario()
        {
            //var scenarioId = PluginHelper.GetCurrentTestCase(scenarioContext).uuid;

            //// update status to passed if there were no step of binding failures
            //Suzianna
            //    .UpdateTestCase(scenarioId,
            //        x => x.status = (x.status != Status.none) ? x.status : Status.passed)
            //    .StopTestCase(scenarioId);
            //scenarioContext.ScenarioContainer.
            //reporter.MarkScenarioAsPassed();
        }
        [BeforeStep(Order = int.MinValue)]
        public static void FirstBeforeStep()
        {
        }

        [AfterStep(Order = int.MaxValue)]
        public static void LastAfterStep()
        {
        }

        [AfterTestRun(Order = int.MaxValue)]
        public static void LastAfterTestRun()
        {
            //var scenarioId = PluginHelper.GetCurrentTestCase(scenarioContext).uuid;

            //// update status to passed if there were no step of binding failures
            //Suzianna
            //    .UpdateTestCase(scenarioId,
            //        x => x.status = (x.status != Status.none) ? x.status : Status.passed)
            //    .StopTestCase(scenarioId);
            //scenarioContext.ScenarioContainer.
            //reporter.MarkScenarioAsPassed();
        }
    }

  
}
