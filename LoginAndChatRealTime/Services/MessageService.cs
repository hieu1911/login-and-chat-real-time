using LoginAndChatRealTime.Entities;
using LoginAndChatRealTime.Helper;
using LoginAndChatRealTime.Interfaces;

namespace LoginAndChatRealTime.Services
{
    public class MessageService : IMessageSerivce
    {
        public void CreateMessage(int senderId, int recieveId, string content)
        {
            using (var db = new MyDbContext())
            {
                var message = new Message()
                {
                    SenderId = senderId,
                    RecieveId = recieveId,
                    Content = content,
                    TimeStamp = DateTime.Now
                };

                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        public List<Message> GetMessages(int senderId, int recieveId)
        {
            using (var db = new MyDbContext())
            {
                var messages = db.Messages.Where<Message>(m => 
                (m.SenderId == senderId &&
                m.RecieveId == recieveId) ||
                (m.SenderId == recieveId && 
                m.RecieveId == senderId)).ToList();

                return messages;
            }
        }
    }
}
