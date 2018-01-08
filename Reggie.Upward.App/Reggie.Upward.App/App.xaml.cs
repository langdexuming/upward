using Reggie.BasicBracket.Extensions;
using Reggie.BasicBracket.Utils.File;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Reggie.Upward.App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private const string TAG = nameof(App);
        static App()
        {
            //创建Logs目录
            FileUtil.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Logs"));

            //配置Log
            Logger.ConfigureAndWatch(new FileInfo(ResourceAssembly.Location+ ".config"));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Logger.Info(TAG, "App start");

            this.DispatcherUnhandledException += DispatcherUnhandledExceptionHandler;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Logger.Info(TAG, "App exit");
        }

        /// <summary>
        /// 记录未处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherUnhandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Fatal(TAG,e.Exception);
        }
    }
}
