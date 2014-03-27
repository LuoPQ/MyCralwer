using HtmlAgilityPack;
using ProxyCollector.Entities;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;

namespace ProxyCollector.Helpers
{
    public static class ProxyHtmlHelper
    {
        public static List<IPInfo> GetIpList(string url) {
            var browser = new ScrapingBrowser() {
                Encoding = Encoding.UTF8
            };
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(browser.DownloadString(new Uri(url)));
            var htmlNode = htmlDocument.DocumentNode;
            var ipInfoList = (from row in htmlNode.CssSelect("tbody > tr")
                              let tds = row.CssSelect("td")
                              select new IPInfo {
                                  Address = tds.ElementAt(1).InnerText,
                                  Port = int.Parse(tds.ElementAt(2).InnerText),
                                  Region = tds.ElementAt(3).CssSelect("a").First().InnerText,
                                  AnonymousRank = (AnonymousRank)Enum.Parse(typeof(AnonymousRank), tds.ElementAt(4).InnerText, true)
                              }).ToList();
            return ipInfoList;
        }
    }
}
