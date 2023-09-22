using ConsoleApp2.Core.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2.Core.DTO.TabTwo.PersonalInfo
{
    public class PersonalInfoItemTabTwoDTO
    {
        [Required(ErrorMessage ="Internal Property is Missing")]
        public bool Internal { get; set; }
        [Required(ErrorMessage = "Hide Property is Missing")]
        public bool Hide { get; set; }

    }
}
