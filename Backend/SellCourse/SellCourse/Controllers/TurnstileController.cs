using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers
{
    public class TurnstileController : Controller
    {
        private readonly IConfiguration _configuration;

        // Inject IConfiguration vào controller
        public TurnstileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("verify-turnstile")]
        public async Task<IActionResult> VerifyTurnstile(string token)
        {
            var secretKey = _configuration["Cloudflare:Turnstile:SecretKey"];
            var apiUrl = _configuration["Cloudflare:Turnstile:ApiUrl"];

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", secretKey),
                new KeyValuePair<string, string>("response", token)
            }));

            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }
    }
}
