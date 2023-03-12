using Microsoft.Xna.Framework;
using MiniMonoGame.Component;
using MiniMonoGame.Service;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.System
{
    internal class SpawnTrackParticlesSystem : EntityProcessingSystem
    {
        private readonly TrackParticleSystem particleSystem;

        private ComponentMapper<Tank> tankMapper;
        private ComponentMapper<Transform2> transformMapper;

        public SpawnTrackParticlesSystem(TrackParticleSystem particleSystem) : base(Aspect.All(typeof(Tank), typeof(Transform2)))
        {
            this.particleSystem = particleSystem;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            tankMapper = mapperService.GetMapper<Tank>();
            transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Process(GameTime gameTime, int entity)
        {
            var tank = tankMapper.Get(entity);
            var transform = transformMapper.Get(entity);

            if ((gameTime.TotalGameTime - tank.LastTrackParticle) <= tank.ParticleSpawnCooldown)
            {
                return;
            }

            if ((tank.LastParticalPosition - transform.Position).LengthSquared() <
                tank.ParticleMinDistance * tank.ParticleMinDistance)
            {
                return;
            }

            tank.LastTrackParticle = gameTime.TotalGameTime;
            tank.LastParticalPosition = transform.Position;

            var direction = new Vector2(MathF.Cos(transform.Rotation), MathF.Sin(transform.Rotation));
            var perpendicularDirection = new Vector2(-direction.Y, direction.X);
            var position = transform.Position - (direction * (0.5f * 40f / 64f));
            var horizontal = perpendicularDirection * (0.5f * 32f / 64f);

            particleSystem.Spawn(new TrackParticle(position + horizontal, transform.Rotation, gameTime.TotalGameTime));
            particleSystem.Spawn(new TrackParticle(position - horizontal, transform.Rotation, gameTime.TotalGameTime));
        }
    }
}
