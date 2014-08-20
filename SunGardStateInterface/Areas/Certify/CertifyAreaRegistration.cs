using System.Web.Mvc;

namespace StateInterface.Areas.Certify
{
    public class CertifyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Certify";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UpdateForm_default",
                "Certify/{controller}/{action}/{recordsCenter}/{formId}",
                new { controller = "Home", action = "UpdateForm" },
                new[] { "StateInterface.Areas.Certify.Controllers" }
                );

            context.MapRoute(
                "Status_OpenIssues_default",
                "Certify/{controller}/{action}/{recordsCenterName}",
                new { controller = "Home", action = "Status" },
                new[] { "StateInterface.Areas.Certify.Controllers" }
                );

            context.MapRoute(
                "Certify_default",
                "Certify/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "StateInterface.Areas.Certify.Controllers" }
            );
        }
    }
}