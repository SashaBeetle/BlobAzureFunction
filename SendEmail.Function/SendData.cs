using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SendEmail.Function
{
    public class SendData
    {
        private readonly ILogger _logger;

        public SendData(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SendData>();
        }

        [Function("SendData")]
        public static async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
        FunctionContext executionContext,
        string email)
        {
            var logger = executionContext.GetLogger("SendData");

            logger.LogInformation("C# HTTP trigger function processed a request.");

            logger.LogInformation($"Received email: {email}");


            //var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            //var client = new SendGridClient(apiKey);
            //var msg = new SendGridMessage()
            //{
            //    From = new EmailAddress("bot2112421234@gmail.com", "Bot"),
            //    Subject = "The Blob Form responce",
            //    PlainTextContent = $"File: ",
            //    HtmlContent = $"<strong>The file is successfully uploaded</strong>"
            //};
            //msg.AddTo(new EmailAddress(email, "Client"));

            //var response2 = await client.SendEmailAsync(msg);

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync($"Received email: {email}");

            return response;
        }
    }
}
