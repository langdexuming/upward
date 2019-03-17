using Microsoft.Expression.Interactivity.Core;
using Reggie.WPF.CommonComponents.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Reggie.WPF.CommonComponents.Actions
{
    public class ClickToCloseWindowAction : TargetedTriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            var window = ControlsSearchHelper.GetParentObject<Window>(this.AssociatedObject);
            window?.Close();
        }
    }
}
