using ConCrawler.Entities;
using NCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConCrawler.Extensions;
using System.Net;
using System.Configuration;
using ConCrawler.Helpers;

namespace ConCrawler.Core {
    internal class UrlCollectorStep : IPipelineStep {
        public string[] excludeFilter = ConfigurationManager.AppSettings["excludeFilter"].Split('|');
        public string[] includeFilter = ConfigurationManager.AppSettings["includeFilter"].Split('|');
        static int count = 0;
        public void Process(NCrawler.Crawler crawler, NCrawler.PropertyBag propertyBag) {
            lock (this) {
                var url = propertyBag.Step.Uri.AbsoluteUri;
                if (includeFilter.Count(k => url.Contains(k)) <= 0) {
                    return;
                }
                if (Global.LinkList.Count(l => l.Url == url) > 0) {
                    return;
                }
                if (excludeFilter.Count(e => url.Contains(e)) <= 0) {
                    if (!url.Contains("cityCode")) {
                        return;
                    }
                    Global.LinkList.Add(new Link() {
                        Id = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        Title = propertyBag.Title,
                        Url = url
                    });
                    Global.CityList.Add(CityHtmlHelper.GetCityDetails(url));
                    Console.Out.WriteLine(ConsoleColor.Gray, "Url: {0},Status:{1},解析完成。", url, propertyBag.StatusCode);
                }
            }
        }
    }
}
