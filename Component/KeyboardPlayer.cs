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
        public readonly float movementSpeed = 3f;

        public TimeSpan LastBulletFired { get; set; } = TimeSpan.Zero;
    }
}
