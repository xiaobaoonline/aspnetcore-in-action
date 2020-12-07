using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRJavaScriptDemo
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
