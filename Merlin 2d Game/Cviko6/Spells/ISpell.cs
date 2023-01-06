using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game.Actions;

namespace Cviko6.Spells
{
    public interface ISpell
    {
        void AddEffect(Command effect);

        void AddEffects(IEnumerable<Command> effects);
        int GetCost();
        void Cast();
    }
}
