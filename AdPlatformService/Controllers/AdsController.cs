using System.Threading.Tasks;
using AdPlatformService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdPlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdsService _adsService;

        public AdsController(IAdsService adsService)
        {
            _adsService = adsService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected.");

            await _adsService.UploadFileAsync(file);
            return Ok(new { message = "The file was uploaded successfully." });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("The search query cannot be empty.");

            var result = await _adsService.SearchAsync(query);

            // If the result is a string, split it into lines
            var lines = result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            return Ok(new { result = lines });
        }

    }
}
