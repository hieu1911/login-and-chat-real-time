using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Models
{
    public class MessageViewModel
    {
        public User Sender { get; set; }

        public User Reciever { get; set; }

        public List<Message> Messages { get; set; }

        public string GroupName { get; set; }
    }
}
