using System.Text.RegularExpressions;
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
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void DigitOnlyTextBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.SelectionStart = this.Text.Length == 0 ? 0 : this.Text.Length;
        }
    }
}
