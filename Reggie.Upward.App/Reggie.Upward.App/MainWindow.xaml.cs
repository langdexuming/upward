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
            var obj=this.cbxBrand.SelectedValue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cbxBrand.SelectedValue = (this.cbxBrand.ItemsSource as List<Brand>)[1];
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ;
            Console.WriteLine("123");
        }
    }
}
