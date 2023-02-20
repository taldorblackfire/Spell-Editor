using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;
using System.Text;
namespace Spell_Editor.Controllers
{
    public class MeritController : Controller
    {
        private WoDEntities db = new WoDEntities();

        private static readonly Dictionary<char, char> Replacements = new Dictionary<char, char>()
        {
            {'\u0095', '\u2022'}, {'\u0096', '\u002D'}, {'\u0094', '\u0022'}, {'\u0092', '\u0027'}, {'\u0093', '\u0022'}, {'\u0097', '\u002D'}
        };

        // GET: Merit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merit merit_Table = db.Merits.Find(id);
            if (merit_Table == null)
            {
                return HttpNotFound();
            }
            return View(merit_Table);
        }

        // GET: Merit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Merit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Category,MaxValue,Leveled,LevelStart")] Merit merit_Table)
        {
            if (ModelState.IsValid)
            {
                merit_Table.Description = Encoding.GetEncoding(1252).GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1252), Encoding.UTF8.GetBytes(merit_Table.Description)));
                foreach (char character in Replacements.Keys) merit_Table.Description = merit_Table.Description.Replace(character, Replacements[character]);

                merit_Table.Name = Encoding.GetEncoding(1252).GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1252), Encoding.UTF8.GetBytes(merit_Table.Name)));
                foreach (char character in Replacements.Keys) merit_Table.Name = merit_Table.Name.Replace(character, Replacements[character]);

                db.Merits.Add(merit_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merit_Table);
        }

        // GET: Merit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merit merit_Table = db.Merits.Find(id);
            if (merit_Table == null)
            {
                return HttpNotFound();
            }
            return View(merit_Table);
        }

        // POST: Merit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeritId,Name,Description,Category,MaxValue,Leveled,LevelStart")] Merit merit_Table)
        {
            if (ModelState.IsValid)
            {
                merit_Table.Description = Encoding.GetEncoding(1252).GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1252), Encoding.UTF8.GetBytes(merit_Table.Description)));
                foreach (char character in Replacements.Keys) merit_Table.Description = merit_Table.Description.Replace(character, Replacements[character]);
                db.Entry(merit_Table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merit_Table);
        }

        // GET: Merit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merit merit_Table = db.Merits.Find(id);
            if (merit_Table == null)
            {
                return HttpNotFound();
            }
            return View(merit_Table);
        }

        // POST: Merit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Merit merit_Table = db.Merits.Find(id);
            db.Merits.Remove(merit_Table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Merit
        public ActionResult Index()
        {
            MeritListModel merit_list = new MeritListModel();
            merit_list.Merits = db.Merits.ToList();
            return View(merit_list);
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
