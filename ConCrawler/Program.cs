using ConCrawler.Core;
using ConCrawler.Entities;
using ConCrawler.Helpers;
using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;
using NCrawler.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConCrawler {
    class Program {
        static string strIncReg = @"city\?cityCode=|destination";
        //string str = @"(\.jpg|\.css|\.js|\.gif|\.jpeg|\.png|\.ico|page)";
        static IFilter[] IncludeFilter = new[]
            {
                (RegexFilter)new Regex(strIncReg,
                    RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
            };
        static IFilter[] ExcludeFilter = ExcludeFilter = new[]
            {                
                (RegexFilter)new Regex(@"(\.jpg|\.css|\.js|\.gif|\.jpeg|\.png|\.ico|column|page|kpflight|user|evalsys|krmblog|qq|entryexit|assistant)",
                    RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
            };
        static int maxCrawlDepth = int.Parse(ConfigurationManager.AppSettings["maxCrawlDepth"]);
        static int maxThreadCount = int.Parse(ConfigurationManager.AppSettings["maxThreadCount"]);
        static string url = ConfigurationManager.AppSettings["url"];
        static string path = Environment.CurrentDirectory + "/Data/";

        static void Main(string[] args) {
            ReadyToCrawl();
            Console.WriteLine("爬虫开始行动....");

            NCrawlerHelper crawler = new NCrawlerHelper();
            crawler.Crawl(url, maxThreadCount, maxCrawlDepth, null, ExcludeFilter);

            XMLHelper.XmlSerializeToFile(Global.LinkList, path, "dest.xml", Encoding.UTF8);
            XMLHelper.XmlSerializeToFile(Global.CityList, path, "citydetails" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml", Encoding.UTF8);

            Console.WriteLine("解析完成。");
            Console.ReadKey();
        }

        /// <summary>
        /// 为爬虫做准备
        /// </summary>
        private static void ReadyToCrawl() {
            RemoveLimits();
            string filePath = path + "/dest.xml";
            if (File.Exists(filePath)) {
                Global.LinkList = XMLHelper.XmlDeserializeFromFile<List<Link>>(filePath, Encoding.UTF8);
            }
        }

        /// <summary>
        /// 移除限制 
        /// </summary>
        private static void RemoveLimits() {
            // Remove limits from Service Point Manager
            ServicePointManager.MaxServicePoints = 999999;
            ServicePointManager.DefaultConnectionLimit = 999999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.EnableDnsRoundRobin = true;
        }
    }
}
