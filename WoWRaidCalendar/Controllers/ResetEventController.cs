using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCRC.Models;
using WCRC.Repository;

namespace WCRC.Controllers
{
    [Authorize]
    public class ResetEventController : Controller
    {
        // GET: ResetEvent
        public ActionResult Index()
        {
            var results = ResetEventRepo.Get().GetResetEvents();

            return View(results);
        }

        // GET: ResetEvent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResetEvent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResetEvent/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResetEvent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResetEvent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResetEvent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResetEvent/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteAll()
        {
            int deleteCount = ResetEventRepo.Get().Clear();
            ViewBag.message = string.Format("{0} deleted.", deleteCount);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteByName(string name)
        {
            if ("" != name)
            {
                int deleteCount = ResetEventRepo.Get().Clear("", "", name);
                ViewBag.message = string.Format("{0} deleted.", deleteCount);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MakeEvent(string name, string start, string end)
        {
            if ("" == start || "" == end)
            {
                return RedirectToAction("Index");
            }

            MakeEventsByName(start, end, name);

            return RedirectToAction("Index");
        }

        public ActionResult Manage()
        {
            return View();
        }

        public int MakeEventsAllRaid(string start, string end)
        {
            int insertCount = 0;
            IEnumerable<RaidInfo> raidInfos = RaidInfoRepo.Get().GetRaidInfos();
            foreach (var item in raidInfos)
            {
                insertCount += MakeEventsEachRaid(item, start, end);
            }

            return insertCount;
        }

        public int MakeEventsByName(string start, string end, string name = "")
        {
            int insertCount = 0;
            if ("" == name)
            {
                return MakeEventsAllRaid(start, end);
            }

            IEnumerable<RaidInfo> raidInfos = RaidInfoRepo.Get().GetRaidInfos(name);
            foreach (var item in raidInfos)
            {
                insertCount += MakeEventsEachRaid(item, start, end);
            }

            return insertCount;
        }

        public int MakeEventsEachRaid(RaidInfo info, string start, string end)
        {
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);
            List<ResetEvent> events = new List<ResetEvent>();
            DateTime termDate = DateTime.Parse(info.start);

            while (startDate < endDate)
            {
                if (true == info.isThurReset)
                {
                    if ((DayOfWeek.Thursday == startDate.DayOfWeek) &&
                        (startDate >= termDate))
                    {
                        var newEvent = new ResetEvent(info.name, startDate, info.resourceId);
                        events.Add(newEvent);
                        startDate = startDate.AddDays(7f);
                    }
                    else
                    {
                        startDate = startDate.AddDays(1f);
                    }
                }
                else
                {
                    if (startDate == termDate)
                    {
                        var newEvent = new ResetEvent(info.name, startDate, info.resourceId);
                        events.Add(newEvent);
                        startDate = startDate.AddDays(info.term);
                        termDate = termDate.AddDays(info.term);
                    }
                    else
                    {
                        startDate = startDate.AddDays(1f);
                    }
                }
            }

            int insertCount = ResetEventRepo.Get().Add(events);

            return insertCount;
        }
    }
}
