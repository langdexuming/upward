/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/7/18 19:33:39
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using System.Windows.Data;

namespace Reggie.WPF.CommonComponents.Converters
{
    /// <summary>
    /// Converts the value from true to false and false to true.
    /// </summary>
    public sealed class IsNullConverter : IValueConverter
    {
        private static IsNullConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static IsNullConverter()
        {
        }

        private IsNullConverter()
        {
        }

        public static IsNullConverter Instance
        {
            get { return _instance ?? (_instance = new IsNullConverter()); }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null == value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
