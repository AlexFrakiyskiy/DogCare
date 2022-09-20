using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DogCare.Models;
using System.Threading.Tasks;
using DogCare.Filters;

namespace DogCare.Controllers
{
    
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult LoginForm()
        {
            return View("LoginForm");
        }

        [HttpGet]
        public ActionResult RegistrForm()
        {
            return View("RegistrForm");
        }        

        [HttpPost]
        public ActionResult RegistrForm(RegistrModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!DataLoader.RegisterNewCustomer(model.FirstName, model.UserName, model.Password))
            {
                ViewBag.ErrorMsg = "Invalid register attempt.";
                return View(model);
            }

            return RedirectToAction("Details","Home");
        }

        [HttpPost]
        public ActionResult LoginForm(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(!DataLoader.LoginCheck(model.UserName,model.Password))
            {
                ViewBag.ErrorMsg = "Invalid login attempt.";
                return View(model);                
            }

            Session["UserName"] = model.UserName;

            return RedirectToAction("Details", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["UserName"] = string.Empty;
            return RedirectToAction("LoginForm", "Login");
        }

        
    }
}
