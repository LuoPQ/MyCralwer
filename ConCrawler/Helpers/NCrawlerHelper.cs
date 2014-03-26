using ConCrawler.Core;
using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConCrawler.Helpers {
    public class NCrawlerHelper {
        public void Crawl(string url, int maxThreadCount, int maxCrawlDepth, IFilter[] IncludeFilter, IFilter[] ExcludeFilter) {

            NCrawlerModule.Setup();
            using (Crawler crawler = new Crawler(new Uri(url),
                 new HtmlDocumentProcessor(),
                 new UrlCollectorStep()) {
                     MaximumThreadCount = maxThreadCount,
                     MaximumCrawlDepth = maxThreadCount,
                     IncludeFilter = IncludeFilter,
                     ExcludeFilter = ExcludeFilter,
                     MaximumCrawlCount = 100,
                     UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)"
                     
                 }) {
                crawler.Crawl();
            }
        }
    }
}
