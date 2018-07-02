using Kuff.Service.Interfaces.OrderRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Common.DTOs.OrderRelated;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class ShipmentMethodsController : Controller
    {

        IShipmentMethodService _shipmentMethodService;

        public ShipmentMethodsController(IShipmentMethodService shipmentMethodService)
        {
            this._shipmentMethodService = shipmentMethodService;
        }

        public ActionResult List()
        {
            return View(_shipmentMethodService.Get().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipmentMethodId,Description")] ShipmentMethodDto shipmentMethod)
       {
            if (ModelState.IsValid)
            {
                _shipmentMethodService.Insert(shipmentMethod);
                
                {
                    return RedirectToAction("List");
                }

            }
            return View(shipmentMethod);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var shipmentMethod = _shipmentMethodService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            return View(shipmentMethod);
        }

        [HttpPost]
        public ActionResult Edit(ShipmentMethodDto viewModel)
        {
            _shipmentMethodService.Update(viewModel);
            return RedirectToAction("List");
        }

    }
}