using System;
using PetaPoco;
using Newtonsoft.Json;

namespace WCRC.Models
{
    [TableName("ResetEvent")]
    [PrimaryKey("guid")]
    public class ResetEvent
    {
        [JsonIgnore]
        public int guid { get; set; }
        [JsonIgnore]
        public int date { get; set; }

        [Column("name")]
        public string title { get; set; }
        public string start { get; set; }
        public string resourceId { get; set; }
        public string id { get; set; }

        public ResetEvent()
        {
            guid = 0;
            date = 0;
            title = "";
            start = "";
            resourceId = "";
            id = "";
        }

        public ResetEvent(string _title, DateTime _date, string _resourceId)
        {
            title = _title;
            resourceId = _resourceId;
            start = _date.ToString("yyyy-MM-dd");
            date = int.Parse(_date.ToString("yyyyMMdd"));
            id = string.Format("{0}{1}", date, title);
        }
    }

    [TableName("RaidInfo")]
    [PrimaryKey("guid")]
    public class RaidInfo
    {
        public int guid { get; set; }
        public string name { get; set; }
        public bool isThurReset { get; set; }
        public float term { get; set; }
        public string start { get; set; }
        public string resourceId { get; set; }
        public RaidInfo()
        {
            name = "";
            isThurReset = true;
            term = 0f;
            start = "";
            resourceId = "";
        }
    }
}