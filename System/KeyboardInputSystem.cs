using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiniMonoGame.Component;
using MiniMonoGame.Service;
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
        private readonly Globals globals;
        private readonly BulletSpecification playerBullet;

        private ComponentMapper<KeyboardPlayer> keyboardPlayerMapper;
        private ComponentMapper<Transform2> transformMapper;

        public KeyboardInputSystem(Globals globals, BulletSpecification playerBullet) : base(Aspect.All(typeof(KeyboardPlayer), typeof(Transform2)))
        {
            this.globals = globals;
            this.playerBullet = playerBullet;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            keyboardPlayerMapper = mapperService.GetMapper<KeyboardPlayer>();
            transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Process(GameTime gameTime, int entity)
        {
            var keyboard = Keyboard.GetState();
            var keyboardPlayer = keyboardPlayerMapper.Get(entity);
            var transform = transformMapper.Get(entity);

            // Handle movement and rotation.
            var direction = Vector2.Zero;
            if (keyboard.IsKeyDown(Keys.A)) { direction.X -= 1; }
            if (keyboard.IsKeyDown(Keys.D)) { direction.X += 1; }
            if (keyboard.IsKeyDown(Keys.W)) { direction.Y -= 1; }
            if (keyboard.IsKeyDown(Keys.S)) { direction.Y += 1; }

            var lengthSquared = direction.LengthSquared();
            if (lengthSquared > 0.001f)
            {
                direction *= 1f / MathF.Sqrt(lengthSquared);
                transform.Position += direction * (keyboardPlayer.movementSpeed * globals.TileSize * (float)gameTime.ElapsedGameTime.TotalSeconds);
                transform.Rotation = MathF.Atan2(direction.Y, direction.X);
            }

            // Handle bullet firing.
            if (keyboard.IsKeyDown(Keys.F) && gameTime.TotalGameTime - keyboardPlayer.LastBulletFired > playerBullet.FiringCooldown)
            {
                keyboardPlayer.LastBulletFired = gameTime.TotalGameTime;
                var bullet = CreateEntity();
                bullet.Attach(new SpriteComponent(SpriteType.Bullet));
                bullet.Attach(new Transform2(transform.Position, transform.Rotation));
                bullet.Attach(new Velocity(new Vector2(MathF.Cos(transform.Rotation), MathF.Sin(transform.Rotation)) * playerBullet.FlyingSpeed));
                bullet.Attach(new Expiring(gameTime.TotalGameTime + TimeSpan.FromSeconds(20)));
            }
        }
    }
}
