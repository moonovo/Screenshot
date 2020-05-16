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
using System.Windows.Shapes;

namespace Screenshot
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : MetroWindow
    {
        public SettingView(SettingViewModel settingViewModel)
        {
            InitializeComponent();
            DataContext = settingViewModel;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
