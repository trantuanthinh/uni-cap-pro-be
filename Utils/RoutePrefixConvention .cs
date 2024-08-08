using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace uni_cap_pro_be.Utils
{
	public class RoutePrefixConvention : IControllerModelConvention
	{
		private readonly string _prefix;

		public RoutePrefixConvention(string prefix)
		{
			_prefix = prefix;
		}

		public void Apply(ControllerModel controller)
		{
			if (controller.Selectors.Count > 0)
			{
				var routeTemplate = controller.Selectors[0].AttributeRouteModel.Template;
				if (!string.IsNullOrEmpty(routeTemplate))
				{
					controller.Selectors[0].AttributeRouteModel.Template = $"{_prefix}/{routeTemplate.TrimStart('/')}";
				}
			}
		}
	}
}