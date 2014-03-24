using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCrawler.Core
{
    public class UrlFilter
    {
        public string ExcludeRegex { get; set; }

        public string IncludeRegex { get; set; }
    }
}
