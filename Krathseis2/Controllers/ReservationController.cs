using Krathseis2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Krathseis2.Controllers
{
    public class ReservationController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Reservation
        public ActionResult Index()
        {
            var reservations = _context.Reservations.ToList();
            return View(reservations);
        }
        //Get
        public ActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StartDare,StartLocation,EndLocation")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(reservation);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Reservation reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            return View(reservation);

        }
        [HttpPost]
        public ActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Reservation reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            return View(reservation);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Reservation reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            return View(reservation);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCon(int id)
        {
            Reservation reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}