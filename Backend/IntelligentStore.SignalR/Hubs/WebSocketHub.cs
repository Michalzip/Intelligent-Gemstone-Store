using System.Text.Json;
using IntelligentStore.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace IntelligentStore.SignalR.Hubs
{
    public class WebSocketHub : Hub
    {
        private readonly IWebSocketService _webSocketService;

        public WebSocketHub(IWebSocketService webSocketService)
        {
            _webSocketService = webSocketService;
        }

        public override Task OnConnectedAsync()
        {
            _webSocketService.ConnectAdmin(Context);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _webSocketService.DisconnectAdmin(Context);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendGemstoneDetailMessage(List<object> message)
        {
            List<Gemstone> gemstones = new List<Gemstone>();

            foreach (object item in message)
            {
                if (item is JsonElement jsonElement)
                {
                    Gemstone gemstone = JsonConvert.DeserializeObject<Gemstone>(
                        jsonElement.ToString()
                    );
                    gemstones.Add(gemstone);
                }
            }

            string json = JsonConvert.SerializeObject(gemstones);

            await Clients.All.SendAsync("clientgotmessage", json);
        }
    }

    public class Gemstone
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CurrentPrice { get; set; }
        public string DiscountPrice { get; set; }
        public string OriginalPrice { get; set; }
        public string FeedbackPercentage { get; set; }
        public int FeedbackScore { get; set; }
        public double ProfitabilityResult { get; set; }
    }
}
