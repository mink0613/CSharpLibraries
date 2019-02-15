using System.Windows.Controls;

namespace UILibrary.TextBoxes
{
    public class DigitOnlyTextBox : TextBox
    {
        public DigitOnlyTextBox()
        {
            PreviewTextInput += DigitOnlyTextBox_PreviewTextInput;
            MouseLeftButtonUp += DigitOnlyTextBox_MouseLeftButtonUp;
        }

        private void DigitOnlyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            char[] texts = e.Text.ToCharArray();
            bool isHandled = false;

            foreach (char c in texts)
            {
                if (char.IsDigit(c) == false && c != '-' && c != '.')
                {
                    isHandled = true;
                    break;
                }
            }

            e.Handled = isHandled;
        }

        private void DigitOnlyTextBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.SelectionStart = this.Text.Length == 0 ? 0 : this.Text.Length;
        }
    }
}
