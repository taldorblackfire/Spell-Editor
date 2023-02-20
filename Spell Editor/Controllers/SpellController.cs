using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Spell_Editor.Models;
using System.Text;
using System.Data.Entity;
using MoreLinq;
namespace Spell_Editor.Controllers
{
    public class SpellController : Controller
    {
        private WoDEntities db = new WoDEntities();
        private readonly List<SpellView> view = new List<SpellView>();

        public SpellController()
        {
            view = db.SpellView.ToList();
        }

        public ActionResult AddNewArcana()
        {
            Spell_Arcana_Table newArcana = new Spell_Arcana_Table();
            ViewBag.ArcanaList = new SelectList(db.Arcana_Table, "arcana", "arcana");
            return PartialView("_Spell_Arcana", newArcana);
        }

        private static readonly Dictionary<char, char> Replacements = new Dictionary<char, char>()
        {
            {'\u0095', '\u2022'}, {'\u0096', '\u002D'}, {'\u0094', '\u0022'}, {'\u0092', '\u0027'}, {'\u0093', '\u0022'}, {'\u0097', '\u002D'}
        };

        // GET: Spell/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Spell_Table spell_Table = db.Spell_Table.Find(id);

            if (spell_Table == null) return HttpNotFound();

            return View(spell_Table);
        }

        // GET: Spell/Create
        public ActionResult Create()
        {
            SpellModel spell = new SpellModel();
            ViewBag.ArcanaList = new SelectList(db.Arcana_Table, "arcana", "arcana");

            return View(spell);
        }

        // POST: Spell/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpellModel spell_model)
        {
            if(ModelState.IsValid)
            {
                spell_model.spell_table.Description = Encoding.GetEncoding(1252).GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1252), Encoding.UTF8.GetBytes(spell_model.spell_table.Description)));
                foreach (char character in Replacements.Keys) spell_model.spell_table.Description = spell_model.spell_table.Description.Replace(character, Replacements[character]);
                spell_model.spell_table.Description = spell_model.spell_table.Description + "\n";

                spell_model.spell_table.Spell_Name = EasySpellNameGenerator(spell_model.spell_table.Spell_Name, spell_model.spell_arcana);

                db.Spell_Table.Add(spell_model.spell_table);
                db.SaveChanges();

                spell_model.spell_arcana.Select(c => { c.Spell_Table_ID = spell_model.spell_table.Id; return c; }).ToList();

                db.Spell_Arcana_Table.AddRange(spell_model.spell_arcana);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(spell_model);
        }

        private string EasySpellNameGenerator(string name, List<Spell_Arcana_Table> spell_arcana)
        {
            name = name + " (";
            for (int i = 0; i < spell_arcana.Count; i++)
            {
                if (i > 0) name = name + " + ";
                name = name + spell_arcana[i].Arcana + " ";
                for (int j = 0; j < spell_arcana[i].Level; j++) name = name + "\u2022";
            }
            name = name + ")";

            return name;
        }

        // GET: Spell/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SpellModel spell_Table = new SpellModel();
            spell_Table.spell_table = db.Spell_Table.Find(id);
            spell_Table.spell_arcana = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == id).ToList();

            if (spell_Table == null) return HttpNotFound();

            ViewBag.ArcanaList = new SelectList(db.Arcana_Table, "arcana", "arcana");

            return View(spell_Table);
        }

        // POST: Spell/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpellModel spell)
        {
            if (ModelState.IsValid)
            {
                spell.spell_table.Description = Encoding.GetEncoding(1252).GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1252), Encoding.UTF8.GetBytes(spell.spell_table.Description)));
                foreach (char character in Replacements.Keys) spell.spell_table.Description = spell.spell_table.Description.Replace(character, Replacements[character]);

                spell.spell_table.Spell_Name = EasySpellNameGenerator(spell.spell_table.Spell_Name.Substring(0, spell.spell_table.Spell_Name.IndexOf('(')).Trim(), spell.spell_arcana);
                spell.spell_arcana.Select(c => { c.Spell_Table_ID = spell.spell_table.Id; return c; }).ToList();

                for (int i = 0; i < spell.spell_arcana.Count; i++)
                    if (spell.spell_arcana[i].Id == 0) db.Spell_Arcana_Table.Add(spell.spell_arcana[i]);
                    else
                    {
                        db.Entry(spell.spell_arcana[i]).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                db.Entry(spell.spell_table).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(spell);
        }

        // GET: Spell/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Spell_Table spell_Table = db.Spell_Table.Find(id);

            if (spell_Table == null) return HttpNotFound();

            return View(spell_Table);
        }

        public void DeleteArcana(int? id)
        {
            Spell_Arcana_Table spell_arcana = db.Spell_Arcana_Table.Find(id);
            db.Spell_Arcana_Table.Remove(spell_arcana);
            db.SaveChanges();
        }

        // POST: Spell/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spell_Table spell_Table = db.Spell_Table.Find(id);
            List<Spell_Arcana_Table> spell_arcana = db.Spell_Arcana_Table.Where(x => x.Spell_Table_ID == id).ToList();
            db.Spell_Arcana_Table.RemoveRange(spell_arcana);
            db.Spell_Table.Remove(spell_Table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Spell
        public ActionResult Index()
        {
            SpellListModel spell_list = new SpellListModel();

            for(int i = 0; i < view.Count; i++)
            {
                int spellId = view[i].Id;
                if(!spell_list.Spells.Select(x => x.Id).Contains(spellId))
                {
                    SpellInfo info = new SpellInfo();
                    info.SpellName = view[i].Spell_Name;
                    info.Id = spellId;

                    List<string> arcanaList = view.Where(x => x.Id == spellId).Select(x => x.Arcana).ToList();
                    List<byte> levelList = view.Where(x => x.Id == spellId).Select(x => x.Level).ToList();

                    for(int j = 0; j < arcanaList.Count; j++) info.Arcana += arcanaList[j] + " " + levelList[j] + ", ";

                    info.Arcana = info.Arcana.Trim().TrimEnd(',');
                    spell_list.Spells.Add(info);
                }
            }

            ViewBag.Arcana = new SelectList(db.Arcana_Table.ToList(), "Id", "arcana");
            List<string> levels = new List<string>() { "1", "2", "3", "4", "5" };
            ViewBag.Levels = new SelectList(levels);
            return View(spell_list);
        }

        public ActionResult Spells(string id)
        {
            List<Spell_Arcana_Table> spelltype_list = db.Spell_Arcana_Table.Where(x => x.Arcana.Contains(id)).ToList();

            List<Spell_Table> spell_levels = spelltype_list.DistinctBy(x => x.Spell_Table_ID).OrderBy(x => x.Spell_Table.Spell_Name).Select(x => x.Spell_Table).ToList();

            ViewBag.Details = id;
            return View(spell_levels);
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
