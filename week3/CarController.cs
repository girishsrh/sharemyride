using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShareMyDrive.Models;

namespace ShareMyDrive.Controllers
{
    public class CarController : Controller
    {
        private CarContext db = new CarContext();

        // GET: /Car/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        // GET: /Car/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carmodel = db.Cars.Find(id);
            if (carmodel == null)
            {
                return HttpNotFound();
            }
            return View(carmodel);
        }

        // GET: /Car/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,From,To,Email,Date,Price,AvailableSeats")] CarModel carmodel)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(carmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carmodel);
        }

        // GET: /Car/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carmodel = db.Cars.Find(id);
            if (carmodel == null)
            {
                return HttpNotFound();
            }
            return View(carmodel);
        }

        // POST: /Car/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,From,To,Email,Date,Price,AvailableSeats")] CarModel carmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carmodel);
        }

        // GET: /Car/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carmodel = db.Cars.Find(id);
            if (carmodel == null)
            {
                return HttpNotFound();
            }
            return View(carmodel);
        }

        // POST: /Car/Delete/5
       [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarModel carmodel = db.Cars.Find(id);
            db.Cars.Remove(carmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     
        public ActionResult Search(string from = "", string to = "")
        {
            var cars = from m in db.Cars
                       select m;

            if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
            {
                cars = cars.Where(s => (s.From.Equals(from) && s.To.Equals(to)));
            }
            else
            {
                cars = null;
            }

            return View(cars);
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
