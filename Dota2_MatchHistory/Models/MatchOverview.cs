using Dota2_MatchHistory.Repositories;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Models
{
    public class MatchOverview: ObservableObject
    {
        [JsonProperty(PropertyName = "match_id")]
        public long match_id { get; set; }
        //[JsonProperty(PropertyName = "match_seq_num")]
        //public long match_seq_num { get; set; }
        [JsonProperty(PropertyName = "radiant_win")]
        public bool radiant_win { get; set; }
        [JsonProperty(PropertyName = "start_time")]
        public int start_time { get; set; }
        [JsonProperty(PropertyName = "duration")]
        public int duration { get; set; }
        //[JsonProperty(PropertyName = "avg_mmr")]
        //public object avg_mmr { get; set; }
        //[JsonProperty(PropertyName = "num_mmr")]
        //public object num_mmr { get; set; }
        //[JsonProperty(PropertyName = "lobby_type")]
        //public int lobby_type { get; set; }
        [JsonProperty(PropertyName = "game_mode")]
        public int game_mode { get; set; }
        //[JsonProperty(PropertyName = "avg_rank_tier")]
        //public int avg_rank_tier { get; set; }
        //[JsonProperty(PropertyName = "num_rank_tier")]
        //public int num_rank_tier { get; set; }
        //[JsonProperty(PropertyName = "cluster")]
        //public int cluster { get; set; }

        [JsonProperty(PropertyName = "radiant_team")]
        public string RadiantTeam { get;set; }

        [JsonProperty(PropertyName = "dire_team")]
        public string DireTeam { get;set; }

        public List<Hero> RadiantTeamHeroes { get; set; } = new List<Hero>();
        public List<Hero> DireTeamHeroes { get; set; } = new List<Hero>();

        public async Task LoadHeroes()
        {
            await Task.Delay(10);
            IRepository currentRepository = RepositoryManager.GetInstance().CurrentRepository;
            List<Task<Hero>> parallelTasks = new List<Task<Hero>>();

            string[] radiantTeamHeroIds = RadiantTeam.Split(',');
            string[] direTeamHeroIds = DireTeam.Split(',');

            RadiantTeamHeroes.Clear();
            DireTeamHeroes.Clear();

            // Add radiant team tasks
            foreach (string radiantId in radiantTeamHeroIds)
            {
                parallelTasks.Add(currentRepository.GetHero(int.Parse(radiantId)));
            }
            // Add dire team tasks
            foreach (string direId in direTeamHeroIds)
            {
                parallelTasks.Add(currentRepository.GetHero(int.Parse(direId)));
            }

            // Perform all tasks
            await Task.WhenAll(parallelTasks);

            for (int i = 0; i < 5; i++)
            {
                RadiantTeamHeroes.Add(parallelTasks[i].Result);
            }

            for (int i = 5; i < 10; i++)
            {
                DireTeamHeroes.Add(parallelTasks[i].Result);
            }

            Console.WriteLine("Heroes loaded! for match " + this);

            RaisePropertyChanged("DireTeamHeroes");
            RaisePropertyChanged("RadiantTeamHeroes");
        }

        public override string ToString()
        {
            return $"Match id: {match_id} duration: {duration}";
        }
    }


}
