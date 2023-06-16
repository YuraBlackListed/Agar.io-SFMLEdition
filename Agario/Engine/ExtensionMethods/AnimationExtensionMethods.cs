using SFML.Graphics;

namespace Agario.Engine.ExtensionMethods.AnimationExtensionMethods
{
    public static class AnimationExtensionMethods
    {
        public static Sprite TrySwapFrame(this Sprite sprite, Animation animation)
        {
            if (animation.curretFrame < animation.frames.Count - 1)
            {
                animation.curretFrame++;
                return new Sprite(animation.frames[animation.curretFrame]);
            }
            else
            {
                animation.curretFrame = 0;
                return new Sprite(animation.frames[animation.curretFrame]);
            }
        }
    }
}
