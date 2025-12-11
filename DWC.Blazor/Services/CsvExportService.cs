using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using static DWC.Blazor.Pages.Index;

namespace DWC.Blazor.Services
{
    public class CsvExportService
    {
        public byte[] ExportDevelopersToCsv(List<Developer> developers)
        {
            if (developers == null || developers.Count == 0)
            {
                return Array.Empty<byte>();
            }

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(true)); // true = with BOM
            using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            });

            // Write CSV headers
            csvWriter.WriteField("Name");
            csvWriter.WriteField("Skills");
            csvWriter.WriteField("Summary");
            csvWriter.WriteField("GitHub");
            csvWriter.WriteField("LinkedIn");
            csvWriter.WriteField("Twitter");
            csvWriter.WriteField("Website");
            csvWriter.NextRecord();

            // Write developer data
            foreach (var developer in developers)
            {
                csvWriter.WriteField(developer.Name ?? string.Empty);
                csvWriter.WriteField(FormatSkills(developer.Skills));
                csvWriter.WriteField(developer.Summary ?? string.Empty);
                csvWriter.WriteField(developer.Github ?? string.Empty);
                csvWriter.WriteField(developer.LinkedIn ?? string.Empty);
                csvWriter.WriteField(developer.Twitter ?? string.Empty);
                csvWriter.WriteField(developer.Webpage ?? string.Empty);
                csvWriter.NextRecord();
            }

            streamWriter.Flush();
            return memoryStream.ToArray();
        }

        private static string FormatSkills(string skills)
        {
            if (string.IsNullOrWhiteSpace(skills))
            {
                return string.Empty;
            }

            // Replace common separators with commas for CSV
            return skills
                .Replace(" and ", ", ")
                .Replace(" & ", ", ")
                .Trim();
        }
    }
}
