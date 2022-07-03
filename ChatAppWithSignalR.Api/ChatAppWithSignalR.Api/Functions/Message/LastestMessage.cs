namespace ChatAppWithSignalR.Api.Functions.Message
{
    public class LastestMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User.User UserFriendInfo { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
