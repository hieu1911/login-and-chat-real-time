using LoginAndChatRealTime.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LoginAndChatRealTime.Hubs
{
    public class ChatHub : Hub
    {
        private IMessageSerivce _messageService;

        public ChatHub(IMessageSerivce messageSerivce) : base()
        {
            _messageService = messageSerivce;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string userName, string message, int senderId, int recieveId)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", userName, message);

            _messageService.CreateMessage(senderId, recieveId, message);
        }
    }
}
