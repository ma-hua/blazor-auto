using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Blazor.Auto.SelectItem;

namespace Blazor.Auto.Descriptor
{
    public class FieldDescriptor
    {
        public string FieldName { get; set; }

        public string Description { get; set; }

        public ValueDescriptor Value { get; set; }

        public bool IsRequired { get; set; }

        public string Keyword { get; set; }

        public FieldDescriptor(PropertyInfo propertyInfo, object value = null)
        { 
            FieldName = propertyInfo.Name;
            Description = propertyInfo.GetCustomAttribute<DescriptionAttribute>()?.Description ?? FieldName;
            IsRequired = propertyInfo.GetCustomAttribute<RequiredAttribute>() != null;
            Keyword = propertyInfo.GetCustomAttribute<SelectDescriptionAttribute>()?.Keyword ?? FieldName;
            Value = new ValueDescriptor(propertyInfo.PropertyType, value);
        }

        public FieldDescriptor()
        {
        }

        public bool Validate()
        {
            if (IsRequired && (Value == null || Value.T1 == null))
            {
                throw new Exception($"{Description}-不能为空");
            }

            return true;
        }
    }
}
