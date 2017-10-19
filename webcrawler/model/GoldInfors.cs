using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webcrawler.model
{
    public class GoldInfors
    {
        public string SessionName { get; set; }
        public DateTime SessionDate { get; set; }
        public string GoldDistributorName { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
    }
}
