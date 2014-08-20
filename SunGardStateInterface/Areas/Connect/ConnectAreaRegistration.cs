using System.Web.Mvc;

namespace StateInterface.Areas.Connect
{
    public class ConnectAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Connect";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FormSpecification_default",
                "Connect/{controller}/FormSpecification/{recordsCenterName}/{formId}",
                new { controller = "Specification" },
                new[] { "StateInterface.Areas.Connect.Controllers" }
            );

            context.MapRoute(
                "CategorySpecifications_default",
                "Connect/Specifications/CategorySpecification/{recordsCenterName}/{category}",
                new { },
                new[] { "StateInterface.Areas.Connect.Controllers" }
            );

            context.MapRoute(
                "Connect_default",
                "Connect/{controller}/{action}",
                new { controller = "Home", action = "Index" },
                new[] { "StateInterface.Areas.Connect.Controllers" }
            );
        }
    }
}