using DWC.Blazor.Utils;
using System.Linq;
using Xunit;

namespace DWC.Blazor.Tests
{
    public class SkillNormalizerTests
    {
        [Fact]
        public void Normalize_JavaScript_ReturnsNormalized()
        {
            // Arrange
            var input = "javascript";

            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal("JavaScript", result);
        }

        [Theory]
        [InlineData("vue", "Vue.js")]
        [InlineData("VUE", "Vue.js")]
        [InlineData("Vue", "Vue.js")]
        [InlineData("vuejs", "Vue.js")]
        [InlineData("vue.js", "Vue.js")]
        [InlineData("vue js", "Vue.js")]
        public void Normalize_VueVariations_ReturnsVueJs(string input, string expected)
        {
            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(".net", ".NET")]
        [InlineData("dotnet", ".NET")]
        [InlineData(".NET(C#)", ".NET")]
        [InlineData(".net (c#)", ".NET")]
        public void Normalize_DotNetVariations_ReturnsDotNet(string input, string expected)
        {
            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(".net core", ".NET Core")]
        [InlineData("dotnet core", ".NET Core")]
        [InlineData(".net core c#", ".NET Core")]
        public void Normalize_DotNetCoreVariations_ReturnsDotNetCore(string input, string expected)
        {
            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("asp.net", "ASP.NET")]
        [InlineData("aspnet", "ASP.NET")]
        [InlineData("asp.net mvc", "ASP.NET")]
        public void Normalize_AspNetVariations_ReturnsAspNet(string input, string expected)
        {
            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("xamarin", "Xamarin")]
        [InlineData("xamarin.forms", "Xamarin")]
        [InlineData("xamarin.android", "Xamarin")]
        public void Normalize_XamarinVariations_ReturnsXamarin(string input, string expected)
        {
            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Normalize_EmptyString_ReturnsEmpty()
        {
            // Arrange
            var input = "";

            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Normalize_NullString_ReturnsEmpty()
        {
            // Arrange
            string input = null;

            // Act
            var result = SkillNormalizer.Normalize(input);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void NormalizeSkills_WithAndSeparator_SplitsSkills()
        {
            // Arrange
            var input = "Angular and TSQL";

            // Act
            var result = SkillNormalizer.NormalizeSkills(input).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("Angular", result);
            Assert.Contains("T-SQL", result);
        }

        [Fact]
        public void NormalizeSkills_WithAmpersandSeparator_SplitsSkills()
        {
            // Arrange
            var input = "C# & VB";

            // Act
            var result = SkillNormalizer.NormalizeSkills(input).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("C#", result);
            Assert.Contains("Vb", result);
        }

        [Fact]
        public void NormalizeSkills_CommaSeparated_ReturnsNormalizedList()
        {
            // Arrange
            var input = "javascript, python, c#";

            // Act
            var result = SkillNormalizer.NormalizeSkills(input).ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Contains("JavaScript", result);
            Assert.Contains("Python", result);
            Assert.Contains("C#", result);
        }

        [Fact]
        public void NormalizeSkills_EmptyString_ReturnsEmptyList()
        {
            // Arrange
            var input = "";

            // Act
            var result = SkillNormalizer.NormalizeSkills(input).ToList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void NormalizeSkills_NullString_ReturnsEmptyList()
        {
            // Arrange
            string input = null;

            // Act
            var result = SkillNormalizer.NormalizeSkills(input).ToList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void NormalizeSkills_WithExtraSpaces_TrimsCorrectly()
        {
            // Arrange
            var input = "  javascript  ,  python  ";

            // Act
            var result = SkillNormalizer.NormalizeSkills(input).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("JavaScript", result);
            Assert.Contains("Python", result);
        }
    }
}
