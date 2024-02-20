using Microsoft.Azure.Functions.Worker;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BlobAzureFunction
{
    public class BlobSendMail
    {
        [Function("BlobSendMail")]
        public static async Task Run(
            [BlobTrigger("upload-files/{name}", Connection = "")]
            string myBlob,
            string name)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("bot2112421234@gmail.com", "Bot"),
                Subject = "The Blob Form responce",
                PlainTextContent = $"File: {name}",
                HtmlContent = $"<strong>The file is successfully uploaded</strong>"
            };
            msg.AddTo(new EmailAddress("farmgames153234@gmail.com", "Farmgames153234 Client"));

            var response = await client.SendEmailAsync(msg);
        }
    }
}
