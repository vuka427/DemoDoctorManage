using DoctorManage.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorManage.Models.DoctorManage
{
    public class DoctorViewModel
    {
        public int DOCTORID { get; set; }
        public string DOCTORNAME { get; set; }
        public string DOCTORGENDER { get; set; }
        public string DOCTORDATEOFBIRTH { get; set; }
        public string DOCTORMOBILENO { get; set; }
        public string DOCTORADDRESS { get; set; }
        public string DEPARTMENT { get; set; }
        public string WORKINGSTARTDATE { get; set; }
        public string WORKINGENDDATE { get; set; }
        public string CREATEBY { get; set; }
        public string CREATEDATE { get; set; }
        public string UPDATEBY { get; set; }
        public string UPDATEDATE { get; set; }
        


    }
}