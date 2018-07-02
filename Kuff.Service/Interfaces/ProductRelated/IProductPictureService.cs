using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Common.Interfaces.Service;

namespace Kuff.Service.Interfaces.ProductRelated
{
    public interface IProductPictureService : IService
    {
        List<ProductPictureDto> Get(Expression<Func<ProductPictureDto, bool>> filter);
        void Insert(ProductPictureDto item);
        void Update(ProductPictureDto item);
        void Delete(ProductPictureDto item);
    }
}
