using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.Service
{
    internal class Globals
    {
        private readonly static Lazy<Globals> instance = new();
        public static Globals Instance => instance.Value;

        public int TileSize { get; set; }
        public int TileBaseSize { get; } = 64;
        public float RenderScale => (float)TileSize / (float)TileBaseSize;
    }
}
