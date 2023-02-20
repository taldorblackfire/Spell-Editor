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
    public class FetishController : Controller
    {
        private WerewolfEntities db = new WerewolfEntities();

        // GET: Fetish
        public ActionResult Index()
        {
            return View(db.FetishTables.ToList());
        }

        // GET: Fetish/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FetishTable fetishTable = db.FetishTables.Find(id);
            if (fetishTable == null)
            {
                return HttpNotFound();
            }
            return View(fetishTable);
        }

        // GET: Fetish/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fetish/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Effect,Talen,Rating")] FetishTable fetishTable)
        {
            if (ModelState.IsValid)
            {
                db.FetishTables.Add(fetishTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fetishTable);
        }

        // GET: Fetish/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FetishTable fetishTable = db.FetishTables.Find(id);
            if (fetishTable == null)
            {
                return HttpNotFound();
            }
            return View(fetishTable);
        }

        // POST: Fetish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Effect,Talen,Rating")] FetishTable fetishTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fetishTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fetishTable);
        }

        // GET: Fetish/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FetishTable fetishTable = db.FetishTables.Find(id);
            if (fetishTable == null)
            {
                return HttpNotFound();
            }
            return View(fetishTable);
        }

        // POST: Fetish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FetishTable fetishTable = db.FetishTables.Find(id);
            db.FetishTables.Remove(fetishTable);
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
