
using ConsoleApp2.Core.DTO.TabOne;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ConsoleApp2.Core.CustomValidators
{
    public class LocationOrRemoteValidator : ValidationAttribute
    {
        private string RemotePropertyName;

        public LocationOrRemoteValidator(string remotePropertyName)
        {
            RemotePropertyName = remotePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Console.WriteLine($"Location => {value} ");
            string? locationValue = (string?)value;
            IncomingTabOneDTO programInfo = (IncomingTabOneDTO)validationContext.ObjectInstance;
            var remoteProperty = validationContext.ObjectType.GetProperty(RemotePropertyName);

            if (remoteProperty == null || remoteProperty.PropertyType != typeof(bool))
            {
                return new ValidationResult($"Invalid Property! {RemotePropertyName}");
            }
            bool fullyRemote = (bool)remoteProperty.GetValue(programInfo, null)!;
            if (string.IsNullOrWhiteSpace(locationValue) && fullyRemote)
            {
                return ValidationResult.Success;
            }
            if (!string.IsNullOrWhiteSpace(locationValue) && !fullyRemote)
            {
                return ValidationResult.Success;
            }
            if (!string.IsNullOrWhiteSpace(locationValue) && fullyRemote)
            {
                return new ValidationResult("Location cannot be specified if program is remote");
            }
            // string is null and remote is false
            return new ValidationResult("Location must be entered");
        }
    }
}
