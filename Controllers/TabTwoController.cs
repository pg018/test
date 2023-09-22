using ConsoleApp2.Core.DTO.TabTwo;
using ConsoleApp2.Core.Models;
using ConsoleApp2.Core.ServiceContracts;
using Microsoft.Azure.Cosmos;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace ConsoleApp2.Controllers
{
    public class TabTwoController
    {
        private readonly Container _cosmosContainer;
        private readonly ITabTwoService _tabTwoService;
        private readonly ICommonService _commonService;
        public TabTwoController(Container cosmosContainer, ITabTwoService tabTwoService, ICommonService commonService)
        {
            _cosmosContainer = cosmosContainer;
            _tabTwoService = tabTwoService;
            _commonService = commonService;
        }

        public async Task HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine(request.HttpMethod.ToUpper());
            switch (request.HttpMethod.ToUpper())
            {
                case "GET":
                    await HandleGet(request, response);
                    break;
                case "POST":
                    await HandlePut(request, response);
                    break;
                default:
                    break;
            }
        }

        private async Task<bool> ValidateIncomingDTO(IncomingTabTwoDTO? jsonObject, HttpListenerResponse response)
        {
            if (jsonObject == null)
            {
                return false;
            }
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(jsonObject);
            bool isValid = Validator.TryValidateObject(jsonObject, validationContext, validationResults, true);
            if (!isValid)
            {
                // DTO is invalid, handle the validation errors
                string validationErrors = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                await _commonService.SendResponse(HttpStatusCode.BadRequest, validationErrors, response, true);
                return false;
            }
            return true;
        }

        private async Task HandleGet(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                var queryParams = HttpUtility.ParseQueryString(request.Url.Query);
                var userId = queryParams["userId"];
                Console.WriteLine(userId);
                if (string.IsNullOrEmpty(userId))
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                var queryText = $"SELECT * FROM c WHERE c.UserId = '{userId}'";
                var queryDefinition = new QueryDefinition(queryText);
                var queryResultSetIterator = _cosmosContainer.GetItemQueryIterator<TabTwoModel>(queryDefinition);
                TabTwoModel finalDoc=new TabTwoModel();
                while (queryResultSetIterator.HasMoreResults)
                {
                    var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var document in currentResultSet)
                    {
                        if (document != null)
                        {
                            finalDoc = document;
                            break;
                        }
                        // Document with the specified id exists
                        // You can now retrieve it or perform further actions
                    }
                }
                OutgoingTabTwoDTO outgoingDoc = new OutgoingTabTwoDTO
                {
                    PageData = finalDoc,
                    QuestionTypes = _tabTwoService.GetQuestionTypesList()
                };
                string responseString = System.Text.Json.JsonSerializer.Serialize(outgoingDoc);
                await _commonService.SendResponse(HttpStatusCode.OK, responseString, response);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.StatusCode =(int)HttpStatusCode.InternalServerError;
            }
            await Task.CompletedTask;
        }

        private async Task HandlePut(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                using var reader = new StreamReader(request.InputStream);
                var requestBodyJSON = await reader.ReadToEndAsync();
                JsonDocument jsonDocument = JsonDocument.Parse(requestBodyJSON);
                JsonElement additionalQuestionsElementPersonal,
                    additionalQuestionsElementProfile,
                    additionalQuestionsElementSolo;
                jsonDocument.RootElement
                    .GetProperty("PersonalInfo")
                    .TryGetProperty("AdditionalQuestions", out additionalQuestionsElementPersonal);
                jsonDocument.RootElement
                    .GetProperty("ProfileInfo")
                    .TryGetProperty("AdditionalQuestions", out additionalQuestionsElementProfile);
                jsonDocument.RootElement
                    .TryGetProperty("AdditionalQuestions", out additionalQuestionsElementSolo);
                var additionalQuestionsPersonalString = additionalQuestionsElementPersonal.ToString();
                var additionalQuestionsProfileString = additionalQuestionsElementProfile.ToString();
                var additionalQuestionsSoloString = additionalQuestionsElementSolo.ToString();
                var defaultJSONObj = System.Text.Json.JsonSerializer.Deserialize<IncomingTabTwoDTO>(requestBodyJSON);

                if (additionalQuestionsPersonalString != string.Empty)
                {
                    var personalFinalList = _tabTwoService.GetFinalAdditionalQuestionsList(additionalQuestionsPersonalString);
                    defaultJSONObj!.PersonalInfo!.AdditionalQuestions = personalFinalList;
                }
                if (additionalQuestionsProfileString != string.Empty)
                {
                    var profileFinalList = _tabTwoService.GetFinalAdditionalQuestionsList(additionalQuestionsProfileString);
                    defaultJSONObj!.ProfileInfo!.AdditionalQuestions = profileFinalList;
                }
                if (additionalQuestionsSoloString != string.Empty)
                {
                    var soloFinalList = _tabTwoService.GetFinalAdditionalQuestionsList(additionalQuestionsSoloString);
                    defaultJSONObj!.AdditionalQuestions = soloFinalList;
                }

                Console.WriteLine("Starting Validation");
                if (!await ValidateIncomingDTO(defaultJSONObj, response) || defaultJSONObj == null)
                {
                    return;
                }

                Console.WriteLine(defaultJSONObj.id);
                var finalDoc = await _tabTwoService.GetTabTwoModelFromDB(defaultJSONObj);
                
                await _cosmosContainer.ReplaceItemAsync(finalDoc, finalDoc.id);
                response.StatusCode = (int)HttpStatusCode.Created;

            }
            catch (InvalidOperationException ex)
            {
                await _commonService.SendResponse(
                    HttpStatusCode.BadRequest,
                    "Property in Additional Questions is Missing",
                    response, true);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"KeyNotFound {ex.Message}");
                await _commonService.SendResponse(
                    HttpStatusCode.BadRequest,
                    "Property in Additional Questions is Missing",
                    response,
                    true);
            }
            await Task.CompletedTask;
        }
    }
}
