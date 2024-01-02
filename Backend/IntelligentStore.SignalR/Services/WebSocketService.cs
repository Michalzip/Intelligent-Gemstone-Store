using IntelligentStore.SignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace IntelligentStore.SignalR.Services
{
    public class WebSocketService : IWebSocketService
    {
        public static readonly List<AdminDetail> _Connections = new List<AdminDetail>();

        public void ConnectAdmin(HubCallerContext context)
        {
            _Connections.Add(new AdminDetail() { ConnectionId = context.ConnectionId });
        }

        public void DisconnectAdmin(HubCallerContext context)
        {
             var admin = _Connections.Where(u => u.ConnectionId == context.ConnectionId).First();

            _Connections.Remove(admin);
        }

        public async Task SendMessageAsync(
            IHubCallerClients clients,
            HubCallerContext context,
            List<object> message
        )
        {
            await clients.Client(context.ConnectionId).SendAsync("clientGotMessage", message);
        }
    }
}
