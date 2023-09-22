using ConsoleApp2.Core.CustomValidators;
using ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2.Core.DTO.TabTwo.ProfileInfo
{
    public class ProfileInfoTwoDTO
    {
        [Required(ErrorMessage = "Education Property is Missing")]
        public ProfileInfoItemTwoDTO Education { get; set; }
        [Required(ErrorMessage = "Experience Property is Missing")]
        public ProfileInfoItemTwoDTO Experience { get; set; }
        [Required(ErrorMessage = "Resume Property is Missing")]
        public ProfileInfoItemTwoDTO Resume { get; set; }
        [ObjectAttributeValidator("Additional Questions", true)]
        public List<BaseQuestionDTO> AdditionalQuestions { get; set; } = new List<BaseQuestionDTO>();
    }
}
