using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dota2_MatchHistory.Models;
using Dota2_MatchHistory.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Dota2_MatchHistory.ViewModel
{
    class OverviewPageVM : ViewModelBase
    {
        public OverviewPageVM()
        {
            FilterMatchOverviews();
        }

        private async void FilterMatchOverviews()
        {
            IRepository currentRepository = RepositoryManager.GetInstance().CurrentRepository;

            Console.WriteLine("Filtering matches");

            // Filter the matches towards the selected game mode
            if (_selectedGameMode == null || _selectedGameMode.name.Equals("All"))
            {
                Matches = await currentRepository.GetMatchOverviews();
            }
            else
            {
                Matches = await currentRepository.GetMatchesByGameMode(_selectedGameMode);
            }

            // Load all the hero's for all these matches
            await LoadMatchOverviewHeroes();

            if (GameModes == null)
            {
                // Filter the gamemode list to only show used game modes
                GameModes = await currentRepository.GetGameModes(Matches);
                GameMode all = new GameMode() { name = "All", id = 0 };
                GameModes.Insert(0, all);
            }

            Console.WriteLine("Filter done, raising matches property");
            // The matches changed so raise the property
            RaisePropertyChanged("Matches");
        }

        private async Task LoadMatchOverviewHeroes()
        {
            IRepository currentRepository = RepositoryManager.GetInstance().CurrentRepository;
            foreach (MatchOverview m in Matches)
            {
                await m.LoadHeroes();
            }
        }

        private RelayCommand _changeRepositoryCommand;
        public RelayCommand ChangeRepositoryCommand
        {
            get
            {
                if (_changeRepositoryCommand == null)
                    _changeRepositoryCommand = new RelayCommand(ChangeRepository);
                return _changeRepositoryCommand;
            }
        }

        private void ChangeRepository()
        {
            RepositoryManager.GetInstance().ChangeRepository();

            IRepository currentRepository = RepositoryManager.GetInstance().CurrentRepository;
            if (currentRepository is RepositoryOnline)
            {
                CurrentRepositoryText = "Change to local repository";
            }
            else
            {
                CurrentRepositoryText = "Change to online repository";
            }

            FilterMatchOverviews();

            RaisePropertyChanged("CurrentRepositoryText");
        }

        private GameMode _selectedGameMode;
        public GameMode SelectedGameMode
        {
            get { return _selectedGameMode; }
            set
            {
                _selectedGameMode = value;
                //ApplyNewGameMode();
                FilterMatchOverviews();
            }
        }

        public MatchOverview _selectedMatchOverview;
        public MatchOverview SelectedMatchOverview
        {
            get { return _selectedMatchOverview; }
            set
            {
                IRepository currentRepository = RepositoryManager.GetInstance().CurrentRepository;
                _selectedMatchOverview = value;
            }
        }

        private List<MatchOverview> _matches;
        public List<MatchOverview> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
            }
        }

        private List<GameMode> _gameModes;
        public List<GameMode> GameModes
        {
            get { return _gameModes; }
            set
            {
                _gameModes = value;
                RaisePropertyChanged("GameModes");
            }
        }

        public string CurrentRepositoryText { get; set; } = "Change to online repository";
    }
}
