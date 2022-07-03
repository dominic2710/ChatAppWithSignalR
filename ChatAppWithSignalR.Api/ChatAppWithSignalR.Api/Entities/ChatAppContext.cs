using Microsoft.EntityFrameworkCore;

namespace ChatAppWithSignalR.Api.Entities
{
    public class ChatAppContext:DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> options) :base (options)
        { }

        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblUserFriend> TblUserFriends { get; set; } = null!;
        public virtual DbSet<TblMessage> TblMessages { get; set; } = null!;
    }
}
