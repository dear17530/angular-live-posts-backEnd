using Post.Dtos;
using Post.Models;
using System.ComponentModel.DataAnnotations;

namespace Post.ValidationAttributes
{
    public class StartTimeEndTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var st = (PostReqDto)value;
            dynamic st = value;

            if(st.StartTime >= st.EndTime)
            {
                return new ValidationResult("開始時間不可以大於結束時間", ["time"]);
            }

            return ValidationResult.Success;
        }
    }
}
