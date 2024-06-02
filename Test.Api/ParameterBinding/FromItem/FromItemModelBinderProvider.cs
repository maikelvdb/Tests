using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Test.Api.ParameterBinding.FromItem
{
    public class FromItemModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.BindingInfo.BindingSource == null
                || !context.BindingInfo.BindingSource.CanAcceptDataFrom(FromItemBindingSource.FromItem))
            {
                return null;
            }

            var attributes = ((Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadata)context.Metadata).Attributes;
            var attr = attributes.ParameterAttributes?.OfType<FromItemAttribute>().FirstOrDefault();
            if (attr is null)
            {
                return null;
            }
                
            return new FromItemModelBinder(attr.Name);
        }
    }
}
