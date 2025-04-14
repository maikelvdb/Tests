using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Test.Api.ParameterBinding.FromItem;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
public class FromItemAttribute(string name) : Attribute, IBindingSourceMetadata
{
    public string Name => name;

    public BindingSource BindingSource => FromItemBindingSource.FromItem;

}
