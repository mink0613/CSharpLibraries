using System.Windows.Controls;
using System.Windows.Media;

namespace UILibrary.Buttons
{
    public class MenuColorButton : Button
    {
        private bool _isClicked = false;

        public bool IsButtonClicked
        {
            get
            {
                return _isClicked;
            }
            set
            {
                _isClicked = value;
                if (_isClicked == true)
                {
                    SetBackgroundClicked();
                }
                else
                {
                    SetBackgroundUnclicked();
                }
            }
        }

        private void SetBackgroundClicked()
        {
            Background = new SolidColorBrush(Colors.GreenYellow);
            BorderBrush = Background;
        }

        private void SetBackgroundUnclicked()
        {
            Background = new SolidColorBrush(Colors.CadetBlue);
            BorderBrush = Background;
        }

        public MenuColorButton()
        {
            Focusable = false;
            MouseEnter += MenuColorButton_MouseEnter;
            IsButtonClicked = false;
        }

        private void MenuColorButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsMouseOver == true)
            {
                if (_isClicked == true)
                {
                    SetBackgroundClicked();
                }
                else
                {
                    SetBackgroundUnclicked();
                }
            }
        }
    }
}
