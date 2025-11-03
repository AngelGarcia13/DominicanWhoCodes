using System;

namespace DWC.Blazor.Utils
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationResult(bool isValid, string errorMessage = "")
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }

    public static class UrlValidator
    {
        public static ValidationResult ValidateUrl(string url, string platform)
        {
            if (string.IsNullOrWhiteSpace(url))
                return new ValidationResult(false, "URL cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(platform))
                return new ValidationResult(false, "Platform cannot be null or empty.");

            // Trim the URL
            var trimmedUrl = url.Trim();

            // Validate URI format and scheme
            if (!Uri.TryCreate(trimmedUrl, UriKind.Absolute, out Uri uriResult))
                return new ValidationResult(false, "URL is not a valid URI format.");

            // Check if scheme is http or https (part of format validation)
            if (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                return new ValidationResult(false, "URL is not a valid URI format.");

            // Validate HTTPS protocol
            if (uriResult.Scheme != Uri.UriSchemeHttps)
                return new ValidationResult(false, "URL must use HTTPS protocol.");

            // Normalize platform name
            var normalizedPlatform = platform.Trim().ToLowerInvariant();

            // Platform-specific validation
            var host = uriResult.Host.ToLowerInvariant();
            var pathAndQuery = uriResult.PathAndQuery.ToLowerInvariant();

            switch (normalizedPlatform)
            {
                case "linkedin":
                    if (!(host == "linkedin.com" || host.EndsWith(".linkedin.com")))
                        return new ValidationResult(false, "LinkedIn URL must contain 'linkedin.com/in/' or 'linkedin.com/company/'.");

                    if (!pathAndQuery.Contains("/in/") && !pathAndQuery.Contains("/company/"))
                        return new ValidationResult(false, "LinkedIn URL must contain 'linkedin.com/in/' or 'linkedin.com/company/'.");
                    break;

                case "github":
                    if (!(host == "github.com" || host.EndsWith(".github.com")))
                        return new ValidationResult(false, "GitHub URL must contain 'github.com/'.");
                    break;

                case "twitter":
                    if (!(host == "twitter.com" || host.EndsWith(".twitter.com") ||
                          host == "x.com" || host.EndsWith(".x.com")))
                        return new ValidationResult(false, "Twitter URL must contain 'twitter.com/' or 'x.com/'.");
                    break;

                case "telegram":
                    if (!host.Equals("t.me"))
                        return new ValidationResult(false, "Telegram URL must contain 't.me/'.");
                    break;

                case "stackoverflow":
                    if (!(host == "stackoverflow.com" || host.EndsWith(".stackoverflow.com")))
                        return new ValidationResult(false, "StackOverflow URL must contain 'stackoverflow.com/users/'.");

                    if (!pathAndQuery.Contains("/users/"))
                        return new ValidationResult(false, "StackOverflow URL must contain 'stackoverflow.com/users/'.");
                    break;

                case "medium":
                    if (!(host == "medium.com" || host.EndsWith(".medium.com")))
                        return new ValidationResult(false, "Medium URL must contain 'medium.com/@'.");

                    if (!pathAndQuery.Contains("/@"))
                        return new ValidationResult(false, "Medium URL must contain 'medium.com/@'.");
                    break;

                case "youtube":
                    if (!(host == "youtube.com" || host.EndsWith(".youtube.com") ||
                          host == "youtu.be" || host.EndsWith(".youtu.be")))
                        return new ValidationResult(false, "YouTube URL must contain 'youtube.com/' or 'youtu.be/'.");
                    break;

                case "webpage":
                    // Any valid HTTPS URL is acceptable for webpage
                    break;

                default:
                    return new ValidationResult(false, $"Unknown platform: {platform}.");
            }

            return new ValidationResult(true);
        }
    }
}
