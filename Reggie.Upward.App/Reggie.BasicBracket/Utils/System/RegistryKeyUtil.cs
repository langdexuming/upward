using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.BasicBracket.Utils.System
{
    public class RegistryKeyUtil
    {
        private static string _subKeyName = "";

        public static void SetSubKeyName(string name)
        {
            _subKeyName = name;
        }

        public static string GetValue(string key)
        {
            var value = string.Empty;
            try
            {
                RegistryKey software = Registry.CurrentUser.OpenSubKey(_subKeyName, false);

                value = software.GetValue(key).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return value;
        }

        public static void SetValue(string name, string key)
        {
            try
            {
                RegistryKey software = Registry.CurrentUser.OpenSubKey(_subKeyName, true);

                software.SetValue(name, key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
