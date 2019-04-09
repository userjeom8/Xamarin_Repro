using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompucareWard.Events;
using Foundation;
using Microsoft.AppCenter.Push;
using Prism.Events;
using UIKit;
using UserNotifications;

namespace CompucareWard.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private IEventAggregator _eventAggregator;

        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::DevExpress.Mobile.Forms.Init();
            global::Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();

            #if ENABLE_TEST_CLOUD
            // requires Xamarin Test Cloud Agent
            Xamarin.Calabash.Start();
            #endif

            // Ask the user for permission to get notifications on iOS 10.0+
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                (approved, error) => Console.WriteLine(approved));

            var application = new App(new iOSInitializer());
            _eventAggregator = application.Container.Resolve(typeof(IEventAggregator)) as IEventAggregator;
            // Watch for notifications while app is active
            //UNUserNotificationCenter.Current.Delegate = new CustomUNUserNotificationCenterDelegate(_eventAggregator);
            LoadApplication(application);

            return base.FinishedLaunching(app, options);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            var result = Push.DidReceiveRemoteNotification(userInfo);

            if (result)
            {
                if (userInfo["aps"] is NSDictionary notification && int.TryParse(notification["badge"]?.ToString(), out int badge))
                {
                    _eventAggregator.GetEvent<NotificationReceived>().Publish(badge);
                    completionHandler?.Invoke(UIBackgroundFetchResult.NewData);
                }
            }
            else
            {
                completionHandler?.Invoke(UIBackgroundFetchResult.NoData);
            }
        }
    }
}
