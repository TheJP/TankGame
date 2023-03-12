using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniMonoGame.Service;
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
        private readonly List<List<int>> tileVariation = new();
        private readonly Random random = new();

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

        private int GetOrGenerateVariation(int x, int y)
        {
            while (y >= tileVariation.Count)
            {
                tileVariation.Add(new());
            }

            while (x >= tileVariation[y].Count)
            {
                tileVariation[y].Add(random.Next(0, tileSpriteNames.Length));
            }

            return tileVariation[y][x];
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            var tileSize = Globals.Instance.TileSize;

            tilemapSpriteBatch.Begin();
            for (int y = 0; y * tileSize < GraphicsDevice.Viewport.Height; ++y)
            {
                for (int x = 0; x * tileSize < GraphicsDevice.Viewport.Width; ++x)
                {
                    var variation = GetOrGenerateVariation(x, y);
                    var rectangle = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    tilemapSpriteBatch.Draw(tileSprites[variation], rectangle, Color.White);
                }
            }
            tilemapSpriteBatch.End();
        }
    }
}
