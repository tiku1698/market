using DeliveryMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryMarketplace.Controllers
{
    public class HomeController : Controller
    {
        private DeliveryMarketplaceEntities db = new DeliveryMarketplaceEntities();
        public ActionResult Index()
        {
            return View();
        }   
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login LoginUser)
        {
            if (ModelState.IsValid)
            {
                var UserDetails = db.Users.Where(Ud => Ud.Email == LoginUser.Email && Ud.Password == LoginUser.Password).ToList();
                if (UserDetails.Any())
                {
                    User UserD = new User();
                    Session["LoginUserID"] = UserDetails.FirstOrDefault().UserID;
                    Session["LoginRole"] = UserDetails.FirstOrDefault().Role;
                    Session["LoginUserName"] = UserDetails.FirstOrDefault().Name;
                    Session["LoginUserEmail"] = UserDetails.FirstOrDefault().Email;
                    Session["UserPhoneNo"] = UserDetails.FirstOrDefault().PhoneNumber;
                    Session["UserAddress"] = UserDetails.FirstOrDefault().Address;
                    if (Session["LoginRole"].ToString() =="Driver")
                    {
                        return RedirectToAction("../Bookings/DriverBookingList");
                    }
                    else
                    {
                        return RedirectToAction("../Bookings/Index");
                    }
                }
            }
            TempData["LoginCheck"] = "Please Enter Valid UserName & Password";
            return RedirectToAction("Index");
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}