using System.Linq;
using System.Web.Mvc;
using Spell_Editor.Models;
namespace Spell_Editor.Controllers
{
    public class FateHexBoonsController : Controller
    {
        private WoDEntities db = new WoDEntities();

        // GET: FateHexBoons
        public ActionResult Index()
        {
            return View(db.Fate_Hex_Boons.ToList());
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
