using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using MVC3Demo.AppCode;

namespace MVC3Demo.Models
{
    public class ValidationModel
    {
        //[Display(Name = "特殊数字")]
        //[Required(ErrorMessage = "请输入{0}")]
        //[StringLength(20, ErrorMessage = "{0}在{2}位至{1}位之间", MinimumLength = 1)]
        //public string InputNumber { get; set; }

        //[RegularExpression(@"^[0-5]*$", ErrorMessage = "只能输入0-5间的数字")]
        //public string InputNumber { get; set; }
                
        [SpecialValidation]
        public string InputNumber { get; set; }
    }
}