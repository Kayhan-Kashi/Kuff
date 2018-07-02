using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;

namespace Kuff.Dal.Repositories.ProductRelated
{
    public class ProductPropertyValueRepository : IRepository<ProductPropertyValueDto>
    {
        private KuffEntities _context;

        public KuffEntities Context
        {
            get { return _context ?? new KuffEntities(); }
            set { _context = value; }
        }

        DbContext IRepository.Context
        {
            get { return Context; }

            set
            {
                Context = value as KuffEntities;
            }
        }

        public void Delete(ProductPropertyValueDto item, bool save = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductPropertyValueDto> Get()
        {
            return Context.ProductPropertyValues.Select(p => new ProductPropertyValueDto
            {
                Id = p.Id,
                ProductId = p.ProductId,
                ProductComments = p.Product.Comments,
                ProductDateOfAdding = p.Product.DateOfAdding,
                ProductSummary = p.Product.Summary,
                Value = p.Value,
                ProductTypePropertyId = p.ProductTypeProperty.Id,
                ProductTypePropertyTitle = p.ProductTypeProperty.Title,
                ProductTypePropertyIsUserDecision = p.ProductTypeProperty.IsUserDecision,
                ProductTypePropertyOrderNumber = p.ProductTypeProperty.OrderNumber
            });
        }

        public void Insert(ProductPropertyValueDto item, bool save = true)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductPropertyValueDto item, bool save = true)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Methods
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion
    }
}
