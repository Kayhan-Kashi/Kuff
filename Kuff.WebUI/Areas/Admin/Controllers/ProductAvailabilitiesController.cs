using Kuff.Common.DTOs.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class ProductAvailabilitiesController : Controller
    {
        private readonly IProductAvailabilityService _productAvailabilityService;
        private readonly IProductService _productService;

        public ProductAvailabilitiesController(IProductAvailabilityService productAvailabilityService, IProductService productService)
        {
            this._productAvailabilityService = productAvailabilityService;
            this._productService = productService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(_productService.Get(), "Id", "Summary");
            return View(new ProductAvailabilityDto());
        }

        [HttpPost]
        public ActionResult Create(ProductAvailabilityDto viewModel)
        {
            _productAvailabilityService.Insert(viewModel);
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            List<ProductAvailabilityDto> productAvailavilities = _productAvailabilityService.Get();
            return View(productAvailavilities);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var productAvailability = _productAvailabilityService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            return View(productAvailability);
        }

        [HttpPost]
        public ActionResult Edit(ProductAvailabilityDto viewModel)
        {
            _productAvailabilityService.Update(viewModel);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var productAvailability = _productAvailabilityService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            return View(productAvailability);
        }

        [HttpPost]
        public ActionResult Delete(ProductAvailabilityDto ViewModel)
        {
            //var categoryService = new CategoryService();
            _productAvailabilityService.Delete(p => p.Id.Equals(ViewModel.Id));
            return RedirectToAction("List");
        }
    }
}