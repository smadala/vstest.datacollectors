
namespace Vstest.Datacollectors
{
    using System;
    using System.IO;
    using System.Runtime.ExceptionServices;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollector.InProcDataCollector;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;

    /// <summary>
    ///  In process data collector example.
    /// </summary>
    [DataCollectorFriendlyName("FirstChanceExceptionsInProcDataCollector")]
    [DataCollectorTypeUri("InProcDataCollector://Vstest.Datacollectors/FirstChanceExceptionsInProcDataCollector/1.0")]
    public class FirstChanceExceptionsInProcDataCollector : InProcDataCollection
    {
        private readonly string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDataCollector"/> class.
        /// </summary>
        public FirstChanceExceptionsInProcDataCollector()
        {
            this.fileName = Path.Combine(Path.GetTempPath(), $"UnhandledExceptionsInProcDataCollector_{Guid.NewGuid()}.txt");
        }

        public void Initialize(IDataCollectionSink dataCollectionSink)
        {
            AppDomain.CurrentDomain.FirstChanceException += FirstChanceHandler;
            File.AppendAllText(this.fileName, "Creating file");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            File.AppendAllText(this.fileName, "Sender: === " +  sender);
            File.AppendAllText(this.fileName, "Event args: ===" + e?.ToString());
            File.AppendAllText(this.fileName, " ========================== " + Environment.NewLine + Environment.NewLine);
        }

        private void FirstChanceHandler(object source, FirstChanceExceptionEventArgs e)
        {
            File.AppendAllText(this.fileName, " ============ FirstChanceHandler ============== " + Environment.NewLine + Environment.NewLine);
            File.AppendAllText(this.fileName, "Sender: === " + source);
            File.AppendAllText(this.fileName, "Event args: ===" + e?.Exception);
            File.AppendAllText(this.fileName, Environment.NewLine + " ========================== " + Environment.NewLine + Environment.NewLine);
        }

        /// <summary>
        /// The test session start.
        /// </summary>
        /// <param name="testSessionStartArgs">
        /// The test session start args.
        /// </param>
        public void TestSessionStart(TestSessionStartArgs testSessionStartArgs)
        {
            Console.WriteLine(testSessionStartArgs.Configuration);
            File.WriteAllText(this.fileName, "TestSessionStart : " + testSessionStartArgs.Configuration + Environment.NewLine);
        }

        /// <summary>
        /// The test case start.
        /// </summary>
        /// <param name="testCaseStartArgs">
        /// The test case start args.
        /// </param>
        public void TestCaseStart(TestCaseStartArgs testCaseStartArgs)
        {
            Console.WriteLine(
                "TestCase Name : {0}, TestCase ID:{1}",
                testCaseStartArgs.TestCase.DisplayName,
                testCaseStartArgs.TestCase.Id);
            File.AppendAllText(this.fileName, "TestCaseStart : " + testCaseStartArgs.TestCase.DisplayName + Environment.NewLine);
        }

        /// <summary>
        /// The test case end.
        /// </summary>
        /// <param name="testCaseEndArgs">
        /// The test case end args.
        /// </param>
        public void TestCaseEnd(TestCaseEndArgs testCaseEndArgs)
        {
            Console.WriteLine("TestCase Name:{0}, TestCase ID:{1}, OutCome:{2}", testCaseEndArgs.DataCollectionContext.TestCase.DisplayName, testCaseEndArgs.DataCollectionContext.TestCase.Id, testCaseEndArgs.TestOutcome);
            File.AppendAllText(this.fileName, "TestCaseEnd : " + testCaseEndArgs.DataCollectionContext.TestCase.DisplayName + Environment.NewLine);
        }

        /// <summary>
        /// The test session end.
        /// </summary>
        /// <param name="testSessionEndArgs">
        /// The test session end args.
        /// </param>
        public void TestSessionEnd(TestSessionEndArgs testSessionEndArgs)
        {
            Console.WriteLine("TestSession Ended");
            File.AppendAllText(this.fileName, "TestSessionEnd: ");
        }
    }
}
