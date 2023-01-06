using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Spells
{
    public class SpellInfo
    {
        public string Name { get; set; }
        public SpellType SpellType { get; set; }
        public IEnumerable<string> EffectNames { get; set; }
        public string AnimationPath { get; set; }
        public int AnimationWidth { get; set; }
        public int AnimationHeight { get; set; }

        public static implicit operator SpellInfo(string line)
        {
            string[] values = line.Split(';');

            SpellInfo info = new SpellInfo {Name = values[0], AnimationPath = values[2]};

            if (values[1].ToLower().Equals("projectile"))
            {
                info.SpellType = SpellType.Projectile;
            }
            else if (values[1].ToLower().Equals("self"))
            {
                info.SpellType = SpellType.SelfCast;
            }
            info.EffectNames = values[5].Split(",");
            info.AnimationWidth = Convert.ToInt32(values[3]);
            info.AnimationHeight = Convert.ToInt32(values[4]);

            return info;
        }
    }
}
