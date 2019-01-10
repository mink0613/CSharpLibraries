using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace UILibrary.TextBoxes
{
    public class WatermarkTextBox : TextBox
    {
        private string _watermark;

        public string Watermark
        {
            get
            {
                return _watermark;
            }
            set
            {
                _watermark = value;
            }
        }

        public WatermarkTextBox()
        {
            Initialized += WatermarkTextBox_Initialized;
            GotFocus += WatermarkTextBox_GotFocus;
            LostFocus += WatermarkTextBox_LostFocus;
            TextChanged += WatermarkTextBox_TextChanged;
        }

        private bool CheckTextAfterLostFocus()
        {
            string text = this.Text.Trim();
            if (text.Equals(""))
            {
                this.Text = Watermark;
                this.Foreground = Brushes.Gray;
                return false;
            }
            return true;
        }

        private void WatermarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsFocused == false)
            {
                if (CheckTextAfterLostFocus() == true)
                {
                    this.Foreground = Brushes.Black;
                }
            }
        }

        private void WatermarkTextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckTextAfterLostFocus();
        }

        private void WatermarkTextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Foreground = Brushes.Black;
            string text = this.Text.Trim();
            if (text.Equals(Watermark))
            {
                this.Text = string.Empty;
            }
        }

        private void WatermarkTextBox_Initialized(object sender, EventArgs e)
        {
            this.Text = Watermark;
            this.Foreground = Brushes.Gray;
        }
    }
}
