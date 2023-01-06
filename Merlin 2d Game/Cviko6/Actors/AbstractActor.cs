using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace Cviko6.Actors
{
    public abstract class AbstractActor : IActor
    {
        private Animation animation;
        private bool del;
        protected int x, y;
        private AbstractActor actor;
        private string name;
        private bool isPhysicsEnabled;
        private IWorld world;

        public AbstractActor()
        {
            
        }
        public AbstractActor(string name)
        {
            this.name = name;
        }

        public virtual Animation GetAnimation()
        {
            return animation;
        }

        public int GetHeight()
        {
            return animation.GetHeight();
        }

        public string GetName()
        {
            return name;
        }

        public int GetWidth()
        {
            return animation.GetWidth();
        }

        public IWorld GetWorld()
        {
            return world;
        }

        public virtual int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public bool IntersectsWithActor(IActor other)
        {
            if (other == null)
                return false;
            if(x < other.GetX())
            {
                if (y < other.GetY())
                {
                    if (other.GetX() - x < 20)
                    {
                        if (other.GetY() - y < 20)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                {
                    if (other.GetX() - x < 20)
                    {
                        if (y - other.GetY() < 20)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            else
            {
                if (y < other.GetY())
                {
                    if (x - other.GetX() < 20)
                    {
                        if (other.GetY() - y < 20)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                {
                    if (x - other.GetX() < 20)
                    {
                        if (y - other.GetY() < 20)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
        }

        public bool IsAffectedByPhysics()
        {
            return this.isPhysicsEnabled;
        }

        public void OnAddedToWorld(IWorld world)
        {
            this.world = world;
            del = false;
        }

        public bool RemovedFromWorld()
        {
            return del;
        }

        public void RemoveFromWorld()
        {
            del = true;
        }

        public void SetAnimation(Animation animation)
        {
            this.animation = animation;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetPhysics(bool isPhysicsEnabled)
        {
            this.isPhysicsEnabled = isPhysicsEnabled;
        }

        public void SetPosition(int posX, int posY)
        {
            this.x = posX;
            this.y = posY;
        }

        public abstract void Update();
        
    }
}
