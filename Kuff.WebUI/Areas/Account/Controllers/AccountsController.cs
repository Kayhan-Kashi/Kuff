using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kuff.WebUI.Areas.Account.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Kuff.Common.DTOs.AccountRelated;
using Kuff.Service.Services.AccountRelated;
using Microsoft.Owin.Security;

namespace Kuff.WebUI.Areas.Account.Controllers
{
    public class AccountsController : Controller
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

        private SignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().Get<SignInManager>(); }
        }


        [HttpGet]
        public ActionResult Register()
        {
            var returnUrl = Request.Form["returnUrl"] as string;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = viewModel.User;

                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, "Customer");

                    SendConfirmationEmail(user);
                    return RedirectToAction("Confirm", "Accounts", new { Email = user.Email, area="Account" });
                }
                
                AddErrors(result);     
                             
            }

            return View();
        }



        public ActionResult Login()
        {
            if (Request.Form["returnUrl"] == null)
            {
                ViewBag.ReturnUrl = Request.UrlReferrer.ToString();
            }
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout   
            // To enable password failures to trigger account lockout, change to shouldLockout: true   
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home", new { area = "Store" });
                case SignInStatus.LockedOut:
                    return View("Lockout");
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);

            }
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Confirm(string email)
        {
            ViewBag.Email = email;
            return View();
        }


        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string email, string token )
        {
            ApplicationUser user = this.UserManager.FindById(token);
            if (user != null)
            {
                if (user.Email == email)
                {
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    await SignInManager.SignInAsync(user, false, rememberBrowser: true);
                    return View();
                }
                else
                {
                    return RedirectToAction("Confirm", "Accounts", new { Email = user.Email, area = "Account" });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Accounts", new { Email = "",  area = "Account" });
            }
        }

        [HttpPost]
        public JsonResult FindAndAuthUserWithAjax(string userName, string pass)
        {
            ApplicationUser foundUser = UserManager.Find(userName, pass);
            if (foundUser != null)
            {
                //ClaimsIdentity ident = UserManager.CreateIdentity(foundUser, DefaultAuthenticationTypes.ApplicationCookie);
                //AuthManager.SignOut();
                //AuthManager.SignIn(new AuthenticationProperties
                //{
                //    IsPersistent = false
                //}, ident);

                return Json(true.ToString());
            }
            else
            {
                ModelState.AddModelError("Password", "نام کاربری یا رمز عبور صحیح نیست");
                return Json(false.ToString());
            }
        }

        private void SendConfirmationEmail(ApplicationUser user)
        {
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                new System.Net.Mail.MailAddress("kayhan@kayhan-kashi.com", "Web Registration"),
                new System.Net.Mail.MailAddress(user.Email)
            );
            m.Subject = "Email confirmation";
            //var link = Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = user.Email });

            m.Body =
                string.Format(
                    "Dear {0} <BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\"title=\"User Email Confirm\">{1}</a>",
                    user.UserName, Url.Action("ConfirmEmail", "Accounts",
                        new { Token = user.Id, Email = user.Email, area = "Account" }, Request.Url.Scheme)
                );
            m.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("kayhan-kashi.com");
            smtp.Credentials = new System.Net.NetworkCredential("kayhan@kayhan-kashi.com", "roland14");

            //smtp.EnableSsl = true;
            smtp.Send(m);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}