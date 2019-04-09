using Foundation;
using System;
using UIKit;
using UserNotifications;
using System.Linq;
using CompucareWard.Notifications;
using CompucareWard.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNotificationsImplementation))]
namespace CompucareWard.iOS
{
    public class LocalNotificationsImplementation : ILocalNotifications
    {
        public LocalNotificationsImplementation()
        {

        }

        public void UpdateBadge(int badge)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = badge;
        }

        void ILocalNotifications.Show(string title, string body, int badge, int id, double timeInterval)
        {
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(timeInterval, false);
            ShowUserNotification(title, body, id, badge, trigger);
        }

        void ShowUserNotification(string title, string body, int id, int badge, UNNotificationTrigger trigger)
        {
            if (badge == 0)
                UpdateBadge(0);
            else
            {
                var content = new UNMutableNotificationContent()
                {
                    Title = title,
                    Body = body,
                    Badge = new NSNumber(badge),
                    Sound = UNNotificationSound.Default
                };

                var request = UNNotificationRequest.FromIdentifier(id.ToString(), content, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (error) => System.Diagnostics.Debug.WriteLine("Error: {0}", error));
            }
        }
    }
}
