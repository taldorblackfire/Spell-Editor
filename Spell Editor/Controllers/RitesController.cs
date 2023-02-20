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
    public class RitesController : Controller
    {
        private WerewolfEntities db = new WerewolfEntities();

        // GET: Rites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiteId,RiteName,Description,Level,Symbols,SampleRite,Action,Duration,Success")] Rite rite)
        {
            if (ModelState.IsValid)
            {
                db.Rites.Add(rite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rite);
        }

        // GET: Rites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rite rite = db.Rites.Find(id);
            if (rite == null)
            {
                return HttpNotFound();
            }
            return View(rite);
        }

        // POST: Rites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiteId,RiteName,Description,Level,Symbols,SampleRite,Action,Duration,Success")] Rite rite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rite);
        }

        // GET: Rites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rite rite = db.Rites.Find(id);
            if (rite == null)
            {
                return HttpNotFound();
            }
            return View(rite);
        }

        // POST: Rites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rite rite = db.Rites.Find(id);
            db.Rites.Remove(rite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Rites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rite rite = db.Rites.Find(id);
            if (rite == null)
            {
                return HttpNotFound();
            }
            return View(rite);
        }

        // GET: Rites
        public ActionResult Index()
        {
            return View(db.Rites.ToList());
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
