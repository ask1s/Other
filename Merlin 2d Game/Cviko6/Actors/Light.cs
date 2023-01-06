using Cviko6.Spells;
using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Actors
{
    public class Light : AbstractActor
    {
        public Light(int count)
        {
            SetAnimation(new Animation("resources/sprites/ligh.png", 48, 48));
            
            if (count == 0)
                SetPosition(64, 416);
            if (count == 1)
                SetPosition(288,480);
            if (count == 2)
                SetPosition(464,448);
            if (count == 3)
                SetPosition(640,448);
            if (count == 4)
                SetPosition(816,480);
            if (count == 5)
                SetPosition(960,432);
            if (count == 6)
                SetPosition(1136,480);
            if (count == 7)
                SetPosition(320,144);
            if (count == 8)
                SetPosition(448,208);
            if (count == 9)
                SetPosition(800,160);
            if (count == 10)
                SetPosition(992,144);

        }

        public override void Update()
        {

        }
    }
}
