using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.CORE.Model
{
    /// <summary>
    /// 位置信息
    /// </summary>
    public class LocationInfo
    {
        public Info info { get; set; }
        public Detail detail { get; set; }
    }

    public class Info
    {
        public int type { get; set; }
        public int error { get; set; }
        public int time { get; set; }
    }

    public class Detail
    {
        public string name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string adcode { get; set; }
        public string pointx { get; set; }
        public string pointy { get; set; }
        public string gps_type { get; set; }
        public string reliability { get; set; }
        public string province { get; set; }
        public string deviation { get; set; }
        public string pcd_conflict_flag { get; set; }
        public string query_status { get; set; }
        public string server_retcode { get; set; }
        public string similarity { get; set; }
        public string split_addr { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
        public string key_poi { get; set; }
        public string category_code { get; set; }
        public string address_type { get; set; }
        public string poi_id { get; set; }
        public string town { get; set; }
        public string town_code { get; set; }
        public int town_level { get; set; }
        public string key_role { get; set; }
        public string short_address { get; set; }
        public string analysis_address { get; set; }
        public string format_address { get; set; }
        public string person_name { get; set; }
        public string tel { get; set; }
    }

}
