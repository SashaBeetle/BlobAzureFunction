using BlobAzureFunction;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    public class UnitTest1
    {       
        [Fact]
        public async Task Test1Async()
        {


            var uri = new Uri("https://example.blob.core.windows.net/upload-files/filename.txt");
            var executionContext = new Mock<FunctionContext>();
            executionContext.Setup(x => x.GetLogger("BlobSendMail")).Returns(NullLogger<BlobSendMail>.Instance);

            

            var sendGridClientMock = new Mock<ISendGridClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent("Response content");
            response.Headers.Add("HeaderName", "HeaderValue");
            var statusCode = HttpStatusCode.OK;

            var message = new HttpResponseMessage();
            var headers = message.Headers;

            headers.Add("HeaderKey", "HeaderValue");

            var httpContent = new StringContent("Response content");
            var mockedResponse = new Response(statusCode, httpContent, headers);

            sendGridClientMock.Setup(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), default)).ReturnsAsync(mockedResponse);

            Environment.SetEnvironmentVariable("ApiKeySendEmail", " ");

            var blobSendMail = new BlobSendMail(); 

            await blobSendMail.Run(executionContext.Object, uri);

            // Assert
            sendGridClientMock.Verify(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), default), Times.Once);
        }

        
    }
}