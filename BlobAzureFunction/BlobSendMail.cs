using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.WindowsAzure.Storage.Blob;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Reflection.Metadata;

namespace BlobAzureFunction
{
    public class BlobSendMail
    {
        [Function("BlobSendMail")]
        public async Task Run(
            [BlobTrigger("upload-files/{name}", Connection = "")]
            FunctionContext executionContext,
            System.Uri Uri)
        {     
            var logger = executionContext.GetLogger("BlobSendMail");

            string[] email = EmailReturn(Uri.ToString()); // email[0](Email), email[1](UserFileName)
                logger.LogInformation("Trigger function during the process...");

                logger.LogInformation($"Received email: {email[0]}");

            string apiKeySendEmail = Environment.GetEnvironmentVariable("ApiKeySendEmail");

            var client = new SendGridClient(apiKeySendEmail);
                logger.LogInformation($"API was connected.");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("bot2112421234@gmail.com", "Bot"),
                Subject = "The Blob Form responce",
                PlainTextContent = $"File: {email[1]}",
                HtmlContent = $"<strong>The file is successfully uploaded</strong> (blobreactform.azurewebsites.net)"
            };
            msg.AddTo(new EmailAddress($"{email[0]}", "Oleksandr Client"));
            logger.LogInformation($"Message has been built.");

            var response = await client.SendEmailAsync(msg);
                logger.LogInformation($"Message has been send.\n Responce:"+response.StatusCode);

            string[] EmailReturn(string uri)
            {
                string[] parts = uri.Split('/');
                string[] part = parts[4].Split('|');

                return part;
            }
        }
    }
}
