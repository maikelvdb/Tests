using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace Test.Api.ParameterBinding.FromItem
{
    public class FromItemModelBinder(string name) : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!bindingContext.HttpContext.Items.TryGetValue(name, out var value))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(value);

            return Task.CompletedTask;
        }
    }
}
