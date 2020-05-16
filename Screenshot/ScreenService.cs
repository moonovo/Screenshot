using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Screenshot
{
    public class ScreenService
    {

        private string PictureDir
        {
            get => Properties.Settings.Default.PicDir;
        }

        private bool CheckPicDir()
        {
            if (!Directory.Exists(PictureDir))
            {
                EventManager.Notify("警告", "保存文件夹路径无效", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Warning);
                return false;
            }
            return true;
        }

        public void OnScreenshotAndSave()
        {
            if (!CheckPicDir()) return;
            //double screenLeft = SystemParameters.VirtualScreenLeft;
            //double screenTop = SystemParameters.VirtualScreenTop;
            //double screenWidth = SystemParameters.VirtualScreenWidth;
            //double screenHeight = SystemParameters.VirtualScreenHeight;

            //using (Bitmap bmp = new Bitmap((int)screenWidth, (int)screenHeight))
            //{
            //    using (Graphics g = Graphics.FromImage(bmp))
            //    {
            //        var filename = "ScreenCapture-" + DateTime.Now.ToString("ddMMyyyy-hhmmssms") + ".png";
            //        g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);
            //        bmp.Save(Path.Combine(PictureDir, filename));
            //        EventManager.Notify("提示", $"已保存截图{filename}", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
            //    }
            //}

            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (!Properties.Settings.Default.Displays.Contains(screen.DeviceName)) continue;

                using (var screenshot = new Bitmap(screen.Bounds.Width,
                    screen.Bounds.Height,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    // Create a graphics object from the bitmap
                    using (var gfxScreenshot = Graphics.FromImage(screenshot))
                    {
                        // Take the screenshot from the upper left corner to the right bottom corner
                        gfxScreenshot.CopyFromScreen(
                            screen.Bounds.X,
                            screen.Bounds.Y,
                            0,
                            0,
                            screen.Bounds.Size,
                            CopyPixelOperation.SourceCopy);
                        // Save the screenshot
                        var filename = $"ScreenCapture-" +
                            $"{DateTime.Now.ToString("ddMMyyyy-hhmmssms")}-" +
                            $"{FormatDeviceName(screen.DeviceName)}.png";
                        screenshot.Save(Path.Combine(PictureDir, filename));
                    }
                }
            }
            EventManager.Notify("提示", $"已保存截图", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
        }

        private string FormatDeviceName(string deviceName)
        {
            if (string.IsNullOrWhiteSpace(deviceName)) return deviceName;
            var newName = string.Empty;
            foreach (var c in deviceName)
            {
                if (c >= 'a' && c <= 'z' 
                    || c >= 'A' && c <= 'Z' 
                    || c >= '0' && c <= '9')
                {
                    newName += c;
                }
                else if (newName.LastOrDefault() != '-' 
                         && newName.LastOrDefault() != '_'
                         && newName.LastOrDefault() != default(char))
                {
                    newName += '_';
                }
            }
            return newName;
        }

        public List<DisplayItem> GetDisplayItems()
        {
            List<DisplayItem> items = new List<DisplayItem>();
            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                var item = new DisplayItem
                {
                    DisplayName = screen.DeviceName,
                    FormatDisplayName = FormatDeviceName(screen.DeviceName) + (screen.Primary ? "(主屏幕)" : ""),
                };
                if (Properties.Settings.Default.Displays != null)
                {
                    item.IsChecked = Properties.Settings.Default.Displays.Contains(screen.DeviceName);
                }
                items.Add(item);
            }
            return items;
        }
    }
}
