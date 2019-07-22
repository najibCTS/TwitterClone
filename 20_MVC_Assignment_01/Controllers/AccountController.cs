using _20_MVC_Assignment_01.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace _20_MVC_Assignment_01.Controllers
{
    [Authorize]
    public class AccountController : Controller, IDisposable
    {
        AccountRepository accountRepository = new AccountRepository();
        public AccountController()
        {
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person
                {
                    user_id = model.Username,
                    password = Helper.EncodePasswordMd5(model.Password),
                    fullName = model.FullName,
                    email = model.Email,
                    joined = DateTime.Now,
                    active = true
                };

                int result =  accountRepository.Register(person);
                if (result > 0)
                    return RedirectToAction("Login");
            }
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (accountRepository.AuthenticateUser(model))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Index", "Twitterhome");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }
        
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        } 
    }
}