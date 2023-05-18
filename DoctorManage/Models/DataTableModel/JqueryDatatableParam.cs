using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorManage.Models.DataTableModel
{
    public class JqueryDatatableParam
    {
        public string sEcho { get; set; } // số thứ tư yêu cầu
        public string sSearch { get; set; } // chuổi tìm kiếm
        public int iDisplayLength { get; set; } // số phần tử mổi page
        public int iDisplayStart { get; set; } // vị trí bắt đầu
        public int iColumns { get; set; } // số cột
        public int iSortCol_0 { get; set; } // cột cần sấp xếp
        public string sSortDir_0 { get; set; } // kiểu sấp sếp
        public int iSortingCols { get; set; } // số cột dùng để sấp xếp
        public string sColumns { get; set; } // danh sách các tên cột
    }
}