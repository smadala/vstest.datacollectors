
namespace vstest.logger
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

    [FriendlyName("TimelineLogger")]
    [ExtensionUri("logger://Microsoft/TeamFoundation/TimelineLogger")]
    public sealed class TimelineLogger : ITestLogger
    {
        public void Initialize(
            TestLoggerEvents events,
            String testRunDirectory)
        {
            events.TestResult += TestResultHandler;
            events.TestRunComplete += TestRunCompleteHandler;
        }

        private void TestResultHandler(
            Object sender,
            TestResultEventArgs e)
        {
            String assembly = Path.GetFileName(e.Result.TestCase.Source);
            if (assembly != currentAssembly)
            {
                if (currentAssembly != null)
                {
                    Console.WriteLine("[TestAssemblyCompleted] name=\"{0}\"", currentAssembly);
                }

                currentAssembly = assembly;
                Console.WriteLine("[TestAssemblyStarted] name=\"{0}\"", currentAssembly);
            }

            Console.WriteLine("[TestCaseStarted] name=\"{0}\" startTime=\"{1:O}\"",
                e.Result.TestCase.FullyQualifiedName, e.Result.StartTime.DateTime.ToUniversalTime());

            String result = "Succeeded";
            if (e.Result.Outcome == TestOutcome.Failed)
            {
                result = "Failed";
            }
            else if (e.Result.Outcome == TestOutcome.Skipped)
            {
                result = "Skipped";
            }

            Console.WriteLine("[TestCaseCompleted] name=\"{0}\" endTime=\"{1:O}\" result=\"{2}\"",
                e.Result.TestCase.FullyQualifiedName, e.Result.EndTime.DateTime.ToUniversalTime(), result);
        }

        private void TestRunCompleteHandler(
            Object sender,
            TestRunCompleteEventArgs e)
        {
            if (currentAssembly != null)
            {
                Console.WriteLine("[TestAssemblyCompleted] name=\"{0}\"", currentAssembly);
            }
        }

        private String currentAssembly;
    }
}