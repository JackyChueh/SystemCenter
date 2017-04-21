using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SystemCenter
{
    public class USERS
    {
        [Display(Name = "編號")]
        public int SID { get; set; }
        [Display(Name = "使用者帳號")]
        public string USR_ID { get; set; }
        [Display(Name = "使用者名稱")]
        public string USR_NAME { get; set; }
        [Display(Name = "登入密碼")]
        public string PWD { get; set; }
        [Display(Name = "郵件")]
        public string EMAIL { get; set; }
        [Display(Name = "啟用")]
        public string USR_STATUS { get; set; }
        [Display(Name = "備註")]
        public string MEMO { get; set; }
        [Display(Name = "建檔時間")]
        [DisplayFormat(DataFormatString = "{0:yyyyMMdd}")]
        public DateTime CDATE { get; set; }
        [Display(Name = "建檔者")]
        public string CUSER { get; set; }
        [Display(Name = "異動時間")]
        [DisplayFormat(DataFormatString = "{0:yyyyMMdd}")]
        public DateTime MDATE { get; set; }
        [Display(Name = "異動者")]
        public string MUSER { get; set; }

    }
}
