using System;
using Merlin2d.Game;
using Cviko6.Factories;
using Cviko6.Commands;
using Cviko6.Actors;
using Merlin2d.Game.Enums;

namespace Cviko6
{
    class Program
    {
        static void Main(string[] args)
        {
            GameContainer container = new GameContainer("Game", 1280, 720);
            IWorld world = container.GetWorld();
            world.SetFactory(new ActorFactory());

            container.SetMap("resources/maps/map03.tmx");

            Gravity gravity = new Gravity();
            gravity.SetWorld(world);
            container.GetWorld().SetPhysics(gravity);

            Action<IWorld> setCamera = world =>
            {
                world.CenterOn(world.GetActors().Find(a => a.GetName() == "Player"));
            };
            world.AddInitAction(setCamera);
            

            container.Run();
        }
    }
}
