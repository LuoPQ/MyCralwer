using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;
using NCrawler.Extensions;

namespace WPFCrawler.Helpers
{
    class NCrawlerHelper
    {
        public Crawler Crawler { get; set; }

        public NCrawlerHelper(string url, int maxThreadCount, int maxCrawlDepth) {
            //Crawler=new Crawler(new Uri(url))
        }

        public void Crawl() {
            using (Crawler) {

            }
        }
    }

    internal class UrlCollectorStep : IPipelineStep
    {
        public void Process(Crawler crawler, PropertyBag propertyBag)
        {
            lock (this)
            {
                

            }
        }
    }
}
