using Blazor.Auto.Descriptor;

namespace Blazor.Auto.Components.AutoFoundation
{
    public class DescriptorValidator
    {
        public virtual ValidateResult Validate(FieldDescriptor descriptor)
        {
            if (descriptor.IsRequired)
            {
                if (descriptor.Value == null || string.IsNullOrWhiteSpace(descriptor.Value.ToString()))
                {
                    return new ValidateResult(false, "必填项不能为空");
                }
            }

            return ValidateResult.Success();
        }
    }
}
