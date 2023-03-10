using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.Component
{
    internal class Velocity
    {
        public Vector2 Value { get; }

        public Velocity(Vector2 value) => Value = value;
    }
}
