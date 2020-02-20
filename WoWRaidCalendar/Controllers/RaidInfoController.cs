using PetaPoco;
using System.Web.Mvc;
using WCRC.Models;

namespace WCRC.Controllers
{
    [Authorize]
    public class RaidInfoController : Controller
    {
        private Database _db = new Database("DefaultConnection");
        // GET: RaidInfo
        public ActionResult Index()
        {
            var results = _db.Query<RaidInfo>("");

            return View(results);
        }

        // GET: RaidInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RaidInfo/Create
        [HttpPost]
        public ActionResult Create(RaidInfo model)
        {
            try
            {
                // TODO: Add insert logic here
                _db.Insert(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RaidInfo/Edit/5
        public ActionResult Edit(int id)
        {
            var find = _db.SingleOrDefault<RaidInfo>("WHERE guid = @0", id);
            return View(find);
        }

        // POST: RaidInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RaidInfo model)
        {
            try
            {
                // TODO: Add update logic here
                int i = _db.Update(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RaidInfo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RaidInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, RaidInfo model)
        {
            try
            {
                // TODO: Add delete logic here
                _db.Delete<RaidInfo>("WHERE guid = @0", id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
