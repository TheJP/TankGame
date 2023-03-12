using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniMonoGame.Component;
using MiniMonoGame.Service;
using MiniMonoGame.System;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using System;

namespace MiniMonoGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphics;
        private World world;
        private Tilemap tilemap;
        private TrackParticleSystem trackParticleSystem;

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
            trackParticleSystem = new TrackParticleSystem(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            tilemap.LoadContent(Content);

            var spriteRegistry = new SpriteRegistry();
            spriteRegistry.LoadContent(Content);

            var playerBullet = new BulletSpecification()
            {
                FiringCooldown = TimeSpan.FromMilliseconds(300),
                FlyingSpeed = 8f,
            };

            world = new WorldBuilder()
                .AddSystem(new RenderSystem(GraphicsDevice, spriteRegistry))
                .AddSystem(new KeyboardInputSystem(playerBullet))
                .AddSystem(new ExpirationSystem())
                .AddSystem(new VelocitySystem())
                .AddSystem(new HitSystem())
                .AddSystem(new SpawnTrackParticlesSystem(trackParticleSystem))
                .Build();

            var tank = world.CreateEntity();
            tank.Attach(new Transform2(5, 5));
            tank.Attach(new SpriteComponent(SpriteType.Tank));
            tank.Attach(new KeyboardPlayer());

            var crate = world.CreateEntity();
            crate.Attach(new Transform2(7, 7));
            crate.Attach(new SpriteComponent(SpriteType.Crate));
            crate.Attach(new Hitable(new CircleHitbox(20f / (float)Globals.Instance.TileBaseSize)));
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
            trackParticleSystem.Update(gameTime);
            world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            UpdateTileSize();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            tilemap.Draw(gameTime);
            trackParticleSystem.Draw(gameTime);
            world.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Always scale so 10 tiles fit vertically on screen.
        /// </summary>
        private void UpdateTileSize() => Globals.Instance.TileSize = (int)(GraphicsDevice.Viewport.Height * 0.1f);
    }
}