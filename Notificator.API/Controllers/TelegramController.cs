using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Notificator.API.Models;
using System.Text;

namespace Notificator.API.Controllers
{
    [Route("api/telegram")]
    [ApiController]
    public class TelegramController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private string URL_BASE =
            $"https://api.telegram.org/bot{Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN")}/";

        public TelegramController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("useradded/{username}")]
        public async Task<IActionResult> IsUserRegistered(string username)
        {
            if (await IsValidTelegramToken() == false)
                return StatusCode(500, new { error = "Invalid telegram token" });

            bool isAdded = false;
            string? chatId = await GetChatIdFromUsername(username);

            if (chatId != null)
                isAdded = true;

            return Ok(new { isAdded });
        }

        [HttpPost("send")]
        public async Task<IActionResult> sendMessage(TelegramMessage telegramMessage)
        {
            if (await IsValidTelegramToken() == false)
                return StatusCode(500, new { error = "Invalid telegram token"});

            string? chatId = await GetChatIdFromUsername(telegramMessage.username);

            if (chatId == null)
                return BadRequest(new { error = "Telegram chat not found" });

            using StringContent jsonContent = new(
                JsonConvert.SerializeObject(new
                {
                    chat_id = chatId,
                    text = telegramMessage.text,
                }),
                Encoding.UTF8,
                "application/json");

            using (var client = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await client.PostAsync(
                    URL_BASE + "sendMessage", jsonContent);
            }

            return Ok();
        }

        private async Task<string?> GetChatIdFromUsername(string username)
        {
            string? chatId = null;

            using (var client = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync(URL_BASE + "getUpdates");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                dynamic? jsonObj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                foreach (var update in jsonObj["result"])
                {
                    if (update["message"]["chat"]["username"] == username)
                    {
                        chatId = update["message"]["chat"]["id"];
                        break;
                    }
                }

                return chatId;
            }
        }

        private async Task<Boolean> IsValidTelegramToken()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(URL_BASE + "getMe");
                if (response.IsSuccessStatusCode)
                    return true;

                return false;
            }
        }
    }
}
