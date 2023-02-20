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
    public class ManifestationsController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Manifestations
        public ActionResult Index()
        {
            List<Haunt> manifestations = db.Haunts.ToList();
            List<string> types = manifestations.Select(x => x.Name).Distinct().ToList();
            types.Sort();
            return View(types);
        }

        // GET: Manifestations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haunt Haunts = db.Haunts.Find(id);
            if (Haunts == null)
            {
                return HttpNotFound();
            }
            return View(Haunts);
        }

        // GET: Manifestations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manifestations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,type,description")] Haunt Haunts)
        {
            if (ModelState.IsValid)
            {
                db.Haunts.Add(Haunts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Haunts);
        }

        // GET: Manifestations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haunt Haunts = db.Haunts.Find(id);
            if (Haunts == null)
            {
                return HttpNotFound();
            }
            return View(Haunts);
        }

        // POST: Manifestations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,type,description")] Haunt Haunts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Haunts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Haunts);
        }

        // GET: Manifestations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haunt Haunts = db.Haunts.Find(id);
            if (Haunts == null)
            {
                return HttpNotFound();
            }
            return View(Haunts);
        }

        // POST: Manifestations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Haunt Haunts = db.Haunts.Find(id);
            db.Haunts.Remove(Haunts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Manifestations (string id)
        {
            List<Haunt> manifestations_list = db.Haunts.Where(x => x.HauntId == Convert.ToInt32(id)).ToList();

            ViewBag.Details = id;
            return View(manifestations_list);
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
