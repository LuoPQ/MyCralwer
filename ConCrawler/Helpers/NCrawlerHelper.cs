﻿using ConCrawler.Core;
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
                     UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0",
                     MaxThreadSleepTime = 5000,
                     //UseCookies = true
                 }) {
                try {
                    crawler.Crawl();
                }
                catch (Exception error) {
                    Console.WriteLine(error.Message);
                }
            }
        }
    }
}
