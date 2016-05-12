using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace AutoRift.BuildParser
{
    class Program
    {
        public static List<string> Errors { get; set; } 
        public static Queue<Champion> ChampionsToParse { get; set; }
        public static int RunningTasks = 0;
        public static List<ChampionBuildGroup> ChampionBuildGroups = new List<ChampionBuildGroup>();
        static void Main(string[] args)
        {
            ChampionsToParse = new Queue<Champion>();
            Errors = new List<string>();
            foreach (Champion champion in Enum.GetValues(typeof(Champion)))
            {
                Console.WriteLine("Parsing Champion {0}", champion);
                ChampionsToParse.Enqueue(champion);
            }
            StartParsingChampions();
            while (RunningTasks > 0) { }
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var error in Errors)
            {
                Console.WriteLine(error);
            }
            Console.ResetColor();
            Console.WriteLine("Saving Data....");
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Builds.json"), JsonConvert.SerializeObject(ChampionBuildGroups));
            Console.WriteLine("Saved Data!");
            Console.ReadLine();
        }

        private static void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var document = new HtmlDocument();
            document.LoadHtml(e.Result);

            var itemBuilds = GetBuilds((Champion) e.UserState, document);
            var spellBuilds = GetSpellBuilds((Champion) e.UserState, document);
            
            ChampionBuildGroups.Add(new ChampionBuildGroup(itemBuilds, spellBuilds) { Champion = (Champion) e.UserState });
            RunningTasks--;
        }

        private static List<SpellBuild> GetSpellBuilds(Champion champion, HtmlDocument document)
        {
            var spellBuilds = new List<SpellBuild>();
            var spellBlocks = document.DocumentNode.SelectNodes("//script[@type]");
            if (spellBlocks == null)
            {
                Errors.Add("Downloading Data For " + champion + "Failed!");
                RunningTasks--;
                return null;
            }
            foreach (var spellBlock in spellBlocks)
            {
                if(!spellBlock.InnerHtml.Contains("window.skillOrder")) continue;
                var match = Regex.Matches(spellBlock.InnerHtml, @"\[([^\]]+)\]");
                if (match.Count > 1)
                {
                    var levels = match[1].Value.Replace("]", "").Replace("[", "").Split(',');
                    var spellSlots = levels.Select(x => (SpellSlot) (int.Parse(x) - 1)).ToArray();
                    spellBuilds.Add(new SpellBuild(0, spellSlots));
                }

            }
            return spellBuilds;
        }

        private static List<ItemBuild> GetBuilds(Champion champion, HtmlDocument document)
        {
            var parsedBuilds = new List<ItemBuild>();
            var builds = document.DocumentNode.SelectNodes("//div[@data-build-id]");
            if (builds == null)
            {
                Errors.Add("Downloading Data For " + champion + "Failed!");
                RunningTasks--;
                return null;
            }
            foreach (var node in builds)
            {
                var buildId = int.Parse(node.GetAttributeValue("data-build-id", "0"));
                try
                {
                    var parsedBuild = new ItemBuild(buildId);

                    foreach (var section in node.Descendants().Where(x => x.Name == "section"))
                    {
                        if (section.GetAttributeValue("class", "") != "starting-items" &&
                            section.GetAttributeValue("class", "") != "core-items" &&
                            section.GetAttributeValue("class", "") != "final-items") continue;
                        foreach (
                            var item in
                                section.Descendants()
                                    .Where(x => x.Name == "small" && x.GetAttributeValue("style", null) == null))
                        {

                            var itemName =
                                item.LastChild.InnerText.Replace(' ', '_')
                                    .Replace("'", "")
                                    .Replace("(", "")
                                    .Replace(")", "")
                                    .Replace(".", "")
                                    .Replace('-', '_');
                            var itemId = (ItemId) Enum.Parse(typeof(ItemId), itemName);
                            parsedBuild.Items.Add(itemId);
                        }
                    }
                    parsedBuilds.Add(parsedBuild);
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could Not Parse, Ignoring: " + ex.Message);
                    Errors.Add(string.Format("Could Not Parse Build (ID: {0}, Champion: {1}). Ignoring Build. \n Error: {2}", buildId, champion, ex.Message));
                    Console.ResetColor();
                }
            }
            return parsedBuilds;
        }

        private static void StartParsingChampions()
        {
            while (ChampionsToParse.Count > 0)
            {
                var champion = ChampionsToParse.Dequeue();

                var client = new WebClient();
                client.DownloadStringCompleted += Client_DownloadStringCompleted;
                client.DownloadStringAsync(new Uri("http://lolbuilder.net/" + champion), champion);
                RunningTasks++;
            }
            
        }
    }
}
