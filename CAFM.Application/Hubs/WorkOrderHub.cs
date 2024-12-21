using Microsoft.AspNetCore.SignalR;

namespace CAFM.Application.Hubs
{
    public class WorkOrderHub : Hub
    {
        public async Task Notify(string message)
        {
            await Clients.All.SendAsync("ReceiveWorkOrderUpdate", message);
        }
    }
}
