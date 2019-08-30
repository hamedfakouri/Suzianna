using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.Tracing;
using TechTalk.SpecFlow.UnitTestProvider;
using Suzianna.Reporting;
[assembly: RuntimePlugin(typeof(Suzianna.SpecFlowPlugin.SuziannaPlugin))]

namespace Suzianna.SpecFlowPlugin
{

    public class SuziannaPlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeGlobalDependencies += (sender, args) =>
                args.ObjectContainer.RegisterTypeAs<SuziannaBindingInvoker, IBindingInvoker>();

            runtimePluginEvents.CustomizeTestThreadDependencies += (sender, args) =>
                args.ObjectContainer.RegisterTypeAs<SuziannaTestTracerWrapper, ITestTracer>();

            runtimePluginEvents.CustomizeGlobalDependencies += (sender, args) =>
          args.ObjectContainer.RegisterFactoryAs<IReporter>(a => new Reporter());
        }
    }
}
