using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;

namespace Investimento.Api.Convetions
{
    public class ApiVersionRouteConvention : RouteConvention
    {
        public ApiVersionRouteConvention(string template) : base(new RouteAttribute(template)) { }
    }

    public class RouteConvention : IApplicationModelConvention
    {
        readonly AttributeRouteModel _template;

        public RouteConvention(IRouteTemplateProvider template)
        {
            if (template == null) throw new System.ArgumentException(nameof(template));

            _template = new AttributeRouteModel(template);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var selectors = controller.Selectors.Where(x => x.AttributeRouteModel != null);

                if (selectors.Any())
                {
                    foreach (var model in selectors)
                    {
                        model.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                            _template,
                            model.AttributeRouteModel
                        );
                    }
                }
            }
        }
    }
}