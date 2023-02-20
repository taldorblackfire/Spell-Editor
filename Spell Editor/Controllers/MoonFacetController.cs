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
    public class MoonFacetController : Controller
    {
        private WerewolfEntities db = new WerewolfEntities();

        // GET: MoonFacet/Create
        public ActionResult Create()
        {
            ViewBag.MoonCategory = new SelectList(db.RefMoonGifts, "MoonGiftId", "MoonGift");
            return View();
        }

        // POST: MoonFacet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MoonGiftFacetId,MoonCategory,MoonFacet,Level,Cost,DiePool,Duration,Action,Description,DramaticFailure,Failure,Success,ExceptionalSuccess")] RefMoonGiftFacet refMoonGiftFacet)
        {
            if (ModelState.IsValid)
            {
                db.RefMoonGiftFacets.Add(refMoonGiftFacet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MoonCategory = new SelectList(db.RefMoonGifts, "MoonGiftId", "MoonGift", refMoonGiftFacet.MoonCategory);
            return View(refMoonGiftFacet);
        }

        // GET: MoonFacet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefMoonGiftFacet refMoonGiftFacet = db.RefMoonGiftFacets.Find(id);
            if (refMoonGiftFacet == null)
            {
                return HttpNotFound();
            }
            return View(refMoonGiftFacet);
        }

        // POST: MoonFacet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefMoonGiftFacet refMoonGiftFacet = db.RefMoonGiftFacets.Find(id);
            db.RefMoonGiftFacets.Remove(refMoonGiftFacet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: MoonFacet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefMoonGiftFacet refMoonGiftFacet = db.RefMoonGiftFacets.Find(id);
            if (refMoonGiftFacet == null)
            {
                return HttpNotFound();
            }
            return View(refMoonGiftFacet);
        }

        // GET: MoonFacet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefMoonGiftFacet refMoonGiftFacet = db.RefMoonGiftFacets.Find(id);
            if (refMoonGiftFacet == null)
            {
                return HttpNotFound();
            }
            ViewBag.MoonCategory = new SelectList(db.RefMoonGifts, "MoonGiftId", "MoonGift", refMoonGiftFacet.MoonCategory);
            return View(refMoonGiftFacet);
        }

        // POST: MoonFacet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MoonGiftFacetId,MoonCategory,MoonFacet,Level,Cost,DiePool,Duration,Action,Description,DramaticFailure,Failure,Success,ExceptionalSuccess")] RefMoonGiftFacet refMoonGiftFacet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refMoonGiftFacet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MoonCategory = new SelectList(db.RefMoonGifts, "MoonGiftId", "MoonGift", refMoonGiftFacet.MoonCategory);
            return View(refMoonGiftFacet);
        }

        // GET: MoonFacet
        public ActionResult Index()
        {
            var refMoonGiftFacets = db.RefMoonGiftFacets.Include(r => r.RefMoonGift);
            return View(refMoonGiftFacets.ToList());
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
