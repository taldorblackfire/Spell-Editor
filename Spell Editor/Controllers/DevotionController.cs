using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;

namespace Spell_Editor.Controllers
{
    public class DevotionController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Devotion
        public ActionResult Index()
        {
            return View(db.Vampire_Devotion.ToList());
        }

        // GET: Devotion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vampire_Devotion vampire_Devotion = db.Vampire_Devotion.Find(id);
            if (vampire_Devotion == null)
            {
                return HttpNotFound();
            }
            return View(vampire_Devotion);
        }

        // GET: Devotion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Devotion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cost,Name,Effect")] Vampire_Devotion vampire_Devotion)
        {
            if (ModelState.IsValid)
            {
                db.Vampire_Devotion.Add(vampire_Devotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vampire_Devotion);
        }

        // GET: Devotion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vampire_Devotion vampire_Devotion = db.Vampire_Devotion.Find(id);
            if (vampire_Devotion == null)
            {
                return HttpNotFound();
            }
            return View(vampire_Devotion);
        }

        // POST: Devotion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cost,Name,Effect")] Vampire_Devotion vampire_Devotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vampire_Devotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vampire_Devotion);
        }

        // GET: Devotion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vampire_Devotion vampire_Devotion = db.Vampire_Devotion.Find(id);
            if (vampire_Devotion == null)
            {
                return HttpNotFound();
            }
            return View(vampire_Devotion);
        }

        // POST: Devotion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vampire_Devotion vampire_Devotion = db.Vampire_Devotion.Find(id);
            db.Vampire_Devotion.Remove(vampire_Devotion);
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
