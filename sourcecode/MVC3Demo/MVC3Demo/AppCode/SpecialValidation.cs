using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC3Demo.AppCode
{
    public class SpecialValidation : RegularExpressionAttribute
    {
        public SpecialValidation() : base(@"^[0-5]*$") { }

        public override string FormatErrorMessage(string name)
        {
            return String.Format("{0}在0-5之间", name);
        }
    }
}