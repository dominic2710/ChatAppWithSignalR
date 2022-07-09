namespace ChatAppWithSignalR.Client.Pages;

public partial class ListChatPage : ContentPage
{
	public ListChatPage(ListChatPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(this.BindingContext as ListChatPageViewModel).Initialize();
	}
}