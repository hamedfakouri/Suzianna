namespace Suzianna.Reporting
{
    public interface IReporter
    {
        void EventPublished(string featureTitle, string scenarioTitle, string eventText);
        string ExportXml();
        void FeatureStarted(string title, string description);
        void MarkScenarioAsFailed(string featureTitle, string scenarioTitle, string reason = null);
        void MarkScenarioAsPassed(string featureTitle, string scenarioTitle);
        void ScenarioStarted(string featureTitle, string scenarioTitle);
        void StepStarted(string featureTitle, string scenarioTitle, string stepText);
        void TestSuiteEnded();
        void TestSuiteStarted();
    }
}