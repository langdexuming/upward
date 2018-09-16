using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Reggie.Utilities.Utils.Wpf
{
    public class PresentationUtil
    {
        public static double GetScreenScale(Visual visual)
        {
            if (visual == null)
            {
                return 1;
            }

            var presentationSource=PresentationSource.FromVisual(visual);
            return 1.0/presentationSource.CompositionTarget.TransformFromDevice.M11;
        }
    }
}
