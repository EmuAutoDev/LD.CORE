using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.CORE.Model
{
    /// <summary>
    /// 查询解析经纬度
    /// </summary>
    public class RLocation
    {


        public InfoR info { get; set; }
        public DetailR detail { get; set; }
    }

    public class InfoR
    {
        public int type { get; set; }
        public int error { get; set; }
        public int time { get; set; }
        public string message { get; set; }
    }

    public class DetailR
    {
        public int poi_count { get; set; }
        public Poilist[] poilist { get; set; }
        public string request_id { get; set; }
        public Result[] results { get; set; }
    }

    public class Poilist
    {
        public string addr { get; set; }
        public Addr_Info addr_info { get; set; }
        public Base_Map_Info base_map_info { get; set; }
        public string catacode { get; set; }
        public string catalog { get; set; }
        public string direction { get; set; }
        public string dist { get; set; }
        public string dtype { get; set; }
        public string final_score { get; set; }
        public string name { get; set; }
        public string pointx { get; set; }
        public string pointy { get; set; }
        public object[] tags { get; set; }
        public string uid { get; set; }
        public string weight { get; set; }
    }

    public class Addr_Info
    {
        public string adcode { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public string p { get; set; }
        public string short_addr { get; set; }
    }

    public class Base_Map_Info
    {
        public string base_map_flag { get; set; }
    }

    public class Result
    {
        public string adcode { get; set; }
        public string c { get; set; }
        public string c_cht { get; set; }
        public string c_en { get; set; }
        public string city_code { get; set; }
        public string d { get; set; }
        public string d_cht { get; set; }
        public string d_en { get; set; }
        public string dtype { get; set; }
        public string n { get; set; }
        public string n_cht { get; set; }
        public string n_en { get; set; }
        public string name { get; set; }
        public string nation_code { get; set; }
        public string p { get; set; }
        public string p_cht { get; set; }
        public string p_en { get; set; }
        public string phone_area_code { get; set; }
        public string pointx { get; set; }
        public string pointy { get; set; }
        public int stat { get; set; }
        public string address_children_scene { get; set; }
        public string address_name { get; set; }
        public string address_scene { get; set; }
        public string rough_address_name { get; set; }
        public Second_Landmark second_landmark { get; set; }
        public string desc_weight { get; set; }
        public string direction { get; set; }
        public string dist { get; set; }
        public string uid { get; set; }
        public string standard_address { get; set; }
        public string standard_address_cht { get; set; }
        public string standard_address_en { get; set; }
    }

    public class Second_Landmark
    {
        public string addr { get; set; }
        public object address { get; set; }
        public string address_cht { get; set; }
        public string address_en { get; set; }
        public string catacode { get; set; }
        public string direction { get; set; }
        public string dist { get; set; }
        public string dtype { get; set; }
        public string landmark_level { get; set; }
        public string name { get; set; }
        public string pointx { get; set; }
        public string pointy { get; set; }
        public string uid { get; set; }
    }

}
