using Bunit;
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
            using var context = new BunitContext();

            // Act
            var component = context.Render<CardComponent>(parameters => parameters
                .Add(p => p.Name, "Test Name")
                .Add(p => p.Initials, "TN")
                .Add(p => p.Image, "image.jpg")
                .Add(p => p.Summary, "Test Summary")
                .Add(p => p.Webpage, "http://mypage.com")
                .Add(p => p.Twitter, "http://twitter.com")
                .Add(p => p.Skills, "C#, ASP.NET Core, T-SQL, TypeScript, JavaScrpit")
                .Add(p => p.Telegram, "http://telegram.com")
                .Add(p => p.StackOverflow, "http://stackoverflow.com")
                .Add(p => p.Medium, "http://medium.com")
                .Add(p => p.Github, "http://github.com/nminaya")
                .Add(p => p.YouTube, "http://youtube.com")
            );

            // Assert
            Assert.Throws<ElementNotFoundException>(() => component.Find("div .social-networks .fa-linkedin"));
        }

        [Fact]
        public void SocialNetworks_AllSocialNetworkFilled_Should_DisplaysAllSocilNetworks()
        {
            // Arrange
            using var context = new BunitContext();

            // Act
            var component = context.Render<CardComponent>(parameters => parameters
                .Add(p => p.Name, "Test Name")
                .Add(p => p.Initials, "TN")
                .Add(p => p.Image, "image.jpg")
                .Add(p => p.Summary, "Test Summary")
                .Add(p => p.Webpage, "http://mypage.com")
                .Add(p => p.Skills, "C#, ASP.NET Core, T-SQL, TypeScript, JavaScrpit")
                .Add(p => p.Twitter, "http://twitter.com")
                .Add(p => p.Telegram, "http://telegram.com")
                .Add(p => p.StackOverflow, "http://stackoverflow.com")
                .Add(p => p.Medium, "http://medium.com")
                .Add(p => p.Github, "http://github.com")
                .Add(p => p.YouTube, "http://youtube.com")
                .Add(p => p.LinkedIn, "http://linkedin.com")
            );

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