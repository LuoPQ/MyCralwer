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
using WPFCrawler.Core;

namespace WPFCrawler.Helpers {
    public class NCrawlerHelper {
        public void Crawl(string url, int maxThreadCount, int maxCrawlDepth, IFilter[] IncludeFilter, IFilter[] ExcludeFilter) {
            using (Crawler c = new Crawler(new Uri(url),
                 new HtmlDocumentProcessor(),
                 new UrlCollectorStep()) {
                     MaximumThreadCount = maxThreadCount,
                     MaximumCrawlDepth = maxThreadCount,
                     IncludeFilter = IncludeFilter,
                     ExcludeFilter = ExcludeFilter
                 }) {
                c.Crawl();
            }
        }
    }
}
