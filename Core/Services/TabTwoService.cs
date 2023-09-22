
using ConsoleApp2.Core.DTO.TabTwo;
using ConsoleApp2.Core.DTO.TabTwo.AdditionalQuestions;
using ConsoleApp2.Core.Enums;
using ConsoleApp2.Core.ModelExtensions;
using ConsoleApp2.Core.Models;
using ConsoleApp2.Core.ServiceContracts;
using Microsoft.Azure.Cosmos;
using System.Text.Json;

namespace ConsoleApp2.Core.Services
{
    public class TabTwoService: ITabTwoService
    {
        private readonly Container _cosmosContainer;
        public TabTwoService(Container cosmosContainer) 
        { 
            _cosmosContainer = cosmosContainer;
        }
        public List<QuestionTypeMapping> GetQuestionTypesList()
        {
            List<QuestionTypeMapping> questionTypes = new List<QuestionTypeMapping>();
            foreach (QuestionTypeEnum type in Enum.GetValues(typeof(QuestionTypeEnum)))
            {
                Console.WriteLine(type.ToString());
                Console.WriteLine((int)type);
                questionTypes.Add(new QuestionTypeMapping
                {
                    Id = (int)type,
                    Name = type.ToString()
                });
            }
            return questionTypes;
        }

        public async Task<TabTwoModel> GetTabTwoModelFromDB(IncomingTabTwoDTO defaultJSONObj)
        {
            var queryText = $"SELECT * FROM c WHERE c.id = '{defaultJSONObj.id}'";

            var queryDefinition = new QueryDefinition(queryText);
            var queryResultSetIterator = _cosmosContainer.GetItemQueryIterator<TabOneModel>(queryDefinition);
            var finalTabTwoDocument = new TabTwoModel();

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (var document in currentResultSet)
                {
                    if (document != null)
                    {
                        finalTabTwoDocument = TabTwoModelExtension.ConvertTabTwoDTOToModel(defaultJSONObj, document);
                        break;
                    }
                    // Document with the specified id exists
                    // You can now retrieve it or perform further actions
                }
            }

            return finalTabTwoDocument;
        }

        public List<BaseQuestionDTO> GetFinalAdditionalQuestionsList(string additionalQuestionsJson)
        {
            List<BaseQuestionDTO> finalList = new();
            Console.WriteLine("Within the parsing function");
            Console.WriteLine(additionalQuestionsJson);
            foreach (var element in JsonDocument.Parse(additionalQuestionsJson).RootElement.EnumerateArray())
            {
                int type = element.GetProperty("Type").GetInt32();
                switch (type)
                {
                    case 1: // Paragraph
                        ParagraphQuestionDTO paragraphQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        Console.WriteLine(paragraphQuestionDTO.ToString());
                        finalList.Add(paragraphQuestionDTO);
                        break;
                    case 3: // yesNo
                        YesNoQuestionDTO yesNoQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                            DisqualifyIfNo = element.GetProperty("DisqualifyIfNo").GetBoolean(),
                        };
                        Console.WriteLine(yesNoQuestionDTO.ToString());
                        finalList.Add(yesNoQuestionDTO);
                        break;
                    case 4:
                        DropdownQuestionDTO dropdownQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                            Options = element.GetProperty("Options").EnumerateArray().Select(option => option.GetString()).ToList()!
                        };
                        finalList.Add(dropdownQuestionDTO);
                        break;
                    case 5: // Multiple Choice
                        MultipleChoiceQuestionDTO multipleChoiceQuestionsDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                            Options = element.GetProperty("Options").EnumerateArray().Select(option => option.GetString()).ToList()!,
                            MaxChoiceAllowed = element.GetProperty("MaxChoiceAllowed").GetInt32(),
                            ShowOther = element.GetProperty("ShowOther").GetBoolean(),
                        };
                        finalList.Add(multipleChoiceQuestionsDTO);
                        break;
                    case 6: // Date
                        DateQuestionDTO dateQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        finalList.Add(dateQuestionDTO);
                        break;
                    case 7: // Number
                        NumberQuestionDTO numberQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        finalList.Add(numberQuestionDTO);
                        break;
                    default:
                        break;
                }
            }
            return finalList;
        }
    }
}
