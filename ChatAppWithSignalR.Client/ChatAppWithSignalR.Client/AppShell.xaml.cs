using ChatAppWithSignalR.Client.Pages;

namespace ChatAppWithSignalR.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("ListChatPage", typeof(ListChatPage));
	}
}
