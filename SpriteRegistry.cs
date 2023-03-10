using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniMonoGame.SpriteType;

namespace MiniMonoGame
{
    internal enum SpriteType
    {
        Tank,
    }

    internal readonly struct Sprite
    {
        public readonly Texture2D texture;
        public readonly float scaleX;
        public readonly float scaleY;

        public Sprite(Texture2D texture, float scaleX, float scaleY)
        {
            this.texture = texture;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }
    }

    internal class SpriteRegistry
    {
        private readonly (SpriteType type, string name)[] spriteInfos =
        {
        };

        private readonly (SpriteType type, string name, float scaleX, float scaleY)[] spriteInfosWithScale =
        {
            (Tank, "Sprites/tank_green", 46f / 64f, 42f / 64f),
        };

        private readonly Dictionary<SpriteType, Sprite> sprites = new();

        public void LoadContent(ContentManager content)
        {
            var spriteInfosCombined = spriteInfos
                .Select(name => (name.type, name.name, scaleX: 1f, scaleY: 1f))
                .Union(spriteInfosWithScale);

            foreach (var spriteInfo in spriteInfosCombined)
            {
                var texture = content.Load<Texture2D>(spriteInfo.name);
                var sprite = new Sprite(texture, spriteInfo.scaleX, spriteInfo.scaleY);
                sprites.Add(spriteInfo.type, sprite);
            }
        }

        public Sprite this[SpriteType type] => sprites[type];
        public Sprite GetSprite(SpriteType type) => sprites[type];
    }
}
