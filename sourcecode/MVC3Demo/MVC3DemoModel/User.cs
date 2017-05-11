using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MVC3DemoModel
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        [StringLength(20, ErrorMessage = "{0}在{2}位至{1}位之间", MinimumLength = 1)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "{0}在{2}位至{1}位之间", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "重复密码")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密码与重复密码必须相同")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "请输入手机号码")]
        [Display(Name = "手机号码")]
        public string Phone { get; set; }
        [Display(Name = "小区")]
        public string Residential { get; set; }
        [Display(Name = "楼号")]
        public string FloorNo { get; set; }
        [Display(Name = "单元号")]
        public string UnitNo { get; set; }
        [Display(Name = "门牌号")]
        public string DoorplateNo { get; set; }

        [Display(Name = "记住我")]
        public bool Remember { get; set; }

        public DateTime? SubmitTime { get; set; }
    }
}
