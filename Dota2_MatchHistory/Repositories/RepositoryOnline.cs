using Dota2_MatchHistory.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Repositories
{
    public class RepositoryOnline : IRepository
    {
        List<MatchOverview> _matches;
        List<GameMode> _gameModes;
        List<Hero> _heroes;

        public RepositoryOnline()
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
            // Request a card (GET)
            string endpoint = "https://api.opendota.com/api/constants/game_mode";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a get request
                    var response = await client.GetAsync(endpoint);
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    // Read the json from the api
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize json...
                    _gameModes = JsonConvert.DeserializeObject<List<GameMode>>(json, new GameModesConverter());
                    return _gameModes;
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Exception! " + e.Message);
                    throw new Exception(e.Message);
                }
            }
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
            // Request a card (GET)
            string endpoint = "https://api.opendota.com/api/publicMatches";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a get request
                    var response = await client.GetAsync(endpoint);
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    // Read the json from the api
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize json...
                    _matches = JsonConvert.DeserializeObject<List<MatchOverview>>(json);
                    return _matches;
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Exception! " + e.Message);
                    throw new Exception(e.Message);
                }
            }
        }

        public async Task<Match> GetMatch(long matchId)
        {
            Match match;

            // Request a card (GET)
            string endpoint = $"https://api.opendota.com/api/matches/{matchId}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a get request
                    var response = await client.GetAsync(endpoint);
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    // Read the json from the api
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize json...
                    match = JsonConvert.DeserializeObject<Match>(json);
                    await match.LoadHeroes();
                    return match;
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Exception! " + e.Message);
                    throw new Exception(e.Message);
                }
            }
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
            if (_heroes == null)
            {
                // Request a card (GET)
                string endpoint = "https://api.opendota.com/api/heroes";
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // Send a get request
                        var response = await client.GetAsync(endpoint);
                        if (!response.IsSuccessStatusCode)
                            throw new HttpRequestException(response.ReasonPhrase);

                        // Read the json from the api
                        string json = await response.Content.ReadAsStringAsync();

                        // Deserialize json...
                        _heroes = JsonConvert.DeserializeObject<List<Hero>>(json);
                        return _heroes;
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine("Exception! " + e.Message);
                        throw new Exception(e.Message);
                    }
                }
            }

            return _heroes;
        }

        public async Task<Hero> GetHero(int heroId)
        {
            if (_heroes == null)
            {
                await GetHeroes();
            }

            return _heroes.FirstOrDefault(hero => hero.Id == heroId);
        }
    }
}
