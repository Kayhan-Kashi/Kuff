using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;

namespace Kuff.WebUI.Areas.Store.Controllers
{
    public class CategoriesController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;

        public CategoriesController(IProductService productService, ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }


        public ActionResult FilterByType(string productTypeName, int pageSize = 10, int page = 1, string sortPredicate = "")
        {
            ViewBag.pageSize = pageSize;
            ViewBag.page = page;
            ViewBag.Term = productTypeName;
            int[] pageSizes = { 5, 10, 20, 40, 50 };
            ViewBag.PageSizes = pageSizes.Select(p => new SelectListItem { Text = p.ToString(), Value = p.ToString(), Selected = p.ToString().Equals(pageSize) });
            IEnumerable<ProductDto> products;

            if (sortPredicate == string.Empty)
            {
                products = _productService.Get(p => p.ProductTypeTitle.Equals(productTypeName));
            }
            else
            {
                products = _productService.Get(p => p.ProductTypeTitle.Contains(productTypeName));

                products =  SortByPredicate(sortPredicate, products);
            }

            IEnumerable<ProductDto> productsToShow = products.OrderByDescending(p => p.IsAvailable).Skip((page - 1) * pageSize).Take(pageSize);
            ViewBag.PageNumbers = Math.Ceiling(products.ToList().Count / (decimal)pageSize);

            ViewBag.MostDiscounted = _productService.Get().OrderByDescending(p => p.Discount).ToList();
            ViewBag.Categories = _categoryService.Get();

            //string[,] sortPredicates = new string[,] { { "mostDiscount", "پرتخفیف ترین" }, { "mostExpensive", "گرانترین" }, { "cheapest", "ارزانترین" } };

            var sortPredicates = new[] { new { title = "پرتخفیف ترین", val = "mostDiscount" }, new { title = "گرانترین", val = "mostExpensive" }, new { title = "ارزانترین", val = "cheapest" } };
            ViewBag.SortPredicates = sortPredicates.Select(s => new SelectListItem { Text = s.title, Value = s.val });

            return View("~/Areas/Store/Views/Categories/FilterByType.cshtml", productsToShow);
        }

        public ActionResult FilterByCategory(string categoryName, int pageSize = 10, int page = 1, string sortPredicate = "")
        {
            ViewBag.pageSize = pageSize;
            ViewBag.page = page;
            ViewBag.Term = categoryName;
            int[] pageSizes = { 5, 10, 20, 40, 50 };
            ViewBag.PageSizes = pageSizes.Select(p => new SelectListItem { Text = p.ToString(), Value = p.ToString(), Selected = p.ToString().Equals(pageSize) });
            IEnumerable<ProductDto> products;

            products = _productService.Get(p => p.CategoryName.Equals(categoryName));

            if (sortPredicate != string.Empty)
            {
                products = SortByPredicate(sortPredicate, products);
            }

            IEnumerable<ProductDto> productsToShow = products.OrderByDescending(p => p.IsAvailable).Skip((page - 1) * pageSize).Take(pageSize);
            ViewBag.PageNumbers = Math.Ceiling(products.ToList().Count / (decimal)pageSize);

            ViewBag.MostDiscounted = _productService.Get().OrderByDescending(p => p.Discount).Take(6).ToList();
            ViewBag.Categories = _categoryService.Get();

            //string[,] sortPredicates = new string[,] { { "mostDiscount", "پرتخفیف ترین" }, { "mostExpensive", "گرانترین" }, { "cheapest", "ارزانترین" } };

            var sortPredicates = new[] { new { title = "پرتخفیف ترین", val = "mostDiscount" }, new { title = "گرانترین", val = "mostExpensive" }, new { title = "ارزانترین", val = "cheapest" } };
            ViewBag.SortPredicates = sortPredicates.Select(s => new SelectListItem { Text = s.title, Value = s.val });



            return View("~/Areas/Store/Views/Categories/FilterByCategory.cshtml", productsToShow);
        }

        private IEnumerable<ProductDto> SortByPredicate(string sortPredicate, IEnumerable<ProductDto> products)
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
    }
}