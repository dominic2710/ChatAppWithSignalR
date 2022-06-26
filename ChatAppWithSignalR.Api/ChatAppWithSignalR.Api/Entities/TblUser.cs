namespace ChatAppWithSignalR.Api.Entities
{
    public class TblUser
    {
        public int Id { get; set; }
        public string LoginId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[] StoredSalt { get; set; } = null!;
        public string AvatarSourceName { get;set; } = null!;
        public bool IsOnline { get; set; }
        public DateTime LastLogonTime { get; set; }
    }
}
