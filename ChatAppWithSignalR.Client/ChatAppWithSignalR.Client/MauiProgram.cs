

namespace ChatAppWithSignalR.Client;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "IconFontTypes");
            });

		builder.Services.AddSingleton<ChatHub>();
		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<ListChatPage>();
        builder.Services.AddSingleton<ChatPage>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<ListChatPageViewModel>();
		builder.Services.AddSingleton<ChatPageViewModel>();
		builder.Services.AddSingleton<ServiceProvider>();

        return builder.Build();
	}
}
