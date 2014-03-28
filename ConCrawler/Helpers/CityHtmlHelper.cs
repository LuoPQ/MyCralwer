using HtmlAgilityPack;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;
using System.Configuration;
using System.Net;
using ConCrawler.Entities;

namespace ConCrawler.Helpers {
    public class CityHtmlHelper {
        static readonly string[] cityItems = ConfigurationManager.AppSettings["cityItems"].Split('|');
        /// <summary>
        /// 获取城市详情
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static CityDetails GetCityDetails(string url) {
            CityDetails cityDetails = new CityDetails();
            cityDetails.CityCode = url.Substring(url.IndexOf("=") + 1);
            cityDetails.Url = url;
            string cityName = "";
            foreach (var item in cityItems) {
                cityDetails.CityItems.Add(GetDataFromHtml(url, item, out cityName));
                if (!string.IsNullOrWhiteSpace(cityName)) {
                    cityDetails.CityName = cityName;
                }
            }
            return cityDetails;
        }

        /// <summary>
        /// 获取城市某一项信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="item"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        private static InfoItem GetDataFromHtml(string url, string item, out string cityName) {
            var itemUrl = url + "&column=" + item;
            var browser = new ScrapingBrowser() {
                Proxy = new WebProxy("182.118.31.110", 80)
            };
            var html = browser.DownloadString(new Uri(itemUrl));

            if (html.Contains("非常抱歉")) {
                cityName = "";
                return new InfoItem() {
                    Key = item,
                    Name = "",
                    Content = "",

                };
            }
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var htmlNode = htmlDocument.DocumentNode;
            cityName = htmlNode.CssSelect("div.intronav").FirstOrDefault().CssSelect("h2").FirstOrDefault().InnerText;
            var node = htmlNode.CssSelect("div.introright").FirstOrDefault();
            return new InfoItem() {
                Key = item,
                Name = node.CssSelect("p.introrighttit").FirstOrDefault().InnerText,
                Content = node.InnerText
            };
        }
    }
}
