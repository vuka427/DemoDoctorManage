//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorManage.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class DOCTORMODEL
    {
        public int DOCTORID { get; set; }
        public int DEPARTMENTID { get; set; }
        public string DOCTORNAME { get; set; }
        public bool DOCTORGENDER { get; set; }
        public System.DateTime DOCTORDATEOFBIRTH { get; set; }
        public string DOCTORMOBILENO { get; set; }
        public string DOCTORADDRESS { get; set; }
        public System.DateTime WORKINGSTARTDATE { get; set; }
        public System.DateTime WORKINGENDDATE { get; set; }
        public string CREATEBY { get; set; }
        public System.DateTime CREATEDATE { get; set; }
        public string UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public bool DELETEFLAG { get; set; }
    
        public virtual DEPARTMENT DEPARTMENT { get; set; }
    }
}
