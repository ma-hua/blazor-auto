using System;

namespace Blazor.Auto.SelectItem
{
    public class SelectDescriptionAttribute : Attribute
    {
        public string Keyword { get; set; }

        public SelectDescriptionAttribute(string keyword)
        {
            Keyword = keyword;
        }
    }
}
