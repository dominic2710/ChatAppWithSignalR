using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using ChatAppWithSignalR.Client.Platforms.Android;

namespace ChatAppWithSignalR.Client;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        MessagingCenter.Unsubscribe<string, string[]>(this, "MessageNotificationService");
        MessagingCenter.Unsubscribe<string, string[]>(this, "MessageForegroundService");
        MessagingCenter.Subscribe<string, string[]>(this, "MessageNotificationService", (sender, args) =>
        {
            var serviceIntent = new Intent(this, typeof(MessageNotificationService));
            serviceIntent.SetAction(sender);
            serviceIntent.PutStringArrayListExtra("param", args);
            StartService(serviceIntent);
        });
        MessagingCenter.Subscribe<string>(this, "MessageForegroundService", value =>
        {
            var serviceIntent = new Intent(this, typeof(MessageForegroundService));
            StartForegroundService(serviceIntent);
        });
        base.OnCreate(savedInstanceState);
    }
}
