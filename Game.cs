using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniMonoGame.Component;
using MiniMonoGame.System;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using System;

namespace MiniMonoGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly Globals globals = new();
        private readonly GraphicsDeviceManager graphics;
        private World world;
        private Tilemap tilemap;

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
            tilemap = new Tilemap(globals, GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            tilemap.LoadContent(Content);

            var spriteRegistry = new SpriteRegistry();
            spriteRegistry.LoadContent(Content);

            world = new WorldBuilder()
                .AddSystem(new RenderSystem(globals, GraphicsDevice, spriteRegistry))
                .AddSystem(new KeyboardInputSystem(globals))
                .AddSystem(new ExpirationSystem())
                .Build();

            var tank = world.CreateEntity();
            tank.Attach(new Transform2(200, 200));
            tank.Attach(new SpriteComponent(SpriteType.Tank));
            tank.Attach(new KeyboardPlayer());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            UpdateTileSize();

            tilemap.Update(gameTime);
            world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            UpdateTileSize();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            tilemap.Draw(gameTime);
            world.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Always scale so 10 tiles fit vertically on screen.
        /// </summary>
        private void UpdateTileSize() => globals.TileSize = (int)(GraphicsDevice.Viewport.Height * 0.1f);
    }
}