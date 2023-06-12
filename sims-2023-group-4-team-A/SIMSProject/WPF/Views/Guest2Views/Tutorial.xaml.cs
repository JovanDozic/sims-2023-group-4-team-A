using System;
using System.IO.Packaging;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Page
    {
        public Tutorial(string filePath)
        {
            InitializeComponent();

            myMedia.Source = new Uri(filePath);
            myMedia.Play();
        }

    }
}
