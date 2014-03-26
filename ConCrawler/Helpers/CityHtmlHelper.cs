using ConCrawler.Core;
using HtmlAgilityPack;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;
using System.Configuration;

namespace ConCrawler.Helpers {
    public class CityHtmlHelper {
        static readonly string[] CityItems = ConfigurationManager.AppSettings["cityItems"].Split('|');

        public static CityDetails GetCityDetails(string url) {
            CityDetails cityDetails = new CityDetails();
            cityDetails.CityCode = url.Substring(url.IndexOf("=") + 1);
            string cityName = "";
            foreach (var item in CityItems) {
                cityDetails.CityItems.Add(GetDataFromHtml(url, item, out cityName));
                if (!string.IsNullOrWhiteSpace(cityName)) {
                    cityDetails.CityName = cityName;
                }
            }
            return cityDetails;
        }

        private static CityItem GetDataFromHtml(string url, string item, out string cityName) {
            var itemUrl = url + "&column=" + item;
            var html = new ScrapingBrowser().DownloadString(new Uri(itemUrl));

            if (html.Contains("非常抱歉")) {
                cityName = "";
                return new CityItem() {
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
            return new CityItem() {
                Key = item,
                Name = node.CssSelect("p.introrighttit").FirstOrDefault().InnerText,
                Content = node.InnerText
            };

        }
    }
}
