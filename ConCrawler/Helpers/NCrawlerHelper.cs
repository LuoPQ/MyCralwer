using ConCrawler.Core;
using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConCrawler.Helpers {
    public class NCrawlerHelper {
        public void Crawl(string url, int maxThreadCount, int maxCrawlDepth, IFilter[] IncludeFilter, IFilter[] ExcludeFilter) {

            NCrawlerModule.Setup();
            using (Crawler crawler = new Crawler(new Uri(url), new HtmlDocumentProcessor(), new UrlCollectorStep())) {
                try {
                    crawler.MaximumThreadCount = maxThreadCount;
                    crawler.MaximumCrawlDepth = maxCrawlDepth;
                    crawler.IncludeFilter = IncludeFilter;
                    crawler.ExcludeFilter = ExcludeFilter;
                    crawler.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0";
                    crawler.Headers.Set("Accept-Language", "en-US,en;q=0.8,zh-Hans-CN;q=0.5,zh-Hans;q=0.3");
                    crawler.Headers.Set("Cache-Control", "max-age=0");
                    //MaxThreadSleepTime = 5000,
                    crawler.WebProxy = new WebProxy("182.118.31.110", 80);
                    crawler.Crawl();
                }
                catch (Exception error) {
                    Console.WriteLine(error.Message);
                }
            }
        }
    }
}
