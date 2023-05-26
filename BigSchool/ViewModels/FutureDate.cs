using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime time;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),"dd/MM/yyyy",CultureInfo.CurrentCulture,DateTimeStyles.None,out time);
            return (isValid && time > DateTime.Now);
        }
    }
}