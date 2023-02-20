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
    public class DreadPowerController : Controller
    {
        private MonsterEntities db = new MonsterEntities();

        // GET: DreadPower
        public ActionResult Index()
        {
            return View(db.DreadPowers.ToList());
        }

        // GET: DreadPower/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DreadPower dreadPower = db.DreadPowers.Find(id);
            if (dreadPower == null)
            {
                return HttpNotFound();
            }
            return View(dreadPower);
        }

        // GET: DreadPower/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DreadPower/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Effect,Reaching")] DreadPower dreadPower)
        {
            if (ModelState.IsValid)
            {
                db.DreadPowers.Add(dreadPower);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dreadPower);
        }

        // GET: DreadPower/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DreadPower dreadPower = db.DreadPowers.Find(id);
            if (dreadPower == null)
            {
                return HttpNotFound();
            }
            return View(dreadPower);
        }

        // POST: DreadPower/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Effect,Reaching")] DreadPower dreadPower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dreadPower).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dreadPower);
        }

        // GET: DreadPower/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DreadPower dreadPower = db.DreadPowers.Find(id);
            if (dreadPower == null)
            {
                return HttpNotFound();
            }
            return View(dreadPower);
        }

        // POST: DreadPower/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DreadPower dreadPower = db.DreadPowers.Find(id);
            db.DreadPowers.Remove(dreadPower);
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
