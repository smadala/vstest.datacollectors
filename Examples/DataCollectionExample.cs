﻿using System;

namespace Vstest.Datacollectors.Examples
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

    /// <summary>
    ///  Data collector example.
    /// </summary>
    [DataCollectorFriendlyName("DataCollectionExample")]
    [DataCollectorTypeUri("DataCollection://Vstest.Datacollectors/DataCollectionExample/1.0")]
    public class DataCollectionExample : DataCollector, ITestExecutionEnvironmentSpecifier
    {
        int i = 0;
        private DataCollectionSink dataCollectionSink;
        private DataCollectionEnvironmentContext context;
        private DataCollectionLogger logger;
        private string tempDirectoryPath = Path.GetTempPath();

        public override void Initialize(
            System.Xml.XmlElement configurationElement,
            DataCollectionEvents events,
            DataCollectionSink dataSink,
            DataCollectionLogger logger,
            DataCollectionEnvironmentContext environmentContext)
        {
            events.SessionStart += this.SessionStarted_Handler;
            events.SessionEnd += this.SessionEnded_Handler;
            events.TestCaseStart += this.Events_TestCaseStart;
            events.TestCaseEnd += this.Events_TestCaseEnd;
            this.dataCollectionSink = dataSink;
            this.context = environmentContext;
            this.logger = logger;
        }

        private void Events_TestCaseEnd(object sender, TestCaseEndEventArgs e)
        {
            this.logger.LogWarning(this.context.SessionDataCollectionContext, "TestCaseEnded " + e.TestCaseName);
        }

        private void Events_TestCaseStart(object sender, TestCaseStartEventArgs e)
        {
            this.logger.LogWarning(this.context.SessionDataCollectionContext, "TestCaseStarted " + e.TestCaseName);
            this.logger.LogWarning(this.context.SessionDataCollectionContext, "TestCaseStarted " + e.TestElement.FullyQualifiedName);
            var filename = Path.Combine(this.tempDirectoryPath, "testcasefilename" + i++ + ".txt");
            File.WriteAllText(filename, string.Empty);
            this.dataCollectionSink.SendFileAsync(e.Context, filename, true);
        }

        private void SessionStarted_Handler(object sender, SessionStartEventArgs args)
        {
            var filename = Path.Combine(this.tempDirectoryPath, "filename.txt");
            File.WriteAllText(filename, string.Empty);
            this.dataCollectionSink.SendFileAsync(this.context.SessionDataCollectionContext, filename, true);
            this.logger.LogWarning(this.context.SessionDataCollectionContext, "SessionStarted");
        }

        private void SessionEnded_Handler(object sender, SessionEndEventArgs args)
        {
            this.logger.LogError(this.context.SessionDataCollectionContext, new Exception("my exception"));
            this.logger.LogWarning(this.context.SessionDataCollectionContext, "my warning");
            this.logger.LogException(this.context.SessionDataCollectionContext, new Exception("abc"), DataCollectorMessageLevel.Error);

            this.logger.LogWarning(this.context.SessionDataCollectionContext, "SessionEnded");
        }

        public IEnumerable<KeyValuePair<string, string>> GetTestExecutionEnvironmentVariables()
        {
            return new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("key", "value") };
        }
    }
}
