using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Suzianna.SpecflowPlugin.Tests.Integration
{
    [Category("NotSpecTest")]
    public class OutputReportFileTests
    {
        private HashSet<TestResult> suziannaTestResults = new HashSet<TestResult>();
        public OutputReportFileTests()
        {
        }

        [OneTimeSetUp]
        public void Init()
        {
            var testDirectory = @"";
            var suziannaDirectory = $@"{testDirectory}\TestResults\suzianna-results";
            if (Directory.Exists(suziannaDirectory))
                Directory.Delete(suziannaDirectory, true);
            var processInfo = new ProcessStartInfo("../../../runtestswithdotnettest.cmd");
            //processInfo.WindowStyle = ProcessWindowStyle.Normal;
            // run SpecFlow scenarios using SpecRun runner
            var process = Process.Start(processInfo);
            process.WaitForExit();
        }

        [TestCase(Status.passed, 2)]
        public void TestStatus(Status status, int count)
        {
            suziannaTestResults.Where(a => a.Status == status).Should().HaveCount(count);
            //Check.That(allureTestResults.Where(a => a.Status == status)).CountIs(count);
        }

    }
}

public partial class TestResult
{
    public Status Status { get; set; }
}

public enum Status
{
    none,
    /// <remarks/>
    failed,

    /// <remarks/>
    broken,

    /// <remarks/>
    passed,

    /// <remarks/>
    skipped,
}