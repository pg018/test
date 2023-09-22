
namespace ConsoleApp2.Core.Enums
{
    public enum QuestionTypeEnum
    {
        Paragraph = 1,
        ShortAnswer = 2,
        YesNo = 3,
        Dropdown = 4,
        MultipleChoice = 5,
        Date = 6,
        Number = 7,
    }

    public class QuestionTypeMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
