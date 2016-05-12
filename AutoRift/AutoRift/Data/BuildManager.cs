using System.Collections.Generic;
using System.IO;
using System.Linq;
using EloBuddy;
using Newtonsoft.Json;

namespace AutoRift.Data
{
    public class BuildManager
    {
        public static readonly string BuildsFilePath = Path.Combine(DataManager.DataPath, "Builds.json");
        public static Dictionary<Champion, ChampionBuildGroup> Builds { get; set; }

        public static void SaveExampleBuilds()
        {
            File.WriteAllText(BuildsFilePath, BuildData.Data);
        }

        public static void Load()
        {
            if (!File.Exists(BuildsFilePath))
            {
                SaveExampleBuilds();
            }
            Builds =
                JsonConvert.DeserializeObject<List<ChampionBuildGroup>>(File.ReadAllText(BuildsFilePath))
                    .ToDictionary(x => x.Champion);
        }

    }
}