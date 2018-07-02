using Kuff.Common.DTOs.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;
using Kuff.WebUI.Areas.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kuff.WebUI.Areas.Store.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IProductTypeService _productTypeService;

        public HomeController(IProductService productService, ICategoryService categoryService, IProductTypeService productTypeService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._productTypeService = productTypeService;
        }

        public ActionResult Index()
        {
            ViewBag.Tshirts = _productService.Get(p => p.ProductTypeTitle.Equals("تی شرت")).ToList().OrderByDescending(pr => pr.IsAvailable);
            ViewBag.Shawls = _productService.Get(p => p.ProductTypeTitle.Equals("شال")).ToList().OrderByDescending(pr => pr.IsAvailable);
            ViewBag.Mugs = _productService.Get(p => p.ProductTypeTitle.Equals("ماگ")).ToList().OrderByDescending(pr => pr.IsAvailable);

            return View("~/Areas/Store/Views/Home/Index.cshtml");
        }

        [HttpGet]
        public ActionResult ProductDetails(Guid id)
        {
            var prod = _productService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            ViewBag.Relateds = _productService.Get(p => p.ProductTypeTitle.Equals(prod.ProductTypeTitle)).Where(p => p.Id != prod.Id).OrderByDescending(p => p.IsAvailable).Take(10).ToList();
            ViewBag.MostDiscounted = _productService.Get().Where(p => p.IsAvailable).OrderByDescending(p => p.Discount);
            return View("~/Areas/Store/Views/Home/ProductDetails.cshtml", new ProductDetailsViewModel(prod));
        }

        public ActionResult Search(string term, string categoryId = "", int pageSize = 5, int page = 1, string sortPredicate = "")
        {
            ViewBag.Page = page;
            ViewBag.Term = term;
            ViewBag.CategoryId = categoryId;
            int[] pageSizes = { 5, 10, 20, 50 };
            ViewBag.PageSizes = pageSizes.Select(p => new SelectListItem { Text = p.ToString(), Value = p.ToString(), Selected = p.ToString().Equals(pageSize) });

            ViewBag.PageSize = pageSize;
            ViewBag.Categories = _categoryService.Get().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });


            IEnumerable<ProductDto> products;
            if (categoryId == string.Empty)
            {
                products = _productService.FindProductsByTerm(term);
            }
            else
            {
                products = _productService.FindProductsByTerm(term, categoryId);
            }

            products = SortByPredicate(sortPredicate, products);

            var sortPredicates = new[] { new { title = "پرتخفیف ترین", val = "mostDiscount" }, new { title = "گرانترین", val = "mostExpensive" }, new { title = "ارزانترین", val = "cheapest" } };
            ViewBag.SortPredicates = sortPredicates.Select(s => new SelectListItem { Text = s.title, Value = s.val });

            ViewBag.PageNumbers = Math.Ceiling(products.ToList().Count / (decimal)pageSize);
            return View("~/Areas/Store/Views/Home/Search.cshtml", products.OrderByDescending(p => p.IsAvailable).Skip((page - 1) * pageSize).Take(pageSize));
        }

        private  IEnumerable<ProductDto> SortByPredicate(string sortPredicate, IEnumerable<ProductDto> products)
        {
            if (sortPredicate != string.Empty)
            {
                if (sortPredicate == "mostDiscount")
                {
                    products = products.OrderByDescending(p => p.Discount);
                }
                else if (sortPredicate == "mostExpensive")
                {
                    products = products.OrderByDescending(p => p.LastPriceWithDiscount);
                }
                else if (sortPredicate == "cheapest")
                {
                    products = products.OrderBy(p => p.LastPriceWithDiscount);
                }
            }
            return products;
        }

        public ActionResult ContactUs()
        {
            return View("~/Areas/Store/Views/Home/ContactUs.cshtml");
        }

        public ActionResult AboutUs()
        {
            return View("~/Areas/Store/Views/Home/AboutUs.cshtml");
        }

        private void InitializeReturnUrl()
        {
            
        }
    }
}