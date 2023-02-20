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
    public class NuminaController : Controller
    {
        private MonsterEntities db = new MonsterEntities();

        // GET: Numina
        public ActionResult Index()
        {
            return View(db.Numinas.ToList());
        }

        // GET: Numina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Numina numina = db.Numinas.Find(id);
            if (numina == null)
            {
                return HttpNotFound();
            }
            return View(numina);
        }

        // GET: Numina/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Numina/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Effect")] Numina numina)
        {
            if (ModelState.IsValid)
            {
                db.Numinas.Add(numina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(numina);
        }

        // GET: Numina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Numina numina = db.Numinas.Find(id);
            if (numina == null)
            {
                return HttpNotFound();
            }
            return View(numina);
        }

        // POST: Numina/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Effect")] Numina numina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(numina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(numina);
        }

        // GET: Numina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Numina numina = db.Numinas.Find(id);
            if (numina == null)
            {
                return HttpNotFound();
            }
            return View(numina);
        }

        // POST: Numina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Numina numina = db.Numinas.Find(id);
            db.Numinas.Remove(numina);
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
