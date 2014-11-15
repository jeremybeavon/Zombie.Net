using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Cookie
    {
        private readonly dynamic cookie;

        internal Cookie(dynamic cookie)
        {
            this.cookie = cookie;
        }

        public string Name
        {
            get { return cookie.name; }
        }

        public string Value
        {
            get { return cookie.value; }
        }
    }
}
