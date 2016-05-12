using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutoRift.BuildParser
{
    public class ChampionBuildGroup
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Champion Champion { get; set; }
        public List<ItemBuild> ItemBuilds { get; set; }
        public List<SpellBuild> SpellBuilds { get; set; }

        public ChampionBuildGroup()
        {

        }

        public ChampionBuildGroup(List<ItemBuild> itemBuilds, List<SpellBuild> spellBuilds)
        {
            ItemBuilds = itemBuilds;
            SpellBuilds = spellBuilds;
        }
    }
    public interface IBuild<T> : IEnumerator<T>
    {
        List<T> Items { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class ItemBuild : IBuild<ItemId>
    {
        public int Id { get; set; }
        [JsonProperty("Items", ItemConverterType = typeof(StringEnumConverter))]
        public List<ItemId> Items{ get; set; }

        private int _currentIndex;
        public ItemId Current
        {
            get
            {
                try
                {
                    return Items[_currentIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public ItemBuild()
        {

        }
        public ItemBuild(int id, params ItemId[] itemsId)
        {
            Id = id;
            Items = itemsId?.ToList() ?? new List<ItemId>();
        }

        public bool MoveNext()
        {
            _currentIndex++;
            return (_currentIndex < Items.Count);
        }

        public void Reset()
        {
            _currentIndex = -1;
        }


        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {

        }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class SpellBuild : IBuild<SpellSlot>
    {
        public int Id { get; set; }
        [JsonProperty("Items", ItemConverterType = typeof(StringEnumConverter))]
        public List<SpellSlot> Items { get; set; }
        public SpellBuild()
        {

        }

        public SpellBuild(int id, params SpellSlot[] build)
        {
            Id = id;
            Items = build.ToList();
        }

        public bool MoveNext()
        {
            _currentIndex++;
            return (_currentIndex < Items.Count);
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        private int _currentIndex;

        public SpellSlot Current
        {
            get
            {
                try
                {
                    return Items[_currentIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }


        public void Dispose()
        {

        }
    }
}