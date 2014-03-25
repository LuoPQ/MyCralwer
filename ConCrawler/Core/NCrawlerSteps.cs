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

namespace ConCrawler.Core
{
    internal class UrlCollectorStep : IPipelineStep
    {
        public string[] extFilter = ConfigurationManager.AppSettings["extFilter"].Split('|');
        public string[] keywords = ConfigurationManager.AppSettings["keywords"].Split('|');
        public void Process(NCrawler.Crawler crawler, NCrawler.PropertyBag propertyBag) {
            lock (this) {
                //if (propertyBag.Step.Uri.AbsoluteUri.Contains("destination")) {
                var url = propertyBag.Step.Uri.AbsoluteUri;
                if (keywords.Count(k => url.Contains(k)) <= 0) {
                    return;
                }

                if (extFilter.Count(e => url.Contains(e)) <= 0) {
                    Console.Out.WriteLine(ConsoleColor.Gray, "Url: {0},Status:{1}", propertyBag.Step.Uri.AbsoluteUri, propertyBag.StatusCode);

                    Global.LinkList.Add(new Link() {
                        Id = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        Title = propertyBag.Title,
                        Url = propertyBag.Step.Uri.AbsoluteUri
                    });
                }
                //}
            }
        }
    }
}
