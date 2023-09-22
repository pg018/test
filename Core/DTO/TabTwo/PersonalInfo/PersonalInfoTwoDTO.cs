using ConsoleApp2.Core.CustomValidators;
using ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2.Core.DTO.TabTwo.PersonalInfo
{
    public class PersonalInfoTwoDTO
    {
        [ObjectAttributeValidator("Phone Number")]
        public PersonalInfoItemTabTwoDTO? PhoneNumber { get; set; }
        [ObjectAttributeValidator("Personal Info")]
        public PersonalInfoItemTabTwoDTO Nationality { get; set; }
        [ObjectAttributeValidator("Current Residence")]
        public PersonalInfoItemTabTwoDTO CurrentResidence { get; set; }
        [ObjectAttributeValidator("ID Number")]
        public PersonalInfoItemTabTwoDTO IDNumber { get; set; }

        [ObjectAttributeValidator("Date Of Birth")]
        public PersonalInfoItemTabTwoDTO DOB { get; set; }
        [ObjectAttributeValidator("Gender")]
        public PersonalInfoItemTabTwoDTO Gender { get; set; }

        [ObjectAttributeValidator("Additional Questions", true)]
        public List<BaseQuestionDTO> AdditionalQuestions { get; set; } = new List<BaseQuestionDTO>();
    }
}
