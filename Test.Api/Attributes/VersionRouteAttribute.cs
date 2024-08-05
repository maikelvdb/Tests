using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Test.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class VersionRouteAttribute(string template) : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var defaultRoute = new AttributeRouteModel(new RouteAttribute(template));
            var versionRoute = new AttributeRouteModel(new RouteAttribute(template + "/{VersionNr:int}"));

            controller.Selectors.Add(new() { AttributeRouteModel = defaultRoute, });
            controller.Selectors.Add(new() { AttributeRouteModel = versionRoute, });
        }
    }
}
