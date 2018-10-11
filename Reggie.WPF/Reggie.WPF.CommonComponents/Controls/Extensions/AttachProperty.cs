/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/8 13:51:07
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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Reggie.WPF.CommonComponents.Controls
{
    public class AttachProperty
    {
        #region 三态背景
        public static Brush GetMouseOverBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(AttachProperty), new PropertyMetadata(default(Brush)));

        public static Brush GetPressedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedBackgroundProperty);
        }

        public static void SetPressedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.RegisterAttached("PressedBackground", typeof(Brush), typeof(AttachProperty), new PropertyMetadata(default(Brush)));
        #endregion

        #region 三态前景
        public static Brush GetMouseOverForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverForegroundProperty);
        }

        public static void SetMouseOverForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.RegisterAttached("MouseOverForeground", typeof(Brush), typeof(AttachProperty), new PropertyMetadata(default(Brush)));

        public static Brush GetPressedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedForegroundProperty);
        }

        public static void SetPressedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.RegisterAttached("PressedForeground", typeof(Brush), typeof(AttachProperty), new PropertyMetadata(default(Brush)));
        #endregion
    }
}
