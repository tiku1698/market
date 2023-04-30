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
    public class BookingsController : Controller
    {
        private DeliveryMarketplaceEntities db = new DeliveryMarketplaceEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            if (Session["LoginUserEmail"] == null)
            {
                return RedirectToAction("../Home");
            }
            if (Session["LoginRole"].ToString() == "Admin" || Session["LoginRole"].ToString() == "Driver")
            {
                var Abookings = db.Bookings.Include(b => b.Product).Include(b => b.User);
                return View(Abookings.ToList());
            }
            var LoginUserEmail = Session["LoginUserEmail"].ToString();
            var bookings = db.Bookings.Include(b => b.Product).Include(b => b.User).Where(Ui => Ui.User.Email == LoginUserEmail);
            return View(bookings.ToList());
        }

        public ActionResult DriverBookingList()
        {
            if (Session["LoginUserEmail"] == null)
            {
                return RedirectToAction("../Home");
            }
            if (Session["LoginRole"].ToString() == "Admin" || Session["LoginRole"].ToString() == "Driver")
            {
                var Abookings = db.Bookings.Include(b => b.Product).Include(b => b.User);
                return View(Abookings.ToList());
            }
            var LoginUserEmail = Session["LoginUserEmail"].ToString();
            var bookings = db.Bookings.Include(b => b.Product).Include(b => b.User).Where(Ui => Ui.User.Email == LoginUserEmail);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }
        
    // GET: Bookings/Create
    public ActionResult Create()
        {
            if (Session["LoginUserEmail"] == null)
            {
                return RedirectToAction("../Home");
            }

            var LoginUserEmail =  Session["LoginUserEmail"].ToString();
            var LoginRole = Session["LoginRole"].ToString();
            var LoginUserID = Convert.ToInt32(Session["LoginUserID"]);
            ViewBag.UserID = new SelectList(db.Users.ToList(), "UserID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,ProductID,UserID,PickupAddress,DeliveryAddress,PickupLoc,DeliverLoc,Status,Price")] Booking booking)
        {
            var LoginUserEmail = Session["LoginUserEmail"].ToString();
            var LoginRole = Session["LoginRole"].ToString();
            var LoginUserID = Convert.ToInt32(Session["LoginUserID"]);
            booking.UserID = LoginUserID;
            var RandomNum = new Random();
            int NumPrice = RandomNum.Next(10, 99);
            booking.Price = NumPrice;
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", booking.ProductID);
            db.Bookings.Add(booking);
            db.SaveChanges();
            return RedirectToAction("Confirm/" + booking.BookingID);
        }
        public ActionResult Confirm(int? id)
        {
            if (Session["LoginRole"] == null)
            {
                return RedirectToAction("Index");
            }
            Booking booking = db.Bookings.Find(id);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm()
        {
            if (Session["LoginRole"] == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Success");
        }
        public ActionResult Success()
        {
            return View();
        }
        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["LoginRole"] == null)
            {
                return RedirectToAction("../Home");
            }
            var LoginUserName = Session["LoginUserName"].ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            if(Session["LoginRole"].ToString() == "Admin")
            { 
                ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", booking.UserID);
            }
            else
            {
                ViewBag.UserID = new SelectList(db.Users.Where(Ui => Ui.Name == LoginUserName), "UserID", "Name", booking.UserID);
            }
            
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", booking.ProductID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,ProductID,UserID,PickupAddress,DeliveryAddress,PickupLoc,DeliverLoc,Status,Price,DriverID")] Booking booking)
        
        {
            if (Session["LoginRole"].ToString() == "Driver")
            {
                var LoginUserID = Convert.ToInt32(Session["LoginUserID"]);
                booking.DriverID = LoginUserID;
            }
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                if (Session["LoginRole"].ToString() == "Driver")
                {
                    return RedirectToAction("DriverBookingList");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                    
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", booking.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", booking.UserID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["LoginRole"] ==null || Session["LoginRole"].ToString() == "Driver")
            {
                return RedirectToAction("/Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
