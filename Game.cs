using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MiniMonoGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Tilemap tilemap;
        private Texture2D tankSprite;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            tilemap = new Tilemap(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tankSprite = Content.Load<Texture2D>("Sprites/tank_green");

            tilemap.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            tilemap.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Always scale so 10 tiles fit vertically on screen.
            int tileSize = (int)(GraphicsDevice.Viewport.Height * 0.1f);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            tilemap.Draw(gameTime, tileSize);

            spriteBatch.Begin();
            spriteBatch.Draw(tankSprite, new Rectangle(0, 0, 42, 46), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}