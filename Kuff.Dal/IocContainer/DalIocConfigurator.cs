using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Dal.Repositories.AccountRelated;
using Kuff.Dal.Repositories.OrderRelated;
using Kuff.Dal.Repositories.ProductRelated;
using Microsoft.Practices.Unity;

namespace Kuff.Dal.IocContainer
{
    public class DalIocConfigurator
    {
        
        public static IUnityContainer ConfigureDalLayerContainer(IUnityContainer container)
        {
            RegisterServices(container);
            return container;
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IRepository<CategoryDto>, CategoryRepository>();
            container.RegisterType<IRepository<DataTypeDto>, DataTypeRepository>();
            container.RegisterType<IRepository<ProductTypeDto>, ProductTypeRepository>();
            container.RegisterType<IRepository<ProductDto>, ProductRepository>();
            container.RegisterType<IRepository<ProductPictureDto>, ProductPictureRepository>();
            container.RegisterType<IRepository<ProductPropertyValueDto>, ProductPropertyValueRepository>();
            container.RegisterType<IRepository<ProductAvailabilityDto>, ProductAvailabilityRepository>();
            container.RegisterType<IRepository<ProductPriceDto>, ProductPriceRepository>();
            container.RegisterType<IRepository<ShipmentMethodDto>, ShipmentMethodRepository>();
            container.RegisterType<IRepository<ShipmentCostDto>, ShipmentCostRepository>();
            container.RegisterType<IRepository<PaymentMethodDto>, PaymentMethodRepository>();
            container.RegisterType<IRepository<OrderDto>, OrderRepository>();
        }
    }
}
