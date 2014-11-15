using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Error
    {
        private readonly dynamic error;

        public Error(string message)
        {
            error = new ExpandoObject();
            error.message = message;
        }

        internal Error(dynamic error)
        {
            this.error = error;
        }

        public string Message
        {
            get { return error.message; }
        }

        public string Description
        {
            get { return error.description; }
        }

        public string Name
        {
            get { return error.name; }
        }

        public int Number
        {
            get { return error.number; }
        }

        public string Stack
        {
            get { return error.stack; }
        }

        internal dynamic ToDynamic()
        {
            return error;
        }
    }
}
