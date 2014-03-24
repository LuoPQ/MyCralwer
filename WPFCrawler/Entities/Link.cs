using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCrawler.Entities
{
    public class Link
    {
        public string Id { get; set; }
        public bool IsSelected { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
