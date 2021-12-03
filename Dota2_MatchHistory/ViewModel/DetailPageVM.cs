using Dota2_MatchHistory.Models;
using Dota2_MatchHistory.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.ViewModel
{
    class DetailPageVM: ViewModelBase
    {
        private long _matchId;
        public long MatchId
        {
            get { return _matchId; }
            set
            {
                _matchId = value;
            }
        }

        public Match CurrentMatch { get; set; }

        public async Task LoadMatch()
        {
            IRepository currentRepository = RepositoryManager.GetInstance().CurrentRepository;
            CurrentMatch = await currentRepository.GetMatch(MatchId);
            RaisePropertyChanged("CurrentMatch");
        }
    }
}
