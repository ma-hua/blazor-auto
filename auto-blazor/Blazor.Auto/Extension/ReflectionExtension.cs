using System;
using System.Collections.Generic;
using System.Reflection;
using Blazor.Auto.Descriptor;

namespace Blazor.Auto.Extension
{
    public static class ReflectionExtension
    {
        public static IEnumerable<FieldDescriptor> ToDescriptor(this Type modelType)
        {
            foreach (var propertyInfo in modelType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                FieldDescriptor fieldDescriptor = new FieldDescriptor(propertyInfo);
                yield return fieldDescriptor;
            }
        }

        public static IEnumerable<FieldDescriptor> ToDescriptor(this object model)
        {
            foreach (var propertyInfo in model.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                var value = propertyInfo.GetValue(model);
                FieldDescriptor fieldDescriptor = new FieldDescriptor(propertyInfo, value);
                yield return fieldDescriptor;
            }
        }
    }
}
