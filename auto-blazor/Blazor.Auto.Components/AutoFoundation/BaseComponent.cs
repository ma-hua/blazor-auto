using System;
using Blazor.Auto.Descriptor;
using Microsoft.AspNetCore.Components;

namespace Blazor.Auto.Components.AutoFoundation
{
    public class BaseComponent : ComponentBase
    {
        public readonly string Id = new Guid().ToString();

        [Parameter]
        public FieldDescriptor Descriptor { get; set; }

        [Parameter]
        public EventCallback<FieldDescriptor> DescriptorChanged { get; set; }

        public DescriptorValidator Validator { get; set; }

        public ValidateResult ValidateResult { get; set; }

        protected virtual void ValueChangedCallBack<T>(T value)
        {
            Descriptor.Value.SetValue(value);
            DescriptorChanged.InvokeAsync(Descriptor);
        }

        protected virtual ValidateResult Validate() => ValidateResult = Validator.Validate(Descriptor);
    }
}
