using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCRC.Models;

namespace WCRC.Repository
{
    public class RaidInfoRepo
    {
        private static RaidInfoRepo _instance = null;
        private static object _lock = new object();
        private static Database _db = new Database("DefaultConnection");
        public static RaidInfoRepo Get()
        {
            lock (_lock)
            {
                if (null == _instance)
                {
                    _instance = new RaidInfoRepo();
                }

                return _instance;
            }
        }

        public IEnumerable<RaidInfo> GetRaidInfos(string name = "")
        {
            IEnumerable<RaidInfo> results;
            if ("" == name)
            {
                results = _db.Query<RaidInfo>("");
            }
            else
            {
                results = _db.Query<RaidInfo>("WHERE name = @0", name);
            }

            return results;
        }
    }
}