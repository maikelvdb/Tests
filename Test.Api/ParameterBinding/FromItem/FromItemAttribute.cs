using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Test.Api.ParameterBinding.FromItem
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class FromItemAttribute(string name) : Attribute, IBindingSourceMetadata
    {
        public string Name => name;

        public BindingSource BindingSource => FromItemBindingSource.FromItem;

    }
}
