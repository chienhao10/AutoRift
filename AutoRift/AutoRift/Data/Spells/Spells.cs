﻿using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

#region Credits

//=====================================================================
//+ Massive thanks to the entire community of EB for making this
//+ Spell library possible. Special thanks to: Coman3 (Who is a sexy awesome babe <3), MarioGK (Just as sexy), 
//+ KarmaPanda (Still Sexy), Bloodimir, Hellsing, iRaxe, plebsot, Chaos, 
//+ zpitty and many others!
//+ This spell database was last updated 2/29/2016
//======================================================================

#endregion

// ReSharper disable once CheckNamespace

namespace GenesisSpellLibrary.Spells
{
    public class Aatrox : SpellBase // Quality Tested, Genesis Approved
    {
        public Aatrox()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 650, SkillShotType.Circular, 250, 450, 285)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, 250, 1200, 100)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Active(SpellSlot.R);
            QisCC = true;
            QisDash = true;
            WisToggle = true;
            EisCC = true;
            LogicDictionary = new Dictionary<string, Func<AIHeroClient, Obj_AI_Base, bool>>();
            LogicDictionary.Add("RLogic", RLogic);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }

        public static bool RLogic(AIHeroClient player, object _)
        {
            if (player == null)
            {
               return false;
            }
            return EntityManager.Heroes.Enemies.Count(e => e.Distance(player) < 500) >= 1;
        }
    }

    public class Ahri : SpellBase
    {
        public Ahri()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1750, 100);
            W = new Spell.Active(SpellSlot.W, 550);
            E = new Spell.Skillshot(SpellSlot.E, 950, SkillShotType.Linear, 250, 1550, 60);
            R = new Spell.Active(SpellSlot.R, 600);
            Options.Clear();
            Options.Add("EisCC", true);
            Options.Add("RisDash", true);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Akali : SpellBase
    {
        public Akali()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
            WisCC = true;
            RisDash = true;
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Alistar : SpellBase
    {
        public Alistar()
        {
            Q = new Spell.Active(SpellSlot.Q, 315);
            W = new Spell.Targeted(SpellSlot.W, 625);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R);
            QisCC = true;
            WisDash = true;
            WisCC = true;
            LogicDictionary = new Dictionary<string, Func<AIHeroClient, Obj_AI_Base, bool>>();
            LogicDictionary.Add("RLogic", RLogic);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }

        public static bool RLogic(AIHeroClient player, Obj_AI_Base objBase)
        {
            if (player == null)
            {
                return false;
            }
            var x = EntityManager.Heroes.Enemies.Count(e => e.Distance(player) < 1000);
            if (player.HasBuffOfType(BuffType.Fear) ||
                player.HasBuffOfType(BuffType.Silence) ||
                player.HasBuffOfType(BuffType.Snare) ||
                player.HasBuffOfType(BuffType.Stun) ||
                player.HasBuffOfType(BuffType.Charm) ||
                player.HasBuffOfType(BuffType.Blind) ||
                player.HasBuffOfType(BuffType.Taunt)
                || (x > 3)
                )
            {
                return true;
            }
            return false;
        }
    }

    public class Amumu : SpellBase
    {
        public Amumu()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 80);
            W = new Spell.Active(SpellSlot.W, 300);
            E = new Spell.Active(SpellSlot.E, 350);
            R = new Spell.Active(SpellSlot.R, 550);
            QisCC = true;
            QisDash = true;
            RisCC = true;
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Anivia : SpellBase
    {
        public Anivia()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 250, 850, 100);
            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 0, int.MaxValue, 20);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Skillshot(SpellSlot.R, 600, SkillShotType.Circular, 0, int.MaxValue, 200);
            QisCC = true;
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Annie : SpellBase
    {
        public Annie()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 500, SkillShotType.Cone, 250, 100, 80);
            E = new Spell.Active(SpellSlot.E, 0);
            R = new Spell.Skillshot(SpellSlot.R, 600, SkillShotType.Circular, 250, 0, 290);
            LogicDictionary = new Dictionary<string, Func<AIHeroClient, Obj_AI_Base, bool>>();
            LogicDictionary.Add("RLogic", RLogic);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }

        public bool RLogic(AIHeroClient player, Obj_AI_Base target)
        {
            if (player == null)
            {
                return false;
            }


            return EntityManager.Heroes.Enemies.Count(e => e.Distance(target) < 300) >= 1;
        }
    }

    public class Ashe : SpellBase
    {
        public Ashe()
        {
            Q = new Spell.Active(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 1200, SkillShotType.Cone);
            E = new Spell.Active(SpellSlot.E, 1000);
            R = new Spell.Skillshot(SpellSlot.R, 10000, SkillShotType.Linear, 250, 1600, 100);
            RisCC = true;
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Azir : SpellBase
    {
        public Azir()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 825, SkillShotType.Linear, 250, 1000, 70);
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular);
            E = new Spell.Skillshot(SpellSlot.E, 1200, SkillShotType.Linear, 250, 1600, 100);
            R = new Spell.Skillshot(SpellSlot.R, 300, SkillShotType.Linear, 500, 1000, 532);
            RisCC = true;
            EisDash = true;
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Bard : SpellBase
    {
        public Bard()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 860, SkillShotType.Linear, 250, 1600, 65);
            //Q2 = new Spell.Skillshot(SpellSlot.Q, 1310, SkillShotType.Linear, 250, 1600, 65);
            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular);
            E = new Spell.Skillshot(SpellSlot.E, int.MaxValue, SkillShotType.Linear);
            R = new Spell.Skillshot(SpellSlot.R, 3400, SkillShotType.Circular, 250, int.MaxValue, 650);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Blitzcrank : SpellBase
    {
        public Blitzcrank()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 980, SkillShotType.Linear, 250, 1800, 70);
            W = new Spell.Active(SpellSlot.W, 0);
            E = new Spell.Active(SpellSlot.E, 150);
            R = new Spell.Active(SpellSlot.R, 550);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Brand : SpellBase
    {
        public Brand()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 1600, 120);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 850, int.MaxValue, 250);
            E = new Spell.Targeted(SpellSlot.E, 640);
            R = new Spell.Targeted(SpellSlot.R, 750);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Braum : SpellBase
    {
        public Braum()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 250, 1700, 60);
            W = new Spell.Targeted(SpellSlot.W, 650);
            E = new Spell.Skillshot(SpellSlot.E, 500, SkillShotType.Cone, 250, 2000, 250);
            R = new Spell.Skillshot(SpellSlot.R, 1300, SkillShotType.Linear, 250, 1300, 115);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Caitlyn : SpellBase
    {
        public Caitlyn()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1240, SkillShotType.Linear, 250, 2000, 60);
            W = new Spell.Skillshot(SpellSlot.W, 820, SkillShotType.Circular, 500, int.MaxValue, 80);
            E = new Spell.Skillshot(SpellSlot.E, 800, SkillShotType.Linear, 250, 1600, 80);
            R = new Spell.Targeted(SpellSlot.R, 2000);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Cassiopeia : SpellBase
    {
        public Cassiopeia()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 850, SkillShotType.Circular, 400, spellWidth: 75);
            W = new Spell.Skillshot(SpellSlot.W, 850, SkillShotType.Circular, spellWidth: 125);
            E = new Spell.Targeted(SpellSlot.E, 700);
            R = new Spell.Skillshot(SpellSlot.R, 825, SkillShotType.Cone, spellWidth: 80);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Chogath : SpellBase
    {
        public Chogath()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, 750, int.MaxValue, 175);
            W = new Spell.Skillshot(SpellSlot.W, 575, SkillShotType.Cone, 250, 1750, 100);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Targeted(SpellSlot.R, 500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Corki : SpellBase
    {
        public Corki()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 825, SkillShotType.Circular, 300, 1000, 250);
            W = new Spell.Skillshot(SpellSlot.W, 600, SkillShotType.Linear);
            //W2 = new Spell.Skillshot(SpellSlot.W, 1800, SkillShotType.Linear);
            E = new Spell.Active(SpellSlot.E, 600);
            R = new Spell.Skillshot(SpellSlot.R, 1300, SkillShotType.Linear, 200, 1950, 40);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Darius : SpellBase
    {
        public Darius()
        {
            Q = new Spell.Active(SpellSlot.Q, 400);
            W = new Spell.Active(SpellSlot.W, 145);
            E = new Spell.Skillshot(SpellSlot.E, 540, SkillShotType.Cone, 250, 100, 120);
            R = new Spell.Targeted(SpellSlot.R, 460);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Diana : SpellBase
    {
        public Diana()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 830, SkillShotType.Cone, 500, 1600, 195);
            W = new Spell.Active(SpellSlot.W, 350);
            E = new Spell.Active(SpellSlot.E, 200);
            R = new Spell.Targeted(SpellSlot.R, 825);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class DrMundo : SpellBase
    {
        public DrMundo()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 50, 2000, 60);
            W = new Spell.Active(SpellSlot.W, 162);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Draven : SpellBase
    {
        public Draven()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Linear);
            R = new Spell.Skillshot(SpellSlot.R, 2000, SkillShotType.Linear);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Ekko : SpellBase
    {
        public Ekko()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 750, SkillShotType.Linear, 250, 2200, 60);
            W = new Spell.Skillshot(SpellSlot.W, 1620, SkillShotType.Circular, 500, 1000, 500);
            E = new Spell.Skillshot(SpellSlot.E, 400, SkillShotType.Linear, 250, int.MaxValue, 1);
            R = new Spell.Active(SpellSlot.R, 400);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    //TODO: Elise
    /*public class Elise : SpellBase
    {
        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
        public Elise()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular);
            E = new Spell.Skillshot(SpellSlot.E, 1075, SkillShotType.Linear, 250, 1600, 80) { AllowedCollisionCount = 0 };
            R = new Spell.Active(SpellSlot.R);
        }
    }*/

    public class Evelynn : SpellBase
    {
        public Evelynn()
        {
            Q = new Spell.Active(SpellSlot.Q, 475);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 225);
            R = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Circular, 250, 1200, 150);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Ezreal : SpellBase
    {
        public Ezreal()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1200, SkillShotType.Linear, 250, 2000, 60) {AllowedCollisionCount = 0};
            W = new Spell.Skillshot(SpellSlot.W, 1050, SkillShotType.Linear, 250, 1600, 80);
            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Linear, 250, 2000, 80);
            R = new Spell.Skillshot(SpellSlot.R, 5000, SkillShotType.Linear, 1000, 2000, 160);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class FiddleSticks : SpellBase
    {
        public FiddleSticks()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 575);
            W = new Spell.Targeted(SpellSlot.W, 575);
            E = new Spell.Targeted(SpellSlot.E, 750);
            R = new Spell.Skillshot(SpellSlot.R, 800, SkillShotType.Circular, 1750, int.MaxValue, 600);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Fiora : SpellBase
    {
        public Fiora()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 750, SkillShotType.Linear);
            W = new Spell.Skillshot(SpellSlot.W, 750, SkillShotType.Linear, 500, 3200, 70);
            E = new Spell.Active(SpellSlot.E, 200);
            R = new Spell.Targeted(SpellSlot.R, 500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Fizz : SpellBase
    {
        public Fizz()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 550);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 400, SkillShotType.Circular, 250, int.MaxValue, 330);
            R = new Spell.Skillshot(SpellSlot.R, 1300, SkillShotType.Linear, 250, 1200, 80);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Galio : SpellBase
    {
        public Galio()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 940, SkillShotType.Circular, 250, 1300, 120);
            W = new Spell.Targeted(SpellSlot.W, 830);
            E = new Spell.Skillshot(SpellSlot.E, 1180, SkillShotType.Linear, 250, 1200, 140);
            R = new Spell.Active(SpellSlot.R, 560);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Gangplank : SpellBase
    {
        public Gangplank()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1150, SkillShotType.Circular, 450, 2000, 390);
            R = new Spell.Skillshot(SpellSlot.R, int.MaxValue, SkillShotType.Circular, 250, int.MaxValue, 600);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Garen : SpellBase
    {
        public Garen()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E, 300);
            R = new Spell.Targeted(SpellSlot.R, 400);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    //TODO: Gnar
    /*public class Gnar : SpellBase 
    {
        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
        public Gnar()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 1200, 55);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Circular, 500, int.MaxValue, 150);
            R = new Spell.Active(SpellSlot.R);
        }
    }*/

    public class Gragas : SpellBase
    {
        public Gragas()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 775, SkillShotType.Circular, 1, 1000, 110);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 675, SkillShotType.Linear, 0, 1000, 50);
            R = new Spell.Skillshot(SpellSlot.R, 1100, SkillShotType.Circular, 1, 1000, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Graves : SpellBase
    {
        public Graves()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 0, 3000, 40) {AllowedCollisionCount = 0};
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 500, 1500, 120);
            E = new Spell.Skillshot(SpellSlot.E, 425, SkillShotType.Linear, 500, 0, 50);
            R = new Spell.Skillshot(SpellSlot.R, 1000, SkillShotType.Linear, 500, 2100, 100);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Hecarim : SpellBase
    {
        public Hecarim()
        {
            Q = new Spell.Active(SpellSlot.Q, 350);
            W = new Spell.Active(SpellSlot.W, 525);
            E = new Spell.Active(SpellSlot.E, 450);
            R = new Spell.Skillshot(SpellSlot.R, 1000, SkillShotType.Linear, 250, 800, 200);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Heimerdinger : SpellBase
    {
        public Heimerdinger()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 350, SkillShotType.Linear, (int) 0.5f, 1450, (int) 40f);
            W = new Spell.Skillshot(SpellSlot.W, 1325, SkillShotType.Cone, (int) 0.5f, 902, 200);
            E = new Spell.Skillshot(SpellSlot.E, 970, SkillShotType.Circular, (int) 0.5f, 2500, 120);
            R = new Spell.Active(SpellSlot.R, 350);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Illaoi : SpellBase
    {
        public Illaoi()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 850, SkillShotType.Linear, 750, int.MaxValue, 100);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 950, SkillShotType.Linear, 250, 1900, 50);
            R = new Spell.Active(SpellSlot.R, 450);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Irelia : SpellBase
    {
        public Irelia()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 425);
            R = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Linear, 250, 1600, 120);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Janna : SpellBase
    {
        public Janna()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 300);
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Active(SpellSlot.R, 725);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class JarvanIV : SpellBase
    {
        public JarvanIV()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 830, SkillShotType.Linear);
            W = new Spell.Active(SpellSlot.W, 520);
            E = new Spell.Skillshot(SpellSlot.E, 860, SkillShotType.Circular);
            R = new Spell.Targeted(SpellSlot.R, 650);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Jax : SpellBase
    {
        public Jax()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 700);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E, 187);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    //TODO: Jayce
    /*public class Jayce : SpellBase
    {
        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
        public Jayce()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }
    }*/
    //TODO: Jhin
    /*public class Jhin : SpellBase
    {
        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
        public Jhin()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }
    }*/

    public class Jinx : SpellBase
    {
        public Jinx()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 1450, SkillShotType.Linear, 500, 1500, 60) {AllowedCollisionCount = 0};
            E = new Spell.Skillshot(SpellSlot.E, 900, SkillShotType.Circular, 1200, 1750, 100);
            R = new Spell.Skillshot(SpellSlot.R, 3000, SkillShotType.Linear, 700, 1500, 140);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Kalista : SpellBase
    {
        public Kalista()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1150, SkillShotType.Linear, 250, 1200, 40);
            W = new Spell.Targeted(SpellSlot.W, 5000);
            E = new Spell.Active(SpellSlot.E, 1000);
            R = new Spell.Active(SpellSlot.R, 1500); //You are gonna suck until you get logic
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Karma : SpellBase
    {
        public Karma()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 1500, 100);
            W = new Spell.Targeted(SpellSlot.W, 675);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Karthus : SpellBase
    {
        public Karthus()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, 1000, int.MaxValue, 160);
            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Circular, 500, int.MaxValue, 70);
            E = new Spell.Active(SpellSlot.E, 505);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Kassadin : SpellBase
    {
        public Kassadin()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 650);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 400, SkillShotType.Cone, (int) 0.5f, int.MaxValue, 10);
            R = new Spell.Skillshot(SpellSlot.R, 700, SkillShotType.Circular, (int) 0.5f, int.MaxValue, 150);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Katarina : SpellBase
    {
        public Katarina()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 675);
            W = new Spell.Active(SpellSlot.W, 375);
            E = new Spell.Targeted(SpellSlot.E, 700);
            R = new Spell.Active(SpellSlot.R, 550);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Kayle : SpellBase
    {
        public Kayle()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 650);
            W = new Spell.Targeted(SpellSlot.W, 900);
            E = new Spell.Skillshot(SpellSlot.E, 650, SkillShotType.Circular, 1, 50, 400);
            R = new Spell.Targeted(SpellSlot.R, 900);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Kennen : SpellBase
    {
        public Kennen()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 125, 1700, 50);
            W = new Spell.Active(SpellSlot.W, 900);
            E = new Spell.Active(SpellSlot.E, 500); //Kappa ;)
            R = new Spell.Active(SpellSlot.R, 500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Khazix : SpellBase
    {
        public Khazix()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 325);
            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Linear, 225, 828, 80);
            E = new Spell.Skillshot(SpellSlot.E, 600, SkillShotType.Circular, 25, 1000, 100);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Kindred : SpellBase
    {
        public Kindred()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1125, SkillShotType.Linear);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 500);
            R = new Spell.Targeted(SpellSlot.R, 500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class KogMaw : SpellBase
    {
        public KogMaw()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 980, SkillShotType.Linear, 250, 2000, 50)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W = new Spell.Active(SpellSlot.W, 700);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, 250, 1530, 60);
            R = new Spell.Skillshot(SpellSlot.R, 1200, SkillShotType.Circular, 250, 1200, 30);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Leblanc : SpellBase
    {
        public Leblanc()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 700);
            W = new Spell.Skillshot(SpellSlot.W, 600, SkillShotType.Circular, 250, 1450, 250);
            E = new Spell.Skillshot(SpellSlot.E, 950, SkillShotType.Linear, 250, 1550, 55) {AllowedCollisionCount = 0};
            R = new Spell.Targeted(SpellSlot.R, 950);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    //TODO: Second Spells
    public class LeeSin : SpellBase
    {
        public LeeSin()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 1800, 60) {AllowedCollisionCount = 0};
            //Q2 = new Spell.Active(SpellSlot.Q, 1300);
            W = new Spell.Skillshot(SpellSlot.W, 1200, SkillShotType.Linear, 50, 1500, 100);
            //W2 = new Spell.Active(SpellSlot.W, 700);
            E = new Spell.Skillshot(SpellSlot.E, 350, SkillShotType.Linear, 250, 2500, 100);
            //E2 = new Spell.Skillshot(SpellSlot.E, 675, SkillShotType.Linear, 250, 2500, 100)
            R = new Spell.Targeted(SpellSlot.R, 375);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Leona : SpellBase
    {
        public Leona()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W, 275);
            E = new Spell.Skillshot(SpellSlot.E, 875, SkillShotType.Linear, 250, 2000, 70);
            R = new Spell.Skillshot(SpellSlot.R, 1200, SkillShotType.Circular, 1000, int.MaxValue, 250);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Lissandra : SpellBase
    {
        public Lissandra()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 725, SkillShotType.Linear);
            //Q1 = new Spell.Skillshot(SpellSlot.Q, 825, SkillShotType.Linear);
            W = new Spell.Active(SpellSlot.W, 450);
            E = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Linear);
            //E1 = new Spell.Active(SpellSlot.E);
            R = new Spell.Targeted(SpellSlot.R, 550);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Lucian : SpellBase
    {
        public Lucian()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 675);
            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Linear, 250, 1600, 80);
            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Linear);
            R = new Spell.Skillshot(SpellSlot.R, 1400, SkillShotType.Linear, 500, 2800, 110);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Lulu : SpellBase
    {
        public Lulu()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 1450, 60);
            W = new Spell.Targeted(SpellSlot.W, 650);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Targeted(SpellSlot.R, 900);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Lux : SpellBase
    {
        public Lux()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1175, SkillShotType.Linear, 250, 1200, 65) {AllowedCollisionCount = 1};
            W = new Spell.Skillshot(SpellSlot.W, 1075, SkillShotType.Linear, 0, 1400, 85);
            E = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Circular, 250, 1300, 330);
            R = new Spell.Skillshot(SpellSlot.R, 3200, SkillShotType.Circular, 500, int.MaxValue, 160);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Malphite : SpellBase
    {
        public Malphite()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E, 400);
            R = new Spell.Skillshot(SpellSlot.R, 1000, SkillShotType.Circular, 250, 700, 270);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Malzahar : SpellBase
    {
        public Malzahar()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 500, int.MaxValue, 100)
            {
                MinimumHitChance = HitChance.High
            };
            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 500, int.MaxValue, 250);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Maokai : SpellBase
    {
        public Maokai()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 600, SkillShotType.Linear, 500, 1200, 110);
            W = new Spell.Targeted(SpellSlot.W, 525);
            E = new Spell.Skillshot(SpellSlot.E, 1075, SkillShotType.Circular, 1000, 1500, 225);
            R = new Spell.Active(SpellSlot.R, 475);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class MasterYi : SpellBase
    {
        public MasterYi()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class MissFortune : SpellBase
    {
        public MissFortune()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 650);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Circular, 500, int.MaxValue, 200);
            R = new Spell.Skillshot(SpellSlot.R, 1400, SkillShotType.Cone, 0, int.MaxValue);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Mordekaiser : SpellBase
    {
        public Mordekaiser()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Targeted(SpellSlot.W, 1000);
            E = new Spell.Skillshot(SpellSlot.E, 670, SkillShotType.Cone, (int) 0.25f, 2000, 12*2*(int) Math.PI/180);
            R = new Spell.Targeted(SpellSlot.R, 1500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Morgana : SpellBase
    {
        public Morgana()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1200, SkillShotType.Linear, 250, 1200, 80);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 250, 2200, 400);
            E = new Spell.Targeted(SpellSlot.E, 750);
            R = new Spell.Active(SpellSlot.R, 600);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Nami : SpellBase
    {
        public Nami()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 875, SkillShotType.Circular, 1, int.MaxValue, 150);
            W = new Spell.Targeted(SpellSlot.W, 725);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Skillshot(SpellSlot.R, 2750, SkillShotType.Linear, 250, 500, 160);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Nasus : SpellBase
    {
        public Nasus()
        {
            Q = new Spell.Active(SpellSlot.Q, 150);
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Skillshot(SpellSlot.E, 650, SkillShotType.Circular, 250, 190, int.MaxValue);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Nautilus : SpellBase
    {
        public Nautilus()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E,
                (uint) ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).SData.CastRange);
            R = new Spell.Targeted(SpellSlot.R,
                (uint) ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).SData.CastRange);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    //TODO: Nidalee
    /*public class Nidalee : SpellBase
    {
        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
        public Nidalee()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }
    }*/

    public class Nocturne : SpellBase
    {
        public Nocturne()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1125, SkillShotType.Linear);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 425);
            R = new Spell.Active(SpellSlot.R, 2500);
            // R1 = new Spell.Targeted(SpellSlot.R, R.Range);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Nunu : SpellBase
    {
        public Nunu()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 350);
            W = new Spell.Targeted(SpellSlot.W, 700);
            E = new Spell.Targeted(SpellSlot.E, 550);
            R = new Spell.Active(SpellSlot.R, 650);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Olaf : SpellBase
    {
        public Olaf()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 250, 1550, 75);
            //Q2 = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1550, 75)     
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 325);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    //TODO: Orianna
    /*public class Orianna : SpellBase
    {
        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
        public Orianna()
        {
            {
                Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 250, 1550, 75);
                //Q2 = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1550, 75)     
                W = new Spell.Active(SpellSlot.W);
                E = new Spell.Targeted(SpellSlot.E, 325);
                R = new Spell.Active(SpellSlot.R);
            }
    } */

    public class Pantheon : SpellBase
    {
        public Pantheon()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Skillshot(SpellSlot.E, 600, SkillShotType.Cone, 250, 2000, 70);
            R = new Spell.Skillshot(SpellSlot.R, 2000, SkillShotType.Circular);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Poppy : SpellBase
    {
        public Poppy()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 430, SkillShotType.Linear, 250, null, 100);
            W = new Spell.Active(SpellSlot.W, 400);
            E = new Spell.Targeted(SpellSlot.E, 525);
            R = new Spell.Chargeable(SpellSlot.R, 500, 1200, 4000, 250, int.MaxValue, 90);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Quinn : SpellBase
    {
        public Quinn()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1025, SkillShotType.Linear, 0, 750, 210);
            W = new Spell.Active(SpellSlot.W, 2100);
            E = new Spell.Targeted(SpellSlot.E, 675);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Rammus : SpellBase
    {
        public Rammus()
        {
            Q = new Spell.Active(SpellSlot.Q, 200);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 325);
            R = new Spell.Active(SpellSlot.R, 300);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class RekSai : SpellBase
    {
        public RekSai()
        {
            Q = new Spell.Active(SpellSlot.Q, 325);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 250);
            R = new Spell.Targeted(SpellSlot.R, 0);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Renekton : SpellBase
    {
        public Renekton()
        {
            Q = new Spell.Active(SpellSlot.Q, 225);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 450, SkillShotType.Linear);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Rengar : SpellBase
    {
        public Rengar()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 500, SkillShotType.Circular, 250, 2000, 100);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, 250, 1500, 140);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Riven : SpellBase
    {
        public Riven()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            W = new Spell.Active(SpellSlot.W, (uint) (70 + ObjectManager.Player.BoundingRadius + 120));
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Rumble : SpellBase
    {
        public Rumble()
        {
            Q = new Spell.Active(SpellSlot.Q, 600);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 840, SkillShotType.Linear, 250, 2000, 70);
            R = new Spell.Skillshot(SpellSlot.R, 1700, SkillShotType.Linear, 400, 2500, 120);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Ryze : SpellBase
    {
        public Ryze()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1700, 100);
            //Q2 = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1700, 100);
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Targeted(SpellSlot.E, 600);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Sejuani : SpellBase
    {
        public Sejuani()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 650, SkillShotType.Linear, 0, 1600, 70);
            W = new Spell.Active(SpellSlot.W, 350);
            E = new Spell.Active(SpellSlot.E, 1000);
            R = new Spell.Skillshot(SpellSlot.R, 1175, SkillShotType.Linear, 250, 1600, 110);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Shaco : SpellBase
    {
        public Shaco()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 400);
            //Q2 = new Spell.Targeted(SpellSlot.Q, 1100);
            W = new Spell.Targeted(SpellSlot.W, 425);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Targeted(SpellSlot.R, 200);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Shen : SpellBase
    {
        public Shen()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 2000, SkillShotType.Linear, 500, 2500, 150);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 610, SkillShotType.Linear, 500, 1600, 50);
            R = new Spell.Targeted(SpellSlot.R, 31000);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Shyvana : SpellBase
    {
        public Shyvana()
        {
            Q = new Spell.Active(SpellSlot.Q, (uint) Player.Instance.GetAutoAttackRange());
            W = new Spell.Active(SpellSlot.W, 425);
            E = new Spell.Skillshot(SpellSlot.E, 925, SkillShotType.Linear, 250, 1500, 60);
            R = new Spell.Skillshot(SpellSlot.R, 1000, SkillShotType.Circular, 250, 1500, 150);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Singed : SpellBase
    {
        public Singed()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Circular, 500, 700, 350);
            E = new Spell.Targeted(SpellSlot.E, 125);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Sion : SpellBase
    {
        public Sion()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 740, SkillShotType.Cone, 250, 100, 500);
            //Q2 = new Spell.Active(SpellSlot.Q, 680);
            W = new Spell.Active(SpellSlot.W, 490);
            E = new Spell.Skillshot(SpellSlot.E, 755, SkillShotType.Linear);
            R = new Spell.Active(SpellSlot.R, 800);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Sivir : SpellBase
    {
        public Sivir()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1245, SkillShotType.Linear, (int) 0.25, 1030, 90);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R, 1000);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Skarner : SpellBase
    {
        public Skarner()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Sona : SpellBase
    {
        public Sona()
        {
            Q = new Spell.Active(SpellSlot.Q, 850);
            W = new Spell.Active(SpellSlot.W, 1000);
            E = new Spell.Active(SpellSlot.E, 350);
            R = new Spell.Skillshot(SpellSlot.R, 1000, SkillShotType.Circular, 250, 2400, 140);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Soraka : SpellBase
    {
        public Soraka()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, (int) 0.283f, 1100, (int) 210f);
            W = new Spell.Targeted(SpellSlot.W, 550);
            E = new Spell.Skillshot(SpellSlot.E, 925, SkillShotType.Circular, (int) 0.5f, 1750, (int) 70f);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Swain : SpellBase
    {
        public Swain()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 820, SkillShotType.Circular, 500, 1250, 275);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Syndra : SpellBase
    {
        public Syndra()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Circular, 600, int.MaxValue, 125);
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 250, 1600, 140);
            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Cone, 250, 2500, 22);
            R = new Spell.Targeted(SpellSlot.R, 675);
            //EQ = new Spell.Skillshot(SpellSlot.Q, 1200, SkillShotType.Linear, 500, 2500, 55);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class TahmKench : SpellBase
    {
        public TahmKench()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Talon : SpellBase
    {
        public Talon()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 600, SkillShotType.Cone, 1, 2300, 75)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Targeted(SpellSlot.E, 700);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Taric : SpellBase
    {
        public Taric()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 750);
            W = new Spell.Active(SpellSlot.W, 400);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Active(SpellSlot.R, 400);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Teemo : SpellBase
    {
        public Teemo()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 680);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R, 300, SkillShotType.Circular, 500, 1000, 120);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Thresh : SpellBase
    {
        public Thresh()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1040, SkillShotType.Linear, 500, 1900, 60) {AllowedCollisionCount = 0};
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 250, 1800, 300)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Skillshot(SpellSlot.E, 480, SkillShotType.Linear, 0, 2000, 110)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Active(SpellSlot.R, 450);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Tristana : SpellBase
    {
        public Tristana()
        {
            Q = new Spell.Active(SpellSlot.Q, 550);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 450, int.MaxValue, 180);
            E = new Spell.Targeted(SpellSlot.E, 550);
            R = new Spell.Targeted(SpellSlot.R, 550);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Trundle : SpellBase
    {
        public Trundle()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 0, int.MaxValue, 1000);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Circular, 250, int.MaxValue, 225);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Tryndamere : SpellBase
    {
        public Tryndamere()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W, 400);
            E = new Spell.Skillshot(SpellSlot.E, 660, SkillShotType.Linear, 250, 700, (int) 92.5);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class TwistedFate : SpellBase
    {
        public TwistedFate()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1450, SkillShotType.Linear, 0, 1000, 40);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R, 5500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Twitch : SpellBase
    {
        public Twitch()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 925, SkillShotType.Circular, 250, 1400, 275)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Active(SpellSlot.E, 1200);
            R = new Spell.Active(SpellSlot.R, 900);
            //R2 = new Spell.Skillshot(SpellSlot.R, 1200, SkillShotType.Linear, 0, 3000, 100)
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Udyr : SpellBase
    {
        public Udyr()
        {
            Q = new Spell.Active(SpellSlot.Q, 250);
            W = new Spell.Active(SpellSlot.W, 250);
            E = new Spell.Active(SpellSlot.E, 250);
            R = new Spell.Active(SpellSlot.R, 500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Urgot : SpellBase
    {
        public Urgot()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 125, 1600, 60);
            //Q2 = new Spell.Targeted(SpellSlot.Q, 1200);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 900, SkillShotType.Circular, 250, 1500, 210);
            R = new Spell.Targeted(SpellSlot.R, 850);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Varus : SpellBase
    {
        public Varus()
        {
            //Q2 = new Spell.Skillshot(SpellSlot.Q, 925, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 0, 1900, 100);
            //Q2.AllowedCollisionCount = int.MaxValue;
            Q = new Spell.Chargeable(SpellSlot.Q, 925, 1625, 2000, 0, 1900, 100);
            E = new Spell.Skillshot(SpellSlot.E, 925, SkillShotType.Circular, 500, int.MaxValue, 750);
            R = new Spell.Skillshot(SpellSlot.R, 1075, SkillShotType.Linear, 0, 1200, 120);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Vayne : SpellBase
    {
        public Vayne()
        {
            Q = new Spell.Active(SpellSlot.Q, 300);
            //Q2 = new Spell.Skillshot(SpellSlot.Q, 300, SkillShotType.Linear);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 590);
            //E2 = new Spell.Skillshot(SpellSlot.E, 590, SkillShotType.Linear, 250, 1250);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Veigar : SpellBase
    {
        public Veigar()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 2000, 70);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 1350, 0, 225);
            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Circular, 500, 0, 425);
            R = new Spell.Targeted(SpellSlot.R, 650);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Velkoz : SpellBase
    {
        public Velkoz()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1050, SkillShotType.Linear, 250, 1300, 50)
            {
                MinimumHitChance = HitChance.High,
                AllowedCollisionCount = 0
            };
            W = new Spell.Skillshot(SpellSlot.W, 1050, SkillShotType.Linear, 250, 1700, 80)
            {
                MinimumHitChance = HitChance.High,
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Skillshot(SpellSlot.E, 850, SkillShotType.Circular, 500, 1500, 120)
            {
                MinimumHitChance = HitChance.High,
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Skillshot(SpellSlot.R, 1550, SkillShotType.Linear)
            {
                MinimumHitChance = HitChance.High,
                AllowedCollisionCount = int.MaxValue
            };
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Vi : SpellBase
    {
        public Vi()
        {
            Q = new Spell.Chargeable(SpellSlot.Q, 250, 875, 1250, 0, 1400, 55);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E, 600);
            //E2 = new Spell.Skillshot(SpellSlot.E, 600, SkillShotType.Cone);
            R = new Spell.Targeted(SpellSlot.R, 800);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Viktor : SpellBase
    {
        public Viktor()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular, 500, int.MaxValue, 300)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Skillshot(SpellSlot.E, 525, SkillShotType.Linear, 250, int.MaxValue, 100)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Skillshot(SpellSlot.R, 700, SkillShotType.Circular, 250, int.MaxValue, 450)
            {
                AllowedCollisionCount = int.MaxValue
            };
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Vladimir : SpellBase
    {
        public Vladimir()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Active(SpellSlot.W, 150);
            E = new Spell.Active(SpellSlot.E, 600);
            R = new Spell.Skillshot(SpellSlot.R, 750, SkillShotType.Circular, 250, int.MaxValue, 170);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Volibear : SpellBase
    {
        public Volibear()
        {
            Q = new Spell.Active(SpellSlot.Q, 750);
            W = new Spell.Targeted(SpellSlot.W, 395);
            E = new Spell.Active(SpellSlot.E, 415);
            R = new Spell.Active(SpellSlot.R);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Warwick : SpellBase
    {
        public Warwick()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 400);
            W = new Spell.Active(SpellSlot.W, 1250);
            E = new Spell.Active(SpellSlot.E, 1500);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class MonkeyKing : SpellBase
    {
        public MonkeyKing()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Xerath : SpellBase
    {
        public Xerath()
        {
            Q = new Spell.Chargeable(SpellSlot.Q, 750, 1500, 1500, 500, int.MaxValue, 100);
            W = new Spell.Skillshot(SpellSlot.W, 1100, SkillShotType.Circular, 250, int.MaxValue, 100);
            E = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Linear, 250, 1600, 70);
            R = new Spell.Skillshot(SpellSlot.R, 3200, SkillShotType.Circular, 500, int.MaxValue, 120);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class XinZhao : SpellBase
    {
        public XinZhao()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Active(SpellSlot.R, 500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Yasuo : SpellBase
    {
        public Yasuo()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear);
            W = new Spell.Skillshot(SpellSlot.W, 400, SkillShotType.Linear);
            E = new Spell.Targeted(SpellSlot.E, 475);
            R = new Spell.Active(SpellSlot.R, 1200);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Yorick : SpellBase
    {
        public Yorick()
        {
            Q = new Spell.Active(SpellSlot.Q, 125);
            W = new Spell.Skillshot(SpellSlot.W, 585, SkillShotType.Circular, 250, int.MaxValue, 200);
            E = new Spell.Targeted(SpellSlot.E, 540);
            R = new Spell.Targeted(SpellSlot.R, 835);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Zac : SpellBase
    {
        public Zac()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 550, SkillShotType.Linear, 500, int.MaxValue, 120);
            W = new Spell.Active(SpellSlot.W, 350);
            E = new Spell.Chargeable(SpellSlot.E, 0, 1750, 1500, 500, 1500, 250);
            R = new Spell.Active(SpellSlot.R, 300);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Zed : SpellBase
    {
        public Zed()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Ziggs : SpellBase
    {
        public Ziggs()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 850, SkillShotType.Linear, 300, 1700, 130);
            W = new Spell.Active(SpellSlot.W, 1000);
            E = new Spell.Skillshot(SpellSlot.E, 900, SkillShotType.Linear, 250, 1530, 60);
            R = new Spell.Skillshot(SpellSlot.R, 5300, SkillShotType.Circular, 1000, 2800, 500);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Zilean : SpellBase
    {
        public Zilean()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Circular, 300, 2000, 150);
            W = new Spell.Active(SpellSlot.W, 700);
            E = new Spell.Targeted(SpellSlot.E, 1000);
            R = new Spell.Targeted(SpellSlot.R, 410);
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }

    public class Zyra : SpellBase
    {
        public Zyra()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Linear, 250, int.MaxValue, 85);
            W = new Spell.Skillshot(SpellSlot.W, 825, SkillShotType.Circular, 250, int.MaxValue, 20);
            E = new Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Linear, 250, 1150, 70);
            R = new Spell.Skillshot(SpellSlot.R, 700, SkillShotType.Circular, 250, 1200, 500);

            QisCC = true;
            EisCC = true;
            RisCC = true;
        }

        public sealed override Spell.SpellBase Q { get; set; }
        public sealed override Spell.SpellBase W { get; set; }
        public sealed override Spell.SpellBase E { get; set; }
        public sealed override Spell.SpellBase R { get; set; }
    }
}