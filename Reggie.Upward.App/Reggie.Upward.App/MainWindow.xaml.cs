using Reggie.Upward.App.Business.Modules.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Reggie.Utilities.Utils.Wpf;

namespace Reggie.Upward.App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Width = this.Width /6;

            this.gdRoot.Height = 600;
            //this.Height = this.Height / 2;
            this.Loaded += (s, e) =>
            {
                var scale = PresentationUtil.GetScreenScale(this as Visual);

                ;
                //this.gdRoot.Height = 400;
                var path = "d:/044001800111_82056336.pdf";

                // this.webBrowser.Navigate(new Uri(path));


                AdobePDFUserControl pdfCtl = new AdobePDFUserControl();
                windowsFormsHost.Child = pdfCtl;
            };

        }
        private static void Test()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ;
            Console.WriteLine("123");
        }
    }
}
