using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniMonoGame.Component;
using MiniMonoGame.Service;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.System
{
    internal class RenderSystem : EntityDrawSystem
    {
        private readonly Globals globals;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteRegistry spriteRegistry;

        private ComponentMapper<SpriteComponent> spriteMapper;
        private ComponentMapper<Transform2> transformMapper;

        public RenderSystem(Globals globals, GraphicsDevice graphicsDevice, SpriteRegistry spriteRegistry) : base(Aspect.All(typeof(SpriteComponent), typeof(Transform2)))
        {
            this.globals = globals;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.spriteRegistry = spriteRegistry;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            spriteMapper = mapperService.GetMapper<SpriteComponent>();
            transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Draw(GameTime gameTime)
        {
            float globalScale = (float)globals.TileSize / (float)globals.TileBaseSize;

            spriteBatch.Begin();
            foreach (var entity in ActiveEntities)
            {
                var spriteType = spriteMapper.Get(entity).Type;
                var transform = transformMapper.Get(entity);
                var sprite = spriteRegistry[spriteType];

                var scale = new Vector2(sprite.scaleX, sprite.scaleY) * globalScale;
                var middle = new Vector2(sprite.texture.Bounds.Width * 0.5f, sprite.texture.Bounds.Height * 0.5f);

                spriteBatch.Draw(sprite.texture, transform.Position, null, Color.White, transform.Rotation, middle, scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }
    }
}
