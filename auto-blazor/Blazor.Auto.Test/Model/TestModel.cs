using System;
using System.Collections.Generic;
using System.ComponentModel;
using Blazor.Auto.SelectItem;

namespace Blazor.Auto.Test.Model
{
    public class TestModel
    {
        [Description("时间")]
        public DateTime Time1 { get; set; }

        [Description("可空时间")]
        public DateTime? Time2 { get; set; }

        [Description("字符串")]
        public string Str { get; set; }

        [Description("整型")]
        public int Number { get; set; }

        [Description("Double")]
        public double Double { get; set; }

        [Description("可空浮点型")]
        public float? Float { get; set; }

        [Description("布尔")]
        public bool Switch1 { get; set; }

        [Description("可空布尔")]
        public bool? Switch2 { get; set; }

        [Description("枚举")]
        public TestTag Tag { get; set; }


        [Description("多选")]
        public List<TestTag> Tags { get; set; }

        [Description("需要从外部查询才能得到的下拉列表")]
        [SelectDescription("PriceGroup")]
        public List<int> PriceGroup { get; set; }

        [Description("门店列表1")]
        [SelectDescription("StoreGroup")]
        public List<int> StoreGroup1 { get; set; }

        [Description("门店列表2")]
        [SelectDescription("StoreGroup")]
        public List<int> StoreGroup2 { get; set; }
    }
}
