using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;
using Kuff.Service.Services.ProductRelated;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Create()
        {     
            return View(new CategoryDto());
        }

        [HttpPost]
        public ActionResult Create(CategoryDto categoryViewModel)
        {
            _categoryService.Insert(categoryViewModel);
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            List<CategoryDto> categories = _categoryService.Get();
            return View(categories as object);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var cat = _categoryService.Get(c => c.Id.Equals(id)).FirstOrDefault();
            return View(cat as object);
        }

        [HttpPost]
        public ActionResult Edit(CategoryDto categoryViewModel)
        {           
            _categoryService.Update(categoryViewModel);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var cat = _categoryService.Get(c => c.Id.Equals(id)).FirstOrDefault();
            return View(cat as object);
        }

        [HttpPost]
        public ActionResult Delete(CategoryDto categoryViewModel)
        {
            //var categoryService = new CategoryService();
            _categoryService.Delete(c => c.Id.Equals(categoryViewModel.Id));
            return RedirectToAction("List");
        }

        public ActionResult Combine()
        {
            //IDataTypeService dataTypeSrv = ((IDataTypeService)DependencyResolver.Current.GetService(typeof(IDataTypeService)));
            //var srv = EnterpriseLibraryContainer.Current.GetInstance<IDataTypeService>();
            _categoryService.CombineService();
            
            return RedirectToAction("List");
        }

    }
}