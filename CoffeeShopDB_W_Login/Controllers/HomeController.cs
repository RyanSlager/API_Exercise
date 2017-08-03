using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShopDB_W_Login.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetData()
        {
            System.Net.HttpWebRequest request = System.Net.WebRequest.CreateHttp("http://www.reddit.com/r/Coffee/top/.json?count=20");
            request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string ApiText = rd.ReadToEnd();
            JObject o = JObject.Parse(ApiText);
            ViewBag.Object = o;

            for(int i = 0; i < o["data"]["children"].Count(); i++)
            {
                ViewBag.Posts += "<a href='" + o["data"]["children"][i]["data"]["url"] + "'>" + o["data"]["children"][i]["data"]["title"] + "</a><br><br>" + "posted by: " + o["data"]["children"][i]["data"]["author"] + "<br><br>" + o["data"]["children"][i]["data"]["selftext"] + "<br><br><br>";
                
            }
            return View("Data");
        }

        public ActionResult Data()
        {
            return GetData();
        }
    }
}