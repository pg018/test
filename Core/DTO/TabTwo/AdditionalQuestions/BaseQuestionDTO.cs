using ConsoleApp2.Core.CustomValidators;

namespace ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions
{
    public class BaseQuestionDTO
    {
        [DefaultPropertyValidator("Question")]
        public string? Question {  get; set; }
        [DefaultPropertyValidator("Type")]
        public int? Type {  get; set; } 
    }
}
