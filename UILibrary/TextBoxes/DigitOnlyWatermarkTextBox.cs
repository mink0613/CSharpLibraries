using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UILibrary.TextBoxes
{
    public class DigitOnlyWatermarkTextBox : TextBox
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

        public DigitOnlyWatermarkTextBox()
        {
            Initialized += WatermarkTextBox_Initialized;
            GotFocus += WatermarkTextBox_GotFocus;
            LostFocus += WatermarkTextBox_LostFocus;
            TextChanged += WatermarkTextBox_TextChanged;
            
            PreviewTextInput += DigitOnlyWatermarkTextBox_PreviewTextInput;
            MouseLeftButtonUp += DigitOnlyWatermarkTextBox_MouseLeftButtonUp;
        }

        private void DigitOnlyWatermarkTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void DigitOnlyWatermarkTextBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.SelectionStart = this.Text.Length == 0 ? 0 : this.Text.Length;
        }

        private bool CheckTextAfterLostFocus()
        {
            string text = this.Text.Trim();
            if (text.Equals("0"))
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
                this.Text = "0";
            }
        }

        private void WatermarkTextBox_Initialized(object sender, EventArgs e)
        {
            if (this.Text == "0" || this.Text.Length == 0)
            {
                this.Text = Watermark;
                this.Foreground = Brushes.Gray;
            }
        }
    }
}
