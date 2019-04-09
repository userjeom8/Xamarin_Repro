using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompucareWard.Notifications;
using Foundation;
using Microsoft.AppCenter.Push;
using Prism;
using Prism.Ioc;
using UIKit;
using UserNotifications;

namespace CompucareWard.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILocalNotifications, LocalNotificationsImplementation>();
        }
    }
}
