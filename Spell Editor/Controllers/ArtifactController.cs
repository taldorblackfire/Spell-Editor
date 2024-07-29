using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;
using System;

namespace Spell_Editor.Controllers
{
    public class ArtifactController : Controller
    {
        private WoDEntities db = new WoDEntities();

        public ActionResult AddAttainment()
        {
            ViewBag.ArtifactAttainments = new SelectList(db.Attainment_Table.Where(x => x.ItemUsable).OrderBy(x => x.attainment_name), "Id", "attainment_name");

            return PartialView("_ArtifactAttainment", new ArtifactAttainment());
        }

        // GET: Artifact
        public ActionResult Index()
        {
            var artifact_Table = db.Artifact_Table.Include(a => a.Character_List);
            return View(artifact_Table.ToList());
        }

        // GET: Artifact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Artifact_Table artifact_Table = db.Artifact_Table.Find(id);
            if (artifact_Table == null) return HttpNotFound();

            ArtifactViewModel vm = new ArtifactViewModel();

            vm.ArtifactInfo = artifact_Table;
            vm.Attainments = db.ArtifactAttainments.Where(x => x.ArtifactId == id).ToList();
            ViewBag.ArtifactAttainments = new SelectList(db.Attainment_Table.Where(x => x.ItemUsable).OrderBy(x => x.attainment_name), "Id", "attainment_name");

            return View(vm);
        }

        // GET: Artifact/Create
        public ActionResult Create()
        {
            return View(new ArtifactViewModel());
        }

        // POST: Artifact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtifactViewModel artifact_Table)
        {
            if (ModelState.IsValid)
            {
                int cost = 0;
                if (artifact_Table.ArtifactInfo.ImperialSurcharge) cost += 4;
                if (artifact_Table.ArtifactInfo.YantraBonus) cost++;

                cost += artifact_Table.ArtifactInfo.Reach;
                cost += artifact_Table.Attainments.Where(x => x.AttainmentId > 0).ToList().Count;

                if (cost < 3) artifact_Table.ArtifactInfo.Cost = 3;
                else artifact_Table.ArtifactInfo.Cost = (short)cost;

                decimal gnosis = (decimal)(artifact_Table.ArtifactInfo.Cost) / 2;

                if (gnosis < 6 && artifact_Table.ArtifactInfo.ImperialSurcharge) artifact_Table.ArtifactInfo.Gnosis = 6;
                else artifact_Table.ArtifactInfo.Gnosis = (short)Math.Ceiling(gnosis);
                artifact_Table.ArtifactInfo.Mana = (short)(artifact_Table.ArtifactInfo.Cost * 2);

                db.Artifact_Table.Add(artifact_Table.ArtifactInfo);

                for (int i = 0; i < artifact_Table.Attainments.Count; i++)
                {
                    if (artifact_Table.Attainments[i].AttainmentId > 0)
                    {
                        artifact_Table.Attainments[i].ArtifactId = artifact_Table.ArtifactInfo.ID;
                        db.ArtifactAttainments.Add(artifact_Table.Attainments[i]);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artifact_Table);
        }

        // GET: Artifact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Artifact_Table artifact_Table = db.Artifact_Table.Find(id);
            if (artifact_Table == null) return HttpNotFound();

            ArtifactViewModel vm = new ArtifactViewModel();

            vm.ArtifactInfo = artifact_Table;
            vm.Attainments = db.ArtifactAttainments.Where(x => x.ArtifactId == id).ToList();
            var list = db.Attainment_Table.Where(x => x.ItemUsable).OrderBy(x => x.attainment_name).ToList();
            ViewBag.ArtifactAttainments = new SelectList(db.Attainment_Table.Where(x => x.ItemUsable).OrderBy(x => x.attainment_name), "Id", "attainment_name");

            return View(vm);
        }

        // POST: Artifact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArtifactViewModel artifact_Table)
        {
            if (ModelState.IsValid)
            {
                int cost = 0;
                if (artifact_Table.ArtifactInfo.ImperialSurcharge) cost += 4;
                if (artifact_Table.ArtifactInfo.YantraBonus) cost++;

                cost += artifact_Table.ArtifactInfo.Reach;

                for (int i = 0; i < artifact_Table.Attainments.Count; i++)
                {
                    if (artifact_Table.Attainments[i].AttainmentId == 0)
                    {
                        db.ArtifactAttainments.Attach(artifact_Table.Attainments[i]);
                        db.Entry(artifact_Table.Attainments[i]).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                    else if (artifact_Table.Attainments[i].Id == 0)
                    {
                        artifact_Table.Attainments[i].ArtifactId = artifact_Table.ArtifactInfo.ID;
                        db.ArtifactAttainments.Add(artifact_Table.Attainments[i]);
                        cost++;
                    }
                    else
                    {
                        cost++;
                        db.Entry(artifact_Table.Attainments[i]).State = EntityState.Modified;
                    }
                }

                if (cost < 3) artifact_Table.ArtifactInfo.Cost = 3;
                else artifact_Table.ArtifactInfo.Cost = (short)cost;

                decimal gnosis = (decimal)(artifact_Table.ArtifactInfo.Cost) / 2;
                if (gnosis < 6 && artifact_Table.ArtifactInfo.ImperialSurcharge) artifact_Table.ArtifactInfo.Gnosis = 6;
                else artifact_Table.ArtifactInfo.Gnosis = (short)Math.Ceiling(gnosis);
                artifact_Table.ArtifactInfo.Mana = (short)(artifact_Table.ArtifactInfo.Cost * 2);

                db.Entry(artifact_Table.ArtifactInfo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ArtifactAttainments = new SelectList(db.Attainment_Table.Where(x => x.ItemUsable).OrderBy(x => x.attainment_name), "Id", "attainment_name");
            return View(artifact_Table);
        }

        // GET: Artifact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Artifact_Table artifact_Table = db.Artifact_Table.Find(id);
            if (artifact_Table == null) return HttpNotFound();

            return View(artifact_Table);
        }

        // POST: Artifact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artifact_Table artifact_Table = db.Artifact_Table.Find(id);
            db.Artifact_Table.Remove(artifact_Table);
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
