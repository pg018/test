
using ConsoleApp2.Core.DTO;
using ConsoleApp2.Core.ServiceContracts;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ConsoleApp2.Core.Services
{
    public class CommonServices: ICommonService
    {
        private OutgoingErrorDTO GetErrorJSON(string message)
        {
            List<string> errorList = message.Split("\n")
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToList();
            return new OutgoingErrorDTO
            {
                Errors = errorList
            };
        }

        public async Task SendResponse(
            HttpStatusCode statusCode,
            string message,
            HttpListenerResponse responseObj,
            bool isError=false)
        {
            string finalMessage = message;
            if (isError)
            {
                finalMessage = JsonSerializer.Serialize(this.GetErrorJSON(message));
            }
            byte[] responseMessage = Encoding.UTF8.GetBytes(finalMessage);
            responseObj.ContentLength64 = responseMessage.Length;
            responseObj.StatusCode = (int)statusCode;
            await responseObj.OutputStream.WriteAsync(responseMessage, 0, responseMessage.Length);
        }
    }
}
