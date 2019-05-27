using System;
using GameEngineCore.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngineCore.AbstractClasses
{
    public abstract class DrawComponent : IComponent
    {
        public int EntityId { get; set; }
        public Type ComponentType { get; set; }
        //public abstract void Draw(IRenderer renderer); // TODO: break dependency on Monogame framework
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Receive(string attributeName, object payload);
    }
}