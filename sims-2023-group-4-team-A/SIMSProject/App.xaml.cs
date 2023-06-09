using SIMSProject.WPF.Languages;
using System;
using System.Threading;
using System.Windows;

namespace SIMSProject
{
    public partial class App : System.Windows.Application
    {
        public string CurrentTheme { get; set; } = "Light";
        public string CurrentLanguage { get; set; } = "sr-LATN";

        public void ChangeTheme(string themeName)
        {
            Resources.MergedDictionaries.Clear();

            var dictionary = new ResourceDictionary();

            if (themeName == "Light")
                dictionary.Source = new Uri("pack://application:,,,/WPF/Themes/LightTheme.xaml");
            else if (themeName == "Dark")
                dictionary.Source = new Uri("pack://application:,,,/WPF/Themes/DarkTheme.xaml");

            Resources.MergedDictionaries.Add(dictionary);
        }

        public void ChangeLanguage(string language)
        {
            if (language.Equals("en-US"))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                CurrentLanguage = "en-US";
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                return;
            }
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-LATN");
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sr-LATN");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr-LATN");
            CurrentLanguage = "sr-LATN";
        }
    }
}
