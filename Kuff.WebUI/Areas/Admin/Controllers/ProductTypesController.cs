using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Service.Interfaces.ProductRelated;
using Kuff.Common.DTOs.ProductRelated;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IDataTypeService _dataTypeService;
        private readonly ICategoryService _categoryService;

        public ProductTypesController(IProductTypeService productTypeService, IDataTypeService dataTypeService, ICategoryService categoryService)
        {
            _productTypeService = productTypeService;
            _dataTypeService = dataTypeService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DefaultPropertyDataType = _dataTypeService.Get(d => d.Name.Equals("متن"))
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString(), Selected = true });

            ViewBag.Categories = _categoryService.Get()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return View("~/Areas/Admin/Views/ProductTypes/Create.cshtml", new ProductTypeDto());
        }

        [HttpPost]
        public ActionResult Create(ProductTypeDto viewModel)
        {
            ViewBag.DefaultPropertyDataType = _dataTypeService.Get(d => d.Name.Contains("متن"))
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString(), Selected = true });

            ViewBag.Categories = _categoryService.Get()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            if (ModelState.IsValid)
            {
                _productTypeService.Insert(viewModel);
                ViewBag.Success = true;
            }

            return View("~/Areas/Admin/Views/ProductTypes/Create.cshtml", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ViewBag.DataTypes = _dataTypeService.Get()
                .Select(d => new SelectListItem() { Text = d.Name, Value = d.Id.ToString() });

            ProductTypeDto viewModel = _productTypeService.Get(p => p.Id.Equals(id)).Single();
            if (viewModel != null)
            {
                ViewBag.ProductTypeCategorySelected = (IEnumerable<SelectListItem>)(_categoryService.Get()
                    .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id.Equals(viewModel.CategoryId)) }));

                viewModel.ProductTypeProperties = viewModel.ProductTypeProperties.OrderBy(p => p.OrderNumber).ToList();
                return View("~/Areas/Admin/Views/ProductTypes/Edit.cshtml", viewModel);
            }

            return HttpNotFound("The ProductType with this id is not found");
        }

        [HttpPost]
        public ActionResult Edit(ProductTypeDto viewModel)
        {
            ViewBag.DataTypes = _dataTypeService.Get()
                .Select(d => new SelectListItem() { Text = d.Name, Value = d.Id.ToString() });

            ViewBag.ProductTypeCategorySelected = (IEnumerable<SelectListItem>)(_categoryService.Get()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id.Equals(viewModel.CategoryId)) }));

            if (ModelState.IsValid)
            {
                _productTypeService.Update(viewModel);
                ViewBag.Success = true;
            }
            
            return View("~/Areas/Admin/Views/ProductTypes/Edit.cshtml", viewModel);
        }

        public ActionResult List()
        {
            var productTypes = _productTypeService.Get();
            return View(productTypes as IEnumerable<Kuff.Common.DTOs.ProductRelated.ProductTypeDto>);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var prodType = _productTypeService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            return View(prodType as ProductTypeDto);
        }
        [HttpPost]
        public ActionResult Delete(ProductTypeDto viewModel)
        {
            _productTypeService.Delete(viewModel);
            return RedirectToAction("List");
        }

    }
}