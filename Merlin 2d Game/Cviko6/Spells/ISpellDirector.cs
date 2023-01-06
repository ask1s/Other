using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Spells
{
    public interface ISpellDirector
    {
        ISpell Build(string spellName);
    }
}
