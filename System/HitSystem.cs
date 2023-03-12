using Microsoft.Xna.Framework;
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
    internal class HitSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> transformMapper;
        private ComponentMapper<Bullet> bulletMapper;
        private ComponentMapper<Hitable> hitableMapper;

        public HitSystem() : base(Aspect.All(typeof(Transform2)).One(typeof(Bullet), typeof(Hitable)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform2>();
            bulletMapper = mapperService.GetMapper<Bullet>();
            hitableMapper = mapperService.GetMapper<Hitable>();
        }

        public override void Update(GameTime gameTime)
        {
            var bullets = ActiveEntities.Where(bulletMapper.Has)
                .Select(e => (entity: e, transform: transformMapper.Get(e)));
            var hitables = ActiveEntities.Where(hitableMapper.Has)
                .Select(e => (entity: e, hitbox: hitableMapper.Get(e).Hitbox, transform: transformMapper.Get(e)));

            foreach (var bullet in bullets)
            {
                foreach (var hitable in hitables)
                {
                    var hit = hitable.hitbox.Hit(hitable.transform.Position, bullet.transform.Position);
                    if (!hit)
                    {
                        continue;
                    }

                    DestroyEntity(bullet.entity);
                    DestroyEntity(hitable.entity);
                    break;
                }
            }
        }
    }
}
