using System;
using System.Collections.Generic;
using System.Text;
using ECharts.Entities;

namespace EChartsGen
{
    public class ExportOption
    {
        public string ChartOptionPath { get; set; }
        public ChartOption ChartOption { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }
        public string Format { get; set; }
    }
}
