using BlobAzureFunction;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.WindowsAzure.Storage.Blob;
using Moq;
using NSubstitute;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace BlobAzureFunctionTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var uri = new Uri("https://example.blob.core.windows.net/upload-files/filename.txt");
            var executionContext = new Mock<FunctionContext>();
            executionContext.Setup(x => x.GetLogger("BlobSendMail")).Returns(NullLogger<BlobSendMail>.Instance);
            var sendGridClientMock = new Mock<ISendGridClient>();
            sendGridClientMock.Setup(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), default)).ReturnsAsync(new Response());

            Environment.SetEnvironmentVariable("ApiKeySendEmail", "your-api-key");

            // Act
            await BlobSendMail.Run(executionContext.Object, uri);

            // Assert
            sendGridClientMock.Verify(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), default), Times.Once);
        }
    }
}