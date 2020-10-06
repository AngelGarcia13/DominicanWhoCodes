using Newtonsoft.Json;
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
            var jsonString = File.ReadAllText("wwwroot\\data\\developers.json");

            // Act
            var developers = JsonConvert.DeserializeObject<Developer[]>(jsonString);

            // Assert
            Assert.NotNull(developers);
            Assert.NotEmpty(developers);
        }
    }
}