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
    public class SpellBuild : IBuild<SpellSlot>
    {

        public SpellBuild()
        {
        }

        public SpellBuild(int id, params SpellSlot[] build)
        {
            Id = id;
            Items = build;
        }

        public int Id { get; set; }

        [JsonProperty("Items", ItemConverterType = typeof (StringEnumConverter))]
        public SpellSlot[] Items { get; set; }

        public IEnumerator<SpellSlot> GetEnumerator()
        {
            return Items.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}