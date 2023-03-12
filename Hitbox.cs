using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame
{
    internal interface Hitbox
    {
        /// <summary>Checks if the given point is inside the hitbox.</summary>
        /// <param name="centre">Centre of the hitbox.</param>
        /// <param name="point">Point to check if it is inside the hitbox.</param>
        bool Hit(Vector2 centre, Vector2 point);
    }

    internal class CircleHitbox : Hitbox
    {
        private readonly float radiusSquared;

        public CircleHitbox(float radius) => radiusSquared = radius * radius;

        public bool Hit(Vector2 centre, Vector2 point)
            => (point - centre).LengthSquared() <= radiusSquared;
    }
}
