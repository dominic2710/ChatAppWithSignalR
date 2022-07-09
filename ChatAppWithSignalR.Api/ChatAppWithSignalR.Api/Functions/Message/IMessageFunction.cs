namespace ChatAppWithSignalR.Api.Functions.Message
{
    public interface IMessageFunction
    {
        Task<IEnumerable<LastestMessage>> GetLastestMessage(int userId);

        Task<IEnumerable<Message>> GetMessages(int fromUserId, int toUserId);

        Task<int> AddMessage(int fromUserId, int toUserId, string message);
    }
}
