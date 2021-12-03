using Dota2_MatchHistory.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Models
{
    public class Match
    {
        public async Task LoadHeroes()
        {
            IRepository currentRepository = RepositoryManager.GetInstance().CurrentRepository;
            List<Task<Hero>> parallelTasks = new List<Task<Hero>>();

            for (int i = 0; i < 10; i++)
            {
                parallelTasks.Add(currentRepository.GetHero(players[i].hero_id));
            }

            await Task.WhenAll(parallelTasks);
            for (int i = 0; i < parallelTasks.Count; i++)
            {
                players[i].PlayerHero = parallelTasks[i].Result;
            }
        }

        [JsonProperty(PropertyName = "match_id")]
        public long match_id { get; set; }
        //public int barracks_status_dire { get; set; }
        //public int barracks_status_radiant { get; set; }
        //public object chat { get; set; }
        //public int cluster { get; set; }
        //public object cosmetics { get; set; }
        [JsonProperty(PropertyName = "dire_score")]
        public int dire_score { get; set; }
        //public object dire_team_id { get; set; }
        //public object draft_timings { get; set; }
        [JsonProperty(PropertyName = "duration")]
        public int duration { get; set; }
        //public int engine { get; set; }
        //public int first_blood_time { get; set; }
        [JsonProperty(PropertyName = "game_mode")]
        public int game_mode { get; set; }
        //public int human_players { get; set; }
        [JsonProperty(PropertyName = "leagueid")]
        public int leagueid { get; set; }
        //public int lobby_type { get; set; }
        //public long match_seq_num { get; set; }
        //public int negative_votes { get; set; }
        //public object objectives { get; set; }
        //public Picks_Bans[] picks_bans { get; set; }
        //public int positive_votes { get; set; }
        //public object radiant_gold_adv { get; set; }
        [JsonProperty(PropertyName = "radiant_score")]
        public int radiant_score { get; set; }
        //public object radiant_team_id { get; set; }
        [JsonProperty(PropertyName = "radiant_win")]
        public bool radiant_win { get; set; }
        //public object radiant_xp_adv { get; set; }
        //public int skill { get; set; }
        [JsonProperty(PropertyName = "start_time")]
        public int start_time { get; set; }
        //public object teamfights { get; set; }
        //public int tower_status_dire { get; set; }
        //public int tower_status_radiant { get; set; }
        //public object version { get; set; }
        //public int replay_salt { get; set; }
        //public int series_id { get; set; }
        //public int series_type { get; set; }
        [JsonProperty(PropertyName = "players")]
        public Player[] players { get; set; }

        //public int patch { get; set; }
        //public int region { get; set; }
        //public string replay_url { get; set; }
    }

    //public class Picks_Bans
    //{
    //    public bool is_pick { get; set; }
    //    public int hero_id { get; set; }
    //    public int team { get; set; }
    //    public int order { get; set; }
    //}

    public class Player
    {
        //public long match_id { get; set; }
        [JsonProperty(PropertyName = "player_slot")]
        public int player_slot { get; set; }
        //public object ability_targets { get; set; }
        //public int[] ability_upgrades_arr { get; set; }
        //public object ability_uses { get; set; }
        //public int? account_id { get; set; }
        //public object actions { get; set; }
        //public object additional_units { get; set; }
        [JsonProperty(PropertyName = "assists")]
        public int assists { get; set; }
        //public int backpack_0 { get; set; }
        //public int backpack_1 { get; set; }
        //public int backpack_2 { get; set; }
        //public object backpack_3 { get; set; }
        //public object buyback_log { get; set; }
        //public object camps_stacked { get; set; }
        //public object connection_log { get; set; }
        //public object creeps_stacked { get; set; }
        //public object damage { get; set; }
        //public object damage_inflictor { get; set; }
        //public object damage_inflictor_received { get; set; }
        //public object damage_taken { get; set; }
        //public object damage_targets { get; set; }
        [JsonProperty(PropertyName = "deaths")]
        public int deaths { get; set; }
        [JsonProperty(PropertyName = "denies")]
        public int denies { get; set; }
        //public object dn_t { get; set; }
        //public object firstblood_claimed { get; set; }
        //public int gold { get; set; }
        //public int gold_per_min { get; set; }
        //public object gold_reasons { get; set; }
        //public int gold_spent { get; set; }
        //public object gold_t { get; set; }
        [JsonProperty(PropertyName = "hero_damage")]
        public int hero_damage { get; set; }
        //public int hero_healing { get; set; }
        //public object hero_hits { get; set; }
        [JsonProperty(PropertyName = "hero_id")]
        public int hero_id { get; set; }
        public Hero PlayerHero { get; set; }
        //public int item_0 { get; set; }
        //public int item_1 { get; set; }
        //public int item_2 { get; set; }
        //public int item_3 { get; set; }
        //public int item_4 { get; set; }
        //public int item_5 { get; set; }
        //public int item_neutral { get; set; }
        //public object item_uses { get; set; }
        //public object kill_streaks { get; set; }
        //public object killed { get; set; }
        //public object killed_by { get; set; }
        [JsonProperty(PropertyName = "kills")]
        public int kills { get; set; }
        //public object kills_log { get; set; }
        //public object lane_pos { get; set; }
        //public int last_hits { get; set; }
        //public int leaver_status { get; set; }
        [JsonProperty(PropertyName = "level")]
        public int level { get; set; }
        //public object lh_t { get; set; }
        //public object life_state { get; set; }
        //public object max_hero_hit { get; set; }
        //public object multi_kills { get; set; }
        //public int net_worth { get; set; }
        //public object obs { get; set; }
        //public object obs_left_log { get; set; }
        //public object obs_log { get; set; }
        //public object obs_placed { get; set; }
        //public int party_id { get; set; }
        //public int party_size { get; set; }
        //public object performance_others { get; set; }
        //public Permanent_Buffs[] permanent_buffs { get; set; }
        //public object pings { get; set; }
        //public object pred_vict { get; set; }
        //public object purchase { get; set; }
        //public object purchase_log { get; set; }
        //public object randomed { get; set; }
        //public object repicked { get; set; }
        //public object roshans_killed { get; set; }
        //public object rune_pickups { get; set; }
        //public object runes { get; set; }
        //public object runes_log { get; set; }
        //public object sen { get; set; }
        //public object sen_left_log { get; set; }
        //public object sen_log { get; set; }
        //public object sen_placed { get; set; }
        //public object stuns { get; set; }
        //public object teamfight_participation { get; set; }
        //public object times { get; set; }
        [JsonProperty(PropertyName = "tower_damage")]
        public int tower_damage { get; set; }
        //public object towers_killed { get; set; }
        //public int xp_per_min { get; set; }
        //public object xp_reasons { get; set; }
        //public object xp_t { get; set; }
        //public bool radiant_win { get; set; }
        //public int start_time { get; set; }
        //public int duration { get; set; }
        //public int cluster { get; set; }
        //public int lobby_type { get; set; }
        //public int game_mode { get; set; }
        //public bool is_contributor { get; set; }
        //public int patch { get; set; }
        //public int region { get; set; }
        [JsonProperty(PropertyName = "isRadiant")]
        public bool isRadiant { get; set; }
        //public int win { get; set; }
        //public int lose { get; set; }
        [JsonProperty(PropertyName = "total_gold")]
        public int total_gold { get; set; }
        //public int total_xp { get; set; }
        //public float kills_per_min { get; set; }
        //public int kda { get; set; }
        //public int abandons { get; set; }
        //public int? rank_tier { get; set; }
        //public object[] cosmetics { get; set; }
        //public Benchmarks benchmarks { get; set; }
        //public string personaname { get; set; }
        //public object name { get; set; }
        //public object last_login { get; set; }
    }

    //public class Benchmarks
    //{
    //    public Gold_Per_Min gold_per_min { get; set; }
    //    public Xp_Per_Min xp_per_min { get; set; }
    //    public Kills_Per_Min kills_per_min { get; set; }
    //    public Last_Hits_Per_Min last_hits_per_min { get; set; }
    //    public Hero_Damage_Per_Min hero_damage_per_min { get; set; }
    //    public Hero_Healing_Per_Min hero_healing_per_min { get; set; }
    //    public Tower_Damage tower_damage { get; set; }
    //    public Stuns_Per_Min stuns_per_min { get; set; }
    //    public Lhten lhten { get; set; }
    //}

    //public class Gold_Per_Min
    //{
    //    public int raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Xp_Per_Min
    //{
    //    public int raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Kills_Per_Min
    //{
    //    public float raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Last_Hits_Per_Min
    //{
    //    public float raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Hero_Damage_Per_Min
    //{
    //    public float raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Hero_Healing_Per_Min
    //{
    //    public float raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Tower_Damage
    //{
    //    public int raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Stuns_Per_Min
    //{
    //    public int raw { get; set; }
    //    public float pct { get; set; }
    //}

    //public class Lhten
    //{
    //}

    //public class Permanent_Buffs
    //{
    //    public int permanent_buff { get; set; }
    //    public int stack_count { get; set; }
    //}

}
