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
    public class SkillController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Skill
        public ActionResult Index()
        {
            return View(db.Skill_Table.ToList());
        }

        // GET: Skill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill_Table skill_Table = db.Skill_Table.Find(id);
            if (skill_Table == null)
            {
                return HttpNotFound();
            }
            return View(skill_Table);
        }

        // GET: Skill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Skill_Table skill_Table)
        {
            if (ModelState.IsValid)
            {
                db.Skill_Table.Add(skill_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skill_Table);
        }

        // GET: Skill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill_Table skill_Table = db.Skill_Table.Find(id);
            if (skill_Table == null)
            {
                return HttpNotFound();
            }
            return View(skill_Table);
        }

        // POST: Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Skill_Table skill_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill_Table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skill_Table);
        }

        // GET: Skill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill_Table skill_Table = db.Skill_Table.Find(id);
            if (skill_Table == null)
            {
                return HttpNotFound();
            }
            return View(skill_Table);
        }

        // POST: Skill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill_Table skill_Table = db.Skill_Table.Find(id);
            db.Skill_Table.Remove(skill_Table);
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
