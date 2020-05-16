using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot
{
    public class EventManager
    {
        public static event Action<string, string, BalloonIcon> NotifyEvent;

        public static void Notify(string title, string text, BalloonIcon balloonIcon)
        {
            NotifyEvent?.Invoke(title, text, balloonIcon);
        }
    }
}
