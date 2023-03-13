using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.System
{
    internal static class Util
    {
        public static float AngleDifference(float from, float to)
        {
            float difference = to - from;
            if (-MathF.PI < difference && difference <= MathF.PI) { return difference; }
            return difference + (-MathF.Sign(difference) * MathHelper.TwoPi);
        }

        public static float AngleAdd(float angle, float add)
        {
            angle += add;
            while (angle > MathHelper.TwoPi) { angle -= MathHelper.TwoPi; }
            return angle;
        }

        public static float AngleSub(float angle, float sub)
        {
            angle -= sub;
            while (angle < 0f) { angle += MathHelper.TwoPi; }
            return angle;
        }
    }
}
