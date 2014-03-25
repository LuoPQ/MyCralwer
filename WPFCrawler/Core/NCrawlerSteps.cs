using NCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCrawler.Entities;

namespace WPFCrawler.Core {
    public class UrlCollectorStep : IPipelineStep {

        public void Process(NCrawler.Crawler crawler, NCrawler.PropertyBag propertyBag) {
            lock (this) {
                Global.LinkList.Add(new Link() {
                    Id = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Title = propertyBag.Title,
                    Url = propertyBag.Step.Uri.AbsoluteUri
                });
            }
        }
    }
}
