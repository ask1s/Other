using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Commands;
using Merlin2d.Game.Actions;

namespace Cviko6.Actors
{
    public interface ICharacter : IMovable
    {
        void ChangeHealth(int delta);
        int GetHealth();
        void Die();
        void AddEffect(Command effect);
        void RemoveEffect(Command effect);
    }
}
