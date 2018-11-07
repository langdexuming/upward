/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/27 15:08:10
*   描述说明：日志打印
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using log4net;
using log4net.Config;
using System.IO;
using log4net.Core;

namespace Reggie.WPF.Utilities.Extensions
{
    public static class Logger
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            /// 调试
            /// </summary>
            Debug,
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 警告
            /// </summary>
            Warn,
            /// <summary>
            /// 错误
            /// </summary>
            Error,
            /// <summary>
            /// 致命
            /// </summary>
            Fatal
        }

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

            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = ConvertToLevel(logLevel);
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }

        private static Level ConvertToLevel(LogLevel logLevel)
        {
            Level level=Level.Debug;
            switch (logLevel)
            {
                case LogLevel.Debug:
                    level = Level.Debug;
                    break;
                case LogLevel.Info:
                    level = Level.Info;
                    break;
                case LogLevel.Warn:
                    level = Level.Warn;
                    break;
                case LogLevel.Error:
                    level = Level.Error;
                    break;
                case LogLevel.Fatal:
                    level = Level.Fatal;
                    break;
                default:
                    break;
            }

            return level;
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
