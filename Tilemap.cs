using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame
{
    internal class Tilemap
    {
        private readonly string[] tileSpriteNames = { "Sprites/tileSand1", "Sprites/tileSand2" };
        private Texture2D[] tileSprites;
        private SpriteBatch tilemapSpriteBatch;

        private GraphicsDevice GraphicsDevice { get; }

        public Tilemap(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        public void LoadContent(ContentManager content)
        {
            tilemapSpriteBatch = new SpriteBatch(GraphicsDevice);
            tileSprites = tileSpriteNames.Select(content.Load<Texture2D>).ToArray();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, int tileSize)
        {
            tilemapSpriteBatch.Begin();
            for (int y = 0; y < GraphicsDevice.Viewport.Height; y += tileSize)
            {
                for (int x = 0; x < GraphicsDevice.Viewport.Width; x += tileSize)
                {
                    tilemapSpriteBatch.Draw(tileSprites[0], new Rectangle(x, y, tileSize, tileSize), Color.White);
                }
            }
            tilemapSpriteBatch.End();
        }
    }
}
