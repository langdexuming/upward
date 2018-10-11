/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/7/19 10:09:50
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using Reggie.WPF.CommonComponents.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Reggie.WPF.CommonComponents.Behaviors
{
    public class WindowAutoCenterBehavior: Behavior<System.Windows.Controls.Control>
    {
        private Window _window;
        protected override void OnAttached()
        {
            if (this.AssociatedObject == null)
            {
                throw new ArgumentException();
            }

            this.AssociatedObject.Loaded += (s, e) =>
            {
                //获取窗体
                if (this.AssociatedObject is Window)
                {
                    _window = (Window)this.AssociatedObject;
                }
                else
                {
                    _window = ControlsSearchHelper.GetParentObject<Window>(this.AssociatedObject);
                }

                if (_window != null)
                {
                    _window.SizeChanged += WindowSizeChangedHandler;
                }
            };

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            if (_window != null)
            {
                _window.SizeChanged -= WindowSizeChangedHandler;
                _window = null;

            }
            base.OnDetaching();
        }

        private void WindowSizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
            var win = Application.Current.MainWindow;
            _window.Left = (win.ActualWidth - _window.ActualWidth) / 2;
            _window.Top = (win.ActualHeight - _window.ActualHeight) / 2;
        }
    }
}
