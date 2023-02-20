using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Spell_Editor.Models;

namespace Spell_Editor.Controllers
{
    public class TiltController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Tilt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tilt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Effect,Description,Cause,Resolution,Type")] Tilt tilt)
        {
            if (ModelState.IsValid)
            {
                db.Tilts.Add(tilt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tilt);
        }

        // GET: Tilt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilt tilt = db.Tilts.Find(id);
            if (tilt == null)
            {
                return HttpNotFound();
            }
            return View(tilt);
        }

        // POST: Tilt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilt tilt = db.Tilts.Find(id);
            db.Tilts.Remove(tilt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tilt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilt tilt = db.Tilts.Find(id);
            if (tilt == null)
            {
                return HttpNotFound();
            }
            return View(tilt);
        }

        // GET: Tilt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilt tilt = db.Tilts.Find(id);
            if (tilt == null)
            {
                return HttpNotFound();
            }
            return View(tilt);
        }

        // POST: Tilt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Effect,Description,Cause,Resolution,Type")] Tilt tilt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tilt);
        }

        // GET: Tilt
        public ActionResult Index()
        {
            return View(db.Tilts.ToList());
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
