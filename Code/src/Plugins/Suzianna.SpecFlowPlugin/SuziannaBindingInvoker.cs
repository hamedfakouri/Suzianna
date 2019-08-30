using Suzianna.Reporting;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.ErrorHandling;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Tracing;

namespace Suzianna.SpecFlowPlugin
{

    internal class SuziannaBindingInvoker : BindingInvoker
    {
        private readonly IReporter reporter;

        public SuziannaBindingInvoker(IReporter reporter, SpecFlowConfiguration specFlowConfiguration,
            IErrorProvider errorProvider, ISynchronousBindingDelegateInvoker synchronousBindingDelegateInvoker) : base(specFlowConfiguration, errorProvider, synchronousBindingDelegateInvoker)
        {
            this.reporter = reporter;
        }

        public override object InvokeBinding(IBinding binding, IContextManager contextManager, object[] arguments, ITestTracer testTracer, out TimeSpan duration)
        {
            var hook = binding as HookBinding;
            var featureContext = contextManager.FeatureContext;
            var scenarioContext = contextManager.ScenarioContext;
            // process hook
            if (hook != null)
            {
                switch (hook.HookType)
                {
                    case HookType.BeforeFeature:
                        if (hook.HookOrder == int.MinValue)
                        {
                            // starting point
                            reporter.FeatureStarted(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
                        }
                        break;
                    case HookType.BeforeScenario:
                        if (hook.HookOrder == int.MinValue)
                        {
                            reporter.ScenarioStarted(featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title);
                        }
                        break;
                    case HookType.BeforeStep:
                        if (hook.HookOrder == int.MinValue)
                        {
                            reporter.StepStarted(featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title, getStepDefinitionTypeTitle(contextManager) + " " + contextManager.StepContext.StepInfo.Text);
                        }
                        try
                        {
                            return base.InvokeBinding(binding, contextManager, arguments, testTracer, out duration);
                        }
                        catch (Exception ex)
                        {
                            reporter.MarkScenarioAsFailed(featureContext.FeatureInfo.Title,
                                scenarioContext.ScenarioInfo.Title, ex.Message + Environment.NewLine + ex.StackTrace);
                            throw;
                        }
                    case HookType.AfterStep:
                        {
                            try
                            {
                                return base.InvokeBinding(binding, contextManager, arguments, testTracer, out duration);
                            }
                            catch (Exception ex)
                            {
                                reporter.MarkScenarioAsFailed(featureContext.FeatureInfo.Title,
                                    scenarioContext.ScenarioInfo.Title, ex.Message + Environment.NewLine + ex.StackTrace);
                                throw;
                            }
                        }

                    case HookType.AfterScenario:
                        if (hook.HookOrder == int.MaxValue)
                        {
                            reporter.MarkScenarioAsPassed(featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title);
                        }
                        break;
                    case HookType.AfterFeature:
                        if (hook.HookOrder == int.MaxValue)
                        {
                            var xml = reporter.ExportXml();
                            File.WriteAllText($"e:/suzianna_{featureContext.FeatureInfo.Title}.xml", xml);
                        }
                        break;
                }
            }
            return base.InvokeBinding(binding, contextManager, arguments, testTracer, out duration);
        }

        private static string getStepDefinitionTypeTitle(IContextManager contextManager)
        {
            switch (contextManager.StepContext.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    return "Given";
                case StepDefinitionType.When:
                    return "When";
                case StepDefinitionType.Then:
                    return "Then";
            }
            return "";
        }

        protected override CultureInfoScope CreateCultureInfoScope(IContextManager contextManager)
        {
            return base.CreateCultureInfoScope(contextManager);
        }

        protected override Delegate CreateMethodDelegate(MethodInfo method)
        {
            return base.CreateMethodDelegate(method);
        }

        protected override Expression GetBindingMethodCallExpression(Expression instance, MethodInfo method, Expression[] argumentsExpressions)
        {
            return base.GetBindingMethodCallExpression(instance, method, argumentsExpressions);
        }
    }
}