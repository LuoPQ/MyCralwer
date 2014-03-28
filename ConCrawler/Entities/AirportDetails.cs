using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConCrawler.Entities {
    public class AirportDetails {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Url { get; set; }
        public AirportSimplyInfo Info { get; set; }
        public AirportComplexInfo Traffic { get; set; }
        public AirportComplexInfo Service { get; set; }
        public AirportSimplyInfo Contact { get; set; }
        public AirportSimplyInfo Taboo { get; set; }

        public AirportDetails() {
            Info = new AirportSimplyInfo();
            Traffic = new AirportComplexInfo();
            Service = new AirportComplexInfo();
            Contact = new AirportSimplyInfo();
            Taboo = new AirportSimplyInfo();
        }
    }
}
