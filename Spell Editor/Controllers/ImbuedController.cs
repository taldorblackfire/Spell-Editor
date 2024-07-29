using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;
namespace Spell_Editor.Controllers
{
    public class ImbuedController : Controller
    {
        private WoDEntities db = new WoDEntities();

        public ActionResult AddItemEnhancement()
        {
            ViewBag.Enhancement = new SelectList(db.RefEnhancements, "Id", "Bonus");
            ViewBag.Spell = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");

            return PartialView("_ItemEnhancement", new ItemEnhancement());
        }

        public ActionResult AddSpell()
        {
            ViewBag.Spells = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");
            return PartialView("_ItemSpell", new ImbuedItemSpell());
        }

        // GET: Imbued
        public ActionResult Index()
        {
            var imbued_Table = db.Imbued_Table.Include(i => i.Character_List);
            return View(imbued_Table.ToList());
        }

        // GET: Imbued/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Imbued_Table imbued_Table = db.Imbued_Table.Find(id);
            if (imbued_Table == null) return HttpNotFound();

            ImbuedViewModel vm = new ImbuedViewModel();
            vm.ImbuedInfo = imbued_Table;
            vm.ItemEnhancement = db.ItemEnhancements.Where(x => x.ImbuedItemId == id).ToList();
            vm.Spells = db.ImbuedItemSpells.Where(x => x.ImbuedItemId == id).ToList();

            return View(vm);
        }

        // GET: Imbued/Create
        public ActionResult Create()
        {
            ImbuedViewModel item = new ImbuedViewModel();
            item.ImbuedInfo.Name = "";
            return View(item);
        }

        // POST: Imbued/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImbuedViewModel imbued_Table)
        {
            if (ModelState.IsValid)
            {
                imbued_Table.ImbuedInfo.Mana = 0;
                imbued_Table.ImbuedInfo.Battery_Dots = 0;

                List<ImbuedItemSpell> imbuedSpells = new List<ImbuedItemSpell>();

                if (imbued_Table.Spells.Count > 0)
                {
                    imbued_Table.ImbuedInfo.Mana = 1;
                }

                int bonusDots = 0, spellDots = 0;

                if (imbued_Table.ItemEnhancement.Count > 0)
                {
                    List<ItemEnhancement> spellEnhancements = imbued_Table.ItemEnhancement.Where(x => x.SpellId > 0).ToList();
                    List<ItemEnhancement> bonusEnhancements = imbued_Table.ItemEnhancement.Where(x => x.EnhancementId > 0).ToList();

                    for (int i = 0; i < spellEnhancements.Count; i++)
                    {
                        int spellId = (int)spellEnhancements[i].SpellId;
                        List<byte> spellLevels = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == spellId).Select(x => x.Level).ToList();
                        spellLevels.Sort();
                        int highestLevel = spellLevels[0];
                        if ((spellDots + highestLevel) <= 5) spellDots += highestLevel;
                        else
                        {
                            while (spellDots < 5) { spellDots++; highestLevel--; }
                            for (int j = 0; j < highestLevel; j++) spellDots += 2;
                        }
                    }

                    for (int i = 0; i < bonusEnhancements.Count; i++)
                    {
                        int enhancementId = (int)bonusEnhancements[i].EnhancementId;
                        RefEnhancement enhancement = db.RefEnhancements.SingleOrDefault(x => x.Id == enhancementId);
                        bonusDots += enhancement.Cost;
                        if (enhancement.Bonus.Contains("Mana"))
                        {
                            imbued_Table.ImbuedInfo.Mana += 2;
                            imbued_Table.ImbuedInfo.Battery_Dots += 1;
                        }
                    }
                }

                if (imbued_Table.Spells.Count > 0)
                {
                    List<int> spellLevels = new List<int>();
                    for (int i = 0; i < imbued_Table.Spells.Count; i++)
                    {
                        int spellid = imbued_Table.Spells[i].SpellId;
                        List<byte> currentLevels = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == spellid).Select(x => x.Level).ToList();
                        currentLevels.Sort();
                        spellLevels.Add(currentLevels[0]);
                        ImbuedItemSpell imbuedItemSpell = new ImbuedItemSpell
                        {
                            ImbuedItemId = imbued_Table.ImbuedInfo.ID,
                            SpellId = spellid
                        };
                        imbuedSpells.Add(imbuedItemSpell);
                    }
                    spellLevels.Sort();
                    imbued_Table.ImbuedInfo.Spell_Level = (short)spellLevels.Last();
                }

