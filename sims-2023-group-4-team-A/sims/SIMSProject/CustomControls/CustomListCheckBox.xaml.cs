using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.CustomControls
{
    /// <summary>
    ///     Interaction logic for CustomListCheckBox.xaml
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
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void ItemsSourcePropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var customListBox = d as CustomListCheckBox;
            customListBox.listBox.ItemsSource = e.NewValue as IEnumerable<object>;
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomListCheckBox),
                new FrameworkPropertyMetadata(null));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
    }
}