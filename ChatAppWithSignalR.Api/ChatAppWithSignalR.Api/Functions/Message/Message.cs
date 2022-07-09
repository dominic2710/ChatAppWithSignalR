namespace ChatAppWithSignalR.Api.Functions.Message
{
    public class Message
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; }
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
