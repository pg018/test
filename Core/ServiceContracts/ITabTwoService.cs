
using ConsoleApp2.Core.DTO.TabTwo;
using ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions;
using ConsoleApp2.Core.Enums;
using ConsoleApp2.Core.Models;

namespace ConsoleApp2.Core.ServiceContracts
{
    public interface ITabTwoService
    {
        public List<BaseQuestionDTO> GetFinalAdditionalQuestionsList(string additionalQuestionsJson);
        public List<QuestionTypeMapping> GetQuestionTypesList();
        public Task<TabTwoModel> GetTabTwoModelFromDB(IncomingTabTwoDTO defaultJSONObj);
    }
}
