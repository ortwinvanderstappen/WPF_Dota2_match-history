using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Models
{
    public class Hero
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        //[JsonProperty(PropertyName = "localized_name")]
        //public string localized_name { get; set; }
        public string HeroImage
        {
            get
            {
                int count = "npc_dota_hero_".Length;
                string heroUrlName = Name.Remove(0, count).ToLower();
                return $"https://steamcdn-a.akamaihd.net/apps/dota2/images/heroes/{heroUrlName}_full.png";
            }
        }
        public string HeroName
        {
            get
            {
                int count = "npc_dota_hero_".Length;
                string heroName = Name.Remove(0, count).ToUpper();
                heroName = heroName.Replace('_', ' ');
                return heroName;
            }
        }

        public override string ToString()
        {
            return HeroName;
        }
    }
}
