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
    public class DisciplinesController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Disciplines
        public ActionResult Index()
        {
            return View(db.Vampire_Disciplines.ToList());
        }

        // GET: Disciplines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vampire_Disciplines vampire_Disciplines = db.Vampire_Disciplines.Find(id);
            if (vampire_Disciplines == null)
            {
                return HttpNotFound();
            }
            return View(vampire_Disciplines);
        }

        // GET: Disciplines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disciplines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Discipline,Description")] Vampire_Disciplines vampire_Disciplines)
        {
            if (ModelState.IsValid)
            {
                db.Vampire_Disciplines.Add(vampire_Disciplines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vampire_Disciplines);
        }

        // GET: Disciplines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vampire_Disciplines vampire_Disciplines = db.Vampire_Disciplines.Find(id);
            if (vampire_Disciplines == null)
            {
                return HttpNotFound();
            }
            return View(vampire_Disciplines);
        }

        // POST: Disciplines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Discipline,Description")] Vampire_Disciplines vampire_Disciplines)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vampire_Disciplines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vampire_Disciplines);
        }

        // GET: Disciplines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vampire_Disciplines vampire_Disciplines = db.Vampire_Disciplines.Find(id);
            if (vampire_Disciplines == null)
            {
                return HttpNotFound();
            }
            return View(vampire_Disciplines);
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vampire_Disciplines vampire_Disciplines = db.Vampire_Disciplines.Find(id);
            db.Vampire_Disciplines.Remove(vampire_Disciplines);
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
