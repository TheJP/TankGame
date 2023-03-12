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
    internal class SpawnCrateSystem : EntityUpdateSystem
    {
        private const int MaxCrates = 10;
        private const float SpawnRangeX = 18f;
        private const float SpawnRangeY = 10f;
        private const float CrateHitBox = 20f / 64f;
        private static readonly TimeSpan SpawnCooldown = TimeSpan.FromSeconds(3);

        private readonly Random random = new();
        private TimeSpan lastSpawn = -SpawnCooldown;

        public SpawnCrateSystem() : base(Aspect.All(typeof(Crate))) { }

        public override void Initialize(IComponentMapperService mapperService) { }

        public override void Update(GameTime gameTime)
        {
            if (ActiveEntities.Count >= MaxCrates)
            {
                return;
            }
            
            if (gameTime.TotalGameTime - lastSpawn < SpawnCooldown)
            {
                return;
            }

            lastSpawn = gameTime.TotalGameTime;

            var crate = CreateEntity();
            crate.Attach(new Crate());
            crate.Attach(new Transform2(
                random.NextSingle(0.5f, SpawnRangeX),
                random.NextSingle(0.5f, SpawnRangeY)));
            crate.Attach(new SpriteComponent(SpriteType.Crate));
            crate.Attach(new Hitable(new CircleHitbox(CrateHitBox)));
        }
    }
}
