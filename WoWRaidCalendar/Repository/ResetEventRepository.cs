using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCRC.Models;
using WCRC.ConstantData;

namespace WCRC.Repository
{
    public class ResetEventRepo
    {
        private static ResetEventRepo _instance = null;
        private static object _lock = new object();
        private static Database _db = new Database("DefaultConnection");

        public static ResetEventRepo Get()
        {
            lock (_lock)
            {
                if (null == _instance)
                {
                    _instance = new ResetEventRepo();
                }

                return _instance;
            }
        }

        public int Add(IEnumerable<ResetEvent> data)
        {
            int insertCount = 0;
            foreach (var item in data)
            {
                ResetEvent find = _db.SingleOrDefault<ResetEvent>("SELECT * FROM ResetEvent WHERE id = @0", item.id);
                if (null == find)
                {
                    _db.Insert(item);
                    insertCount++;
                }
            }

            return insertCount;
        }

        public List<ResetEvent> GetResetEvents(string start = "", string end = "", string name = "")
        {
            IEnumerable<ResetEvent> results;
            if ("" == start && "" == end && "" == name)
            {
                results = _db.Query<ResetEvent>("");
            }
            else
            {
                ClampDateTimeString(ref start, ref end);
                DateTime startDate = DateTime.Parse(start);
                DateTime endDate = DateTime.Parse(end);
                if ("" == name)
                {
                    results = _db.Query<ResetEvent>(
                        "WHERE date >= @0 AND date <= @1"
                        , int.Parse(startDate.ToString(StringData.DateIntFormat))
                        , int.Parse(endDate.ToString(StringData.DateIntFormat)));
                }
                else
                {
                    results = _db.Query<ResetEvent>(
                        "WHERE date >= @0 AND date <= @1 AND name = @2"
                        , int.Parse(startDate.ToString(StringData.DateIntFormat))
                        , int.Parse(endDate.ToString(StringData.DateIntFormat))
                        , name);
                }
            }

            return results.ToList();
        }

        public int Clear(string start = "", string end = "", string name = "")
        {
            int deleteCount = 0;
            if ("" == start && "" == end)
            {
                if ("" == name)
                {
                    deleteCount = _db.Delete<ResetEvent>("");
                }
                else
                {
                    deleteCount = _db.Delete<ResetEvent>("WHERE name = @0", name);
                }
            }
            else
            {
                ClampDateTimeString(ref start, ref end);
                DateTime startDate = DateTime.Parse(start);
                DateTime endDate = DateTime.Parse(end);
                if ("" == name)
                {
                    deleteCount = _db.Delete<ResetEvent>(
                        "WHERE date >= @0 AND date <= @1"
                        , int.Parse(startDate.ToString(StringData.DateIntFormat))
                        , int.Parse(endDate.ToString(StringData.DateIntFormat)));
                }
                else
                {
                    deleteCount = _db.Delete<ResetEvent>(
                        "WHERE date >= @0 AND date <= @1 AND name = @2"
                        , int.Parse(startDate.ToString(StringData.DateIntFormat))
                        , int.Parse(endDate.ToString(StringData.DateIntFormat))
                        , name);
                }
            }

            return deleteCount;
        }

        private static void ClampDateTimeString(ref string start, ref string end)
        {
            if ("" == start)
            {
                start = StringData.WoWClassicBegin;
            }
            if ("" == end)
            {
                end = DateTime.Now.ToString(StringData.DateStringFormat);
                end = DateTime.Parse(end).AddYears(1).ToString(StringData.DateStringFormat);
            }
        }
    }
}