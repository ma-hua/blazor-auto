using System;
using System.Collections.Generic;
using EnumsNET;

namespace Blazor.Auto.Extension
{
    public static class EnumExtension
    {
        public static Dictionary<int, string> GetSelectItems<T>() where T : struct, Enum
        {
            var result = new Dictionary<int, string>();
            foreach (var item in Enums.GetMembers<T>())
            {
                result.Add(item.ToInt32(), item.AsString(EnumFormat.Description));
            }

            return result;
        }

        public static Dictionary<int, string> ToSelectItem<T>(this T enumValue) where T : struct, Enum
        {
            var result = new Dictionary<int, string>();
            foreach (var item in enumValue.GetFlags())
            {
                result.TryAdd(Enums.ToInt32(item), item.AsString(EnumFormat.Description));
            }

            return result;
        }
    }
}
