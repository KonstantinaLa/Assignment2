using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Models.Custom_Validations
{
    public class ValidationMethods
    {
        public static ValidationResult ValidationDate(DateTime value , ValidationContext context)
        {
            var date = DateTime.Now.Year - 18;
            if (value.Year < date)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(string.Format("Invalid date time, please try again!"), new List<string> { context.MemberName });
        }


    }
}