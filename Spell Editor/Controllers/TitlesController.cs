using Spell_Editor.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
namespace Spell_Editor.Controllers
{
    public class TitlesController : Controller
    {
        private WoDEntities db = new WoDEntities();

        public ActionResult CreateCaucus()
        {
            ViewBag.Order = new SelectList(db.MageOrders.ToList(), "ID", "OrderName");
            return View(new MageCaucusInfo());
        }

        [HttpPost]
        public ActionResult CreateCaucus(MageCaucusInfo caucus)
        {
            if (ModelState.IsValid)
            {
                db.MageCaucusInfoes.Add(caucus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Order = new SelectList(db.MageOrders.ToList(), "ID", "OrderName");
                return View(caucus);
            }
        }

        public ActionResult CreateConsilium()
        {
            return View(new MageConsiliumTitle());
        }

        [HttpPost]
        public ActionResult CreateConsilium(MageConsiliumTitle consilium)
        {
            if (ModelState.IsValid)
            {
                db.MageConsiliumTitles.Add(consilium);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View(consilium);
        }

        public ActionResult DetailsCaucus(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MageCaucusInfo caucus = db.MageCaucusInfoes.FirstOrDefault(x => x.MageCaucusId == id);
            return View(caucus);
        }

        public ActionResult DetailsConsilium(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MageConsiliumTitle consilium = db.MageConsiliumTitles.FirstOrDefault(x => x.ConsiliumTitleId == id);
            return View(consilium);
        }

        public ActionResult EditCaucus(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MageCaucusInfo caucus = db.MageCaucusInfoes.FirstOrDefault(x => x.MageCaucusId == id);
            ViewBag.Order = new SelectList(db.MageOrders.ToList(), "ID", "OrderName");
            return View(caucus);
        }

        [HttpPost]
        public ActionResult EditCaucus(MageCaucusInfo caucus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caucus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Order = new SelectList(db.MageOrders.ToList(), "ID", "OrderName");
                return View(caucus);
            }
        }

        public ActionResult EditConsilium(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            MageConsiliumTitle consilium = db.MageConsiliumTitles.FirstOrDefault(x => x.ConsiliumTitleId == id);
            return View(consilium);
        }

        [HttpPost]
        public ActionResult EditConsilium(MageConsiliumTitle consilium)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consilium).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View(consilium);
        }

        // GET: Titles
        public ActionResult Index()
        {
            TitleViewModel title = new TitleViewModel();
            title.MageCaucus = db.MageCaucusInfoes.ToList();
            title.Consilium = db.MageConsiliumTitles.ToList();
            return View(title);
        }
    }
}