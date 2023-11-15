using Microsoft.VisualStudio.TestTools.UnitTesting;
using EChartsGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EChartsGen.Tests
{
    [TestClass()]
    public class ExporterTests
    {
        private static readonly string basePath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
        [TestMethod()]
        public  void ExportTest()
        {
            var optionPath = Path.Combine(basePath, "js\\option.json");
            Exporter exporter = new Exporter();
            var pngPath =  exporter.ExportAsync(new EChartsGen.ExportOption() { ChartOptionPath = optionPath }).Result;
            Assert.IsNotNull(pngPath);
        }
    }
}