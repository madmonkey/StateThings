using System.Web.Mvc;

namespace SunGardStateInterface.Areas.Design
{
    public class DesignAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Design";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            //context.MapRoute("Snippet_CreateFieldSnippet",
            //                "Design/Transaction/CreateSnippetField",
            //                new { controller = "Transaction", action = "CreateSnippetField" },
            //                new[] { "StateInterface.Areas.Design.Controllers" });

            context.MapRoute("Snippet_Details",
                            "Design/Transaction/Details/{recordsCenterName}/{tokenName}",
                            new { controller="Transaction", action = "Details" },
                            new[] { "StateInterface.Areas.Design.Controllers" });

            context.MapRoute("Snippet_Update",
                "Design/Transaction/Details/{recordsCenterName}/{tokenName}",
                new { controller = "Transaction", action = "Details" },
                new[] { "StateInterface.Areas.Design.Controllers" });


            context.MapRoute("Field_Details",
                            "Design/Field/Details/{recordsCenterName}/{tagName}",
                            new { controller = "Field", action = "Details" },
                            new[] { "StateInterface.Areas.Design.Controllers" });

            context.MapRoute("List_Details",
                "Design/List/Details/{recordsCenterName}/{listName}",
                new { controller = "List", action = "Details" },
                new[] { "StateInterface.Areas.Design.Controllers" });

            context.MapRoute("UpdateForm",
                            "Design/{controller}/UpdateForm/{recordsCenterName}/{formId}",
                            new { action = "UpdateForm" },
                            new[] { "StateInterface.Areas.Design.Controllers" });

            context.MapRoute("FormPath_default",
                            "Design/{controller}/{action}/{recordsCenterName}/{formId}",
                            new { action = "Index" },
                            new[] { "StateInterface.Areas.Design.Controllers" });

            context.MapRoute("Design_default",
                            "Design/{controller}/{action}/{id}",
                            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                            new[] { "StateInterface.Areas.Design.Controllers" });
        }
    }
}