using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using StateInterface.Designer.Model;
using Designer.Tasks;
using StateInterface.Designer.Repository;
using System.Security.Principal;
using System.Threading;

namespace StateInterface
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class StateInterfaceApplication : System.Web.HttpApplication
    {
        protected void Application_AuthenticateRequest(object sender, EventArgs args)
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                var designerTasks = DependencyResolver.Current.GetService<IDesignerTasks>();

                User user = designerTasks.GetUser(new TaskParameter(User.Identity.Name));

                if (user != null)
                {
                    List<string> roles = user.Roles.Select(x => x.Name).ToList();

                    GenericPrincipal principal = new GenericPrincipal(HttpContext.Current.User.Identity, roles.ToArray());

                    Thread.CurrentPrincipal = HttpContext.Current.User = principal;
                }
            }
        }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(StateInterfaceApplication).Assembly);
            builder.RegisterApiControllers(typeof(StateInterfaceApplication).Assembly);

            builder.RegisterType<DesignerTasks>().As<IDesignerTasks>().InstancePerRequest();
            builder.RegisterType<DesignerRepository>().As<IDesignerRepository>().InstancePerRequest();

            var container = builder.Build();

            // For MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // For Web API
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}