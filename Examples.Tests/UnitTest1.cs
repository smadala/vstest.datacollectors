using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.Tests
{
    using System.Diagnostics;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string vstestPath =
                @"C:\Users\samadala\src\vstest.datacollectors\packages\microsoft.testplatform\15.6.0-preview-20171211-02\tools\net451\Common7\IDE\Extensions\TestPlatform\vstest.console.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo(vstestPath);
            startInfo.Arguments =
                @"C:\Users\samadala\src\vstest.datacollectors\SimpleTestProject\bin\Debug\net46\SimpleTestProject.dll /TestAdapterPath:C:\Users\samadala\src\vstest.datacollectors\Examples\bin\Debug\net46 /Collect:InProcDataCollectionExample";

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

        }
    }
}
