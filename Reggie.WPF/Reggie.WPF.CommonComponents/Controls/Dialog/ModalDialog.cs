/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/6 9:10:23
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
    /// <summary>
    /// Button对类型,确定/取消，是/否
    /// </summary>
    public enum ButtonPairType
    {
        /// <summary>
        /// 确定取消
        /// </summary>
        OkCancel,
        /// <summary>
        /// 是和否
        /// </summary>
        YesNo
    }

    [TemplatePart(Name = "PART_ButtonOk", Type = typeof(Button)),
           TemplatePart(Name = "PART_ButtonCancel", Type = typeof(Button))]
    public class ModalDialog : BaseDialog
    {

        private Button ButtonOk;
        private Button ButtonCancel;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.ButtonOk = EnforceInstance<Button>("PART_ButtonOk");
            this.ButtonCancel = EnforceInstance<Button>("PART_ButtonCancel");

            this.ButtonOk.Click += (s, e) => {
                this.DialogResult = true;
            };
            this.ButtonCancel.Click += (s, e) => {
                this.DialogResult = false;
            };
        }
    }
}