                imbued_Table.ImbuedInfo.Cost = (short)(imbued_Table.ImbuedInfo.Spell_Level + bonusDots + spellDots);
                db.Imbued_Table.Add(imbued_Table.ImbuedInfo);
                db.SaveChanges();

                for (int i = 0; i < imbuedSpells.Count; i++)
                {
                    imbuedSpells[i].ImbuedItemId = imbued_Table.ImbuedInfo.ID;
                    db.ImbuedItemSpells.Add(imbuedSpells[i]);
                }

                for (int i = 0; i < imbued_Table.ItemEnhancement.Count; i++)
                {
                    imbued_Table.ItemEnhancement[i].ImbuedItemId = imbued_Table.ImbuedInfo.ID;
                    if (imbued_Table.ItemEnhancement[i].SpellId > 0)
                    {
                        int spellId = (int)imbued_Table.ItemEnhancement[i].SpellId;
                        List<byte> spellLevels = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == spellId).Select(x => x.Level).ToList();
                        spellLevels.Sort();
                        Spell_Table spell = db.Spell_Table.SingleOrDefault(x => x.Id == spellId);
                        imbued_Table.ItemEnhancement[i].Enhancement = spell.Spell_Name;
                        imbued_Table.ItemEnhancement[i].EnhancementCost = spellLevels[0];
                    }

                    if (imbued_Table.ItemEnhancement[i].EnhancementId > 0)
                    {
                        int enhancementId = (int)imbued_Table.ItemEnhancement[i].EnhancementId;
                        RefEnhancement enhancement = db.RefEnhancements.SingleOrDefault(x => x.Id == enhancementId);

                        if (imbued_Table.ItemEnhancement[i].SpellId > 0)
                        {
                            int spellId = (int)imbued_Table.ItemEnhancement[i].SpellId;
                            Spell_Table spell = db.Spell_Table.SingleOrDefault(x => x.Id == spellId);
                            imbued_Table.ItemEnhancement[i].Enhancement = enhancement.Bonus + " (Yantra Bonus Casting " + spell.Spell_Name + ")";
                        }
                        else imbued_Table.ItemEnhancement[i].Enhancement = enhancement.Bonus;

                        imbued_Table.ItemEnhancement[i].EnhancementCost = enhancement.Cost;
                    }

                    db.ItemEnhancements.Add(imbued_Table.ItemEnhancement[i]);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnhancementId = new SelectList(db.RefEnhancements, "Id", "Bonus");
            ViewBag.SpellId = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");
            ViewBag.Spells = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");

