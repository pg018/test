using ConsoleApp2.Core.DTO.TabTwo.PersonalInfo;

namespace ConsoleApp2.Core.Models
{
    public class PersonalInfoTwoModel: PersonalInfoTwoDTO
    {
        public PersonalInfoItemTabTwoDTO FirstName { get; set; }
        public PersonalInfoItemTabTwoDTO LastName {  get; set; }
        public PersonalInfoItemTabTwoDTO Email {  get; set; }
    }
}
