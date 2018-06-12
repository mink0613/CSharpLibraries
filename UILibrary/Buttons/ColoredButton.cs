using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UILibrary.Buttons
{
    public class ColoredButton : Button
    {
        private bool _isSelected;

        public static readonly DependencyProperty IsSelectedProperty =
                DependencyProperty.Register(
                "IsSelected", typeof(bool), typeof(ColoredButton),
                new PropertyMetadata(OnIsSelectedChanged));

        private static void OnIsSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColoredButton button = o as ColoredButton;
            button.IsSelected = (bool)e.NewValue;
        }

        public ColoredButton()
        {
            Disable(); // Basic background color
        }

        public void Enable(string content = "")
        {
            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#008000"));
            if (content != "")
            {
                Content = content;
            }
        }

        public void Disable(string content = "")
        {
            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#d9d9d9"));
            if (content != "")
            {
                Content = content;
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                if (_isSelected == true)
                {
                    Enable();
                }
                else
                {
                    Disable();
                }
            }
        }
    }
}