using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AdPlatformService.Services;

namespace AdPlatformService.Tests
{
    [TestClass]
    public class AdsServiceTests
    {
        #region UploadFileAsync Tests

        [TestMethod]
        public async Task UploadFileAsync_ShouldStoreFileData()
        {
            // Arrange
            var content = "Hello World!";
            var fileName = "test.txt";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            IFormFile formFile = new FormFile(stream, 0, stream.Length, "file", fileName);

            var service = new TestableAdsService();

            // Act
            await service.UploadFileAsync(formFile);

            // Assert
            CollectionAssert.AreEqual(Encoding.UTF8.GetBytes(content), service.FileData);
        }

        #endregion

        #region SearchAsync Tests

        [TestMethod]
        public async Task SearchAsync_InvalidInput_ReturnsErrorMessage()
        {
            // Arrange
            var service = new TestableAdsService
            {
                IsValidInputResult = false,
                ErrorMessage = "Invalid input!"
            };

            // Act
            var result = await service.SearchAsync("SomeInput");

            // Assert
            Assert.AreEqual("Invalid input!", result);
        }

        [TestMethod]
        public async Task SearchAsync_ValidInput_ReturnsFormattedCompanies()
        {
            // Arrange
            var service = new TestableAdsService
            {
                IsValidInputResult = true,
                MatchingCompanies = new HashSet<string> { "Apple", "Microsoft" }
            };

            // Act
            var result = await service.SearchAsync("SomeInput");

            // Assert
            Assert.IsTrue(result.Contains("Apple"));
            Assert.IsTrue(result.Contains("Microsoft"));
        }

        #endregion

        #region TestableAdsService Stub

        // To override private/protected methods in tests
        private class TestableAdsService : AdsService
        {
            public bool IsValidInputResult { get; set; }
            public string? ErrorMessage { get; set; }
            public HashSet<string> MatchingCompanies { get; set; } = new();

            public byte[]? FileData => typeof(AdsService)
                .GetField("_fileData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(this) as byte[];

            protected override bool IsValidInput(string searchInput, out string? errorMessage)
            {
                errorMessage = ErrorMessage;
                return IsValidInputResult;
            }

            protected override Task<HashSet<string>> GetMatchingCompaniesAsync(string searchInput)
            {
                return Task.FromResult(MatchingCompanies);
            }

            protected override string FormatResult(HashSet<string> companies)
            {
                return string.Join("\n", companies);
            }
        }

        #endregion
    }
}
