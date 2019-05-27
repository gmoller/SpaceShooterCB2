using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic.Components
{
    public interface IComponent
    {
        int EntityId { get; set; }
        ComponentType ComponentType { get; set; }
        void Receive(AttributeType attributeId, object payload);
    }

    //public interface IUpdateComponent : IComponent
    //{
    //    void Update(GameTime gameTime);
    //}

    //public interface IDrawComponent : IComponent
    //{
    //    void Draw(SpriteBatch spriteBatch);
    //}

    public abstract class UpdateComponent : IComponent
    {
        public int EntityId { get; set; }
        public ComponentType ComponentType { get; set; }
        public abstract void Update(GameTime gameTime);
        public abstract void Receive(AttributeType attributeId, object payload);
    }

    public abstract class DrawComponent : IComponent
    {
        public int EntityId { get; set; }
        public ComponentType ComponentType { get; set; }
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Receive(AttributeType attributeId, object payload);
    }
}