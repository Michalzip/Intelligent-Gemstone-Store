using System;
using Microsoft.AspNetCore.SignalR;

namespace IntelligentStore.SignalR.Services
{
    public interface IWebSocketService
    {
        public void ConnectAdmin(HubCallerContext context);
        public void DisconnectAdmin(HubCallerContext context);
        public  Task SendMessageAsync(IHubCallerClients clients, HubCallerContext context, List<object> message);
    }
}

