using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Common.DTOs.ProductRelated;
using Kuff.Service.Interfaces.ProductRelated;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class DataTypesController : Controller
    {
        private readonly IDataTypeService _dataTypeService;

        public DataTypesController(IDataTypeService dataTypeService)
        {
            this._dataTypeService = dataTypeService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new DataTypeDto());
        }

        [HttpPost]
        public ActionResult Create(DataTypeDto categoryViewModel)
        {
            _dataTypeService.Insert(categoryViewModel);
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            List<DataTypeDto> categories = _dataTypeService.Get();
            return View(categories as object);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var cat = _dataTypeService.Get(c => c.Id.Equals(id)).FirstOrDefault();
            return View(cat as object);
        }

        [HttpPost]
        public ActionResult Edit(DataTypeDto categoryViewModel)
        {
            _dataTypeService.Update(categoryViewModel);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var cat = _dataTypeService.Get(c => c.Id.Equals(id)).FirstOrDefault();
            return View(cat as object);
        }

        [HttpPost]
        public ActionResult Delete(DataTypeDto categoryViewModel)
        {
            //var categoryService = new CategoryService();
            _dataTypeService.Delete(c => c.Id.Equals(categoryViewModel.Id));
            return RedirectToAction("List");
        }
    }
}