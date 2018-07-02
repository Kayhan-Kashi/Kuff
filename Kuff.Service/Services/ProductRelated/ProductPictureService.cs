using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Repositories;
using Kuff.Common.Interfaces.Service;
using Kuff.Common.Interfaces.UnitOfWork;
using Kuff.Dal.Repositories.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;

namespace Kuff.Service.Services.ProductRelated
{
    public class ProductPictureService : IProductPictureService
    {

        public ProductPictureService(IRepository<ProductPictureDto> repository)
        {
            Repository = repository as ProductPictureRepository;
        }

        public ProductPictureRepository Repository { get; set; }
        IRepository IService.Repository
        {
            get
            {
                return Repository;
            }

            set
            {
                Repository = value as ProductPictureRepository;
            }
        }

        public IUnitOfWork Unit { get; set; }

        public List<ProductPictureDto> Get(Expression<Func<ProductPictureDto, bool>> filter)
        {
            var query = Repository.Get();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public void Insert(ProductPictureDto item)
        {
            if (Unit != null)
            {
                Repository.Insert(item, false);
            }
            else
            {
                Repository.Insert(item);
            }
        }

        public void Update(ProductPictureDto item)
        {
            //if Unit is not null means that the method is called after another method in the service and so it should not save the changes and commit the transaction.
            if (Unit != null)
            {
                Repository.Update(item, false);
            }
            //if Unit is null means that the method is the first and only method in the transaction and it can save the changes and commit the transaction.
            else
            {
                Repository.Update(item);
            }
        }

        public void Delete(ProductPictureDto item)
        {
            if (Unit != null)
            {
                Repository.Delete(item, false);
            }
            else
            {
                Repository.Delete(item);
            }
        }
    }
}
