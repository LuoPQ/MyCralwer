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

        /// <summary>
        /// 从xml文件中读取的链接
        /// </summary>
        //public static List<Link> OldLink = new List<Link>();

        /// <summary>
        /// 城市信息列表
        /// </summary>
        public static List<CityDetails> CityList = new List<CityDetails>();

        /// <summary>
        /// 保存机场链接
        /// </summary>
        public static List<Link> AirportLinks = new List<Link>();
    }
}
