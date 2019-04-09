using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Notifications
{
    public interface ILocalNotifications
    {
        void Show(string title, string body, int badge, int id, double timeInterval);
        void UpdateBadge(int badge);
    }
}
