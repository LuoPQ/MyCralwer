using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConCrawler.Entities {
    /// <summary>
    /// 信息类别
    /// </summary>
    public class InfoCategory {
        public string Key { get; set; }
        public string Name { get; set; }
        public List<InfoItem> InfoItems = new List<InfoItem>();
    }
}
