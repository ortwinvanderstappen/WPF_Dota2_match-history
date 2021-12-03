using Dota2_MatchHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Repositories
{
    public interface IRepository
    {
        // Game modes
        Task<List<GameMode>> GetGameModes();
        Task<List<GameMode>> GetGameModes(List<MatchOverview> matches);
        // Matches
        Task<Match> GetMatch(long matchId);
        Task<List<MatchOverview>> GetMatchOverviews();
        Task<List<MatchOverview>> GetMatchesByGameMode(GameMode selectedGameMode);
        // Heroes
        Task<List<Hero>> GetHeroes();
        Task<Hero> GetHero(int heroId);
    }
}
