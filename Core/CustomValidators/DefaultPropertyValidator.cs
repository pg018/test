
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2.Core.CustomValidators
{
    public class DefaultPropertyValidator: ValidationAttribute
    {
        private readonly string _property;
        public DefaultPropertyValidator(string property) 
        {
            _property = property;    
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{_property} is Invalid");
            }
            string stringValue = value.ToString();
            if (string.IsNullOrEmpty(stringValue?.Trim()) ) 
            {
                return new ValidationResult($"{_property} is Invalid");
            }
            return ValidationResult.Success;
        }
    }
}
