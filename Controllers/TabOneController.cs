using ConsoleApp2.Core.DTO.TabOne;
using ConsoleApp2.Core.ModelExtensions;
using ConsoleApp2.Core.Models;
using ConsoleApp2.Core.ServiceContracts;
using Microsoft.Azure.Cosmos;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace ConsoleApp2.Controllers
{
    public class TabOneController
    {
        private readonly Container _cosmosContainer;
        private readonly ICommonService _commonService;

        public TabOneController(Container cosmosContainer, ICommonService commonService)
        {
            _cosmosContainer = cosmosContainer;
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
                    await HandlePost(request, response);
                    break;
                case "PUT":
                    await HandlePut(request, response);
                    break;
                default:
                    break;
            }
        }

        private async Task SendResponse(HttpListenerResponse response, HttpStatusCode statusCode)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            await Task.CompletedTask;
        }

        private async Task<bool> ValidateIncomingDTO(IncomingTabOneDTO? jsonObject, HttpListenerResponse response)
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
            if (jsonObject.ApplicationClose > jsonObject.ProgramStart)
            {
                await _commonService.SendResponse(
                    HttpStatusCode.BadRequest,
                    "Application Close cannot be greater than Program Start",
                    response, true);
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
                if (string.IsNullOrEmpty(userId))
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                var queryText = $"SELECT * FROM c WHERE c.userId = '{userId}'";
                var queryDefinition = new QueryDefinition(queryText);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await SendResponse(response, HttpStatusCode.OK);
        }

        private async Task HandlePost(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("Running the controller function");
            using var reader = new StreamReader(request.InputStream);
            var requestBody = await reader.ReadToEndAsync();
            // deserialize into json object
            var jsonObject = JsonSerializer.Deserialize<IncomingTabOneDTO>(requestBody);
            if (!await ValidateIncomingDTO(jsonObject, response) || jsonObject == null)
            {
                return;
            }
            Console.WriteLine("All passed");
            TabOneModel finalModel = TabOneModelExtension.ConvertTabOneDTOToModel(jsonObject);
            var dbResponse = await _cosmosContainer.CreateItemAsync(finalModel);
            Console.WriteLine($"New ID: {dbResponse}");
            response.StatusCode = (int)HttpStatusCode.Created;
            await Task.CompletedTask;
        }

        private async Task HandlePut(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                using var reader = new StreamReader(request.InputStream);
                var requestBody = await reader.ReadToEndAsync();
                // deserialize into json object
                var jsonObject = JsonSerializer.Deserialize<OutgoingTabOneDTO>(requestBody);
                if (!await ValidateIncomingDTO(jsonObject, response) || jsonObject == null)
                {
                    return;
                }
                var finalObject = TabOneModelExtension.ConvertOutTabOneToModel(jsonObject);
                Console.WriteLine(finalObject.id);
                await _cosmosContainer.ReplaceItemAsync(
                    finalObject,
                    finalObject.id);
                response.StatusCode = (int)HttpStatusCode.OK;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            await Task.CompletedTask;
        }
    }
}
