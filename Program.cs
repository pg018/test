using ConsoleApp2.Controllers;
using ConsoleApp2.Core.ServiceContracts;
using ConsoleApp2.Core.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text;
using System.Text.Json;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        services.AddSingleton<IConfiguration>(configuration);

        services.AddSingleton(serviceProvider =>
        {
            return new CosmosClient(configuration["CosmosDB:EndpointUrl"], configuration["CosmosDB:PrimaryKey"]);
        });

        services.AddScoped(serviceProvider =>
        {
            var cosmosClient = serviceProvider.GetRequiredService<CosmosClient>();
            var database = cosmosClient.GetDatabase(configuration["CosmosDB:DatabaseName"]);
            return database.GetContainer(configuration["CosmosDB:ContainerName"]);
        });

        services.AddScoped<TabOneController>(); // Add this line
        services.AddScoped<TabTwoController>();

        services.AddScoped<ITabTwoService, TabTwoService>();
        services.AddScoped<ICommonService, CommonServices>();
    });

var host = builder.Build();

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:5000/");
listener.Start();

Console.WriteLine("Listening to incoming request...");

while (true)
{
    ICommonService _commonService = host.Services.GetRequiredService<ICommonService>();
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;
    try
    {
        switch (request?.Url?.AbsolutePath.ToLower())
        {
            case "/tabone":
                Console.Write("TabOne Here");
                var controller1 = host.Services.GetRequiredService<TabOneController>(); // Resolve the TabOneController
                await controller1.HandleRequest(request, response); // Call the instance method
                break;
            case "/tabtwo":
                var controller2 = host.Services.GetRequiredService<TabTwoController>();
                await controller2.HandleRequest(request, response);
                break;
            default:
                byte[] responseBytes = Encoding.UTF8.GetBytes("Unknown endpoint");
                response.ContentLength64 = responseBytes.Length;
                response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                break;
        }
    }
    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
    {
        await _commonService.SendResponse(
            HttpStatusCode.NotFound,
            "Resource Not Found",
            response,
            true);
    }
    catch (JsonException ex)
    {
        Console.WriteLine("JSON Exception");
        Console.WriteLine(ex.ToString());
        await _commonService.SendResponse(
            HttpStatusCode.BadRequest,
            "Enter Valid JSON Format",
            response,
            true);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        await _commonService.SendResponse(
            HttpStatusCode.InternalServerError,
            "Internal Server Error",
            response,
            true);
    }
    

    response.Close();
}
