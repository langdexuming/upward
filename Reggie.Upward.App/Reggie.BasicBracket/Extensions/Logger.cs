using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Reggie.BasicBracket.Extensions
{
    public static class Logger
    {
        private static ILog _log;

        public static void SetFilePath(string path)
        {

        }

        public static void Debug(this ILog log,string tag,object msg)
        {

        }

        public static void Info(this ILog log, string tag, object msg)
        {

        }

        public static void Warn(this ILog log, string tag, object msg)
        {

        }

        public static void Error(this ILog log, string tag, object msg)
        {

        }

        public static void Fatal(this ILog log, string tag, object msg)
        {
            if (msg is Exception)
            {
                 

            }
        }

    }
}
