using System;
using EloBuddy;
using GenesisSpellLibrary.Spells;

// ReSharper disable once CheckNamespace

namespace GenesisSpellLibrary
{
    /// <summary>
    /// </summary>
    /// <remarks>Copied From: https://github.com/Alweul/EloBuddy </remarks>
    internal class SpellLibrary
    {
        public static SpellBase GetSpells(Champion heroChampion)
        {
            var championType = Type.GetType("GenesisSpellLibrary.Spells." + heroChampion);
            if (championType != null)
            {
                return Activator.CreateInstance(championType) as SpellBase;
            }
            throw new NotImplementedException();
        }

        public static bool IsOnCooldown(AIHeroClient hero, SpellSlot slot)
        {
            if (!hero.Spellbook.GetSpell(slot).IsLearned)
            {
                return true;
            }
            var cooldown = hero.Spellbook.GetSpell(slot).CooldownExpires - Game.Time;
            return cooldown > 0;
        }

        public static void Initialize()
        {
        }
    }
}