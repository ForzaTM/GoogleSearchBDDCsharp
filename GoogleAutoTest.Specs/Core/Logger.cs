using System;
using Serilog;
using System.Reflection;
using System.IO;
using OpenQA.Selenium;
using NUnit.Framework;

namespace GoogleAutoTest.Specs.Core
{
    class Logger
    {
        public const string filePath = "GoogleAutoTestLog.txt";
  
        public Logger()
        {
            var currentDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(currentDir + "\\" + filePath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Executed at {ExecutionTime}", Environment.TickCount);
            Log.Information($"Test:{TestContext.CurrentContext.Test.Name} Started successfully");
        }               
    }
}
