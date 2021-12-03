using Dota2_MatchHistory.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Repositories
{
    public class RepositoryLocal : IRepository
    {
        List<MatchOverview> _matches;
        List<GameMode> _gameModes;
        List<Hero> _heroes;

        public RepositoryLocal()
        {
            LoadStaticData();
        }

        private async void LoadStaticData()
        {
            await GetHeroes();
        }

        // Game modes
        public async Task<List<GameMode>> GetGameModes()
        {
            // Fill the matches if the list is null
            if (_gameModes == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Dota2_MatchHistory.Resources.Data.game_modes.json";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string json = await reader.ReadToEndAsync();
                        _gameModes = JsonConvert.DeserializeObject<List<GameMode>>(json, new GameModesConverter());
                    }
                }
            }

            return _gameModes;
        }

        public async Task<List<GameMode>> GetGameModes(List<MatchOverview> matches)
        {
            List<GameMode> gameModes = await GetGameModes();
            List<GameMode> gameModesFiltered = new List<GameMode>();

            foreach (GameMode gm in gameModes)
            {
                MatchOverview result;
                result = matches.FirstOrDefault(match => match.game_mode == gm.id);

                if (result != null)
                    gameModesFiltered.Add(gm);
            }

            return gameModesFiltered;
        }


        // Matches
        public async Task<List<MatchOverview>> GetMatchOverviews()
        {
            // Fill the matches if the list is null
            if (_matches == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Dota2_MatchHistory.Resources.Data.public_matches.json";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string json = await reader.ReadToEndAsync();
                        _matches = JsonConvert.DeserializeObject<List<MatchOverview>>(json);
                    }
                }
            }

            return _matches;
        }

        public async Task<Match> GetMatch(long matchId)
        {
            Match match;

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Dota2_MatchHistory.Resources.Data.Matches.match_{matchId}.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    match = JsonConvert.DeserializeObject<Match>(json);
                }
            }

            await match.LoadHeroes();
            return match;
        }

        public async Task<List<MatchOverview>> GetMatchesByGameMode(GameMode selectedGameMode)
        {
            List<MatchOverview> matches = await GetMatchOverviews();
            List<MatchOverview> matchesByMode = new List<MatchOverview>();

            foreach (MatchOverview match in matches)
            {
                if (match.game_mode == selectedGameMode.id)
                {
                    matchesByMode.Add(match);
                }
            }

            return matchesByMode;
        }

        // Heroes
        public async Task<List<Hero>> GetHeroes()
        {
            // Fill the matches if the list is null
            if (_heroes == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Dota2_MatchHistory.Resources.Data.heroes.json";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string json = await reader.ReadToEndAsync();
                        _heroes = JsonConvert.DeserializeObject<List<Hero>>(json);
                    }
                }
            }

            return _heroes;
        }

        public async Task<Hero> GetHero(int heroId)
        {
            if(_heroes == null)
            {
                await GetHeroes();
            }
            
            return _heroes.FirstOrDefault(hero => hero.Id == heroId);
        }
    }
}
