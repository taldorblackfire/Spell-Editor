using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;
using System.Text;
using System.Data.Entity;
namespace Spell_Editor.Controllers
{
    public class AttainmentController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Attainment
        public ActionResult Index()
        {
            ArcanaListModel arcana_list = new ArcanaListModel();
            arcana_list.arcana = db.Arcana_Table.Select(x => x.arcana).ToList();
            return View(arcana_list);
        }

        public ActionResult Attainments (string id)
        {
            List<Attainment_Table> attainment_list = db.Attainment_Table.Where(x => x.attainment_arcana.Contains(id)).ToList();
            attainment_list = attainment_list.OrderBy(x => x.attainment_level).ToList();

            ViewBag.Details = id;
            return View(attainment_list);
        }

        // GET: Attainment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attainment_Table attainment_Table = db.Attainment_Table.Find(id);
            if (attainment_Table == null)
            {
                return HttpNotFound();
            }
            return View(attainment_Table);
        }

        // GET: Attainment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attainment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,attainment_name,attainment_arcana,attainment_level,description")] Attainment_Table attainment_Table)
        {
            if (ModelState.IsValid)
            {
                db.Attainment_Table.Add(attainment_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attainment_Table);
        }

        // GET: Attainment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attainment_Table attainment_Table = db.Attainment_Table.Find(id);
            if (attainment_Table == null)
            {
                return HttpNotFound();
            }
            return View(attainment_Table);
        }

        // POST: Attainment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,attainment_name,attainment_arcana,attainment_level,description")] Attainment_Table attainment_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attainment_Table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attainment_Table);
        }

        // GET: Attainment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attainment_Table attainment_Table = db.Attainment_Table.Find(id);
            if (attainment_Table == null)
            {
                return HttpNotFound();
            }
            return View(attainment_Table);
        }

        // POST: Attainment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attainment_Table attainment_Table = db.Attainment_Table.Find(id);
            db.Attainment_Table.Remove(attainment_Table);
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
