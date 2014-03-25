using ConCrawler.Helpers;
using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;
using NCrawler.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConCrawler {
    class Program {

        //static string strIncReg = @"city\?cityCode=|destination";
        //public static IFilter[] IncludeFilter = new[]
        //    {
        //        (RegexFilter)new Regex(strIncReg,
        //            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
        //    };


        public static IFilter[] ExcludeFilter = new[]
            {                
                (RegexFilter)new Regex(@"(\.jpg|\.css|\.js|\.gif|\.jpeg|\.png|\.ico)",
                    RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
            };

        static void Main(string[] args) {
            // Remove limits from Service Point Manager
            ServicePointManager.MaxServicePoints = 999999;
            ServicePointManager.DefaultConnectionLimit = 999999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.EnableDnsRoundRobin = true;

            int maxCrawlDepth = int.Parse(ConfigurationManager.AppSettings["maxCrawlDepth"]);
            int maxThreadCount = int.Parse(ConfigurationManager.AppSettings["maxThreadCount"]);

            Console.WriteLine("爬虫开始行动");

            NCrawlerHelper crawler = new NCrawlerHelper();
            string url = "http://diaosbook.com";
            crawler.Crawl(url, maxThreadCount, maxCrawlDepth, null, ExcludeFilter);

            Console.WriteLine("解析完成");
            Console.ReadKey();
        }
    }
}
