using Suzianna.Reporting;
using System;
using System.Collections.Generic;
using System.Globalization;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;
using TechTalk.SpecFlow.BindingSkeletons;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Tracing;

namespace Suzianna.SpecFlowPlugin
{
    internal class SuziannaTestTracerWrapper : TestTracer,ITestTracer
    {
        protected readonly IReporter Reporter;
        public SuziannaTestTracerWrapper(IReporter reporter,ITraceListener traceListener, IStepFormatter stepFormatter,
          IStepDefinitionSkeletonProvider stepDefinitionSkeletonProvider, SpecFlowConfiguration specFlowConfiguration)
          : base(traceListener, stepFormatter, stepDefinitionSkeletonProvider, specFlowConfiguration)
        {
            Reporter = reporter;
        }
        void ITestTracer.TraceStep(StepInstance stepInstance, bool showAdditionalArguments)
        {
            TraceStep(stepInstance, showAdditionalArguments);
            //StartStep(stepInstance);
        }

        private void StartStep(StepInstance stepInstance)
        {
            Reporter.StepStarted(stepInstance.StepContext.FeatureTitle, stepInstance.StepContext.ScenarioTitle, stepInstance.Text);
        }

        void ITestTracer.TraceStepDone(BindingMatch match, object[] arguments, TimeSpan duration)
        {
            TraceStepDone(match, arguments, duration);
            //Reporter.MarkScenarioAsFailed
        }
        //public SuziannaTestTracerWrapper(IReporter Reporter)
        //{
        //    this.Reporter = Reporter;
        //}

        //public override void TraceStep(StepInstance stepInstance, bool showAdditionalArguments)
        //{
        //    //Reporter.StepStarted(stepInstance.StepContext.FeatureTitle, stepInstance.StepContext.ScenarioTitle, stepInstance.Text);
        //}

    }
}