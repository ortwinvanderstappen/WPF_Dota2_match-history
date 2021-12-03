using Dota2_MatchHistory.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_MatchHistory.Models
{
    public class GameModeList
    {
        [JsonConverter(typeof(GameModesConverter))]
        public List<GameMode> gameModes;
    }

    public class GameMode
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        //public bool balanced { get; set; }

        public override string ToString()
        {
            return $"{name}";
        }
    }
}
