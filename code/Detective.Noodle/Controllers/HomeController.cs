using Detective.Api;
using Detective.DataModel;
using Detective.Noodle.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Detective.Noodle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Search search = new Search(); 
            return View(search);
        }

        public ActionResult Action(Search search)
        {
            QueryResult sDNItem = new QueryResult();
            if (search.KeyValue != null)
            {
                string source = "http://blackcheck.net/Detective.Api/search/" + search.KeyValue;
                string data = GetResult(source);

                search.QueryResult = JsonConvert.DeserializeObject<QueryResult>(data);
            }

            return View(search);
        }

        private string GetResult(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
                return data;
            }
            return string.Empty;
        }
    }
}