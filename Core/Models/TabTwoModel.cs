
using ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions;
using ConsoleApp2.Core.DTO.TabTwo.ProfileInfo;

namespace ConsoleApp2.Core.Models
{
    public class TabTwoModel: TabOneModel
    {
        public string? CoverImage { get; set; }
        public PersonalInfoTwoModel? PersonalInfo { get; set; }
        public ProfileInfoTwoDTO? ProfileInfo { get; set; }
        public List<BaseQuestionDTO>? AdditionalQuestions { get; set; }
    }
}
