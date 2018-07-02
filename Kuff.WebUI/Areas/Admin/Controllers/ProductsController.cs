using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using Kuff.Common.Concretes.Products;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;
using Kuff.Service.Services.ProductRelated;
using System.Reflection;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private IProductTypeService _productTypeService;
        private IDataTypeService _dataTypeService;
        private ICategoryService _categoryService;
        private IProductPictureService _productPictureService;
        private IProductPropertyValueService _productPropertyValueService;

        string virtualPath = "~/images/";

        public ProductsController(IProductService productService, IProductTypeService productTypeService, IDataTypeService dataTypeService, ICategoryService categoryService, IProductPictureService productPictureService, IProductPropertyValueService productPropertyValueService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _productPictureService = productPictureService;
            _dataTypeService = dataTypeService;
            _categoryService = categoryService;
            _productPropertyValueService = productPropertyValueService;
        }

        public ActionResult List()
        {
            var products = _productService.Get();
            return View(products);

        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductTypesSelectListItems = _productTypeService.Get().Select(p => new SelectListItem { Text = p.Title, Value = p.Id.ToString() });
            return View("~/Areas/Admin/Views/Products/Create.cshtml", new ProductDto { Id = Guid.NewGuid() });
        }

        [HttpPost]
        public ActionResult Create(ProductDto product, HttpPostedFileBase[] files)
        {
            // The virtual path which images are stored
            string virtualPath = "~/images/";

            // The physical path which images will be stored
            string physicalPath = Server.MapPath(virtualPath);

            // Pictures will be transfered to service layer and will be saved there. 
            List<PictureTransfer> pictureTransfers = new List<PictureTransfer>();

            if (ModelState.IsValid)
            {
                foreach (var postedFile in files)
                {
                    pictureTransfers.Add(new PictureTransfer
                    {
                        Stream = postedFile.InputStream,
                        ContentLength = postedFile.ContentLength,
                        ContentType = postedFile.ContentType,
                        FileName = postedFile.FileName,
                        State = PictureTransfer.States.Added
                    });
                }

                _productService.Insert(product, virtualPath, physicalPath, pictureTransfers);
                ViewBag.Success = true;
            }
            ViewBag.ProductTypesSelectListItems = _productTypeService.Get().Select(p => new SelectListItem { Text = p.Title, Value = p.Id.ToString() });
            return View("~/Areas/Admin/Views/Products/Create.cshtml", product);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ProductDto productVM = _productService.Get(p => p.Id.Equals(id)).FirstOrDefault();

            if (productVM != null)
            {
                @ViewBag.PicOrders = productVM.ProductPictures.Select(p => new SelectListItem { Text = p.PictureOrder.ToString(), Value = p.PictureOrder.ToString() });
            }

            return View("~/Areas/Admin/Views/Products/Edit.cshtml", productVM);
        }

        [HttpPost]
        public ActionResult Edit(ProductDto product, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                string physicalPath = Server.MapPath(virtualPath);
                PictureTransfer[] picTransfers = new PictureTransfer[files.Length];

                // specify Picture states 
                for (int i = 0; i < product.ProductPictures.Count(); i++)
                {
                    ProductPictureDto prodPic = product.ProductPictures.ElementAt(i);

                    if ((Request.Form["changeRadio_" + i] as string) != null)
                    {
                        if ((Request.Form["changeRadio_" + i] as string).Equals("state_deleted"))
                        {
                            picTransfers[i] = new PictureTransfer { State = PictureTransfer.States.Deleted };
                            //picTransfers[i].State = PictureTransfer.States.Deleted;
                            //System.IO.File.Delete(Server.MapPath(prodPic.FilePath));
                        }
                        else if ((Request.Form["changeRadio_" + i] as string).Equals("state_updated"))
                        {
                            if (files[i] != null)
                            {
                                picTransfers[i] = new PictureTransfer
                                {
                                    State = PictureTransfer.States.Updated,
                                    FileName = files[i].FileName,
                                    ContentType = files[i].ContentType,
                                    ContentLength = files[i].ContentLength,
                                    Stream = files[i].InputStream
                                };

                                //System.IO.File.Delete(Server.MapPath(prodPic.FilePath));
                                //string virtualPath;
                                //SaveProductPicture(files[i], "~/Images", prodPic, out virtualPath);
                                //ModifyProductPicture(files[i], prodPic, virtualPath);
                            }
                        }
                        else if ((Request.Form["changeRadio_" + i] as string).Equals("state_unchanged"))
                        {
                            picTransfers[i] = new PictureTransfer
                            {
                                State = PictureTransfer.States.Unchanged
                        };
                            //picTransfers[i].State = PictureTransfer.States.Unchanged;
                        }
                    }
                    else
                    {
                        picTransfers[i] = new PictureTransfer
                        {
                            State = PictureTransfer.States.Added,
                            Stream = files[i].InputStream,
                            ContentType = files[i].ContentType,
                            FileName = files[i].FileName,
                            ContentLength = files[i].ContentLength
                        };
                        //SaveProductPicture(files[i], "~/Images", prodPic, out virtualPath);
                        //ModifyProductPicture(files[i], prodPic, virtualPath);
                        //prodPic.State = "state_added";
                    }
                }

                _productService.Update(product, virtualPath, physicalPath, picTransfers);
                ViewBag.Success = true;
            }

            ProductDto prod = _productService.Get(p => p.Id.Equals(product.Id)).FirstOrDefault();
            prod.ProductPropertyValues = prod.ProductPropertyValues.OrderBy(p => p.ProductTypePropertyOrderNumber).ToList();
            prod.ProductPictures = product.ProductPictures.OrderBy(pp => pp.PictureOrder).ToList();
            @ViewBag.PicOrders =
                (IEnumerable<SelectListItem>)prod.ProductPictures
                    .Select(p => new SelectListItem { Text = p.PictureOrder.ToString(), Value = p.PictureOrder.ToString() });
            return View("~/Areas/Admin/Views/Products/Edit.cshtml", prod);
        }

        [HttpPost]
        public PartialViewResult GetProductTypePropertiesInputFormWithPost(Guid id)
        {
            var productId = Request.Form["prodId"] as string;
            ViewBag.ProductId = productId;
            var productTypeProperties =
                _productTypeService.Get(p => p.Id.Equals(id)).FirstOrDefault().ProductTypeProperties
                    .OrderBy(pr => pr.OrderNumber).ToArray();          
            return PartialView("~/Areas/Admin/Views/Products/ProductTypePropertiesInputForm.cshtml", productTypeProperties);
        }

        public FileStreamResult GetProductImageById(Guid id)
        {
            ProductPictureDto picture = _productPictureService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            if (picture != null)
            {
                return new FileStreamResult(new FileStream(Server.MapPath(picture.FilePath), FileMode.Open), "image/jpeg");
            }
            return null;
        }

        public JsonResult GetProductPropertyByTitle(Guid id, string propertyTitle)
        {
            ProductDto prod = _productService.Get(p => p.Id.Equals(id)).FirstOrDefault();

            Type type = typeof(ProductDto);
            PropertyInfo[] propertyInfoArray = type.GetProperties();

            foreach (PropertyInfo pInfo in propertyInfoArray)
            {
                if (pInfo.Name.Equals(propertyTitle))
                {
                    return Json(pInfo.GetValue(prod), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPropertyValueById(Guid id)
        {
            ProductPropertyValueDto pValue = _productPropertyValueService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            if (pValue != null)
            {
                return Json(pValue.Value, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}