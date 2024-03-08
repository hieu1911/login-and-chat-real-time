using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Interfaces
{
    public interface IMessageSerivce
    {
        public List<Message> GetMessages(int senderId, int recieveId);

        public void CreateMessage(int senderId, int recieveId, string content);
    }
}
