using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AdPlatformService.Services
{
    public interface IAdsService
    {
        Task UploadFileAsync(IFormFile file);
        Task<string> SearchAsync(string searchInput);
    }

    public class AdsService : IAdsService
    {
        private byte[]? _fileData;

        public async Task UploadFileAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            _fileData = ms.ToArray();
        }

        public async Task<string> SearchAsync(string searchInput)
        {
            if (!IsValidInput(searchInput, out var errorMessage))
                return errorMessage!;

            var companies = await GetMatchingCompaniesAsync(searchInput);
            return FormatResult(companies);
        }

        #region Testable Helpers

        protected virtual bool IsValidInput(string searchInput, out string? errorMessage)
        {
            if (_fileData == null || _fileData.Length == 0)
            {
                errorMessage = "File not uploaded!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(searchInput))
            {
                errorMessage = "Invalid input!";
                return false;
            }

            errorMessage = null;
            return true;
        }

        protected virtual async Task<HashSet<string>> GetMatchingCompaniesAsync(string searchInput)
        {
            var inputParts = searchInput.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(p => p.Trim())
                                        .ToArray();

            var resultSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using var ms = new MemoryStream(_fileData!);
            using var reader = new StreamReader(ms);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var (companyName, locations) = ParseLine(line);
                if (companyName == null) continue;

                foreach (var loc in locations)
                {
                    if (IsLocationMatch(inputParts, loc))
                    {
                        resultSet.Add(companyName);
                        break;
                    }
                }
            }

            return resultSet;
        }

        protected virtual (string? companyName, string[] locations) ParseLine(string line)
        {
            int index = line.IndexOf(':');
            if (index < 0 || index >= line.Length - 1)
                return (null, Array.Empty<string>());

            string companyName = line.Substring(0, index).Trim();
            string[] locations = line.Substring(index + 1)
                                     .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                     .Select(l => l.Trim())
                                     .ToArray();

            return (companyName, locations);
        }

        protected virtual bool IsLocationMatch(string[] inputParts, string location)
        {
            var locParts = location.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(p => p.Trim())
                                   .ToArray();

            if (inputParts.Length < locParts.Length)
                return false;

            for (int i = 0; i < locParts.Length; i++)
            {
                if (!locParts[i].Equals(inputParts[i], StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        protected virtual string FormatResult(HashSet<string> companies)
        {
            if (companies.Count == 0)
                return "No results found!";

            var sb = new StringBuilder();
            foreach (var company in companies)
                sb.AppendLine(company);

            return sb.ToString();
        }

        #endregion
    }
}
