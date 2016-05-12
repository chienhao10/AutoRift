using System.Collections.Generic;
using EloBuddy;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutoRift.Data
{
    public class ChampionBuildGroup
    {
        public ChampionBuildGroup()
        {
        }

        public ChampionBuildGroup(List<ItemBuild> itemBuilds, List<SpellBuild> spellBuilds)
        {
            ItemBuilds = itemBuilds;
            SpellBuilds = spellBuilds;
        }

        [JsonConverter(typeof (StringEnumConverter))]
        public Champion Champion { get; set; }

        public List<ItemBuild> ItemBuilds { get; set; }
        public List<SpellBuild> SpellBuilds { get; set; }
    }
}