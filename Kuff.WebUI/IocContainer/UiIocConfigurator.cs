using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace Kuff.WebUI.IocContainer
{
    public class UiIocConfigurator
    {
        public static IUnityContainer ConfigureUiLayerContainer(IUnityContainer container)
        {
            RegisterServices(container);
            return Kuff.Service.IocContainer.SvcIocConfigurator.ConfigureServiceLayerContainer(container);
        }

        private static void RegisterServices(IUnityContainer container)
        {

        }
    }
}