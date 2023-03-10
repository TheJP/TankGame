using Microsoft.Xna.Framework;
using MiniMonoGame.Component;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.System
{
    internal class ExpirationSystem : EntityProcessingSystem
    {
        private ComponentMapper<Expiring> expiringMapper;

        public ExpirationSystem() : base(Aspect.All(typeof(Expiring)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService) => expiringMapper = mapperService.GetMapper<Expiring>();
        public override void Process(GameTime gameTime, int entity)
        {
            var epirationTime = expiringMapper.Get(entity).ExpirationTime;
            if (gameTime.TotalGameTime > epirationTime)
            {
                DestroyEntity(entity);
            }
        }
    }
}
