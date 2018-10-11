/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/6 14:17:39
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
using System.Windows;
using System.Windows.Data;

namespace Reggie.WPF.CommonComponents.Converters
{
    public class BoolToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var param = parameter as string;
                //是否反转
                var isInvert = false;
                if (!string.IsNullOrEmpty(param))
                {
                    bool.TryParse(param,out isInvert);
                }
                if ((bool)value)
                {
                    return isInvert? Visibility.Collapsed : Visibility.Visible;
                }
                else
                {
                    return isInvert? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
