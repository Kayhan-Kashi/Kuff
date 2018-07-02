using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuff.Common.Concretes.Products
{
    public class PictureTransfer
    {
        public enum States { Added, Updated, Deleted, Unchanged}
        public States State { get; set; }
        public System.IO.Stream Stream { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }

    }
}
