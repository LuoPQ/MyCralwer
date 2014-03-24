using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCrawler {
    class Sample {

        public string UrlId { get; set; }
        public string PageTitle { get; set; }
        public string Url { get; set; }

        public Sample(string urlId, string name, string url) {
            UrlId = urlId;
            PageTitle = name;
            Url = url;            
        }
        
        public static Sample[] GetSamples() {
            return new Sample[]
            {
                new Sample("1","博客园", "http://www.cnblogs.com"),
                new Sample("2","CSDN", "http://www.csdn.net"),
                new Sample("3","谷歌", "http://www.google.com")
            };
        }
    }
}
