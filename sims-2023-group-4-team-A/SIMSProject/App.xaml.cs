using System;
using System.Windows;

namespace SIMSProject
{
    public partial class App : System.Windows.Application
    {
        public string CurrentTheme { get; set; } = "Light";

        public void SwitchTheme(string themeName)
        {
            Resources.MergedDictionaries.Clear();

            var dictionary = new ResourceDictionary();

            if (themeName == "Light")
                dictionary.Source = new Uri("pack://application:,,,/WPF/Themes/LightTheme.xaml");
            else if (themeName == "Dark")
                dictionary.Source = new Uri("pack://application:,,,/WPF/Themes/DarkTheme.xaml");

            Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
