using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class PromptResponse
    {
        private PromptResponse(object response)
        {
            Response = response;
        }

        internal object Response { get; private set; }

        public static PromptResponse Value(string response)
        {
            return new PromptResponse(response);
        }

        public static PromptResponse Cancelled()
        {
            return new PromptResponse(false);
        }

        public static PromptResponse DefaultValue()
        {
            return new PromptResponse(null);
        }
    }
}
