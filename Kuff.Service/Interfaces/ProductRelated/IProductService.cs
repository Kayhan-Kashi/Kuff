using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.Concretes.Products;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Service;

namespace Kuff.Service.Interfaces.ProductRelated
{
    public interface IProductService : IService
    {
        void Insert(ProductDto item, string virtualPath, string physicalPath, ICollection<PictureTransfer> pictureTransfers);
        List<ProductDto> Get(Expression<Func<ProductDto, bool>> filter = null, Func<IQueryable<ProductDto>, IOrderedQueryable<ProductDto>> orderBy = null, int? count = null);

        void Update(ProductDto item, string virtualPath, string physicalPath,
            ICollection<PictureTransfer> pictureTransfers);
        void Delete(Expression<Func<ProductDto, bool>> filter);
        void Delete(ProductDto item);
        IEnumerable<ProductDto> FindProductsByTerm(string term, string categoryId = "");
    }
}
