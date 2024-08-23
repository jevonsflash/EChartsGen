using System;
using System.Collections.Generic;
using System.Text;
using ECharts.Entities;

namespace EChartsGen
{
    public class ExportOption
    {
        /// <summary>
        /// Option的json文件路径
        /// </summary>
        public string ChartOptionPath { get; set; }

        /// <summary>
        /// Option的.Net对象
        /// </summary>
        public ChartOption ChartOption { get; set; }

        /// <summary>
        /// 图片高度（像素）
        /// </summary>
        public int Height { get; set; }


        /// <summary>
        /// 图片宽度（像素）
        /// </summary>
        public int Width { get; set; }
        public string Format { get; set; }

        /// <summary>
        /// 图片文件的生成目录,若为空则默认生成至工作目录下\libs\phantomjs-2.1.1-windows\tmp
        /// </summary>
        public string OutputPath { get; set; }
    }
}
