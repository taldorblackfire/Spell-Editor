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
    public class EssenceShapingController : Controller
    {
        private MonsterEntities db = new MonsterEntities();

        // GET: EssenceShaping
        public ActionResult Index()
        {
            return View(db.Essence_Shaping.ToList());
        }

        // GET: EssenceShaping/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Essence_Shaping essenceShaping = db.Essence_Shaping.Find(id);
            if (essenceShaping == null)
            {
                return HttpNotFound();
            }
            return View(essenceShaping);
        }

        // GET: EssenceShaping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EssenceShaping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Effect")]Essence_Shaping essenceShaping)
        {
            if (ModelState.IsValid)
            {
                db.Essence_Shaping.Add(essenceShaping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(essenceShaping);
        }

        // GET: EssenceShaping/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Essence_Shaping essenceShaping = db.Essence_Shaping.Find(id);
            if (essenceShaping == null)
            {
                return HttpNotFound();
            }
            return View(essenceShaping);
        }

        // POST: EssenceShaping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Effect")]Essence_Shaping essenceShaping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(essenceShaping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(essenceShaping);
        }

        // GET: EssenceShaping/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Essence_Shaping essenceShaping = db.Essence_Shaping.Find(id);
            if (essenceShaping == null)
            {
                return HttpNotFound();
            }
            return View(essenceShaping);
        }

        // POST: EssenceShaping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Essence_Shaping essenceShaping = db.Essence_Shaping.Find(id);
            db.Essence_Shaping.Remove(essenceShaping);
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
