using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;
using static DWC.Blazor.Pages.Index;

namespace DWC.Blazor.Tests
{
    public class DevelopersJsonFileTests
    {
        [Fact]
        public void JsonFile_Should_DeserializeOk()
        {
            // Arrange
            var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var jsonPath = Path.Combine(projectRoot, "DWC.Blazor", "wwwroot", "data", "developers.json");
            var jsonString = File.ReadAllText(jsonPath);

            // Act
            var developers = JsonConvert.DeserializeObject<Developer[]>(jsonString);

            // Assert
            Assert.NotNull(developers);
            Assert.NotEmpty(developers);
        }

        [Fact]
        public void JsonFile_SocialNetworkUrls_Should_HaveValidUrls()
        {    
            // Arrange
            var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var jsonPath = Path.Combine(projectRoot, "DWC.Blazor", "wwwroot", "data", "developers.json");
            var jsonString = File.ReadAllText(jsonPath);

            // Act
            var developers = JsonConvert.DeserializeObject<Developer[]>(jsonString);

            // Assert
            foreach (var developer in developers)
            {
                Assert.True(IsValidUrl(developer.Github), $"{developer.Name} has an invalid Github url");
                Assert.True(IsValidUrl(developer.LinkedIn), $"{developer.Name} has an invalid LinkedIn url");
                Assert.True(IsValidUrl(developer.StackOverflow), $"{developer.Name} has an invalid StackOverflow url");
                Assert.True(IsValidUrl(developer.Twitter), $"{developer.Name} has an invalid Twitter url");
                Assert.True(IsValidUrl(developer.Webpage), $"{developer.Name} has an invalid Webpage url");
                Assert.True(IsValidUrl(developer.YouTube), $"{developer.Name} has an invalid YouTube url");
                Assert.True(IsValidUrl(developer.Telegram), $"{developer.Name} has an invalid Telegram url");
                Assert.True(IsValidUrl(developer.Medium), $"{developer.Name} has an invalid Medium url");
            }
        }

        private bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return true;

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}