using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.Platforms.Android
{
    [Service(Label = "Message Notification Service")]
    public class MessageNotificationService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            switch (intent.Action)
            {
                case "StartService":
                    CreateNotificationChannel();
                    break;
                case "Notify":
                    var param = intent.GetStringArrayListExtra("param");
                    Notify(param[0], param[1]);
                    break;
                default:
                    break;
            }

            return StartCommandResult.Sticky;
        }

        const string NOTIFICATION_CHANNEL_ID = "999901";

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notication channels are new in API 26
                // There is no need to create a notification channel on older versions
                return;
            }

            var channelName = "Message Notification";
            var channelDescription = "Push notication when receive new messages";
            var channel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, channelName,
                NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = GetSystemService(NotificationService) as NotificationManager;
            notificationManager.CreateNotificationChannel(channel);
        }

        void Notify(string userName, string message)
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(
                this, NOTIFICATION_CHANNEL_ID)
                .SetContentTitle(userName)
                .SetContentText(message)
                .SetDefaults((int)NotificationDefaults.Sound)
                .SetSmallIcon(Resource.Drawable.dotnet_bot);

            Notification notification = builder.Build();

            var notificationManager = GetSystemService(NotificationService) as NotificationManager;
            notificationManager.Notify(0, notification);
        }
    }
}
