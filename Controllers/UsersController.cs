using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeliveryMarketplace.Models;

namespace DeliveryMarketplace.Controllers
{
    public class UsersController : Controller
    {
        private DeliveryMarketplaceEntities db = new DeliveryMarketplaceEntities();

        // GET: Users
        public ActionResult Index()
        {
            var LoginRole = Session["LoginRole"].ToString();
            var LoginUserEmail = Session["LoginUserEmail"].ToString();
            var UserInfo = db.Users.ToList();
            if(LoginRole == null)
            {
                return RedirectToAction("../Home/Index");
            }
            if (LoginRole == "Admin")
            {
                return View(UserInfo);
            }
            if (Session["LoginRole"].ToString() != "Admin")
            {
                var UserInf = db.Users.Where(Ui => Ui.Email == LoginUserEmail).ToList();
                return View(UserInf);
            }
            return RedirectToAction("../Bookings/Create");
        }
        public ActionResult Register()
        {
            //ViewBag.Role = new SelectList(db.Users, "Role", "Role");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User RgUser)
        {
                var UserInfo = db.Users.Where(ui => ui.PhoneNumber == RgUser.PhoneNumber).ToList();
                User UDetails = new User();
                if (!UserInfo.Any())
                {
                    //ViewBag.Role = new SelectList(db.Users, "Role", "Role");
                    UDetails.Role = RgUser.Role;
                    UDetails.Name = RgUser.Name;
                    UDetails.Email = RgUser.Email;
                    UDetails.PhoneNumber = RgUser.PhoneNumber;
                    UDetails.Password = RgUser.Password;
                    UDetails.Gender = RgUser.Gender;
                    UDetails.Address = RgUser.Address;
                    UDetails.City = RgUser.City;
                    UDetails.State = RgUser.State;
                    db.Users.Add(UDetails);
                    db.SaveChanges();    
                }
                if(UserInfo.Any())
                {
                TempData["ErrorMessage"] = "User Already Exists.";
                return RedirectToAction("../Users/Register");
            } 
            TempData["SuccessMessage"] = "User Has Been Registered Successfully.";
            return RedirectToAction("../Home/Index");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            //ViewBag.Role = new SelectList(db.Users, "Role", "Role");
            return RedirectToAction("Register");
        }
        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserID,Role,Name,Email,PhoneNumber,Password,Gender,Address,City,State")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(user);
        //}

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["LoginRole"] ==null)
            {
                return RedirectToAction("../Home");
            }
            var LoginRole = Session["LoginRole"].ToString();
            ViewBag.UserRole = db.Users.Where(Ui => Ui.Role == LoginRole);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Role,Name,Email,PhoneNumber,Password,Gender,Address,City,State")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
