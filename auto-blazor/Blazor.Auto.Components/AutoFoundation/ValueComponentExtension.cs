using System.Linq;
using Blazor.Auto.Descriptor;

namespace Blazor.Auto.Components.AutoFoundation
{
    public static class ValueComponentExtension
    {
        public static ValueTag[] DateTimeTags = new []{ ValueTag.DateTime, ValueTag.NullableDateTime};

        public static ValueTag[] InputTags = new[] { ValueTag.String, ValueTag.Int, ValueTag.Double, ValueTag.NullableInt, ValueTag.NullableDouble };

        public static ValueTag[] SwitchTags = new[] { ValueTag.Bool, ValueTag.NullableBool };

        public static ValueTag[] SelectTags = new[] { ValueTag.SelectEnum, ValueTag.MultipleSelectInt, ValueTag.MultipleSelectEnum, ValueTag.MultipleSelectString };

        public static ValueTag[] EnumSelectTags = new[] { ValueTag.MultipleSelectEnum, ValueTag.SelectEnum };

        public static bool IsDateTime(this ValueDescriptor descriptor) => DateTimeTags.Contains(descriptor.Tag);
        public static bool IsInput(this ValueDescriptor descriptor) => InputTags.Contains(descriptor.Tag);
        public static bool IsSwitch(this ValueDescriptor descriptor) => SwitchTags.Contains(descriptor.Tag);
        public static bool IsSelect(this ValueDescriptor descriptor) => SelectTags.Contains(descriptor.Tag);
        public static bool IsEnumSelect(this ValueDescriptor descriptor) => EnumSelectTags.Contains(descriptor.Tag);
    }
}
