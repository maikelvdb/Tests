using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Test.Api.ParameterBinding.FromItem
{
    public class FromItemBindingSource : BindingSource
    {
        public static readonly BindingSource FromItem = new FromItemBindingSource(
            "FromItem",
            "FromItem",
            true,
            true
        );

        public FromItemBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest)
            : base(id, displayName, isGreedy, isFromRequest)
        {
        }

        public override bool CanAcceptDataFrom(BindingSource bindingSource)
        {
            return bindingSource == Custom || bindingSource == this;
        }
    }
}
