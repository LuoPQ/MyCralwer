using ConCrawler.Entities;
using NCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConCrawler.Extensions;

namespace ConCrawler.Core {
    internal class UrlCollectorStep : IPipelineStep {
        public void Process(NCrawler.Crawler crawler, NCrawler.PropertyBag propertyBag) {
            lock (this) {
                Console.Out.WriteLine(ConsoleColor.Gray, "Url: {0}", propertyBag.Step.Uri.AbsoluteUri);

                //Global.LinkList.Add(new Link() {
                //    Id = DateTime.Now.ToString("yyyyMMddHHmmss"),
                //    Title = propertyBag.Title,
                //    Url = propertyBag.Step.Uri.AbsoluteUri
                //});
            }
        }
    }
}
