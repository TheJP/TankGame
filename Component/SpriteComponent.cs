using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniMonoGame.Service;

namespace MiniMonoGame.Component
{
    internal class SpriteComponent
    {
        public SpriteType Type { get; }

        public SpriteComponent(SpriteType type) => Type = type;
    }
}
