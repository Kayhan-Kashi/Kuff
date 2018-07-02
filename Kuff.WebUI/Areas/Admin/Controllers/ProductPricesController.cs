using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class ProductPricesController : Controller
    {
        private IProductPriceService _productPriceService;
        private IProductService _productService;

        public ProductPricesController(IProductPriceService productPriceService, IProductService productService)
        {
            this._productPriceService = productPriceService;
            this._productService = productService;
        }


        public ActionResult List()
        {
            List<ProductPriceDto> productPrices = _productPriceService.Get();
            return View(productPrices);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(_productService.Get(), "Id", "Summary");
            return View(new ProductPriceDto());
        }

        // POST: Admin/ProductPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,DateOfPrice,PriceWithoutDiscount,PriceWithDiscount")] ProductPriceDto productPrice)
        {
            if (ModelState.IsValid)
            {
                _productPriceService.Insert(productPrice);
                return RedirectToAction("List");
            }

            ViewBag.ProductId = new SelectList(_productService.Get(), "Id", "Summary", productPrice.ProductId);
            return View(productPrice);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var productPrice = _productPriceService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            return View(productPrice);
        }

        // POST: Admin/ProductPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,DateOfPrice,PriceWithoutDiscount,PriceWithDiscount")] ProductPriceDto productPrice)
        {
            if (ModelState.IsValid)
            {               
                _productPriceService.Update(productPrice);
                return RedirectToAction("List");
            }

            //ViewBag.ProductId = new SelectList(_productService.Get(), "Id", "Summary", productPrice.ProductId);
            return View(productPrice);
        }


    }
}