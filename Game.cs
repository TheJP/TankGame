using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniMonoGame.Component;
using MiniMonoGame.Renderer;
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
        private Crossair crossair;
        private TrackParticleSystem trackParticleSystem;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            tilemap = new Tilemap(GraphicsDevice);
            crossair = new Crossair(GraphicsDevice);
            trackParticleSystem = new TrackParticleSystem(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            tilemap.LoadContent(Content);
            crossair.LoadContent(Content);

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
                .AddSystem(new SpawnCrateSystem())
                .Build();

            var transform = new Transform2(5, 5);

            var tank = world.CreateEntity();
            tank.Attach(transform);
            tank.Attach(new SpriteComponent(SpriteType.Tank));
            tank.Attach(new KeyboardPlayer());

            var barrel = world.CreateEntity();
            barrel.Attach(new Transform2(transform.Position));
            barrel.Attach(new SpriteComponent(SpriteType.Barrel));

            tank.Attach(new Tank(barrel.Id));
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
            crossair.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Always scale so 10 tiles fit vertically on screen.
        /// </summary>
        private void UpdateTileSize() => Globals.Instance.TileSize = (int)(GraphicsDevice.Viewport.Height * 0.1f);
    }
}