using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.Repositories.OrderRelated;
using Kuff.Dal.Repositories.ProductRelated;
using Kuff.Service.Interfaces.AccountRelated;
using Kuff.Service.Interfaces.OrderRelated;
using Kuff.Service.Interfaces.ProductRelated;
using Kuff.Service.Services.AccountRelated;
using Kuff.Service.Services.OrderRelated;
using Kuff.Service.Services.ProductRelated;
using Microsoft.Practices.Unity;

namespace Kuff.Service.IocContainer
{
    public class SvcIocConfigurator
    {
        public static IUnityContainer ConfigureServiceLayerContainer(IUnityContainer container)
        {
            Kuff.Dal.IocContainer.DalIocConfigurator.ConfigureDalLayerContainer(container);
            RegisterServices(container);
            return container;
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IDataTypeService, DataTypeService>();
            container.RegisterType<IProductTypeService, ProductTypeService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductPictureService, ProductPictureService> ();
            container.RegisterType<IProductPropertyValueService, ProductPropertyValueService>();
            container.RegisterType<IProductAvailabilityService, ProductAvailabilityService>();
            container.RegisterType<IProductPriceService, ProductPriceService>();
            container.RegisterType<IShipmentMethodService, ShipmentMethodService>();
            container.RegisterType<IShipmentCostService, ShipmentCostService>();
            container.RegisterType<IPaymentMethodService, PaymentMethodService>();
            //container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IOrderService, OrderService>();
        }
    }
}
