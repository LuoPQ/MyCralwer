using HtmlAgilityPack;
using ProxyCollector.Entities;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;

namespace ProxyCollector.Helpers {
    public static class ProxyHtmlHelper {
        private static List<IPInfo> GetIpList(string url) {
            List<IPInfo> ipInfoList = new List<IPInfo>();
            var html = new ScrapingBrowser().DownloadString(new Uri(url));
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var htmlNode = htmlDocument.DocumentNode;
            //htmlNode.CssSelect()
            return null;
        }
    }
}
