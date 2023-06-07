using System;
using System.Windows;

namespace SIMSProject
{
    public partial class App : System.Windows.Application
    {
        public string CurrentTheme { get; set; } = "Light";

        public void SwitchTheme(string themeName)
        {
            // Clear the existing resources.
            Resources.MergedDictionaries.Clear();

            // Create a new resource dictionary.
            var dictionary = new ResourceDictionary();

            // Load the theme.
            if (themeName == "Light")
                dictionary.Source = new Uri("pack://application:,,,/WPF/Themes/LightTheme.xaml");
            else if (themeName == "Dark")
                dictionary.Source = new Uri("pack://application:,,,/WPF/Themes/DarkTheme.xaml");

            // Add the new dictionary to the application resources.
            Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
