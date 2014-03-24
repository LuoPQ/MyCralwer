using HtmlAgilityPack;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;

namespace HtmlParserDemo {
    class Program {
        static void Main(string[] args) {

            var uri1 = new Uri("http://www.kopu.cn/airport/info/hkg.html");
            var uri2 = new Uri("http://diaosbook.com");

            var browser = new ScrapingBrowser();
            var html = browser.DownloadString(uri1);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var htmlNode = htmlDocument.DocumentNode;

            Console.WriteLine(htmlNode.CssSelect(".main-title").FirstOrDefault().InnerText);

            var items = htmlNode.CssSelect("div.row");
            foreach (var item in items) {
                Console.WriteLine(item.CssSelect("h5").FirstOrDefault().InnerText + ":" + item.CssSelect("p").FirstOrDefault().InnerText);
            }

            Console.ReadKey();
        }
    }
}
