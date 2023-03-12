using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.Component
{
    internal class Hitable
    {
        public Hitbox Hitbox { get; }

        public Hitable(Hitbox hitbox) => Hitbox = hitbox;
    }
}
