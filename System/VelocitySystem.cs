﻿using Microsoft.Xna.Framework;
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
    internal class VelocitySystem : EntityProcessingSystem
    {
        private ComponentMapper<Velocity> velocityMapper;
        private ComponentMapper<Transform2> transformMapper;

        public VelocitySystem() : base(Aspect.All(typeof(Velocity), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            velocityMapper = mapperService.GetMapper<Velocity>();
            transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Process(GameTime gameTime, int entity)
        {
            var velocity = velocityMapper.Get(entity).Value;
            var transform = transformMapper.Get(entity);
            transform.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
