using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;
namespace Spell_Editor.Controllers
{
    public class ConditionController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: Condition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Condition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Effect,Beat,Resolution")] Condition condition)
        {
            if (ModelState.IsValid)
            {
                db.Conditions.Add(condition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(condition);
        }

        // GET: Condition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condition condition = db.Conditions.Find(id);
            if (condition == null)
            {
                return HttpNotFound();
            }
            return View(condition);
        }

        // POST: Condition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Condition condition = db.Conditions.Find(id);
            db.Conditions.Remove(condition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Condition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condition condition = db.Conditions.Find(id);
            if (condition == null)
            {
                return HttpNotFound();
            }
            return View(condition);
        }

        // GET: Condition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condition condition = db.Conditions.Find(id);
            if (condition == null)
            {
                return HttpNotFound();
            }
            return View(condition);
        }

        // POST: Condition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Effect,Beat,Resolution")] Condition condition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(condition);
        }

        // GET: Condition
        public ActionResult Index()
        {
            return View(db.Conditions.ToList());
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
