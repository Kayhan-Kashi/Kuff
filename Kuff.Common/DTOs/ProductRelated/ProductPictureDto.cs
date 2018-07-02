using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.DTOs.ProductRelated
{
    public class ProductPictureDto
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        #endregion

        #region Static Properties
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string FileExtension { get; set; }
        public int PictureOrder { get; set; }
        public bool IsForSummaryPreview { get; set; }
        public bool IsMainPicture { get; set; }
        #endregion

        #region Product Properties
        public string ProductDateOfAdding { get; set; }

        public string ProductSummary { get; set; }

        public string ProductComments { get; set; }
        #endregion
    }
}
