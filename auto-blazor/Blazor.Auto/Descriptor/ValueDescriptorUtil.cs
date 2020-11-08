using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Blazor.Auto.Descriptor
{
    public static class ValueDescriptorUtil
    {
        public static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs(Type enumType)
        {
            var instance = Activator.CreateInstance(enumType);
            foreach (var field in enumType.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var description = field.GetCustomAttribute<DescriptionAttribute>();
                string key = Convert.ToInt32(field.GetValue(instance)).ToString();
                yield return new KeyValuePair<string, string>(key, description?.Description ?? field.Name);
            }
        }

        public static ValueDescriptor SetTag(this ValueDescriptor descriptor, Type fieldType)
        {
            descriptor.Tag = GetValueTag(fieldType);
            return descriptor;
        }

        private static ValueTag GetValueTag(Type fieldType)
        {
            if (fieldType.TypeEquals(typeof(int)))
                return ValueTag.Int;
            if (fieldType.TypeEquals(typeof(int?)))
                return ValueTag.NullableInt;

            if (fieldType.TypeEquals(typeof(double)))
                return ValueTag.Double;
            if (fieldType.TypeEquals(typeof(decimal)))
                return ValueTag.Double;
            if (fieldType.TypeEquals(typeof(float)))
                return ValueTag.Double;
            if (fieldType.TypeEquals(typeof(double?)))
                return ValueTag.NullableDouble;
            if (fieldType.TypeEquals(typeof(decimal?)))
                return ValueTag.NullableDouble;
            if (fieldType.TypeEquals(typeof(float?)))
                return ValueTag.NullableDouble;

            if (fieldType.TypeEquals(typeof(DateTime)))
                return ValueTag.DateTime;
            if (fieldType.TypeEquals(typeof(DateTime?)))
                return ValueTag.NullableDateTime;
            if (fieldType.TypeEquals(typeof(bool)))
                return ValueTag.Bool;
            if (fieldType.TypeEquals(typeof(bool?)))
                return ValueTag.NullableBool;

            if (fieldType.IsGenericType)
            {
                var genericType = fieldType.GetGenericArguments()[0];
                if (genericType.IsEnum)
                    return ValueTag.MultipleSelectEnum;
                if (genericType.TypeEquals(typeof(int)))
                    return ValueTag.MultipleSelectInt;
                if (genericType.TypeEquals(typeof(string)))
                    return ValueTag.MultipleSelectString;
            }

            if (fieldType.IsEnum)
                return ValueTag.SelectEnum;

            return ValueTag.String;
        }

        private static bool TypeEquals(this Type type, Type other)
        {
            return type.FullName == other.FullName;
        }
    }
}
