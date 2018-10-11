/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/6 9:07:44
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
using System.Windows.Controls;

namespace Reggie.WPF.CommonComponents.Controls
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button)),
        TemplatePart(Name = "PART_BorderTitle", Type = typeof(Button))]
    public class BaseDialog : Window
    {
        private Button CloseButton;

        private Border TitleBorder;

        //Get element from name. If it exist then element instance return, if not, new will be created
        protected T EnforceInstance<T>(string partName) where T : FrameworkElement, new()
        {
            T element = this.GetTemplateChild(partName) as T ?? new T();
            return element;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.CloseButton = EnforceInstance<Button>("PART_CloseButton");
            this.TitleBorder = EnforceInstance<Border>("PART_BorderTitle");

            this.CloseButton.Click += (s, e) => {
                this.Close();
            };

            this.TitleBorder.MouseLeftButtonDown += ((s, e) => {
                this.DragMove();
            });

        }
    }
}
