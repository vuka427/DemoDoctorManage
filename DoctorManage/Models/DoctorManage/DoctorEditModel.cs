using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorManage.Models.DoctorManage
{
    public class DoctorEditModel
    {
        public int DOCTORID { get; set; }
        public int DEPARTMENTID { get; set; }
        public string DOCTORNAME { get; set; }
        public bool DOCTORGENDER { get; set; }
        public string DOCTORDATEOFBIRTH { get; set; }
        public string DOCTORMOBILENO { get; set; }
        public string DOCTORADDRESS { get; set; }
        public string WORKINGSTARTDATE { get; set; }
        public string WORKINGENDDATE { get; set; }
        
    }
}