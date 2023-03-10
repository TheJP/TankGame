using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiniMonoGame.Component;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.System
{
    internal class KeyboardInputSystem : EntityProcessingSystem
    {

        private ComponentMapper<KeyboardPlayer> keyboardPlayerMapper;
        private ComponentMapper<Transform2> transformMapper;

        public KeyboardInputSystem() : base(Aspect.All(typeof(KeyboardPlayer), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            keyboardPlayerMapper = mapperService.GetMapper<KeyboardPlayer>();
            transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Process(GameTime gameTime, int entity)
        {
            var keyboardPlayer = keyboardPlayerMapper.Get(entity);
            var transform = transformMapper.Get(entity);

            var direction = Vector2.Zero;
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.A)) { direction.X -= 1; }
            if (keyboard.IsKeyDown(Keys.D)) { direction.X += 1; }
            if (keyboard.IsKeyDown(Keys.W)) { direction.Y -= 1; }
            if (keyboard.IsKeyDown(Keys.S)) { direction.Y += 1; }

            var lengthSquared = direction.LengthSquared();
            if (lengthSquared > 0.001f)
            {
                direction *= 1f / MathF.Sqrt(lengthSquared);
                transform.Position += direction * keyboardPlayer.movementSpeed;
                transform.Rotation = MathF.Atan2(direction.Y, direction.X);
            }
        }
    }
}
