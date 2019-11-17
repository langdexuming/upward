using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Sybase.Data.AseClient;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var connStr = @"Driver={SYBASE ASE ODBC Driver};Srvr=Aron1;Uid=username;Pwd=password;";
            var sql = "select * from table";
            var oracleStr = @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=system;Password=30757853";
            try
            {
                //using (var conn = new AseConnection(connStr))
                //{
                //    conn.Open();
                //    ;

                //    conn.Dispose();
                //}

                using (var conn = new OracleConnection(oracleStr))
                {
                    conn.Open();
                    ;

                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                //Ignore
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
