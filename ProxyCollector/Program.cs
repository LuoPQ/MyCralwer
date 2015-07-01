using ProxyCollector.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCollector
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("开始抓取IP");
            var list = ProxyHtmlHelper.GetIpList("http://pachong.org/");
            XMLHelper.XmlSerializeToFile(list, Environment.CurrentDirectory + "/Data/", "citydetails" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml", Encoding.UTF8);
            Console.WriteLine("抓取完成");
            Console.ReadKey();
        }
    }
}
