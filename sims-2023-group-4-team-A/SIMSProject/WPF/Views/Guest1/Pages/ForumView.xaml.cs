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
            //CommentVisibility();
            //CloseButtonVisibility();
            ItemsVisibility();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        /*
        public void CommentVisibility()
        {
            if (_viewModel.IsClosed())
            {
                CommentBox.Visibility = Visibility.Hidden;
                CommentButton.Visibility = Visibility.Hidden;
                ClosedForumLabel.Visibility = Visibility.Visible;
            }
        }
        
        public void CloseButtonVisibility()
        {
            if (_viewModel.IsUserOwner() && !_viewModel.IsClosed())
            {
                CloseButton.Visibility = Visibility.Visible;
            }
        }
        */
        private void Button_Close_Forum(object sender, RoutedEventArgs e)
        {
            _viewModel.CloseForum();
            _viewModel.CloseForumToast();
            NavigationService.Navigate(new Forums(_user));
        }
      

        private void Button_Click_Comment(object sender, RoutedEventArgs e)
        {
            _viewModel.LeaveAComment();
            _viewModel.AddNewComment();
            Comments.Items.Refresh();
            CommentsNumber.Content = _viewModel.Forum.CommentsCount;
        }

        public void ItemsVisibility()
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
        }
    }
}
