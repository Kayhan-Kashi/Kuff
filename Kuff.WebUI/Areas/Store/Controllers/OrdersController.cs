using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kuff.Common.DTOs.AccountRelated;
using Kuff.Common.DTOs.OrderRelated;
using Kuff.Service.Interfaces.OrderRelated;
using Kuff.Service.Interfaces.AccountRelated;
using Kuff.Service.Services.AccountRelated;
using Kuff.WebUI.Areas.Store.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Kuff.WebUI.Areas.Store.Controllers
{
    public class OrdersController : Controller
    {
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private UserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
        }


        IShipmentCostService _shipmentCostService;
        IPaymentMethodService _paymentMethodService;
        IOrderService _orderService;

        public OrdersController(IShipmentCostService shipmentCostService, IPaymentMethodService paymentMethodService, IOrderService orderService)
        {
            this._shipmentCostService = shipmentCostService;
            this._paymentMethodService = paymentMethodService;
            this._orderService = orderService;
        }

        public void DeleteSession()
        {
            Session.Clear();
        }

        
        public ActionResult AddToCart(OrderItemDto orderItem)
        {
            // It's called by return url from successful login
            if (orderItem.Quantity == 0)
            {
                return RedirectToAction("ShowCart", "Orders", new {area = "Store"});
            }

            if (ModelState.IsValid)
            {
                PutOrderItemInSession(orderItem);
                ViewBag.uri = Url.Action("ShowCart", "Orders", new {area = "Store"});               
            }
            return View("~/Areas/Store/Views/Orders/Cart.cshtml", new CartViewModel(GetOrderItemsFromSession()));
        }

        public ActionResult ShowCart()
        {
            var items = GetOrderItemsFromSession();
            return View("~/Areas/Store/Views/Orders/Cart.cshtml", new CartViewModel(GetOrderItemsFromSession()));
        }

        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(CartViewModel cartViewModel)
        {

            string username = AuthManager.User.Identity.Name;
            ApplicationUser user = UserManager.FindByName(username);


            OrderDto order = new OrderDto();
            order.Id = Guid.NewGuid();
            order.OrderItems = cartViewModel.OrderItems;
            order.UserEmail = user.Email;
            order.UserId = user.Id;
            order.UserAddress = user.Address;
            order.UserCity = user.City;
            order.UserCityState = user.CityState;
            order.UserCompany = user.Company;
            order.UserFirstName = user.FirstName;
            order.UserLastName = user.LastName;
            order.UserPhoneNumber = user.PhoneNumber;
            order.UserPostalCode = user.PostalCode;
                     
            PutOrderInSession(order);

            ViewBag.ShipmentCosts = _shipmentCostService.Get(s => s.DestinationCity == user.City).ToList();
            ViewBag.IsPeykable = (user.City.Contains("کرج") || user.City.Contains("تهران"));
            ViewBag.PaymentMethods = _paymentMethodService.Get().ToList();
            return View("~/Areas/Store/Views/Orders/CheckOut.cshtml", order);
        }

        [Authorize]
        public ActionResult FinalizeOrder(OrderDto order)
        {
            string username = AuthManager.User.Identity.Name;
            order.UserId = UserManager.FindByName(username).Id;

            PaymentDto payment = new PaymentDto();
            payment.OrderId = order.Id;
            payment.PaymentMethodId = order.PaymentMethodId;
            payment.Id = Guid.NewGuid();

            order.PaymentPayingAmount = order.FinalPriceWithShipmentCost;
            order.PaymentId = payment.Id;
            order.PaymentIsPayingDone = false;
            //order.UserId = Guid.Parse(user.Id);
            //order.UserEmail = user.Email;
            //foreach (var ordItem in order.OrderItems)
            //{
            //    ordItem.OrderId = order.Id;
            //}
            _orderService.Insert(order);
            DeleteSession();
            return View("~/Areas/Store/Views/Orders/FinalizeOrder.cshtml", "خرید با موفقیت انجام شد" as object);
        }

        public void PutOrderInSession(OrderDto order)
        {
            if (order != null)
            {
                Session["Order"] = order;
            }
        }


        public OrderDto GetOrderFromSession()
        {
            var order = Session["Order"] as OrderDto;
            return order;
        }

        public void PutOrderItemInSession(OrderItemDto orderItem)
        {
            if (Session["OrderItems"] as List<OrderItemDto> != null)
            {
                List<OrderItemDto> orderItems = Session["OrderItems"] as List<OrderItemDto>;
                orderItems.Add(orderItem);
            }
            else
            {
                List<OrderItemDto> orderItems = new List<OrderItemDto>();
                orderItems.Add(orderItem);
                Session["OrderItems"] = orderItems;
            }
        }

        [HttpGet]
        public JsonResult GetJSonPaymentSummary()
        {
            List<OrderItemDto> orderItems = GetOrderItemsFromSession();
            if (orderItems != null && orderItems.Count != 0)
            {
                decimal totalPrice = orderItems.Sum(o => o.QuantityWholePriceWithDiscount);
                int numOfItems = orderItems.Count;
                return Json(new { total_price = totalPrice, num_items = numOfItems }, JsonRequestBehavior.AllowGet);
            }
            return null;
            //return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJSonCartSummary()
        {
            List<OrderItemDto> orderItems = GetOrderItemsFromSession();
            if (orderItems != null && orderItems.Count != 0)
            {
                var items = orderItems.Select(o => new { price = o.QuantityWholePriceWithDiscount, PictureId = o.OrderItemPictureId, quantity = o.Quantity, Order_title = o.Description });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            return null;
            //return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJSonOrderItemsFromSession()
        {
            if (Session["OrderItems"] != null)
            {
                return Json(Session["OrderItems"] as List<OrderItemDto>, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public List<OrderItemDto> GetOrderItemsFromSession()
        {
            if (Session["OrderItems"] != null)
            {
                return Session["OrderItems"] as List<OrderItemDto>;
            }
            return null;
        }

        public ActionResult CheckOutFromCartSummary()
        {
            return CheckOut(new CartViewModel(GetOrderItemsFromSession()));
        }

        public void AddOrderItemToSession(OrderItemDto ordItem)
        {
            List<OrderItemDto> orderItems = GetOrderItemsFromSession() as List<OrderItemDto>;
            orderItems.Add(ordItem);
        }

        public bool RemoveOrderItemFromSession(Guid orderItemId)
        {
            var orderItem = GetOrderItemsFromSession().FirstOrDefault(o => o.Id.Equals(orderItemId));
            GetOrderItemsFromSession().Remove(orderItem);
            return true;
        }
    }
}