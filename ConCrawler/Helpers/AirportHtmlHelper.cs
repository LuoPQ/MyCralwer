using ConCrawler.Entities;
using HtmlAgilityPack;
using ScrapySharp.Network;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ConCrawler.Helpers {
    public class AirportHtmlHelper {
        static ScrapingBrowser browser = new ScrapingBrowser() {
            Encoding = Encoding.UTF8
        };
        static HtmlDocument htmlDocument = new HtmlDocument();

        /// <summary>
        /// 获取机场资讯
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AirportDetails GetAirportDetails(string url) {
            string urlTemp = url.Replace("intro/view", "{0}");
            htmlDocument.LoadHtml(browser.DownloadString(new Uri(url)));
            var htmlNode = htmlDocument.DocumentNode;
            string title = htmlNode.CssSelect("h2.main-title").First().InnerText;
            int leftBracketIndex = title.IndexOf("(");
            int rightBracketIndex = title.IndexOf(")");
            int length = title.Length;

            return new AirportDetails() {
                Code = title.Substring(leftBracketIndex + 1, length - rightBracketIndex - 1),
                Name = title.Substring(0, leftBracketIndex),
                Region = title.Substring(rightBracketIndex + 2),
                Url = url,
                Info = GetAirportInfo(string.Format(urlTemp, AirportItems.info.ToString())),
                Traffic = GetAirportComplexInfo(string.Format(urlTemp, AirportItems.traffic.ToString())),
                Service = GetAirportComplexInfo(string.Format(urlTemp, AirportItems.service.ToString())),
                Contact = GetAirportSimplyInfo(string.Format(urlTemp, AirportItems.contact.ToString())),
                Taboo = GetAirportSimplyInfo(string.Format(urlTemp, AirportItems.taboo))
            };
        }


        /// <summary>
        /// 获取机场链接
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<Link> GetAirportLinks(string url) {
            htmlDocument.LoadHtml(browser.DownloadString(new Uri(url)));
            var htmlNode = htmlDocument.DocumentNode;
            var host = browser.Referer.Host;
            var linkList = (from link in htmlNode.CssSelect("#airport_cont a")
                            where link.InnerText.Contains("机场")
                            select new Link {
                                Id = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                Title = link.InnerText,
                                Url = host + link.Attributes["href"].Value
                            }).ToList();
            return linkList;
        }

        /// <summary>
        /// 获取机场基本信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static AirportSimplyInfo GetAirportInfo(string url) {
            htmlDocument.LoadHtml(browser.DownloadString(new Uri(url)));
            var htmlNode = htmlDocument.DocumentNode;
            var regStr = @"row\s?([a-z]+)-ico";
            return new AirportSimplyInfo() {
                InfoItems = (from item in htmlNode.CssSelect(".infor-cont div")
                             select new InfoItem() {
                                 Key = GetItemKeyFromGroup(item.Attributes["class"].Value, regStr),
                                 Name = item.CssSelect("h5").First().InnerText,
                                 Content = item.CssSelect("p").First().InnerText
                             }).ToList()
            };
        }

        /// <summary>
        /// 获取机场复杂项信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static AirportComplexInfo GetAirportComplexInfo(string url) {
            htmlDocument.LoadHtml(browser.DownloadString(new Uri(url)));
            var htmlNode = htmlDocument.DocumentNode;
            var regStr = url.Contains("traffic") ? @"tra-ico tra-(\w+)" : @"service-ico (\w+)-ico";
            var classStr = url.Contains("traffic") ? @"div.tra-ico" : @"div.service-ico";
            return new AirportComplexInfo() {
                Categories = (from category in htmlNode.CssSelect("div.common-cont")
                              let cateName = GetItemKeyFromGroup(category.CssSelect(classStr).First().Attributes["class"].Value, regStr)
                              select new InfoCategory() {
                                  Key = cateName,
                                  Name = cateName,
                                  InfoItems = (from item in category.CssSelect("div.row")
                                               select new InfoItem() {
                                                   Name = item.CssSelect("h5").First().InnerText,
                                                   Content = item.CssSelect("div").First().InnerText
                                               }).ToList()
                              }).ToList()
            };
        }

        /// <summary>
        /// 获取机场简单项信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static AirportSimplyInfo GetAirportSimplyInfo(string url) {
            htmlDocument.LoadHtml(browser.DownloadString(new Uri(url)));
            var htmlNode = htmlDocument.DocumentNode;
            var regStr = @"common-cont (\w+)-cont";
            return new AirportSimplyInfo() {
                InfoItems = (from item in htmlNode.CssSelect("div.common-cont")
                             let name = GetItemKeyFromGroup(item.Attributes["class"].Value, regStr)
                             select new InfoItem() {
                                 Key = name,
                                 Name = name,
                                 Content = item.InnerText
                             }).ToList()
            };
        }

        /// <summary>
        /// 获取信息项Key
        /// </summary>
        /// <param name="str"></param>
        private static string GetItemKeyFromGroup(string str, string pattern) {
            Regex regex = new Regex(pattern);
            return regex.Match(str).Groups[1].Value;
        }
    }

    /// <summary>
    /// 机场信息项
    /// </summary>
    public enum AirportItems {
        info,
        traffic,
        service,
        contact,
        taboo
    }
}
