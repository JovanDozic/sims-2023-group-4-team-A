using System;
using System.Collections;
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

namespace SIMSProject.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomListCheckBox.xaml
    /// </summary>
    public partial class CustomListCheckBox : UserControl
    {
        public CustomListCheckBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable<object>),
                typeof(CustomListCheckBox),
                new PropertyMetadata(null, ItemsSourcePropertyChanged));

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourcePropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var customListBox = d as CustomListCheckBox;
            customListBox.listBox.ItemsSource = e.NewValue as IEnumerable<object>;
        }
        
        
        public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomListCheckBox), new FrameworkPropertyMetadata(null));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

       
    }
}
