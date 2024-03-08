namespace LoginAndChatRealTime.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int RecieveId { get; set; }

        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
