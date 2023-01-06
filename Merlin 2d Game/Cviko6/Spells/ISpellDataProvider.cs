using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Spells
{
    public interface ISpellDataProvider
    {
        public Dictionary<string, SpellInfo> GetSpellInfo();
        public Dictionary<string, int> GetSpellEffects();
    }

}
