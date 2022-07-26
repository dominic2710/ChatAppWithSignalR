using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.Platforms.Android
{
    [Service(Label = "Message Foreground Service")]
    public class MessageForegroundService: Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            CreateNotificationChannel();
            DispatchNotificationThatServiceIsRunning();

            return StartCommandResult.Sticky;
        }

        const string SILENT_CHANNEL_ID = "999902";
        const string SILENT_CHANNEL_NAME = "Message Foreground Service";

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notication channels are new in API 26
                // There is no need to create a notification channel on older versions
                return;
            }

            var channel = new NotificationChannel(SILENT_CHANNEL_ID, SILENT_CHANNEL_NAME,
                NotificationImportance.Default);

            channel.LockscreenVisibility = NotificationVisibility.Secret;
            channel.SetSound(null, null);
            channel.EnableVibration(false);

            var notificationManager = GetSystemService(NotificationService) as NotificationManager;
            notificationManager.CreateNotificationChannel(channel);
        }

        void DispatchNotificationThatServiceIsRunning()
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(
                this, SILENT_CHANNEL_ID)
                .SetContentTitle("Chat App with SignalR")
                .SetContentText("Foreground service is running.")
                .SetSound(null)
                .SetVibrate(null)
                .SetSmallIcon(Resource.Drawable.dotnet_bot);

            StartForeground(1, builder.Build());
        }
    }
}
