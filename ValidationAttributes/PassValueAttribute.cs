using Post.Dtos;
using Post.Models;
using System.ComponentModel.DataAnnotations;

namespace Post.ValidationAttributes
{
    public class PassValueAttribute : ValidationAttribute
    {
        public string PassValue;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            return new ValidationResult(PassValue, ["passValue"]);
        }
    }
}
