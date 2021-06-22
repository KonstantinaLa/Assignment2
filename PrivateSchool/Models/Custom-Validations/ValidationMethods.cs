using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Models.Custom_Validations
{
    public class ValidationMethods
    {
        public static ValidationResult ValidationAdult(DateTime value , ValidationContext context)
        {
            var date = DateTime.Now.Year - 18;
            if (value.Year < date)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(String.Format("Invalid date time.You must be an adult, please try again!"), new List<string> { context.MemberName });
        }
        


    }
}