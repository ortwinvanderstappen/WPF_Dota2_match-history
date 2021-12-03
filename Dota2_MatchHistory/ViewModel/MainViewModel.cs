using Dota2_MatchHistory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using Dota2_MatchHistory.Models;

namespace Dota2_MatchHistory.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CurrentPage = MainPage;
        }

        public string CommandText
        {
            get
            {
                if (CurrentPage is OverviewPage)
                    return "SHOW MATCH";
                else
                    return "GO BACK";
            }
        }

        private OverviewPage _overviewPage = new OverviewPage();
        public OverviewPage MainPage
        {
            get { return _overviewPage; }
            set { _overviewPage = value; }
        }

        private DetailPage _detailPage = new DetailPage();
        public DetailPage MatchDetailPage
        {
            get { return _detailPage; }
            set { _detailPage = value; }
        }

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }

        private RelayCommand _switchPageCommand;
        public RelayCommand SwitchPageCommand
        {
            get
            {
                if (_switchPageCommand == null)
                {
                    _switchPageCommand = new RelayCommand(SwitchPage);
                }
                return _switchPageCommand;
            }
        }

        private async void SwitchPage()
        {
            // Check the current visible page
            if (CurrentPage is OverviewPage)
            {
                // Get the selected pokemon
                MatchOverview matchOverview = (MainPage.DataContext as OverviewPageVM).SelectedMatchOverview;
                if (matchOverview == null) return;
                
                // Replace the current page
                CurrentPage = MatchDetailPage;

                // Set the correct match id, the page itself will fetch the correct data
                (CurrentPage.DataContext as DetailPageVM).MatchId = matchOverview.match_id;
                await(CurrentPage.DataContext as DetailPageVM).LoadMatch();
            }
            else
            {
                CurrentPage = MainPage;
            }

            RaisePropertyChanged("CommandText");
        }
    }
}
