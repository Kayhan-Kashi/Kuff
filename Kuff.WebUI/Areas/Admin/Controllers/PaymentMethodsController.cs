using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Service.Interfaces.OrderRelated;

namespace Kuff.WebUI.Areas.Admin.Controllers
{
    public class PaymentMethodsController : Controller
    {
        IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            this._paymentMethodService = paymentMethodService;
        }

        public ActionResult List()
        {
            return View(_paymentMethodService.Get().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentMethodDto viewModel)
        {
            if (ModelState.IsValid)
            {
                _paymentMethodService.Insert(viewModel);

                {
                    return RedirectToAction("List");
                }

            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var shipmentMethod = _paymentMethodService.Get(p => p.Id.Equals(id)).FirstOrDefault();
            return View(shipmentMethod);
        }

        [HttpPost]
        public ActionResult Edit(PaymentMethodDto viewModel)
        {
            _paymentMethodService.Update(viewModel);
            return RedirectToAction("List");
        }
    }
}