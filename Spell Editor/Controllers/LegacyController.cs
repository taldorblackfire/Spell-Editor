using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;
namespace Spell_Editor.Controllers
{
    public class LegacyController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Legacy/Create
        public ActionResult Create()
        {
            ViewBag.ArcanaList = new SelectList(db.Arcana_Table, "Id", "arcana");
            ViewBag.OrderList = new SelectList(db.MageOrders, "ID", "OrderName");
            ViewBag.PathList = new SelectList(db.MagePaths, "ID", "Name");
            return View();
        }

        // POST: Legacy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LegacyId,PathId,OrderId,SecondOrderId,RulingArcana,SecondaryArcana,LegacyName,Description,LeftHanded")] RefLegacy refLegacy)
        {
            if (ModelState.IsValid)
            {
                db.RefLegacies.Add(refLegacy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArcanaList = new SelectList(db.Arcana_Table, "Id", "arcana");
            ViewBag.OrderList = new SelectList(db.MageOrders, "ID", "OrderName");
            ViewBag.PathList = new SelectList(db.MagePaths, "ID", "Name");
            return View(refLegacy);
        }

        // GET: Legacy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefLegacy refLegacy = db.RefLegacies.Find(id);
            if (refLegacy == null)
            {
                return HttpNotFound();
            }
            return View(refLegacy);
        }

        // POST: Legacy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefLegacy refLegacy = db.RefLegacies.Find(id);
            db.RefLegacies.Remove(refLegacy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Legacy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefLegacy refLegacy = db.RefLegacies.Find(id);
            if (refLegacy == null)
            {
                return HttpNotFound();
            }
            return View(refLegacy);
        }

        // GET: Legacy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            RefLegacy refLegacy = db.RefLegacies.Find(id);
            if (refLegacy == null) return HttpNotFound();

            ViewBag.ArcanaList = new SelectList(db.Arcana_Table, "Id", "arcana");
            ViewBag.OrderList = new SelectList(db.MageOrders, "ID", "OrderName");
            ViewBag.PathList = new SelectList(db.MagePaths, "ID", "Name");
            return View(refLegacy);
        }

        // POST: Legacy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LegacyId,PathId,OrderId,SecondOrderId,RulingArcana,SecondaryArcana,LegacyName,Description,LeftHanded")] RefLegacy refLegacy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refLegacy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArcanaList = new SelectList(db.Arcana_Table, "Id", "arcana");
            ViewBag.OrderList = new SelectList(db.MageOrders, "ID", "OrderName");
            ViewBag.PathList = new SelectList(db.MagePaths, "ID", "Name");
            return View(refLegacy);
        }

        // GET: Legacy
        public ActionResult Index()
        {
            var refLegacies = db.RefLegacies.Include(r => r.Arcana_Table).Include(r => r.Arcana_Table1).Include(r => r.MageOrder).Include(r => r.MageOrder1).Include(r => r.MagePath);
            return View(refLegacies.ToList());
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
