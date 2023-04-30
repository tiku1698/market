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
    public class FeedbacksController : Controller
    {
        private DeliveryMarketplaceEntities db = new DeliveryMarketplaceEntities();

        // GET: Feedbacks
        public ActionResult Index()
        {
            if (Session["LoginRole"] == null)
            {
                return RedirectToAction("../Home");
            }
            if(Session["LoginRole"].ToString() == "Driver")
            {
                return RedirectToAction("../Bookings");
            }
            var LoginUserName = Session["LoginUserName"].ToString();
            var feedbacks = db.Feedbacks.Include(f => f.User).Where(Ui=>Ui.User.Name == LoginUserName);
            if (Session["LoginRole"].ToString() == "Admin")
            {
                feedbacks = db.Feedbacks.Include(f => f.User);
                return View(feedbacks.ToList());
            }
                return View(feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["LoginRole"] == null)
            {
                return RedirectToAction("../Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            if (Session["LoginRole"] ==null)
            {
                return RedirectToAction("../Home");
            }
            if(Session["LoginRole"].ToString() == "Admin" || Session["LoginRole"].ToString() == "Driver")
            {
                return RedirectToAction("Index");
            }
            
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeedbackID,UserID,Feedback1")] Feedback feedback)
        {
            var LoginUserName = Session["LoginUserName"].ToString();
            ViewBag.UserID = new SelectList(db.Users.Where(Ui => Ui.Name == LoginUserName), "UserID", "Name", feedback.UserID);
            var LoginUserID = Convert.ToInt32(Session["LoginUserID"]);
            feedback.UserID = LoginUserID;
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Feedbacks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (Session["LoginRole"] == null || Session["LoginRole"].ToString() == "Admin" || Session["LoginRole"].ToString() == "Driver")
        //    {
        //        return RedirectToAction("../Home/Index");
        //    }
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Feedback feedback = db.Feedbacks.Find(id);
        //    if (feedback == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "Role", feedback.UserID);
        //    return View(feedback);
        //}

        //// POST: Feedbacks/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "FeedbackID,UserID,Feedback1")] Feedback feedback)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(feedback).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "Role", feedback.UserID);
        //    return View(feedback);
        //}

        //// GET: Feedbacks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Feedback feedback = db.Feedbacks.Find(id);
        //    if (feedback == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(feedback);
        //}

        //// POST: Feedbacks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Feedback feedback = db.Feedbacks.Find(id);
        //    db.Feedbacks.Remove(feedback);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
