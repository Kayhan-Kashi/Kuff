using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Service.Interfaces.OrderRelated;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class ShipmentCostsController : Controller
    {
        IShipmentCostService _shipmentCostService;
        IShipmentMethodService _shipmentMethodService;

        public ShipmentCostsController(IShipmentCostService shipmentCostService, IShipmentMethodService shipmentMethodService)
        {
            this._shipmentCostService = shipmentCostService;
            this._shipmentMethodService = shipmentMethodService;
        }

        public ActionResult List()
        {
            return View(_shipmentCostService.Get().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ShipmentMethods = _shipmentMethodService.Get().Select(sh => new SelectListItem
            {
                Value = sh.Id.ToString(),
                Text = sh.Description
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipmentCostDto viewModel)
        {
            if (ModelState.IsValid)
            {
                _shipmentCostService.Insert(viewModel);

                {
                    return RedirectToAction("List");
                }

            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var shipmentCost = _shipmentCostService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            return View(shipmentCost);
        }

        [HttpPost]
        public ActionResult Edit(ShipmentCostDto viewModel)
        {
            _shipmentCostService.Update(viewModel);
            return RedirectToAction("List");
        }
    }
}