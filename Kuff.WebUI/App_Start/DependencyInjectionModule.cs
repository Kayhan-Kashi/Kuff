using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Service;
using Kuff.Service.Interfaces.ProductRelated;
using Kuff.Service.Services.ProductRelated;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity;

namespace Kuff.WebUI.App_Start
{
    public class DependencyInjectionModule
    {
        public static IUnityContainer ConfigureIocContainer(IUnityContainer container)
        {
            return Kuff.WebUI.IocContainer.UiIocConfigurator.ConfigureUiLayerContainer(container);
        }
    }
}