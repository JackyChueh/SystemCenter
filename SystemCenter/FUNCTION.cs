using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCenter
{
    public class FUNCTION
    {
        public int SID { get; set; }
        public string FUN_ID { get; set; }
        public string FUN_NAME { get; set; }
        public string FUN_DESC { get; set; }
        public string FUN_STATUS { get; set; }
        public string FUN_TYPE { get; set; }
        public string FUN_PATH { get; set; }
        public string FUN_FILE { get; set; }
        public string PARENT_ID { get; set; }
        public Nullable<int> SHOW_INDEX { get; set; }
        public DateTime CDATE { get; set; }
        public string CUSER { get; set; }
        public DateTime MDATE { get; set; }
        public string MUSER { get; set; }
        public virtual ICollection<FUNCTION> ChildMenuItems { get; set; }
        public virtual string USR_ID { get; set; }
    }

    
}
