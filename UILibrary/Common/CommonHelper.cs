using System.Windows;
using System.Windows.Media;

namespace UILibrary.Common
{
    public class CommonHelper
    {
        public static Point GetControlPoint(object control)
        {
            Visual visual = control as Visual;
            if (visual == null)
            {
                return new Point(-1, -1);
            }

            Window window = Window.GetWindow(visual);
            if (window == null)
            {
                return new Point(-1, -1);
            }

            return visual.TransformToAncestor(window).Transform(new Point(0, 0));
        }

        public static string GetControlName(object control)
        {
            FrameworkElement element = control as FrameworkElement;
            if (element == null)
            {
                return null;
            }
            
            return element.Name;
        }

        public static DependencyObject GetControlParent(object control)
        {
            FrameworkElement element = control as FrameworkElement;
            if (element == null)
            {
                return null;
            }

            return element.Parent;
        }
    }
}
