using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniMonoGame.Service.SpriteType;

namespace MiniMonoGame.Service
{
    internal enum SpriteType
    {
        Tank,
        Bullet,
        Crate,
        Barrel,
    }

    internal class SpriteRegistry
    {
        private readonly (SpriteType type, string name)[] spriteInfos =
        {
            (Tank, "Sprites/tank_green"),
            (Bullet, "Sprites/bulletGreen3_outline"),
            (Crate, "Sprites/crateMetal"),
            (Barrel, "Sprites/tankGreen_barrel3_outline"),
        };

        private readonly Dictionary<SpriteType, Texture2D> sprites = new();

        public void LoadContent(ContentManager content)
        {
            foreach (var spriteInfo in spriteInfos)
            {
                var texture = content.Load<Texture2D>(spriteInfo.name);
                sprites.Add(spriteInfo.type, texture);
            }
        }

        public Texture2D this[SpriteType type] => sprites[type];
        public Texture2D GetSprite(SpriteType type) => sprites[type];
    }
}
