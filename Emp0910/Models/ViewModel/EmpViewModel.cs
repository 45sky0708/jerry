using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Emp0910.Models
{
    //[Serializable]

    //資料庫資料
    public class EmpViewModel
    {
        public int EmpNumber {get; set;}
        [Required(ErrorMessage = "EmpSex is required")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "EmpSex is required")]
        public string EmpSex { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> EmpBirthday { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> CreationDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> LastloginDate { get; set; }
        public string Seniority { get; set; }
        public string UnitNumber { get; set; }
        public string UnitName { get; set; }
        public string JobTitle { get; set; }
        //datatime to string
        public string EmpBirthdayDataFormat //EmpBirthday to String("yyyy/MM/dd") 
        {
            get
            {
                string formatafter = Convert.ToDateTime(this.EmpBirthday).ToString("yyyy/MM/dd");
                return formatafter;
            }
        }

        public string CreationDateDataFormat
        {
            get
            {
                string formatafter = Convert.ToDateTime(this.CreationDate).ToString("yyyy/MM/dd");
                return formatafter;
            }
        }
        public string LastloginDateDataFormat
        {
            get
            {
                string formatafter = Convert.ToDateTime(this.LastloginDate).ToString("yyyy/MM/dd");
                return formatafter;
            }
        }

    }

    //表單檢核
    public class FormViewModel
    {
        public List<EmpViewModel> empdata;
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }

        public FormViewModel()
        {
            empdata = new List<EmpViewModel>();
        }       
    }
}

//public string EmpBirthdayDataFormat //EmpBirthday to String("yyyy/MM/dd") 
//{
//    get
//    {
//        //DateTime.TryParse()
//        string formatafter = string.Empty;
//        if (this.EmpBirthday == null)       //不能有NULL
//            formatafter = "";
//        else
//            formatafter = Convert.ToDateTime(this.EmpBirthday).ToString("yyyy/MM/dd");
//        return formatafter;
//    }
//}