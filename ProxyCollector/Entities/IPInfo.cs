using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCollector.Entities {
    public class IPInfo {
        public string Address { get; set; }
        public int Port { get; set; }
        public AnonymousRank AnonymousRank { get; set; }
        public string Region { get; set; }
    }

    public enum AnonymousRank {
        Transparent,
        Anonymous,
        High
    }
}
