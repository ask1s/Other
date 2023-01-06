using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Actors;
using Cviko6.Commands;
using Cviko6.Strategies;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;

namespace Cviko6.Spells
{
    public class ProjectileSpell : AbstractActor, ISpell, IMovable
    {
        private IWizard caster;
        private int cost;
        private List<Command> effect = new List<Command>();
        private IEnumerable<Command> effects = new List<Command>();

        private IWorld world;

        private Player player;

        private Command move;

        private Animation animation;


        public ProjectileSpell(IWizard caster, int cost, Animation animation)
        {
            if(caster != null)
            {
                this.caster = caster;
                this.cost = cost;
                player = (Player)caster;
                this.animation = animation;
                world = player.GetWorld();
            }
        }
        public void AddEffect(Command effect)
        {
            this.effect.Add(effect);
        }

        public void AddEffects(IEnumerable<Command> effects)
        {
            this.effects = effects;
        }

        public void Cast()
        {
            player.GetWorld().AddActor(this);
            SetPosition(player.GetX()+5, player.GetY()+10);
            SetAnimation(animation);
            if (player.orientation == 1)
                move = new Move(this, 1, 3, 0);
            else
            {
                move = new Move(this, 1, -3, 0);
                animation.FlipAnimation();
            }
            animation.Start();
        }

        public int GetCost()
        {
            return cost;
        }

        public double GetSpeed()
        {
            throw new NotImplementedException();
        }

        public void SetSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            move.Execute();

            if(IntersectsWithActor(this.GetWorld().GetActors().Find(a => a.GetName() == "Enemy")))
            {
                foreach (Command effect in effects)
                    effect.Execute();
                world.RemoveActor(this);
            }
            if(world.IntersectWithWall(this))
                world.RemoveActor(this);
        }
    }
}
