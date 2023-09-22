
using System.Net;

namespace ConsoleApp2.Core.ServiceContracts
{
    public interface ICommonService
    {
        public Task SendResponse(
            HttpStatusCode statusCode,
            string message,
            HttpListenerResponse responseObj,
            bool isError=false);
    }
}
