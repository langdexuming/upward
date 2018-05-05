using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBrowserApp1
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        private dynamic scriptObject = null;
        public Page1()
        {
            InitializeComponent();

            // Retrieve the script object. The XBAP must be hosted in a frame or
            // the HostScript object will be null.


            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                MessageBox.Show("不满足与JS调用条件");
                return;
            }
            scriptObject = BrowserInteropHelper.HostScript;
            if (scriptObject != null)
            {
                scriptObject.SetWpfObj(new CallbackClass());
            }

        }

        private void JavaScriptInvoke_Click(object sender, RoutedEventArgs e)
        {
            if (scriptObject != null)
                scriptObject.WPFInvokeJS("WPF调用JS函数");
        }

        //记得加上这个特性
        [ComVisible(true)]
        public class CallbackClass
        {
            public string MyMethod(string message)
            {
                return "来自WPF的中转函数，" + message;
            }
        }
    }
}
