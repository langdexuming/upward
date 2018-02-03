using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Blog.Constants
{
    public static class LoggingEvents
    {
        public const int DeleteItem = 1005;

        public const int LoginFail = 3001;

#if DEBUG
        public const int TestOutput = 10001;
#endif
    }
}
