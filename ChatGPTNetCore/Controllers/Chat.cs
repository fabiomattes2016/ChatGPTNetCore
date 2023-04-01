using ChatGPTNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChatGPTNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Chat : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public Chat(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("Ask")]
        public async Task<IActionResult> Ask(string query)
        {
            string apikey = "sua-api-key";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apikey);

            var model = new ChatGptInputModel(query);

            var requestBody = JsonSerializer.Serialize(model);

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            var result = await response.Content.ReadFromJsonAsync<ChatGptViewModel>();

            Choice? promptResponse = null;

            if (result is not null)
            {
                promptResponse = result.choices.First();
            }

            if(promptResponse is null)
            {
                return NotFound("Ocorreu algum problema e sua busca não retornou valores");
            }

            return Ok(promptResponse.text.Replace("\n", "").Replace("\t", ""));
        }
    }
}
