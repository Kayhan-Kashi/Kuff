using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebSockets;
using Kuff.WebUI.App_Start;
using Kuff.WebUI.Infrastructure;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Kuff.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Populating UnityContainer with registrations
            Microsoft.Practices.Unity.IUnityContainer container = Kuff.WebUI.App_Start.DependencyInjectionModule.ConfigureIocContainer(new UnityContainer());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            IServiceLocator locator = new UnityServiceLocator(container);
            EnterpriseLibraryContainer.Current = locator;
            // Initializing EnterpriseLibraryContainer's Container for using ServiceLocator in other layers.
            //var configurator = new UnityContainerConfigurator(container);
            //EnterpriseLibraryContainer.ConfigureContainer(configurator, ConfigurationSourceFactory.Create());
            //Kuff.WebUI.App_Start.DependencyInjectionModule.ConfigureUiLayerContainer(container);

        }
    }
}
