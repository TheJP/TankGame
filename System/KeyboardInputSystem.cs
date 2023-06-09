﻿using Microsoft.Xna.Framework;
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
        private readonly BulletSpecification playerBullet;

        private ComponentMapper<Tank> tankMapper;
        private ComponentMapper<Transform2> transformMapper;

        public KeyboardInputSystem(BulletSpecification playerBullet) : base(Aspect.All(typeof(Tank), typeof(KeyboardPlayer), typeof(Transform2)))
        {
            this.playerBullet = playerBullet;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            tankMapper = mapperService.GetMapper<Tank>();
            transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Process(GameTime gameTime, int entity)
        {
            var mouse = Mouse.GetState();
            var keyboard = Keyboard.GetState();

            var tank = tankMapper.Get(entity);
            var transform = transformMapper.Get(entity);
            var barrel = transformMapper.Get(tank.BarrelEntity);

            // Handle tank movement.
            var direction = Vector2.Zero;
            if (keyboard.IsKeyDown(Keys.A)) { direction.X -= 1; }
            if (keyboard.IsKeyDown(Keys.D)) { direction.X += 1; }
            if (keyboard.IsKeyDown(Keys.W)) { direction.Y -= 1; }
            if (keyboard.IsKeyDown(Keys.S)) { direction.Y += 1; }

            var lengthSquared = direction.LengthSquared();
            if (lengthSquared > 0.001f)
            {
                var drivingDirection = new Vector2(MathF.Cos(transform.Rotation), MathF.Sin(transform.Rotation));
                transform.Position += drivingDirection * (tank.MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                barrel.Position = transform.Position;
            }

            // Handle tank rotation animation.
            if (lengthSquared > 0.001f)
            {
                var targetRotation = MathF.Atan2(direction.Y, direction.X);
                var rotationDifference = Util.AngleDifference(transform.Rotation, targetRotation);
                if (MathF.Abs(rotationDifference) < tank.RotationSpeed * gameTime.ElapsedGameTime.TotalSeconds)
                {
                    transform.Rotation = targetRotation;
                }
                else
                {
                    var rotationDirection = MathF.Sign(rotationDifference);
                    transform.Rotation += rotationDirection * tank.RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            // Make Barrel rotate to mouse position.
            var mouseDirection = (mouse.Position.ToVector2() / Globals.Instance.TileSize) - barrel.Position;
            barrel.Rotation = MathF.Atan2(mouseDirection.Y, mouseDirection.X);

            // Handle bullet firing.
            if (mouse.LeftButton == ButtonState.Pressed && gameTime.TotalGameTime - tank.LastBulletFired > playerBullet.FiringCooldown)
            {
                tank.LastBulletFired = gameTime.TotalGameTime;
                var bullet = CreateEntity();
                bullet.Attach(new SpriteComponent(SpriteType.Bullet));
                bullet.Attach(new Transform2(barrel.Position, barrel.Rotation));
                bullet.Attach(new Velocity(new Vector2(MathF.Cos(barrel.Rotation), MathF.Sin(barrel.Rotation)) * playerBullet.FlyingSpeed));
                bullet.Attach(new Expiring(gameTime.TotalGameTime + TimeSpan.FromSeconds(20)));
                bullet.Attach(new Bullet());
            }
        }
    }
}
