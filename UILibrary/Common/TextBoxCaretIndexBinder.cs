using System.Windows;

namespace UILibrary.Common
{
    public class TextBoxCaretIndexBinder : DependencyObject
    {
        public static readonly DependencyProperty CursorPositionProperty =
            DependencyProperty.Register(
                "CursorPosition",
                typeof(int),
                typeof(TextBoxCaretIndexBinder),
                new FrameworkPropertyMetadata(
                    default(int),
                    new PropertyChangedCallback(CursorPositionChanged)));

        public static void SetCursorPosition(DependencyObject dependencyObject, int i)
        {
            dependencyObject.SetValue(CursorPositionProperty, i);
        }

        public static int GetCursorPosition(DependencyObject dependencyObject)
        {
            return (int)dependencyObject.GetValue(CursorPositionProperty);
        }

        private static void CursorPositionChanged(
            DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
