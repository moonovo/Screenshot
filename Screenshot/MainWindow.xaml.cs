using Hardcodet.Wpf.TaskbarNotification;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Screenshot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private SettingView _settingView;
        private SettingViewModel _settingViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _settingViewModel = new SettingViewModel();
            _settingView = new SettingView(_settingViewModel);
            EventManager.NotifyEvent += Notify;
        }

        private void Notify(string title, string text, BalloonIcon balloonIcon)
        {
            if (balloonIcon != BalloonIcon.Info || Properties.Settings.Default.IsNotifyWhenSuccess)
            {
                notifyIcon.ShowBalloonTip(title, text, balloonIcon);
            }
        }

        private void SettingMenuItemClick(object sender, RoutedEventArgs e)
        {
            _settingViewModel.UpdateDisplayItems();
            _settingView.Show();
            _settingView.Activate();
        }

        private void CloseMenuItemClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
