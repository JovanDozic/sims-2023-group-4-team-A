using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerForumViews
{
    public partial class OwnerForumView : Page
    {
        private User _user;
        private OwnerForumViewModel _viewModel;

        public OwnerForumView(User user, Forum forum)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, forum);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnDownvoteComment_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnAddCommentInputForm_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommentInputForm.Visibility = System.Windows.Visibility.Visible;
            BtnAddCommentInputForm.Visibility = System.Windows.Visibility.Collapsed;
            InputCommentRow.Height = new GridLength(200);
        }

        private void BtnAddComment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add comment using View Model and service
            _viewModel.AddNewComment();



            // TODO: Remove form for adding comment, and refresh comments list (maybe done in viewmodel)
            BtnCancelAddingComment_Click(sender, e);
            LstComments.Items.Refresh();
        }

        private void BtnCancelAddingComment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommentInputForm.Visibility = System.Windows.Visibility.Collapsed;
            BtnAddCommentInputForm.Visibility = System.Windows.Visibility.Visible;
            InputCommentRow.Height = new GridLength(75);
        }
    }
}
