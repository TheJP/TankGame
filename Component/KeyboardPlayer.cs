using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.Component
{
    internal class KeyboardPlayer
    {
        /// <summary>Speed in tiles per second.</summary>
        public readonly float MovementSpeed = 3f;

        public readonly TimeSpan ParticleSpawnCooldown = TimeSpan.FromMilliseconds(50);

        public TimeSpan LastBulletFired { get; set; } = TimeSpan.Zero;

        public TimeSpan LastTrackParticle { get; set; } = TimeSpan.Zero;
    }
}
