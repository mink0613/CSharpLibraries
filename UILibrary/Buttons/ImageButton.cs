using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UILibrary.Buttons
{
    public class ImageButton : ColoredButton
    {
        private Image _image;

        private TextBlock _text;

        private StackPanel _mainPanel;

        private readonly int _imageHeight = 40;

        private readonly int _textHeight = 20;

        public ImageSource Image
        {
            set
            {
                _image.Source = value;
                if (_text.Text == "")
                {
                    _mainPanel.Children.Remove(_text);
                    _image.RenderTransform = new ScaleTransform(0.8, 0.8, _image.Width / 2, _image.Height / 2);
                }
            }
        }

        public string Text
        {
            set
            {
                _text.Text = value;
                if (_text.Text != "")
                {
                    _mainPanel.Children.Add(_text);
                    _image.RenderTransform = new ScaleTransform(0.7, 0.7, _image.Width / 2, _image.Height / 2);
                }
            }
        }

        public ImageButton()
        {
            this.Width = 140;
            this.Height = _imageHeight + _textHeight;

            _mainPanel = new StackPanel();
            _mainPanel.Orientation = Orientation.Vertical;
            _mainPanel.HorizontalAlignment = HorizontalAlignment.Center;
            _mainPanel.VerticalAlignment = VerticalAlignment.Center;

            _image = new Image();
            _image.Width = 70;
            _image.Height = _imageHeight;
            _image.Stretch = Stretch.Uniform;
            _image.HorizontalAlignment = HorizontalAlignment.Center;
            _image.VerticalAlignment = VerticalAlignment.Center;

            _text = new TextBlock();
            _text.Width = 120;
            _text.Height = _textHeight;
            _text.FontSize = 12;
            _text.FontWeight = FontWeights.Bold;
            _text.TextAlignment = TextAlignment.Center;
            _text.HorizontalAlignment = HorizontalAlignment.Center;
            _text.VerticalAlignment = VerticalAlignment.Top;

            _mainPanel.Children.Add(_image);
            _mainPanel.Children.Add(_text);

            this.Content = _mainPanel;
        }
    }
}