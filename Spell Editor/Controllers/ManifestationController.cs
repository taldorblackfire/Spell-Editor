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
    public class ManifestationController : Controller
    {
        private MonsterEntities db = new MonsterEntities();

        // GET: Manifestation
        public ActionResult Index()
        {
            return View(db.Manifestations.ToList());
        }

        // GET: Manifestation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manifestation manifestation = db.Manifestations.Find(id);
            if (manifestation == null)
            {
                return HttpNotFound();
            }
            return View(manifestation);
        }

        // GET: Manifestation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manifestation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Effect")] Manifestation manifestation)
        {
            if (ModelState.IsValid)
            {
                db.Manifestations.Add(manifestation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manifestation);
        }

        // GET: Manifestation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manifestation manifestation = db.Manifestations.Find(id);
            if (manifestation == null)
            {
                return HttpNotFound();
            }
            return View(manifestation);
        }

        // POST: Manifestation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Effect")] Manifestation manifestation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manifestation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manifestation);
        }

        // GET: Manifestation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manifestation manifestation = db.Manifestations.Find(id);
            if (manifestation == null)
            {
                return HttpNotFound();
            }
            return View(manifestation);
        }

        // POST: Manifestation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manifestation manifestation = db.Manifestations.Find(id);
            db.Manifestations.Remove(manifestation);
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
