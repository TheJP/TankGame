using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MiniMonoGame.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MiniMonoGame.Renderer
{
    internal class Crossair
    {
        private const string crossairSpriteName = "Sprites/crossair_red";

        private SpriteBatch spriteBatch;
        private Texture2D crossairSprite;
        private GraphicsDevice GraphicsDevice { get; }

        public Crossair(GraphicsDevice graphicsDevice) => GraphicsDevice = graphicsDevice;

        public void LoadContent(ContentManager content)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            crossairSprite = content.Load<Texture2D>(crossairSpriteName);
        }

        public void Draw(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            var position = new Vector2(mouse.Position.X - (crossairSprite.Width * 0.5f), mouse.Position.Y - (crossairSprite.Height * 0.5f));
            spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            spriteBatch.Draw(crossairSprite, position, Color.White);
            spriteBatch.End();
        }
    }
}
