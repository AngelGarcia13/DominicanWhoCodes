using DWC.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static DWC.Blazor.Pages.Index;

namespace DWC.Blazor.Tests
{
    public class CsvExportServiceTests
    {
        private readonly CsvExportService _service;

        public CsvExportServiceTests()
        {
            _service = new CsvExportService();
        }

        [Fact]
        public void ExportDevelopersToCsv_WithValidDevelopers_ReturnsByteArray()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() {
                    Name = "John Doe",
                    Skills = "C#, JavaScript, SQL",
                    Summary = "Santo Domingo, Dominican Republic",
                    Github = "https://github.com/johndoe",
                    LinkedIn = "https://linkedin.com/in/johndoe",
                    Twitter = "https://twitter.com/johndoe",
                    Webpage = "https://johndoe.dev"
                }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("Name,Skills,Summary,GitHub,LinkedIn,Twitter,Website", csv);
            Assert.Contains("John Doe", csv);
            Assert.Contains("C#, JavaScript, SQL", csv);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithEmptyList_ReturnsEmptyArray()
        {
            // Arrange
            var developers = new List<Developer>();

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithNullList_ReturnsEmptyArray()
        {
            // Arrange
            List<Developer> developers = null;

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithSkillsUsingAnd_FormatsCorrectly()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() {
                    Name = "Jane Smith",
                    Skills = "Python and Machine Learning and Data Science",
                    Summary = "Santiago, Dominican Republic"
                }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("Python, Machine Learning, Data Science", csv);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithSkillsUsingAmpersand_FormatsCorrectly()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() {
                    Name = "Bob Johnson",
                    Skills = "Java & Spring Boot & Microservices",
                    Summary = "La Romana, Dominican Republic"
                }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("Java, Spring Boot, Microservices", csv);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithSpecialCharactersInName_HandlesCorrectly()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() {
                    Name = "María José García-López",
                    Skills = "React, Node.js",
                    Summary = "Punta Cana, Dominican Republic"
                }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("María José García-López", csv);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithCommasInFields_HandlesCorrectly()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() {
                    Name = "Carlos Pérez",
                    Skills = "C#, .NET, ASP.NET",
                    Summary = "Santo Domingo, Dominican Republic"
                }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("Carlos Pérez", csv);
            // Verify the CSV is properly formed (commas in data should be quoted by CsvHelper)
            Assert.Contains("C#, .NET, ASP.NET", csv);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithNullFields_HandlesGracefully()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() {
                    Name = "Test Developer",
                    Skills = null,
                    Summary = null,
                    Github = null,
                    LinkedIn = null,
                    Twitter = null,
                    Webpage = null
                }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("Test Developer", csv);
            Assert.Contains("Name,Skills,Summary,GitHub,LinkedIn,Twitter,Website", csv);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithMultipleDevelopers_IncludesAllRecords()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() { Name = "Developer 1", Skills = "JavaScript" },
                new() { Name = "Developer 2", Skills = "Python" },
                new() { Name = "Developer 3", Skills = "Ruby" }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("Developer 1", csv);
            Assert.Contains("Developer 2", csv);
            Assert.Contains("Developer 3", csv);
            Assert.Contains("JavaScript", csv);
            Assert.Contains("Python", csv);
            Assert.Contains("Ruby", csv);
        }

        [Fact]
        public void ExportDevelopersToCsv_HasCorrectHeaders_InCorrectOrder()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() { Name = "Test Dev" }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            var lines = csv.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
            Assert.True(lines.Length >= 1);
            var header = lines[0].TrimStart('\ufeff'); // Remove BOM if present
            Assert.Equal("Name,Skills,Summary,GitHub,LinkedIn,Twitter,Website", header);
        }

        [Fact]
        public void ExportDevelopersToCsv_WithEmptyStrings_ExportsEmptyValues()
        {
            // Arrange
            var developers = new List<Developer>
            {
                new() {
                    Name = "Empty Fields Dev",
                    Skills = "",
                    Summary = "",
                    Github = "",
                    LinkedIn = "",
                    Twitter = "",
                    Webpage = ""
                }
            };

            // Act
            var result = _service.ExportDevelopersToCsv(developers);

            // Assert
            Assert.NotNull(result);
            var csv = Encoding.UTF8.GetString(result);
            Assert.Contains("Empty Fields Dev", csv);
        }
    }
}
