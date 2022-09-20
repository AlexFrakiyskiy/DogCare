using DogCare.Filters;
using DogCare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DogCare.Controllers
{
    [CustomAuthFilter]
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Details()
        {
            return View(FromDataset2ListDetails(DataLoader.LoadDetails()));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            DataLoader.DeletAppoiintment(id);
            return RedirectToAction("Details");
        }

        public ActionResult NewAppoint(int custid, string FirstName)
        {
            
            var m = new DetailsModel() { Id = 0, CustomerId = custid, UserName = Session["UserName"].ToString(), FirstName = FirstName };
            ViewBag.ErrorMessage = "";

            return View("NewAppoint", m);
        }

        [HttpPost]
        public ActionResult NewAppoint(DetailsModel model)
        {
            if(model != null)
            {
                DateTime apDate;
                try
                {
                    apDate = DateTime.ParseExact(model.AppointDate, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    ViewBag.ErrorMessage = "Incorrect DateTime Format";
                    return View("NewAppoint",model);
                }
               
                DataLoader.NewAppointment(model.CustomerId, apDate);
            }

            return RedirectToAction("Details", "Home");
        }

        [HttpPost]
        public ActionResult CreateNewAppoint(DetailsModel model)
        {
            if (model != null)
            {
                DateTime apDate;
                try
                {
                    apDate = DateTime.ParseExact(model.AppointDate, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    ViewBag.ErrorMessage = "Incorrect DateTime Format";
                     return View("NewAppoint", model);
                }

                DataLoader.NewAppointment(model.CustomerId, apDate);
            }

            return RedirectToAction("Details", "Home");
        }
        public ActionResult CreateNewAppoint()
        {
            var m = new DetailsModel() { Id = 0, CustomerId = (int)Session["CustomerId"], UserName = Session["UserName"].ToString(), FirstName = Session["FirstName"].ToString() };
            ViewBag.ErrorMessage = "";

            return View("NewAppoint", m);
        }

       
        [HttpGet]
        public ActionResult Edit(int id,int custid, string FirstName, string apDate)
        {           
                        
            if(id > 0 && !String.IsNullOrEmpty(apDate))
            {
                var m = new DetailsModel() { Id = 0, CustomerId = custid, UserName = Session["UserName"].ToString(), FirstName = FirstName, AppointDate = apDate };
                return View("Edit", m);
            }
            else
            {
                ViewBag.ErrorMessage = "";
                return RedirectToAction("NewAppoint", "Home", new {custid = custid, FirstName = FirstName });
            }         
        }

        [HttpPost]
        public ActionResult Edit(DetailsModel model)
        {
            if (model != null)
            {
                DateTime apDate;
                try
                {
                    apDate = DateTime.ParseExact(model.AppointDate, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch(Exception e)
                {
                    ViewBag.ErrorMessage = "Incorrect DateTime Format";
                   return View("Edit", model);
                }

                DataLoader.EditCustomerAppointment(model.Id, model.CustomerId, model.FirstName, apDate);
            }

            return RedirectToAction("Details", "Home");
        }    
       
        public ActionResult Index()
        {
            return View("LoginForm");
        }       

       

        private List<DetailsModel> FromDataset2ListDetails(DataTable dt)
        {
            List<DetailsModel> list = new List<DetailsModel>();

            if (dt != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    DetailsModel item = new DetailsModel();
                    if (r["Id"] == DBNull.Value)
                        item.Id = 0;
                    else
                        item.Id = (int)r["Id"];
                    item.CustomerId = (int)r["CustId"];
                    item.UserName= r["UserName"].ToString();
                    item.FirstName = r["FirstName"].ToString();
                    DateTime? tmp = r["VisitDate"] == DBNull.Value ? null : r["VisitDate"] as DateTime?;
                    if(tmp != null)
                        item.AppointDate = tmp.Value.ToString("dd/MM/yyyy HH:mm");
                    item.CreationDate = r["CreationDate"].ToString();

                    list.Add(item);
                }
            }

            return list;
        }

        private DetailsModel FromDataset2Detail(DataTable dt)
        {
            DetailsModel detail = new DetailsModel();

            if (dt != null)
            {
                if(dt.Rows != null && dt.Rows.Count >0)
                { 
                    detail.Id = (int)dt.Rows[0]["Id"];
                    detail.CustomerId = (int)dt.Rows[0]["CustId"];
                    detail.UserName = dt.Rows[0]["UserName"].ToString();
                    detail.FirstName = dt.Rows[0]["FirstName"].ToString();
                    detail.AppointDate = dt.Rows[0]["VisitDate"].ToString();
                    detail.CreationDate = dt.Rows[0]["CreationDate"].ToString();

                }
            }

            return detail;
        }
    }
}