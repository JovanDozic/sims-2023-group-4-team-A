using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.WPF.ViewModels.Guest1ViewModels;
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
    /// Interaction logic for ForumView.xaml
    /// </summary>
    public partial class ForumView : Page
    {
        private User _user = new();
        private ForumDisplayViewModel _viewModel;
        public ForumView(User user, Forum forum)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, forum);
            DataContext = _viewModel;
            ItemsUsability();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void Button_Close_Forum(object sender, RoutedEventArgs e)
        {
            _viewModel.CloseForum();
            NavigationService.Navigate(new Forums(_user));
        }
      

        private void Button_Click_Comment(object sender, RoutedEventArgs e)
        {
            _viewModel.LeaveAComment();
            _viewModel.AddNewComment();
            Comments.Items.Refresh();
            CommentsNumber.Content = _viewModel.Forum.CommentsCount;
            CommentBox.Text = "Vaš komentar";
            CommentBox.Foreground = new SolidColorBrush(Colors.Gray);
        }

        public void ItemsUsability()
        {
            if (_viewModel.IsUserOwner() && !_viewModel.IsClosed())
            {
                CloseButton.Visibility = Visibility.Visible;
            }
            if (_viewModel.IsClosed())
            {
                CommentBox.Visibility = Visibility.Hidden;
                CommentButton.Visibility = Visibility.Hidden;
                ClosedForumLabel.Visibility = Visibility.Visible;
            }
            if (_viewModel.IsUseful())
                Useful.Visibility = Visibility.Visible;
            if (CommentBox.Text == "Vaš komentar" || CommentBox.Text == String.Empty)
                CommentButton.IsEnabled = false;
        }
        private void TextComment_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Vaš komentar") textbox.Text = string.Empty;
        }

        private void TextComment_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Vaš komentar";
            }
        }
        private void LoadText(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Text = "Vaš komentar";
        }

        private void CommentBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CommentButton.IsEnabled = CommentBox.Text != "Vaš komentar" && CommentBox.Text != String.Empty;
        }
    }
}
