﻿using System;

namespace Vstest.Datacollectors.Examples
{
    using System.IO;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollector.InProcDataCollector;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;

    /// <summary>
    ///  In process data collector example.
    /// </summary>
    [DataCollectorFriendlyName("InProcDataCollectionExample")]
    [DataCollectorTypeUri("InProcDataCollector://Vstest.Datacollectors/InProcDataCollectionExample/1.0")]
    public class InProcDataCollectionExample : InProcDataCollection
    {
        private readonly string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDataCollector"/> class.
        /// </summary>
        public InProcDataCollectionExample()
        {
            this.fileName = Path.Combine(Path.GetTempPath(), "inproctest.txt");
            System.Diagnostics.Debugger.Launch();
        }

        public void Initialize(IDataCollectionSink dataCollectionSink)
        {
            // Do Nothing
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
