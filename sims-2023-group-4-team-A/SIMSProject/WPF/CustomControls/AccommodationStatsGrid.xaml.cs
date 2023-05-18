using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.CustomControls
{
    public partial class AccommodationStatsGrid : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(AccommodationStatsGrid));

        public bool BestIconVisibility
        {
            get { return (bool)GetValue(BestIconVisibilityProperty); }
            set { SetValue(BestIconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty BestIconVisibilityProperty =
            DependencyProperty.Register("BestIconVisibility", typeof(bool), typeof(AccommodationStatsGrid));

        public int TotalReservations
        {
            get { return (int)GetValue(TotalReservationsProperty); }
            set { SetValue(TotalReservationsProperty, value); }
        }
        public static readonly DependencyProperty TotalReservationsProperty =
            DependencyProperty.Register("TotalReservations", typeof(int), typeof(AccommodationStatsGrid));

        public int CancelledReservations
        {
            get { return (int)GetValue(CancelledReservationsProperty); }
            set { SetValue(CancelledReservationsProperty, value); }
        }
        public static readonly DependencyProperty CancelledReservationsProperty =
            DependencyProperty.Register("CancelledReservations", typeof(int), typeof(AccommodationStatsGrid));

        public int RescheduledReservations
        {
            get { return (int)GetValue(RescheduledReservationsProperty); }
            set { SetValue(RescheduledReservationsProperty, value); }
        }
        public static readonly DependencyProperty RescheduledReservationsProperty =
            DependencyProperty.Register("RescheduledReservations", typeof(int), typeof(AccommodationStatsGrid));
        
        public int RenovationRecommendations
        {
            get { return (int)GetValue(RenovationRecommendationsProperty); }
            set { SetValue(RenovationRecommendationsProperty, value); }
        }
        public static readonly DependencyProperty RenovationRecommendationsProperty =
            DependencyProperty.Register("RenovationRecommendations", typeof(int), typeof(AccommodationStatsGrid));
        
        public AccommodationStatsGrid()
        {
            InitializeComponent();
        }

        private void BtnTrophy_Click(object sender, RoutedEventArgs e)
        {
            PopupTrophyHelp.IsOpen = true;
        }
    }
}