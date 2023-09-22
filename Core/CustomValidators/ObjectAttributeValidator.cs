
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2.Core.CustomValidators
{
    public class ObjectAttributeValidator: ValidationAttribute
    {
        private readonly string PropertyName;
        private readonly bool _nullValueAllowed;

        public ObjectAttributeValidator(string propertyName, bool NullValueAllowed=false)
        {
            PropertyName = propertyName;
            _nullValueAllowed = NullValueAllowed;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                if (_nullValueAllowed)
                {
                    return ValidationResult.Success;
                }
                // check this out for all cases
                return new ValidationResult($"{PropertyName} property is Missing");
            }
            var nestedObjValidator = new ValidationContext(value);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(value, nestedObjValidator, results, true);
            if (results.Count > 0)
            {
                var errorMessage = string.Join(Environment.NewLine, results.Select(e => e.ErrorMessage));
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
