using MahApps.Metro.Controls;
using MakCraft.ViewModels;
using NHotkey.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace Screenshot
{
    public class SettingViewModel : ViewModelBase
    {

        private readonly ScreenService _screenService = null;

        public SettingViewModel()
        {
            _screenService = new ScreenService();
            var kgvs = new KeyGestureValueSerializer();
            if (kgvs.ConvertFromString(Properties.Settings.Default.ShortcutKey, null) is KeyGesture kg)
            {
                HotKey = new HotKey(kg.Key, kg.Modifiers);
            }
            else
            {
                HotKey = new HotKey(Key.E, ModifierKeys.Control);
            }

            PictureDir = Properties.Settings.Default.PicDir;
            IsNotifyWhenSuccess = Properties.Settings.Default.IsNotifyWhenSuccess;
            UpdateDisplayItems();
        }

        public void UpdateDisplayItems()
        {
            DisplayItems = _screenService.GetDisplayItems();
        }

        private HotKey _hotKey = null;

        public HotKey HotKey
        {
            get => _hotKey;
            set
            {
                if (_hotKey != value)
                {
                    _hotKey = value;
                    if (_hotKey != null && _hotKey.Key != Key.None)
                    {
                        HotkeyManager.Current.AddOrReplace("ScreenshotAndSave", HotKey.Key, HotKey.ModifierKeys, 
                            (sender, e) => _screenService.OnScreenshotAndSave());
                    }
                    else
                    {
                        HotkeyManager.Current.Remove("ScreenshotAndSave");
                    }
                    RaisePropertyChanged("HotKey");
                }
            }
        }

        

        private string _pictureDir = null;

        public string PictureDir
        {
            get => _pictureDir;
            set { SetProperty(ref _pictureDir, value); }
        }


        private bool _isNotifyWhenSuccess = false;

        public bool IsNotifyWhenSuccess
        {
            get => _isNotifyWhenSuccess;
            set { SetProperty(ref _isNotifyWhenSuccess, value); }
        }

        private List<DisplayItem> _displayItems;

        public List<DisplayItem> DisplayItems
        {
            get => _displayItems;
            set { SetProperty(ref _displayItems, value); }
        }

        public RelayCommand SelectPictureDirCommand => new RelayCommand(() =>
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (!string.IsNullOrWhiteSpace(PictureDir))
            {
                dialog.SelectedPath = PictureDir;
            }
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            PictureDir = dialog.SelectedPath.Trim();
        });


        public RelayCommand SaveCommand => new RelayCommand(() =>
        {
            Properties.Settings.Default.ShortcutKey = _hotKey.ToString();
            Properties.Settings.Default.PicDir = PictureDir;
            Properties.Settings.Default.IsNotifyWhenSuccess = IsNotifyWhenSuccess;
            Properties.Settings.Default.Displays = new System.Collections.Specialized.StringCollection();
            DisplayItems.Where(o => o.IsChecked).Select(o => o.DisplayName).ToList().ForEach(o =>
            {
                Properties.Settings.Default.Displays.Add(o);
            });
            Properties.Settings.Default.Save();
        });


        
    }
}
