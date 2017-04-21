using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCenter
{
    public class PHRASES
    {
        public int SID { get; set; }
        public string PHR_TYPE { get; set; }
        public string PHR_KEY { get; set; }
        public string PHR_VALUE { get; set; }
        public string PHR_STATUS { get; set; }
        public int PHR_ORDER { get; set; }
        public string PHR_DESC { get; set; }
        public DateTime CDATE { get; set; }
        public string CUSER { get; set; }
        public DateTime MDATE { get; set; }
        public string MUSER { get; set; }

    }
}
