using Autofac;
using Autofac.Integration.WebApi;
using StateInterface.Model.Interface;
using StateInterface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SunGardStateInterface.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            builder.RegisterType<StateInterfaceTasks>().As<IStateInterfaceTasks>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
        }
    }
}
