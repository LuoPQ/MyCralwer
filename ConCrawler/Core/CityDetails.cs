using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConCrawler.Core {
    public class CityDetails {
        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 城市链接
        /// </summary>
        public string Url { get; set; }

        public List<CityItem> CityItems { get; set; }

        public CityDetails() {
            CityItems = new List<CityItem>();
        }
    }
}
