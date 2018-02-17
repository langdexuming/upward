using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.IO;

namespace Reggie.Utilities.Extensions
{
    public class Logger
    {
        public enum LogLevel
        {
            Debug,

            Info,

            Warn,

            Error,

            Fatal
        }

        private static ILog _log;

        private static LogLevel _logLevel;

        public static string DefaultFormat
        {
            get
            {
                return "{0}";
            }
        }

        public static void SetLogLevel(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public static void ConfigureAndWatch(FileInfo fileInfo)
        {
            XmlConfigurator.ConfigureAndWatch(fileInfo);
        }

        private static void SetFilePath(string path)
        {
        }

        public static void Debug(string tag,object msg)
        {
            if(msg is Exception)
            {
                LogManager.GetLogger(tag).Debug("",(Exception)msg);
            }
            else
            {
                LogManager.GetLogger(tag).Debug(string.Format(DefaultFormat, msg));
            }
        }

        public static void Info(string tag, object msg)
        {
            if (msg is Exception)
            {
                LogManager.GetLogger(tag).Info("", (Exception)msg);
            }
            else
            {
                LogManager.GetLogger(tag).Info(string.Format(DefaultFormat, msg));
            }
        }

        public static void Warn(string tag, object msg)
        {
            if (msg is Exception)
            {
                LogManager.GetLogger(tag).Warn("", (Exception)msg);
            }
            else
            {
                LogManager.GetLogger(tag).Warn(string.Format(DefaultFormat, msg));
            }
        }

        public static void Error(string tag, object msg)
        {
            if (msg is Exception)
            {
                LogManager.GetLogger(tag).Error("", (Exception)msg);
            }
            else
            {
                LogManager.GetLogger(tag).Error(string.Format(DefaultFormat, msg));
            }
        }

        public static void Error(string tag, string format,params object[] msg)
        {
            LogManager.GetLogger(tag).Error(string.Format(DefaultFormat, String.Format(format,msg)));
        }

        public static void Fatal(string tag, object msg)
        {
            if (msg is Exception)
            {
                LogManager.GetLogger(tag).Fatal("", (Exception)msg);
            }
            else
            {
                LogManager.GetLogger(tag).Fatal(string.Format(DefaultFormat, msg));
            }
        }

    }
}
