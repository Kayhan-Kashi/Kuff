using Kuff.Dal.DataModel.OrderRelated;
using Kuff.Dal.DataModel.ProductRelated;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Dal.EFMappings
{
    public class EntityTypeConfigs
    {

    }

    #region ProductRelated mappings
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            this.HasKey(c => c.Id);
            // آوردیم اینجا دیگر نیازی نیست بیاید productTypeچون در 
            //.HasMany(c => c.ProductTypes)
            //.WithRequired(p => p.Category)
            //.HasForeignKey(c => c.ProductTypeId);   
        }
    }

    public class DataTypeMapping : EntityTypeConfiguration<DataType>
    {
        public DataTypeMapping()
        {
            this.HasKey(c => c.Id);
        }
    }

    public class ProductTypeMapping : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.Category)
                .WithMany(c => c.ProductTypes)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }

    public class ProductTypePropertyMapping : EntityTypeConfiguration<ProductTypeProperty>
    {
        public ProductTypePropertyMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.ProductType)
                .WithMany(p => p.ProductTypeProperties)
                .HasForeignKey(p => p.ProductTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.DataType)
                .WithMany()
                .HasForeignKey(p => p.DataTypeId)
                .WillCascadeOnDelete(false);
        }
    }

    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId)
                .WillCascadeOnDelete(false);

            //this.HasRequired(p => p.ProductType)
            //    .WithMany()
            //    .HasForeignKey(p => p.ProductTypeId)
            //    .WillCascadeOnDelete(false);
        }
    }

    public class ProductPropertyValueMapping : EntityTypeConfiguration<ProductPropertyValue>
    {
        public ProductPropertyValueMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.ProductTypeProperty)
                .WithMany()
                .HasForeignKey(p => p.ProductTypePropertyId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Product)
                .WithMany(p => p.ProductPropertyValues)
                .HasForeignKey(p => p.ProductId)
                .WillCascadeOnDelete(false);
        }
    }

    public class ProductPictureMapping : EntityTypeConfiguration<ProductPicture>
    {
        public ProductPictureMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.Product)
                .WithMany(p => p.ProductPictures)
                .HasForeignKey(p => p.ProductId)
                .WillCascadeOnDelete(false);
        }
    }

    public class ProductAvailabilityMapping : EntityTypeConfiguration<ProductAvailability>
    {
        public ProductAvailabilityMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.Product)
                .WithMany(p => p.ProductAvailabilities)
                .HasForeignKey(p => p.ProductId)
                .WillCascadeOnDelete(false);
        }
    }

    public class ProductPriceMapping : EntityTypeConfiguration<ProductPrice>
    {
        public ProductPriceMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.Product)
                .WithMany(p => p.ProductPrices)
                .HasForeignKey(p => p.ProductId)
                .WillCascadeOnDelete(false);

            //this.Ignore(p => p.DiscountInPercent);
            //this.Ignore(p => p.Discount);
        }
    }

    #endregion

    #region OrderRelated mappings
    public class OrderItemSpecificationMapping : EntityTypeConfiguration<OrderItemSpecification>
    {
        public OrderItemSpecificationMapping()
        {
            this.HasKey(o => o.Id)
                .HasRequired(o => o.OrderItem)
                .WithMany(o => o.OrderItemSpecifications)
                .HasForeignKey(o => o.OrderItemId)
                .WillCascadeOnDelete();
        }

    }

    public class OrderItemMapping : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMapping()
        {
            this.HasKey(or => or.Id)
                .HasRequired(or => or.ProductPrice)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.ProductPriceId)
                .WillCascadeOnDelete(false);

            this.HasRequired(or => or.Order)
                .WithMany(or => or.OrderItems)
                .HasForeignKey(or => or.OrderId)
                .WillCascadeOnDelete(false);
        }
    }

    public class OrderMapping : EntityTypeConfiguration<Order>
    {
        public OrderMapping()
        {
            this.HasKey(or => or.Id)
                .HasRequired(or => or.Payment)
                .WithRequiredPrincipal(p => p.Order)
                .WillCascadeOnDelete(false);

            this.HasRequired(or => or.ShipmentCost)
                .WithMany()
                .HasForeignKey(or => or.ShipmentCostId)
                .WillCascadeOnDelete();

            this.HasRequired(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            //چرا در اینجا کلید خارجی نمی گیرد
        }
    }

    public class PaymentMapping : EntityTypeConfiguration<Payment>
    {
        public PaymentMapping()
        {
            this.HasKey(p => p.Id);
            //.HasRequired(p => p.PaymentSpecification)
            //.WithRequiredPrincipal(p => p.Payment)
            //.WillCascadeOnDelete(false);
            //HasRequired(p => p.ShipmentCost)
            //.WithMany()
            //.HasForeignKey(p => p.ShipmentCostId);

            this.HasRequired(p => p.PaymentMethod)
                .WithMany()
                .HasForeignKey(p => p.PaymentMethodId)
                .WillCascadeOnDelete(false);
        }
    }
    public class PaymentMethodMapping : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodMapping()
        {
            this.HasKey(p => p.Id);
        }
    }

    public class ShipmentMethodMapping : EntityTypeConfiguration<ShipmentMethod>
    {
        public ShipmentMethodMapping()
        {
            this.HasKey(p => p.Id);
        }
    }

    public class ShipmentCostMapping : EntityTypeConfiguration<ShipmentCost>
    {
        public ShipmentCostMapping()
        {
            this.HasKey(p => p.Id)
                .HasRequired(p => p.ShipmentMethod)
                .WithMany(p => p.ShipmentCosts)
                .HasForeignKey(p => p.ShipmentMethodId)
                .WillCascadeOnDelete(false);
        }
    }
    #endregion 
}
