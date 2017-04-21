using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SystemCenter
{
    public class AUTHORITY
    {
        
        public int SID { get; set; }
        public string USR_ID { get; set; }
        public string FUN_ID { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string GRP_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyyMMdd}")]
        public DateTime CDATE { get; set; }
        public string CUSER { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyyMMdd}")]
        public DateTime MDATE { get; set; }
        public string MUSER { get; set; }
    }
}
