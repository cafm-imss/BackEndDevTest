using Microsoft.AspNetCore.SignalR;
namespace CAFM.Core.Hubs
{ 
    public class WorkOrderHub : Hub
    {
        // Notify clients of a work order update
        public async Task NotifyWorkOrderUpdate(string workOrderId, string message)
        {
            await Clients.All.SendAsync("WorkOrderUpdated", workOrderId, message);
        }

        // Example method for client subscription by company/location
        public async Task Subscribe(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task Unsubscribe(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SubscribeToCompany(string companyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Company-{companyId}");
        }

        public async Task SubscribeToLocation(string locationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Location-{locationId}");
        }
    }
}
