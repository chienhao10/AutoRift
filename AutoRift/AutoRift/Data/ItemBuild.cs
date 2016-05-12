using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutoRift.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ItemBuild : IBuild<ItemId>
    {
        public ItemBuild()
        {
        }

        public ItemBuild(int id, params ItemId[] items)
        {
            Id = id;
            Items = items;
        }

        public int Id { get; set; }

        [JsonProperty("Items", ItemConverterType = typeof (StringEnumConverter))]
        public ItemId[] Items { get; set; }

        public IEnumerator<ItemId> GetEnumerator()
        {
            return Items.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}