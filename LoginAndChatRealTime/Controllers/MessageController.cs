using LoginAndChatRealTime.Interfaces;
using LoginAndChatRealTime.Models;
using LoginAndChatRealTime.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoginAndChatRealTime.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpContextAccessor _context;

        private readonly IMessageSerivce _messageService;

        public MessageController(IHttpContextAccessor context, IMessageSerivce messageService) 
        {
            _context = context;
            _messageService = messageService;
        }

        public IActionResult Index(int id, string userName)
        {
            var context = _context.HttpContext;
            if (!context.User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }

            var senderId = int.Parse(context.User.FindFirst(type: "Id").Value);
            var senderName = context.User.FindFirst(ClaimTypes.Name).Value;

            var messages = _messageService.GetMessages(senderId, id);

            var reciever = new User()
            {
                Id = id,
                Name = userName
            };

            var sender = new User()
            {
                Id = senderId,
                Name = senderName
            };

            var groupName = "";
            if (id < senderId)
            {
                groupName = $"Group{id}{senderId}";
            }
            else
            {
                groupName = $"Group{senderId}{id}";
            }

            var messageViewModel = new MessageViewModel()
            {
                Messages = messages,
                Sender = sender,
                Reciever = reciever,
                GroupName = groupName
            };

            return View(messageViewModel);
        }
    }
}
