using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace uni_cap_pro_be.Utils
{
    // DONE
    public class RoutePrefixConvention(string prefix) : IControllerModelConvention
    {
        private readonly string _prefix = prefix;

        public void Apply(ControllerModel controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(
                    nameof(controller),
                    "ControllerModel cannot be null."
                );
            }

            if (controller.ControllerName == null)
            {
                throw new NullReferenceException("ControllerName cannot be null.");
            }

            if (controller.Selectors.Count > 0)
            {
                var routeTemplate = controller.Selectors[0].AttributeRouteModel.Template;
                if (!string.IsNullOrEmpty(routeTemplate))
                {
                    controller.Selectors[0].AttributeRouteModel.Template =
                        $"{_prefix}/{routeTemplate.TrimStart('/')}";
                }
            }
        }
    }
}
