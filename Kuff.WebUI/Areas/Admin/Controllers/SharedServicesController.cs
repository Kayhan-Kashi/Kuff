using Kuff.Service.Interfaces.ProductRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class SharedServicesController : Controller
    {
        readonly IDataTypeService _dataTypeService;

        public SharedServicesController(IDataTypeService dataTypeService)
        {
            this._dataTypeService = dataTypeService;
        }

        public JsonResult GetDataTypes()
        {
            var dataTypes = _dataTypeService.Get();

            return Json(dataTypes, JsonRequestBehavior.AllowGet);
        }
    }
}