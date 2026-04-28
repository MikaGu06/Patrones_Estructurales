using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight
{
    public class VisualHost : FrameworkElement
    {
        public Visual Visual { get; set; }

        protected override int VisualChildrenCount => Visual == null ? 0 : 1;

        protected override Visual GetVisualChild(int index)
        {
            return Visual;
        }
    }
}
