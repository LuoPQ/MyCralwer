using ConCrawler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConCrawler.Core {
    /// <summary>
    /// 全局变量
    /// </summary>
    public static class Global {
        /// <summary>
        /// 用来保存爬到的链接
        /// </summary>
        public static List<Link> LinkList = new List<Link>();
    }
}
