
using ConsoleApp2.Core.Enums;
using ConsoleApp2.Core.Models;

namespace ConsoleApp2.Core.DTO.TabTwo
{
    public class OutgoingTabTwoDTO
    {
        public List<QuestionTypeMapping> QuestionTypes {  get; set; } = new List<QuestionTypeMapping>();
        public TabTwoModel PageData { get; set; } = new TabTwoModel();
    }
}
