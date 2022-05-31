using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LD.CORE.Model;

namespace LD.CORE.Utils
{

    /// <summary>
    /// 地址信息查询
    /// </summary>
    public static class LRDataHelpers
    {




        public static DateTime serVerTime;



        private static HttpClientHandler clientHandler;
        private static HttpClient httpClient;

        public static Exception exception;

        public static string API_KEY = "TU5BZ-MKD3W-L43RW-O3ZBW-GWMZK-QBB25";




        static LRDataHelpers()
        {




            clientHandler = new HttpClientHandler();
#if DEBUG
            clientHandler.Proxy = new WebProxy("127.0.0.1:8888");
#endif
            httpClient = new HttpClient(clientHandler);
            clientHandler.UseProxy = false;

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.64 Safari/537.36 Edg/101.0.1210.53");
            httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            httpClient.DefaultRequestHeaders.AcceptLanguage.TryParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");

        }



        /// <summary>
        /// 查询经纬度信息
        /// </summary>
        /// <param name="addressname">地址信息</param>
        /// <returns></returns>
        public static async Task<LocationInfo> GeocLocalInfoAsync(string addressname)
        {
            try
            {

                var req = new HttpRequestMessage(HttpMethod.Get, $"http://apis.map.qq.com/jsapi?qt=geoc&addr={HttpUtility.UrlEncode(addressname)}&output=jsonp&key={API_KEY}&pf=jsapi&ref=jsapi");

                var resp = await httpClient.SendAsync(req);
                if (resp.IsSuccessStatusCode)
                {
                    serVerTime = resp.Headers.Date.Value.UtcDateTime;

                    var location = await resp.Content.ReadAsStreamAsync();
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (var sr = new StreamReader(location, Encoding.GetEncoding("GB18030")))
                    {
                        return JsonConvert.DeserializeObject<LocationInfo>(sr.ReadToEnd());
                    }

                }
                return null;
            }
            catch (Exception ex)
            {

                exception = ex;
                return null;
            }


        }

        /// <summary>
        /// 经纬度信息查询
        /// </summary>
        /// <param name="lng">经度 108</param>
        /// <param name="lat">维度 33</param>
        /// <returns></returns>
        public static async Task<RLocation> RgeocAsync(decimal lng, decimal lat)
        {
            //https://apis.map.qq.com/jsapi?qt=rgeoc&lnglat=108,33&key=TU5BZ-MKD3W-L43RW-O3ZBW-GWMZK-QBB25&output=jsonp&pf=jsapi&ref=jsapi&cb=qq.maps._svcb3.geocoder0

            try
            {
                var resp = await httpClient.GetAsync($"https://apis.map.qq.com/jsapi?qt=rgeoc&lnglat=${lng},{lat}&key={API_KEY}&output=jsonp&pf=jsapi&ref=jsapi");

                return JsonConvert.DeserializeObject<RLocation>(await resp.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {

                exception = ex;
                return null;
            }
        }
    }
}