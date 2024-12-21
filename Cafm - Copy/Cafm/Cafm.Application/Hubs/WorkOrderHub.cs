using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Cafm.Application.Hubs
{

    public class WorkOrderHub : Hub
    {
        // Send notifications to clients
        public async Task SendWorkOrderUpdate(string companyId, string locationId, string message)
        {
            await Clients.Group($"{companyId}_{locationId}").SendAsync("ReceiveWorkOrderUpdate", message);
        }

        // Subscribe to location/company updates
        public async Task SubscribeToUpdates(string companyId, string locationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{companyId}_{locationId}");
        }

        // Unsubscribe from location/company updates
        public async Task UnsubscribeFromUpdates(string companyId, string locationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{companyId}_{locationId}");
        }
    }

}
