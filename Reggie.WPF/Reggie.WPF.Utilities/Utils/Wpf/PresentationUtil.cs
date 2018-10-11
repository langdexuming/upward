/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/7/2 17:21:50
*   描述说明：表现层工具
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using Reggie.WPF.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Reggie.WPF.Utilities.Utils.Wpf
{
    public class PresentationUtil
    {
        private const string TAG = nameof(PresentationUtil);

        /// <summary>
        /// 获取屏幕放大比例
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static double GetScreenScaleValue(DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                Logger.Error(TAG, new ArgumentNullException(nameof(dependencyObject)));
                return 1;
            }

            var source = PresentationSource.FromDependencyObject(dependencyObject);
            if (source == null)
            {
                Logger.Error(TAG, new ArgumentNullException(nameof(source)));
                return 1;
            }

            return source.CompositionTarget.TransformToDevice.M11;
        }

        /// <summary>
        /// 获取相对于元素的坐标
        /// </summary>
        /// <returns>经过放大比例转换后的坐标</returns>
        public static Point FromRelativeToElementPoint(Point point, DependencyObject dependencyObject)
        {
            if (point == null)
            {
                Logger.Error(TAG, new ArgumentNullException(nameof(point)));
            }

            if (dependencyObject == null)
            {
                Logger.Error(TAG, new ArgumentNullException(nameof(dependencyObject)));
            }

            var scaleValue = GetScreenScaleValue(dependencyObject);
            var newPoint = new Point(point.X / scaleValue, point.Y / scaleValue);
            return newPoint;
        }
    }
}
