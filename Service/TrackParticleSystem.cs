using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMonoGame.Service
{
    internal class TrackParticle
    {
        public Vector2 Position { get; }
        public float Rotation { get; }
        public TimeSpan SpawnTime { get; }

        public TrackParticle(Vector2 position, float rotation, TimeSpan spawnTime)
        {
            Position = position;
            Rotation = rotation;
            SpawnTime = spawnTime;
        }
    }

    internal class TrackParticleSystem
    {
        private static readonly TimeSpan ParticleLiveTime = TimeSpan.FromSeconds(3);
        private static readonly Color ParticleColour = new Color(50, 50, 50, 200);

        private Deque<TrackParticle> Particles { get; } = new();

        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D particleTexture;

        public TrackParticleSystem(GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            particleTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            particleTexture.SetData(new Color[1] { Color.White });
            spriteBatch.Disposing += (object sender, EventArgs e) => particleTexture?.Dispose();
        }

        internal void Spawn(TrackParticle trackParticle) => Particles.AddToBack(trackParticle);

        public void Update(GameTime gameTime)
        {
            while (Particles.GetFront(out var particle) && (gameTime.TotalGameTime - particle.SpawnTime) > ParticleLiveTime)
            {
                Particles.RemoveFromFront();
            }
        }

        public void Draw(GameTime gameTime)
        {
            var globalScale = Globals.Instance.RenderScale;
            var particleScale = new Vector2(4f, 8f);
            var middle = Vector2.One * 0.5f;

            spriteBatch.Begin(blendState: BlendState.NonPremultiplied);
            foreach (var particle in Particles)
            {
                var colour = new Color(ParticleColour, 1f - (float)((gameTime.TotalGameTime - particle.SpawnTime).TotalSeconds / ParticleLiveTime.TotalSeconds));
                spriteBatch.Draw(particleTexture, particle.Position * Globals.Instance.TileSize, null, colour, particle.Rotation, middle, globalScale * particleScale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }
    }
}
