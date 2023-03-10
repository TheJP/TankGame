using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMonoGame.Component
{
    internal class Expiring
    {
        public TimeSpan ExpirationTime { get; }

        public Expiring(TimeSpan expirationTime) => ExpirationTime = expirationTime;
    }
}