            return View(imbued_Table);
        }

        // GET: Imbued/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Imbued_Table imbued_Table = db.Imbued_Table.Find(id);
            if (imbued_Table == null) return HttpNotFound();

            ImbuedViewModel vm = new ImbuedViewModel();

            vm.ImbuedInfo = imbued_Table;
            vm.ItemEnhancement = db.ItemEnhancements.Where(x => x.ImbuedItemId == id).ToList();
            vm.Spells = db.ImbuedItemSpells.Where(x => x.ImbuedItemId == id).ToList();

            ViewBag.Enhancement = new SelectList(db.RefEnhancements, "Id", "Bonus");
            ViewBag.Spell = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");
            ViewBag.Spells = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");

            return View(vm);
        }

        // POST: Imbued/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImbuedViewModel imbued_Table)
        {
            if (ModelState.IsValid)
            {
                imbued_Table.ImbuedInfo.Mana = 0;
                imbued_Table.ImbuedInfo.Battery_Dots = 0;

                int bonusDots = 0, spellDots = 0;

                if (imbued_Table.Spells.Count > 0)
                {
                    imbued_Table.ImbuedInfo.Mana = 1;
                }

                if (imbued_Table.ItemEnhancement.Count > 0)
                {
                    List<ItemEnhancement> spellEnhancements = imbued_Table.ItemEnhancement.Where(x => x.SpellId > 0).ToList();
                    List<ItemEnhancement> bonusEnhancements = imbued_Table.ItemEnhancement.Where(x => x.EnhancementId > 0).ToList();

                    for (int i = 0; i < spellEnhancements.Count; i++)
                    {
                        int spellId = (int)spellEnhancements[i].SpellId;
                        List<byte> spellLevels = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == spellId).Select(x => x.Level).ToList();
                        spellLevels.Sort();
                        int highestLevel = spellLevels[0];
                        if ((spellDots + highestLevel) <= 5) spellDots += highestLevel;
                        else
                        {
                            while (spellDots < 5) { spellDots++; highestLevel--; }
                            for (int j = 0; j < highestLevel; j++) spellDots += 2;
                        }
                    }

                    for (int i = 0; i < bonusEnhancements.Count; i++)
                    {
                        int enhancementId = (int)bonusEnhancements[i].EnhancementId;
                        RefEnhancement enhancement = db.RefEnhancements.SingleOrDefault(x => x.Id == enhancementId);
                        bonusDots += enhancement.Cost;
                        if (enhancement.Bonus.Contains("Mana"))
                        {
                            imbued_Table.ImbuedInfo.Mana += 2;
                            imbued_Table.ImbuedInfo.Battery_Dots += 1;
                        }
                    }
                }

                if (imbued_Table.Spells.Count > 0)
                {
                    List<int> spellLevels = new List<int>();
                    for (int i = 0; i < imbued_Table.Spells.Count; i++)
                    {
                        int spellid = imbued_Table.Spells[i].SpellId;
                        if (spellid > 0)
                        {
                            List<byte> currentLevels = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == spellid).Select(x => x.Level).ToList();
                            currentLevels.Sort();
                            spellLevels.Add(currentLevels[0]);
                            ImbuedItemSpell imbuedItemSpell = new ImbuedItemSpell
                            {
                                ImbuedItemId = imbued_Table.ImbuedInfo.ID,
                                SpellId = spellid
                            };

                            if (imbued_Table.Spells[i].ImbuedItemSpellId == 0) db.ImbuedItemSpells.Add(imbuedItemSpell);
                            else db.Entry(imbued_Table.Spells[i]).State = EntityState.Modified;
                        }
                        else
                        {
                            int imbuedSpellId = imbued_Table.Spells[i].ImbuedItemSpellId;
                            var imbuedSpell = db.ImbuedItemSpells.Where(x => x.ImbuedItemSpellId == imbuedSpellId).FirstOrDefault();
                            if (imbuedSpell != null)
                            {
                                db.ImbuedItemSpells.Remove(imbuedSpell);
                            }
                        }
                    }
                    spellLevels.Sort();
                    imbued_Table.ImbuedInfo.Spell_Level = (short)spellLevels.Last();
                }

                for (int i = 0; i < imbued_Table.ItemEnhancement.Count; i++)
                {
                    int id = imbued_Table.ItemEnhancement[i].Id;
                    if (id == 0)
                    {
                        imbued_Table.ItemEnhancement[i].ImbuedItemId = imbued_Table.ImbuedInfo.ID;
                        if (imbued_Table.ItemEnhancement[i].SpellId > 0)
                        {
                            int spellId = (int)imbued_Table.ItemEnhancement[i].SpellId;
                            List<byte> spellLevels = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == spellId).Select(x => x.Level).ToList();
                            spellLevels.Sort();
                            Spell_Table spell = db.Spell_Table.SingleOrDefault(x => x.Id == spellId);
                            imbued_Table.ItemEnhancement[i].Enhancement = spell.Spell_Name;
                            imbued_Table.ItemEnhancement[i].EnhancementCost = spellLevels[0];
                        }

                        if (imbued_Table.ItemEnhancement[i].EnhancementId > 0)
                        {
                            int enhancementId = (int)imbued_Table.ItemEnhancement[i].EnhancementId;
                            RefEnhancement enhancement = db.RefEnhancements.SingleOrDefault(x => x.Id == enhancementId);

                            if (imbued_Table.ItemEnhancement[i].SpellId > 0)
                            {
                                int spellId = (int)imbued_Table.ItemEnhancement[i].SpellId;
                                Spell_Table spell = db.Spell_Table.SingleOrDefault(x => x.Id == spellId);
                                imbued_Table.ItemEnhancement[i].Enhancement = enhancement.Bonus + " (Yantra Bonus Casting " + spell.Spell_Name + ")";
                            }
                            else imbued_Table.ItemEnhancement[i].Enhancement = enhancement.Bonus;

                            imbued_Table.ItemEnhancement[i].EnhancementCost = enhancement.Cost;
                        }

                        db.ItemEnhancements.Add(imbued_Table.ItemEnhancement[i]);
                    }
                    else if (id > 0)
                    {
                        if (imbued_Table.ItemEnhancement[i].SpellId > 0)
                        {
                            int spellId = (int)imbued_Table.ItemEnhancement[i].SpellId;
                            List<byte> spellLevels = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == spellId).Select(x => x.Level).ToList();
                            spellLevels.Sort();
                            Spell_Table spell = db.Spell_Table.SingleOrDefault(x => x.Id == spellId);
                            imbued_Table.ItemEnhancement[i].Enhancement = spell.Spell_Name;
                            imbued_Table.ItemEnhancement[i].EnhancementCost = spellLevels.Last();
                        }

                        if (imbued_Table.ItemEnhancement[i].EnhancementId > 0)
                        {
                            int enhancementId = (int)imbued_Table.ItemEnhancement[i].EnhancementId;
                            RefEnhancement enhancement = db.RefEnhancements.SingleOrDefault(x => x.Id == enhancementId);

                            if (imbued_Table.ItemEnhancement[i].SpellId > 0)
                            {
                                int spellId = (int)imbued_Table.ItemEnhancement[i].SpellId;
                                Spell_Table spell = db.Spell_Table.SingleOrDefault(x => x.Id == spellId);
                                imbued_Table.ItemEnhancement[i].Enhancement = enhancement.Bonus + " (Yantra Bonus Casting " + spell.Spell_Name + ")";
                            }
                            else imbued_Table.ItemEnhancement[i].Enhancement = enhancement.Bonus;

                            imbued_Table.ItemEnhancement[i].EnhancementCost = enhancement.Cost;
                        }
                        db.Entry(imbued_Table.ItemEnhancement[i]).State = EntityState.Modified;

                        if (imbued_Table.ItemEnhancement[i].EnhancementId == null && imbued_Table.ItemEnhancement[i].SpellId == null) db.ItemEnhancements.Remove(imbued_Table.ItemEnhancement[i]);
                    }
                }

                imbued_Table.ImbuedInfo.Cost = (short)(imbued_Table.ImbuedInfo.Spell_Level + bonusDots + spellDots);
                db.Entry(imbued_Table.ImbuedInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnhancementId = new SelectList(db.RefEnhancements, "Id", "Bonus");
            ViewBag.SpellId = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");
            ViewBag.Spells = new SelectList(db.Spell_Table.OrderBy(x => x.Spell_Name), "Id", "Spell_Name");
            return View(imbued_Table);
        }

        // GET: Imbued/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Imbued_Table imbued_Table = db.Imbued_Table.Find(id);
            if (imbued_Table == null) return HttpNotFound();

            return View(imbued_Table);
        }

        // POST: Imbued/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imbued_Table imbued_Table = db.Imbued_Table.Find(id);
            db.Imbued_Table.Remove(imbued_Table);
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
