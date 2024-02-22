using BlobAzureFunction;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.WindowsAzure.Storage.Blob;
using Moq;

namespace BlobAzureFunctionTests
{
    [TestClass]
    public class FunctionTest
    {
        [TestMethod]
        public void TestMethod1BlobSendMail_Run_Success()
        {
            // Arrange
            var mockBlob = new Mock<CloudBlockBlob>(MockBehavior.Strict);
            mockBlob.Setup(b => b.DownloadToStreamAsync(It.IsAny<Stream>(), null, null, null))
                    .ReturnsAsync(() =>
                    {
                        var stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.Write("Test blob content");
                        writer.Flush();
                        stream.Position = 0;
                        return stream;
                    });

            var loggerMock = new Mock<ILogger>();

            var function = new BlobTriggerFunction();

            // Act
            await function.ProcessBlobAsync(mockBlob.Object, null, loggerMock.Object);

            // Assert
            // Add assertions here to verify the expected behavior
        }
    }
}