using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Auto.Descriptor
{
    public class ValueDescriptor
    {
        public ValueTag Tag { get; set; }

        public object T1 { get; set; }

        public List<int> T2 { get; set; }

        public List<string> T3 { get; set; }

        public List<KeyValuePair<string, string>> SelectItems { get; set; }

        public ValueDescriptor() { }

        public ValueDescriptor(Type valueType, object value = null)
        {
            this.SetTag(valueType);

            T1 = value;

            if (this.Tag == ValueTag.SelectEnum)
            {
                SelectItems = ValueDescriptorUtil.GetKeyValuePairs(valueType).ToList();
            }

            if (this.Tag == ValueTag.MultipleSelectEnum)
            {
                var genericType = valueType.GetGenericArguments()[0];
                SelectItems = ValueDescriptorUtil.GetKeyValuePairs(genericType).ToList();
            }

            this.SetValue(value);
        }

        public void SetValue(object value)
        {
            if (value == null)
            {
                return;
            }

            T1 = value;

            if (this.Tag == ValueTag.SelectEnum)
            {
                T1 = Convert.ToInt32(value);
            }

            if (this.Tag == ValueTag.MultipleSelectEnum)
            {
                T2 = new List<int>();
                foreach (var item in value as IEnumerable)
                {
                    T2.Add(Convert.ToInt32(item));
                }
            }

            if (Tag == ValueTag.MultipleSelectInt)
            {
                T2 = value as List<int>;
            }

            if (Tag == ValueTag.MultipleSelectString)
            {
                T3 = value as List<string>;
            }
        }


        public bool ValueEquals(ValueDescriptor descriptor)
        {
            if (ReferenceEquals(null, descriptor)) return false;
            if (ReferenceEquals(this, descriptor)) return true;
            if (Tag != descriptor.Tag)
            {
                return false;
            }

            if (Tag == ValueTag.MultipleSelectInt)
            {
                if (NullComparer(T2, descriptor.T2))
                {
                    return T2 == null || !T2.Intersect(descriptor.T2).Any();
                }

                return false;
            }

            if (Tag == ValueTag.MultipleSelectString)
            {
                if (NullComparer(T3, descriptor.T3))
                {
                    return T3 == null || !T3.Intersect(descriptor.T3).Any();
                }

                return false;
            }

            return T1?.ToString() == descriptor.T1?.ToString();
        }

        private bool NullComparer(object v1, object v2)
        {
            if (ReferenceEquals(null, v1) && !ReferenceEquals(null, v2)) return false;
            if (!ReferenceEquals(null, v1) && ReferenceEquals(null, v2)) return false;
            return true;
        }
    }
}
