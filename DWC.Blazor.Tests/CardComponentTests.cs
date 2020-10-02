using Bunit;
using Bunit.Rendering;
using DWC.Blazor.Shared;
using Xunit;

namespace DWC.Blazor.Tests
{
    public class CardComponentTests
    {
        [Fact]
        public void SocialNetworks_NoLinkedin_Should_Not_DisplayLinkedinLink()
        {
            // Arrange
            using var context = new TestContext();

            var parameters = new ComponentParameter[]
            {
                ("Name", "Test Name"),
                ("Initials", "TN"),
                ("Image", "image.jpg"),
                ("Summary", "Test Summary"),
                ("Webpage", "http://mypage.com"),
                ("Twitter", "http://twitter.com"),
                ("Skills","C#, ASP.NET Core, T-SQL, TypeScript, JavaScrpit"),
                ("Twitter", "http://twitter.com"),
                ("Telegram", "http://telegram.com"),
                ("StackOverflow", "http://stackoverflow.com"),
                ("Medium", "http://medium.com"),
                ("YouTube", "http://youtube.com")
            };

            // Act
            var component = context.RenderComponent<CardComponent>(parameters);

            // Assert
            Assert.Throws<ElementNotFoundException>(() => component.Find("div .social-networks .fa-linkedin"));
        }
    }
}