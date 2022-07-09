namespace ChatAppWithSignalR.Client.Pages;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(this.BindingContext as ChatPageViewModel).Initialize();
	}
}