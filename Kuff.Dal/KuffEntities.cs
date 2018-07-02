using Kuff.Dal.DataModel.ProductRelated;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Dal.DataModel.OrderRelated;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kuff.Dal
{
    public class KuffEntities : IdentityDbContext
    {
        public KuffEntities() : base("kayhanka_KuffDbContext") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<DataType> DataTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductTypeProperty> ProductTypeProperties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPropertyValue> ProductPropertyValues { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductAvailability> ProductAvailabilities { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }

        public DbSet<ShipmentCost> ShipmentCosts { get; set; }
        public DbSet<ShipmentMethod> ShipmentMethods { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Kuff.Common.DTOs.AccountRelated.ApplicationUser> AppUsers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.CategoryMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.DataTypeMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ProductTypeMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ProductTypePropertyMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ProductMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ProductPropertyValueMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ProductPictureMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ProductAvailabilityMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ProductPriceMapping());

                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ShipmentMethodMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.ShipmentCostMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.PaymentMethodMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.OrderMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.OrderItemMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.OrderItemSpecificationMapping());
                modelBuilder.Configurations.Add(new Kuff.Dal.EFMappings.PaymentMapping());
            }
            catch
            {
                throw;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
