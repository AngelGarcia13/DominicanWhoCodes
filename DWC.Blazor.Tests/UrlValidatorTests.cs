using DWC.Blazor.Utils;
using Xunit;

namespace DWC.Blazor.Tests
{
    public class UrlValidatorTests
    {
        #region Valid URL Tests

        [Theory]
        [InlineData("https://linkedin.com/in/johndoe")]
        [InlineData("https://www.linkedin.com/in/johndoe")]
        [InlineData("https://linkedin.com/company/microsoft")]
        [InlineData("https://www.linkedin.com/company/microsoft")]
        public void ValidateUrl_ValidLinkedInUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "linkedin");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://github.com/johndoe")]
        [InlineData("https://www.github.com/johndoe")]
        [InlineData("https://github.com/microsoft/vscode")]
        public void ValidateUrl_ValidGitHubUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://twitter.com/johndoe")]
        [InlineData("https://www.twitter.com/johndoe")]
        [InlineData("https://x.com/johndoe")]
        [InlineData("https://www.x.com/johndoe")]
        public void ValidateUrl_ValidTwitterUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "twitter");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://t.me/johndoe")]
        [InlineData("https://t.me/mychannel")]
        public void ValidateUrl_ValidTelegramUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "telegram");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://stackoverflow.com/users/123456/johndoe")]
        [InlineData("https://www.stackoverflow.com/users/987654")]
        public void ValidateUrl_ValidStackOverflowUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "stackoverflow");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://medium.com/@johndoe")]
        [InlineData("https://www.medium.com/@janedoe")]
        public void ValidateUrl_ValidMediumUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "medium");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://youtube.com/c/johndoe")]
        [InlineData("https://www.youtube.com/channel/UC123456")]
        [InlineData("https://youtu.be/dQw4w9WgXcQ")]
        public void ValidateUrl_ValidYouTubeUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "youtube");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://example.com")]
        [InlineData("https://www.example.com/about")]
        [InlineData("https://subdomain.example.com/page")]
        public void ValidateUrl_ValidWebpageUrl_ReturnsValid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "webpage");

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(string.Empty, result.ErrorMessage);
        }

        #endregion

        #region Invalid URL Tests - Missing HTTPS

        [Theory]
        [InlineData("http://github.com/johndoe", "github")]
        [InlineData("http://linkedin.com/in/johndoe", "linkedin")]
        [InlineData("http://twitter.com/johndoe", "twitter")]
        public void ValidateUrl_HttpUrl_ReturnsInvalid(string url, string platform)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, platform);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("URL must use HTTPS protocol.", result.ErrorMessage);
        }

        #endregion

        #region Invalid URL Tests - Wrong Domain

        [Theory]
        [InlineData("https://linkedin.com/johndoe")]
        [InlineData("https://linkedin.com/profile/johndoe")]
        [InlineData("https://notlinkedin.com/in/johndoe")]
        public void ValidateUrl_InvalidLinkedInDomain_ReturnsInvalid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "linkedin");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("LinkedIn URL must contain 'linkedin.com/in/' or 'linkedin.com/company/'.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://gitlab.com/johndoe")]
        [InlineData("https://bitbucket.org/johndoe")]
        public void ValidateUrl_InvalidGitHubDomain_ReturnsInvalid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("GitHub URL must contain 'github.com/'.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://facebook.com/johndoe")]
        [InlineData("https://instagram.com/johndoe")]
        public void ValidateUrl_InvalidTwitterDomain_ReturnsInvalid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "twitter");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Twitter URL must contain 'twitter.com/' or 'x.com/'.", result.ErrorMessage);
        }

        [Fact]
        public void ValidateUrl_InvalidTelegramDomain_ReturnsInvalid()
        {
            // Arrange
            var url = "https://telegram.org/johndoe";

            // Act
            var result = UrlValidator.ValidateUrl(url, "telegram");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Telegram URL must contain 't.me/'.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://stackoverflow.com/questions/123456")]
        [InlineData("https://stackoverflow.com/johndoe")]
        public void ValidateUrl_InvalidStackOverflowDomain_ReturnsInvalid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "stackoverflow");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("StackOverflow URL must contain 'stackoverflow.com/users/'.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://medium.com/johndoe")]
        [InlineData("https://medium.com/publication/article")]
        public void ValidateUrl_InvalidMediumDomain_ReturnsInvalid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "medium");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Medium URL must contain 'medium.com/@'.", result.ErrorMessage);
        }

        [Theory]
        [InlineData("https://vimeo.com/123456")]
        [InlineData("https://dailymotion.com/video")]
        public void ValidateUrl_InvalidYouTubeDomain_ReturnsInvalid(string url)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, "youtube");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("YouTube URL must contain 'youtube.com/' or 'youtu.be/'.", result.ErrorMessage);
        }

        #endregion

        #region Invalid URL Tests - Malformed

        [Theory]
        [InlineData("not-a-url", "github")]
        [InlineData("htp://invalid", "linkedin")]
        [InlineData("://missing-protocol.com", "twitter")]
        [InlineData("github.com/johndoe", "github")]
        public void ValidateUrl_MalformedUrl_ReturnsInvalid(string url, string platform)
        {
            // Act
            var result = UrlValidator.ValidateUrl(url, platform);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("URL is not a valid URI format.", result.ErrorMessage);
        }

        #endregion

        #region Null, Empty, and Whitespace Tests

        [Fact]
        public void ValidateUrl_NullUrl_ReturnsInvalid()
        {
            // Arrange
            string url = null;

            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("URL cannot be null or empty.", result.ErrorMessage);
        }

        [Fact]
        public void ValidateUrl_EmptyUrl_ReturnsInvalid()
        {
            // Arrange
            var url = "";

            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("URL cannot be null or empty.", result.ErrorMessage);
        }

        [Fact]
        public void ValidateUrl_WhitespaceUrl_ReturnsInvalid()
        {
            // Arrange
            var url = "   ";

            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("URL cannot be null or empty.", result.ErrorMessage);
        }

        [Fact]
        public void ValidateUrl_NullPlatform_ReturnsInvalid()
        {
            // Arrange
            string platform = null;

            // Act
            var result = UrlValidator.ValidateUrl("https://github.com/johndoe", platform);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Platform cannot be null or empty.", result.ErrorMessage);
        }

        [Fact]
        public void ValidateUrl_EmptyPlatform_ReturnsInvalid()
        {
            // Arrange
            var platform = "";

            // Act
            var result = UrlValidator.ValidateUrl("https://github.com/johndoe", platform);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Platform cannot be null or empty.", result.ErrorMessage);
        }

        [Fact]
        public void ValidateUrl_WhitespacePlatform_ReturnsInvalid()
        {
            // Arrange
            var platform = "   ";

            // Act
            var result = UrlValidator.ValidateUrl("https://github.com/johndoe", platform);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Platform cannot be null or empty.", result.ErrorMessage);
        }

        #endregion

        #region Unknown Platform Tests

        [Theory]
        [InlineData("facebook")]
        [InlineData("instagram")]
        [InlineData("unknown")]
        public void ValidateUrl_UnknownPlatform_ReturnsInvalid(string platform)
        {
            // Arrange
            var url = "https://example.com";

            // Act
            var result = UrlValidator.ValidateUrl(url, platform);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal($"Unknown platform: {platform}.", result.ErrorMessage);
        }

        #endregion

        #region Platform Name Case Insensitivity Tests

        [Theory]
        [InlineData("GitHub")]
        [InlineData("GITHUB")]
        [InlineData("github")]
        [InlineData("GiTHuB")]
        public void ValidateUrl_PlatformNameCaseInsensitive_ReturnsValid(string platform)
        {
            // Arrange
            var url = "https://github.com/johndoe";

            // Act
            var result = UrlValidator.ValidateUrl(url, platform);

            // Assert
            Assert.True(result.IsValid);
        }

        #endregion

        #region URL Trimming Tests

        [Fact]
        public void ValidateUrl_UrlWithLeadingSpaces_ReturnsValid()
        {
            // Arrange
            var url = "   https://github.com/johndoe";

            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateUrl_UrlWithTrailingSpaces_ReturnsValid()
        {
            // Arrange
            var url = "https://github.com/johndoe   ";

            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateUrl_UrlWithLeadingAndTrailingSpaces_ReturnsValid()
        {
            // Arrange
            var url = "   https://github.com/johndoe   ";

            // Act
            var result = UrlValidator.ValidateUrl(url, "github");

            // Assert
            Assert.True(result.IsValid);
        }

        #endregion
    }
}
