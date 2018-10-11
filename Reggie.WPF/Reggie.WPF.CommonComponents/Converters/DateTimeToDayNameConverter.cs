/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/8 9:57:35
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Reggie.WPF.CommonComponents.Converters
{
    public class DateTimeToDayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = value as DateTime?;
            if (dt != null)
            {
                return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dt.Value.DayOfWeek);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
