using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace BasicBracket
{
    public static class LoggerExtension
    {
        private static ILog _log;
        static LoggerExtension()
        {
            _log = log4net.LogManager.GetLogger(nameof(AppDomain.FriendlyName));
        }

        public static void SetFilePath(string path)
        {

        }
    }
}
