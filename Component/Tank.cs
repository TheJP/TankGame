using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.Component
{
    internal class Tank
    {
        /// <summary>Speed in tiles per second.</summary>
        public readonly float MovementSpeed = 3f;

        /// <summary>Turn speed in radians per second.</summary>
        public readonly float RotationSpeed = MathF.PI;

        public readonly TimeSpan ParticleSpawnCooldown = TimeSpan.FromMilliseconds(50);

        public readonly float ParticleMinDistance = 0.1f;

        public int BarrelEntity { get; }

        public TimeSpan LastBulletFired { get; set; } = TimeSpan.Zero;

        public TimeSpan LastTrackParticle { get; set; } = TimeSpan.Zero;

        public Vector2 LastParticalPosition { get; set; } = Vector2.Zero;

        public Tank(int barrelEntity) => BarrelEntity = barrelEntity;
    }
}
