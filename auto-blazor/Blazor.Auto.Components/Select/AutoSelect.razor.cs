using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Blazor.Auto.Components.AutoFoundation;
using Blazor.Auto.Descriptor;
using Blazor.Auto.SelectItem;
using Microsoft.AspNetCore.Components;

namespace Blazor.Auto.Component
{
    public partial class AutoSelect
    {
        [Inject]
        public IComponentContext ComponentContext { get; set; }

        [Parameter]
        public List<KeyValuePair<string, string>> Items { get; set; }

        [Parameter]
        public string Keyword { get; set; }

        List<KeyValuePair<string, string>> selectedRows;
        string selectedKey;

        List<KeyValuePair<string, string>> filterItems = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> datas => filterItems.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize).ToList();

        protected override async Task OnParametersSetAsync()
        {
            if (Descriptor.Value == null)
            {
                return;
            }

            if (!ValueComponentExtension.IsEnumSelect(Descriptor.Value) && !string.IsNullOrWhiteSpace(Keyword))
            {
                try
                {
                    Items = await ComponentContext.ResolveNamed<ISelectItemProvider>(Keyword).GetSelectItemAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            filterItems = Items ?? new List<KeyValuePair<string, string>>();
            selectedRows = new List<KeyValuePair<string, string>>();
            title = Descriptor.Description;

            if (Descriptor.Value.Tag == ValueTag.MultipleSelectString && Descriptor.Value.T3 != null)
            {
                selectedRows = Items?.Where(x => Descriptor.Value.T3.Contains(x.Key)).ToList();
            }

            if ((Descriptor.Value.Tag == ValueTag.MultipleSelectInt || Descriptor.Value.Tag == ValueTag.MultipleSelectEnum) && Descriptor.Value.T2 != null)
            {
                selectedRows = Items?.Where(x => Descriptor.Value.T2.Contains(Convert.ToInt32(x.Key))).ToList();
            }

            if (Descriptor.Value.Tag == ValueTag.SelectEnum)
            {
                selectedKey = Descriptor.Value.T1?.ToString();
                selectedRows = Items?.Where(x => x.Key == selectedKey).ToList();
            }
        }

        void Search(string context)
        {
            Func<List<KeyValuePair<string, string>>, string, List<KeyValuePair<string, string>>> idsToSelectRowsFunc = (datas, context) =>
            {
                var ids = context.Split(',');
                return datas.Where(x => ids.Contains(x.Key)).ToList();
            };

            filterItems = string.IsNullOrWhiteSpace(context)
                ? Items.ToList()
                : context.Contains(',')
                    ? idsToSelectRowsFunc(Items, context)
                    : Items.Where(x => !string.IsNullOrWhiteSpace(x.Value) && x.Value.Contains(context)).ToList();
        }

        Task Save()
        {
            if (Descriptor.Value.Tag == ValueTag.MultipleSelectInt || Descriptor.Value.Tag == ValueTag.MultipleSelectEnum)
            {
                Descriptor.Value.T2 = selectedRows.Select(x => Convert.ToInt32(x.Key)).ToList();
            }
            else if (Descriptor.Value.Tag == ValueTag.MultipleSelectString)
            {
                Descriptor.Value.T3 = selectedRows.Select(x => x.Key).ToList();
            }
            else
            {
                Descriptor.Value.T1 = Convert.ToInt32(selectedKey);
            }
            _visible = false;
            return DescriptorChanged.InvokeAsync(Descriptor);
        }
    }
}
