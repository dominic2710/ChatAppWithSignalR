using ChatAppWithSignalR.Client.Pages;

namespace ChatAppWithSignalR.Client;

public partial class AppShell : Shell
{
    public AppShell(LoginPage loginPage)
    {
        InitializeComponent();

        Routing.RegisterRoute("ListChatPage", typeof(ListChatPage));
        Routing.RegisterRoute("ChatPage", typeof(ChatPage));

        this.CurrentItem = loginPage;
    }

    //public AppShell(ChatPage chatPage)
    //{
    //    InitializeComponent();

    //    Routing.RegisterRoute("ListChatPage", typeof(ListChatPage));

    //    this.CurrentItem = chatPage;
    //}
}
