using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team6CVGS.Common
{
    public class DateMinAttribute : ValidationAttribute
    {

        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {
            DateTime eventDate = Convert.ToDateTime(value);
            if (eventDate <= DateTime.Now)
            {
                
                return new ValidationResult
                    ("Event date can not be in the past.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

    }
}
