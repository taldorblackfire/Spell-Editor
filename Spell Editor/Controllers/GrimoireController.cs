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
    public class GrimoireController : Controller
    {
        private WoDEntities db = new WoDEntities();

        public ActionResult AddSpell()
        {
            Rote newSpell = new Rote();
            ViewBag.Spells = new SelectList(db.Spell_Table.ToList(), "Id", "Spell_Name");
            ViewBag.Skills = new SelectList(db.Skills.ToList(), "skillId", "skill_name");
            return PartialView("_Spells", newSpell);
        }

        // GET: Grimoire/Create
        public ActionResult Create()
        {
            ViewBag.CharacterID = new SelectList(db.Character_List, "characterId", "name");
            GrimoireModel grimoire = new GrimoireModel();

            return View(grimoire);
        }

        // POST: Grimoire/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Cost,Description,CharacterID,Spells")] GrimoireModel grimoire)
        {
            if (ModelState.IsValid)
            {
                Grimoire_Table grimoire_Table = new Grimoire_Table();

                grimoire_Table.CharacterID = grimoire.CharacterID;
                grimoire_Table.Cost = grimoire.Cost;
                grimoire_Table.Description = grimoire.Description;
                grimoire_Table.Name = grimoire.Name;
                db.Grimoire_Table.Add(grimoire_Table);
                db.SaveChanges();

                for(int i = 0; i < grimoire.Spells.Count(); i++)
                {
                    Rote_Table rote = new Rote_Table();
                    rote.GrimoireID = grimoire_Table.ID;
                    rote.SpellTableID = grimoire.Spells[i].Id;
                    rote.SkillID = grimoire.Spells[i].SkillID;
                    db.Rote_Table.Add(rote);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CharacterID = new SelectList(db.Character_List, "characterId", "name", grimoire.CharacterID);
            return View(grimoire);
        }

        // GET: Grimoire/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Grimoire_Table grimoire_Table = db.Grimoire_Table.Find(id);
            if (grimoire_Table == null) return HttpNotFound();

            return View(grimoire_Table);
        }

        // POST: Grimoire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grimoire_Table grimoire_Table = db.Grimoire_Table.Find(id);
            db.Grimoire_Table.Remove(grimoire_Table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DeleteSpell(int? id)
        {
            Rote_Table rote = db.Rote_Table.Find(id);
            db.Rote_Table.Remove(rote);
            db.SaveChanges();
        }

        // GET: Grimoire/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Grimoire_Table grimoire_Table = db.Grimoire_Table.Find(id);
            if (grimoire_Table == null) return HttpNotFound();

            GrimoireModel grimoire = new GrimoireModel();
            grimoire.Id = id;
            grimoire.Cost = grimoire_Table.Cost;
            grimoire.Name = grimoire_Table.Name;
            grimoire.Description = grimoire_Table.Description;

            if (grimoire_Table.CharacterID != null)
            {
                grimoire.CharacterID = (short)grimoire_Table.CharacterID;
                ViewBag.Owner = db.Character_List.Find(grimoire_Table.CharacterID).name;
            }
            else ViewBag.Owner = "";

            List<Rote_Table> rotes = db.Rote_Table.Where(x => x.GrimoireID == id).ToList();
            for (int i = 0; i < rotes.Count; i++)
            {
                Rote spell = new Rote();
                spell.Id = rotes[i].SpellTableID;
                spell.Spell_Name = db.Spell_Table.Find(spell.Id).Spell_Name;
                spell.RoteId = rotes[i].ID;
                spell.SkillID = rotes[i].SkillID;
                grimoire.Spells.Add(spell);
            }

            ViewBag.Skills = new SelectList(db.Skills.ToList(), "skillId", "skill_name");

            return View(grimoire);
        }

        // GET: Grimoire/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Grimoire_Table grimoire_Table = db.Grimoire_Table.Find(id);
            if (grimoire_Table == null) return HttpNotFound();

            GrimoireModel grimoire = new GrimoireModel();
            grimoire.Id = id;
            grimoire.Cost = grimoire_Table.Cost;
            grimoire.Name = grimoire_Table.Name;
            grimoire.Description = grimoire_Table.Description;

            List<Rote_Table> rotes = db.Rote_Table.Where(x => x.GrimoireID == id).ToList();
            for (int i = 0; i < rotes.Count; i++)
            {
                Rote spell = new Rote();
                spell.Id = rotes[i].SpellTableID;
                spell.Spell_Name = db.Spell_Table.Find(spell.Id).Spell_Name;
                spell.RoteId = rotes[i].ID;
                spell.SkillID = rotes[i].SkillID;
                grimoire.Spells.Add(spell);
            }

            ViewBag.CharacterID = new SelectList(db.Character_List, "characterId", "name", grimoire_Table.CharacterID);
            ViewBag.Skills = new SelectList(db.Skills.ToList(), "skillId", "skill_name");
            return View(grimoire);
        }

        // POST: Grimoire/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Cost,Description,CharacterID,Spells")] GrimoireModel grimoire)
        {
            if (ModelState.IsValid)
            {
                Grimoire_Table grimoire_Table = new Grimoire_Table();

                grimoire_Table.CharacterID = grimoire.CharacterID;
                grimoire_Table.Cost = grimoire.Cost;
                grimoire_Table.Description = grimoire.Description;
                grimoire_Table.Name = grimoire.Name;
                grimoire_Table.ID = (int)grimoire.Id;
                db.Entry(grimoire_Table).State = EntityState.Modified;
                db.SaveChanges();

                for (int i = 0; i < grimoire.Spells.Count(); i++)
                {
                    if (grimoire.Spells[i].RoteId == 0)
                    {
                        Rote_Table rote = new Rote_Table();
                        rote.GrimoireID = grimoire_Table.ID;
                        rote.SpellTableID = grimoire.Spells[i].Id;
                        rote.SkillID = grimoire.Spells[i].SkillID;
                        db.Rote_Table.Add(rote);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(grimoire);
        }

        // GET: Grimoire
        public ActionResult Index()
        {
            var grimoire_Table = db.Grimoire_Table.Include(g => g.Rote_Table);
            return View(grimoire_Table.ToList());
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
