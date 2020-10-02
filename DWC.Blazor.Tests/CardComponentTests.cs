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
                ("Telegram", "http://telegram.com"),
                ("StackOverflow", "http://stackoverflow.com"),
                ("Medium", "http://medium.com"),
                ("Github", "http://github.com/nminaya"),
                ("YouTube", "http://youtube.com")
            };

            // Act
            var component = context.RenderComponent<CardComponent>(parameters);

            // Assert
            Assert.Throws<ElementNotFoundException>(() => component.Find("div .social-networks .fa-linkedin"));
        }

        [Fact]
        public void SocialNetworks_AllSocialNetworkFilled_Should_DisplaysAllSocilNetworks()
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
                ("Skills","C#, ASP.NET Core, T-SQL, TypeScript, JavaScrpit"),
                ("Twitter", "http://twitter.com"),
                ("Telegram", "http://telegram.com"),
                ("StackOverflow", "http://stackoverflow.com"),
                ("Medium", "http://medium.com"),
                ("Github", "http://github.com"),
                ("YouTube", "http://youtube.com"),
                ("LinkedIn", "http://linkedin.com")
            };

            // Act
            var component = context.RenderComponent<CardComponent>(parameters);

            // Assert
            var socialNetworkDiv = component.Find("div .social-networks");

            Assert.True(socialNetworkDiv.HasChildNodes);
            Assert.Equal(8, socialNetworkDiv.ChildElementCount); // There should be 8 elements (Social Networks)

            Assert.NotNull(component.Find("div .social-networks .fa-globe-americas"));
            Assert.NotNull(component.Find("div .social-networks .fa-linkedin"));
            Assert.NotNull(component.Find("div .social-networks .fa-twitter"));
            Assert.NotNull(component.Find("div .social-networks .fa-github"));
            Assert.NotNull(component.Find("div .social-networks .fa-paper-plane"));
            Assert.NotNull(component.Find("div .social-networks .fa-stack-overflow"));
            Assert.NotNull(component.Find("div .social-networks .fa-medium"));
            Assert.NotNull(component.Find("div .social-networks .fa-youtube"));
        }
    }
}