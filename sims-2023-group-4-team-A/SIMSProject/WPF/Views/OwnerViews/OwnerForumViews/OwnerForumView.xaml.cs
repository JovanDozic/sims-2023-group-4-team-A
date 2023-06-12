using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerForumViews
{
    public partial class OwnerForumView : Page
    {
        private User _user;
        private App _app = (App)System.Windows.Application.Current;
        private OwnerForumViewModel _viewModel;

        public OwnerForumView(User user, Forum forum)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, forum);
            DataContext = _viewModel;

            InputCommentRow.Height = _viewModel.CanUserLeaveComment ? new GridLength(75) : new GridLength(0);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }


        private void BtnAddCommentInputForm_Click(object sender, RoutedEventArgs e)
        {
            CommentInputForm.Visibility = Visibility.Visible;
            BtnAddCommentInputForm.Visibility = Visibility.Collapsed;
            InputCommentRow.Height = new GridLength(200);
            //TxtNewComment.Focus();
        }

        private void BtnAddComment_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.AddNewComment())
            {
                TxtNewCommentBorder.BorderBrush = System.Windows.Media.Brushes.Red;
                return;
            }
            BtnCancelAddingComment_Click(sender, e);

            //if (_app.CurrentLanguage == "en-US")
            //{
            //    MessageBox.Show("Comment sent.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Komentar poslat.", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
            //}

            LstComments.Items.Refresh();
        }

        private void BtnCancelAddingComment_Click(object sender, RoutedEventArgs e)
        {
            CommentInputForm.Visibility = Visibility.Collapsed;
            BtnAddCommentInputForm.Visibility = Visibility.Visible;
            InputCommentRow.Height = new GridLength(75);
        }

        private void LstCommentsItem_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem ?? throw new System.Exception("List item not found.");
            Comment? comment = listViewItem.DataContext as Comment;
            _viewModel.HoveredComment = comment;
        }

        private void LstCommentsItem_MouseLeave(object sender, MouseEventArgs e)
        {
            _viewModel.HoveredComment = null;
        }

        private void TxtNewComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_viewModel.IsNewCommentValid)
            {
                TxtNewCommentBorder.BorderBrush = System.Windows.Media.Brushes.Black;
            }
            else
            {
                TxtNewCommentBorder.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }
    }
}
