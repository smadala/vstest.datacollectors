
## Build
- Open vstest.datacollectors.sln with VS
- Build the solution

## Run tests with data collector.
- Assuming C:\Users\samadala\src\vstest.datacollectors as repo root directory. 
> Note: /TestAdapterPath path given two times, One for adapter and another for data collector.

### Example command to run data collector
`C:\Users\samadala\src\vstest\artifacts\Debug\net451\win7-x64\vstest.console.exe C:\Users\samadala\src\vstest.datacollectors\SimpleTestProject\bin\Debug\net46\SimpleTestProject.dll /TestAdapterPath:C:\Users\samadala\src\vstest.datacollectors\Examples\bin\Debug\net46 /TestAdapterPath:C:\Users\samadala\src\vstest.datacollectors\SimpleTestProject\bin\Debug\net46\ /Collect:DataCollectionExample`

### Example command to run in proc data collector



`C:\Users\samadala\src\vstest.datacollectors\packages\microsoft.testplatform\15.6.0-preview-20171211-02\tools\net451\Common7\IDE\Extensions\TestPlatform\vstest.console.exe C:\Users\samadala\src\vstest.datacollectors\SimpleTestProject\bin\Debug\net46\SimpleTestProject.dll /TestAdapterPath:C:\Users\samadala\src\vstest.datacollectors\Examples\bin\Debug\net46 /TestAdapterPath:C:\Users\samadala\src\vstest.datacollectors\SimpleTestProject\bin\Debug\net46\ /settings:inproc_dc.runsettings`
