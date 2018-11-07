/****************************************************************
*   作者：yinruimin
*   CLR版本：4.0.30319.42000
*   创建时间：2018/10/11 21:46:04
*   修改时间：2018/10/11 21:46:04
*   描述说明：
*
*   修改历史：
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
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Reggie.WPF.CommonComponents.Behaviors
{
    public class DragInCanvasBehavior:Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            var canvas = ControlsSearchHelper.GetParentObject<Canvas>(this.AssociatedObject);
            if (canvas == null) return;

            bool isDragging = false;
            //鼠标相对于移动对象的间距
            double dx = 0;
            double dy = 0;

            this.AssociatedObject.PreviewMouseDown += (s, e) => {
                isDragging = true;

                var point = e.GetPosition(this.AssociatedObject);
                dx = point.X;
                dy = point.Y;
            };

            this.AssociatedObject.PreviewMouseMove += (s, e) => {
                if (isDragging)
                {
                    var mousePoint = e.GetPosition(canvas);

                    this.AssociatedObject.SetValue(Canvas.LeftProperty, mousePoint.X - dx);
                    this.AssociatedObject.SetValue(Canvas.TopProperty, mousePoint.Y - dy);
                }
            };

            this.AssociatedObject.PreviewMouseUp += (s, e) => {
                isDragging = false;
            };

        }
    }
}
