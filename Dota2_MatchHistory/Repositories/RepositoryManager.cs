using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Repositories
{
    public sealed class RepositoryManager
    {
        private static RepositoryManager _instance;

        private IRepository _onlineRepository = new RepositoryOnline();
        private IRepository _localRepository = new RepositoryLocal();
        private IRepository _currentRepository;
        private RepositoryManager()
        {
            _currentRepository = _localRepository;
        }

        public static RepositoryManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RepositoryManager();
            }
            return _instance;
        }

        public IRepository CurrentRepository
        {
            get { return _currentRepository; }
        }

        public void ChangeRepository()
        {
            if (_currentRepository is RepositoryOnline)
            {
                _currentRepository = _localRepository;
            }
            else
            {
                _currentRepository = _onlineRepository;
            }
        }
    }
}
