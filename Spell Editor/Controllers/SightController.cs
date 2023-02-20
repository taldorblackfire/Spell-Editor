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
    public class SightController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Sight
        public ActionResult Index()
        {
            return View(db.Sight_Table.ToList());
        }

        // GET: Sight/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sight_Table sight_Table = db.Sight_Table.Find(id);
            if (sight_Table == null)
            {
                return HttpNotFound();
            }
            return View(sight_Table);
        }

        // GET: Sight/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sight/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Id")] Sight_Table sight_Table)
        {
            if (ModelState.IsValid)
            {
                db.Sight_Table.Add(sight_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sight_Table);
        }

        // GET: Sight/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sight_Table sight_Table = db.Sight_Table.Find(id);
            if (sight_Table == null)
            {
                return HttpNotFound();
            }
            return View(sight_Table);
        }

        // POST: Sight/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Description,Id")] Sight_Table sight_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sight_Table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sight_Table);
        }

        // GET: Sight/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sight_Table sight_Table = db.Sight_Table.Find(id);
            if (sight_Table == null)
            {
                return HttpNotFound();
            }
            return View(sight_Table);
        }

        // POST: Sight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sight_Table sight_Table = db.Sight_Table.Find(id);
            db.Sight_Table.Remove(sight_Table);
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
