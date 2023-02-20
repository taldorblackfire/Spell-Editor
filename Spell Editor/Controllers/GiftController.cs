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
    public class GiftController : Controller
    {
        private WerewolfEntities db = new WerewolfEntities();

        // GET: Gift/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.RefShadowGiftCategories, "GiftCategoryId", "CategoryName");
            ViewBag.RenownId = new SelectList(db.RefRenowns, "RenownId", "Renown");
            return View();
        }

        // POST: Gift/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefGiftsId,CategoryId,GiftName,RenownId,Cost,DiePool,Action,Description,Duration,DramaticFailure,Failure,Success,ExceptionalSuccess")] RefShadowGift refShadowGift)
        {
            if (ModelState.IsValid)
            {
                db.RefShadowGifts.Add(refShadowGift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.RefShadowGiftCategories, "GiftCategoryId", "CategoryName", refShadowGift.CategoryId);
            ViewBag.RenownId = new SelectList(db.RefRenowns, "RenownId", "Renown", refShadowGift.RenownId);
            return View(refShadowGift);
        }

        // GET: Gift/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefShadowGift refShadowGift = db.RefShadowGifts.Find(id);
            ViewBag.CategoryId = new SelectList(db.RefShadowGiftCategories, "GiftCategoryId", "CategoryName", refShadowGift.CategoryId);
            ViewBag.RenownId = new SelectList(db.RefRenowns, "RenownId", "Renown", refShadowGift.RenownId);
            if (refShadowGift == null)
            {
                return HttpNotFound();
            }
            return View(refShadowGift);
        }

        // POST: Gift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefShadowGift refShadowGift = db.RefShadowGifts.Find(id);
            db.RefShadowGifts.Remove(refShadowGift);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Gift/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefShadowGift refShadowGift = db.RefShadowGifts.Find(id);
            ViewBag.CategoryId = new SelectList(db.RefShadowGiftCategories, "GiftCategoryId", "CategoryName", refShadowGift.CategoryId);
            ViewBag.RenownId = new SelectList(db.RefRenowns, "RenownId", "Renown", refShadowGift.RenownId);
            if (refShadowGift == null)
            {
                return HttpNotFound();
            }
            return View(refShadowGift);
        }

        // GET: Gift/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefShadowGift refShadowGift = db.RefShadowGifts.Find(id);
            if (refShadowGift == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.RefShadowGiftCategories, "GiftCategoryId", "CategoryName", refShadowGift.CategoryId);
            ViewBag.RenownId = new SelectList(db.RefRenowns, "RenownId", "Renown", refShadowGift.RenownId);
            return View(refShadowGift);
        }

        // POST: Gift/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefGiftsId,CategoryId,GiftName,RenownId,Cost,DiePool,Action,Description,Duration,DramaticFailure,Failure,Success,ExceptionalSuccess")] RefShadowGift refShadowGift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refShadowGift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.RefShadowGiftCategories, "GiftCategoryId", "CategoryName", refShadowGift.CategoryId);
            ViewBag.RenownId = new SelectList(db.RefRenowns, "RenownId", "Renown", refShadowGift.RenownId);
            return View(refShadowGift);
        }

        // GET: Gift
        public ActionResult Index()
        {
            var refShadowGifts = db.RefShadowGifts.Include(r => r.RefShadowGiftCategory);
            return View(refShadowGifts.ToList());
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
