
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ConsoleApp2.Core.CustomValidators
{
    public class StartEndDateValidator: ValidationAttribute
    {
        private string EndDatePropName;

        public StartEndDateValidator(string endDatePropName)
        {
            EndDatePropName = endDatePropName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Application Start Date is Required");
            }
            DateTime startDate = Convert.ToDateTime(value);
            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(EndDatePropName);
            if (otherProperty == null)
            {
                return new ValidationResult("Application End Date is Required");
            }
            DateTime endDate = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));
            Console.WriteLine($"Start Date {startDate}");
            Console.WriteLine($"End Date {endDate}");
            if (startDate > endDate)
            {
                return new ValidationResult("Start Date cannot be greater than end date");
            }
            return ValidationResult.Success;
        }
    }
}
