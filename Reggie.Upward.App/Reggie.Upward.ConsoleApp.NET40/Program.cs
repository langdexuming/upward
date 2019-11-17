using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Reggie.Upward.ConsoleApp.NET40
{
    public class TestModel
    {
        public string ID { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //object missing = System.Reflection.Missing.Value;
            //Excel.Application app = new Excel.Application();
            //app.Application.Workbooks.Add(true);
            //;

            //Excel.Workbook workbook = app.ActiveWorkbook;
            //Excel.Worksheet worksheet = app.ActiveSheet;
            //worksheet.Name = "测试";


            //workbook.SaveCopyAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory , "1.xls"));
            //workbook.Close(false, missing, missing);
            //app.Quit();

            var list = new List<TestModel>();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var memory = GetPrroccessMemory()/1024/1024;
                    Console.WriteLine($"memory:{memory}MB");

                    Thread.Sleep(3 * 1000);
                };
            });

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        list.Add(new TestModel());
                    }
                    Thread.Sleep(3 * 1000);
                };
            });

            GC.Collect();
            Console.ReadLine();
        }

        private static double GetPrroccessMemory()
        {
            string name = Process.GetCurrentProcess().ProcessName;
            //new PerformanceCounter("Process", "Process Time",name);
            return (double)new PerformanceCounter("Process", "Working Set", name).NextValue();
        }
    }
}
