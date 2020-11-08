## 一个能够根据对象或者类型自动生成页面组件的Blazor WebAssembly应用
- 基于Ant.Design-Blazor组件库
- 需要dotnet-sdk v3.1.6及以上

### 需求描述

- 优惠活动相关业务需求常常变更频繁，基于[Sql Server中json存储技术](https://docs.microsoft.com/zh-cn/sql/relational-databases/json/store-json-documents-in-sql-tables?view=sql-server-ver15)，对公司原先的活动规则体系展开重构。将规则划分为多个大类，每一类的对象不再以字段对应的方式存储在数据库中，而是以Json的方式存储，这样需要增加或者删除某些字段的时候，只需要修改相应实体，而不必修改数据库设计。
- 之后也对优惠管理系统提出新的要求，即能够根据最新发布的包含优惠实体的Nuget包，自动生成规则设置页面。基于html+js的前端技术或许并不能做到这一点，因此提出了使用Blazor技术+C#，来完成这一目标：**根据对象或者类型自动生成页面组件，并且完成数据绑定，来完成新增或编辑等操作。**

### 项目介绍

**本项目仅用于学习参考，本人才疏学浅，不足之处还请多多指教**

- Blazor.Auto 包含反射与字段描述等相关工具
- Blazor.Auto.Components 根据字段描述符来自动完成数据绑定的组件库（基于Ant.Design-Blazor）

#### 获取字段主要信息
- 字段描述符 FieldDescriptor &ensp;&ensp;包含字段名，字段描述，IsRequired，值描述符
- 值描述符 ValueDescriptor&ensp;&ensp;包含值的类型，值，下拉框列表

#### 字段组件：输入框&ensp;日期选择器&ensp;开关&ensp;列表选择器
- 根据值类型，输入框分为：字符串输入框，整数输入框，小数输入框
- 开关分为可控开关和非空开关（bool?和bool两种）
- 列表选择器以模态框的形式展示，分为单选列表和多选列表  

&ensp;&ensp;字段组件均继承自父组件BaseComponent，父组件中包含虚方法ValueChangedCallBack，以链接绑定的方式来完成参数绑定。

#### 列表选择器
&ensp;&ensp;列表选择器通常对应类型为List\<T>的字段，选择器内容的获取较为关键。假如泛型类型是枚举的话，可以直接通过反射技术拿到选择列表。  

&ensp;&ensp;但是如果需要通过查询某些接口来获取选择列表的话，需要实现ISelectItemProvider接口。并且为其实现类指定一个Keyword，在字段上使用特性\[SelectDescriptionAttribute]标注同样的Keyword。那么在生成组件时，可以通过Keyword从IOC容器中获取SelectItemProvider，并调用实现方法，来获取选择项。

通常需要使用Named的方式注入ISelectItemProvider实现类，在本项目中，我使用的是Autofac容器。
```
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//注入ISelectItemProvider实现类
builder.ConfigureContainer(new AutofacServiceProviderFactory(cfg =>
    {
        cfg.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
        cfg.RegisterType<StoreGroupProvider>().Named<ISelectItemProvider>(StoreGroupProvider.Keyword).SingleInstance();
        cfg.RegisterType<PriceGroupProvider>().Named<ISelectItemProvider>(PriceGroupProvider.Keyword).SingleInstance();
    }));
```
在列表选择器组件中，根据字段的Keyword从IOC容器中获取选择列表项
```
[Inject]
public IComponentContext ComponentContext { get; set; }

protected override async Task OnParametersSetAsync()
{
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
}

```
