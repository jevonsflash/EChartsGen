using EChartsGen.Entities.style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EChartsGen.Entities.series.Data
{
    public class PolarData
    {
        public object value { get; set; }
        public string name { get; set; }
        public object symbol { get; set; }

        public int? symbolRotate { get; set; }

        public object symbolSize { get; set; }

        public ItemStyle itemStyle { get; set; }

        public ItemStyle ItemStyle()
        {
            if (itemStyle == null)
            {
                itemStyle = new ItemStyle();
            }
            return itemStyle;
        }

        public PolarData(string name)
        {
            this.name = name;
        }

        public PolarData(object value, string name)
        {
            this.value = value;
            this.name = name;
        }

        public PolarData(object value, string name, ItemStyle itemStyle) : this(value, name)
        {
            this.itemStyle = itemStyle;
        }

        public PolarData Value(params object[] values)
        {
            if (values == null)
                return this;
            value = values.ToList();
            return this;
        }

    }
}
