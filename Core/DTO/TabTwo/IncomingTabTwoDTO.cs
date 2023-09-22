
using ConsoleApp2.Core.CustomValidators;
using ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions;
using ConsoleApp2.Core.DTO.TabTwo.PersonalInfo;
using ConsoleApp2.Core.DTO.TabTwo.ProfileInfo;

namespace ConsoleApp2.Core.DTO.TabTwo
{
    public class IncomingTabTwoDTO
    {
        [DefaultPropertyValidator("Id")]
        public string? id { get; set; }
        [DefaultPropertyValidator("Cover Image")]
        public string? CoverImage { get; set; }
        [DefaultPropertyValidator("User Id")]
        public string? UserId {  get; set; }
        [ObjectAttributeValidator("Personal Info")]
        public PersonalInfoTwoDTO PersonalInfo { get; set; }
        [ObjectAttributeValidator("Profile Info")]
        public ProfileInfoTwoDTO ProfileInfo { get; set; }

        [ObjectAttributeValidator("Additional Questions", true)]
        public List<BaseQuestionDTO> AdditionalQuestions {  get; set; } = new List<BaseQuestionDTO>();
    }
}
