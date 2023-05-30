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

namespace SIMSProject.WPF.Views.Guest1.Pages
{
    /// <summary>
    /// Interaction logic for ForumCreation.xaml
    /// </summary>
    public partial class ForumCreation : Page
    {
        public ForumCreation()
        {
            InitializeComponent();
            TodaysDate.Text = DateTime.Now.Date.ToString("dd.MM.yyyy.");
        }
    }
}
