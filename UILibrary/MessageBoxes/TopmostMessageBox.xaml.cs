using System;
using System.Windows;
using System.Windows.Controls;

namespace UILibrary.MessageBoxes
{
    public enum CustomMessageBoxType
    {
        YesNo,
        OK
    }

    public enum CustomMessageBoxResult
    {
        Yes,
        No,
        OK
    }

    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TopmostMessageBox : Window
    {
        public TopmostMessageBox()
        {
            InitializeComponent();
        }

        private CustomMessageBoxResult _result;

        private CustomMessageBoxType _type;

        private static TopmostMessageBox _instance;

        /// <summary>
        /// Modal dialog message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="type"></param>
        /// <param name="topMost"></param>
        /// <returns></returns>
        [STAThread]
        private CustomMessageBoxResult BaseShowDialog(string message, string caption, CustomMessageBoxType type, bool topMost = true)
        {
            _type = type;
            switch (_type)
            {
                case CustomMessageBoxType.YesNo:
                    Yes.Visibility = Visibility.Visible;
                    No.Visibility = Visibility.Visible;
                    OK.Visibility = Visibility.Collapsed;
                    break;
                case CustomMessageBoxType.OK:
                    Yes.Visibility = Visibility.Collapsed;
                    No.Visibility = Visibility.Collapsed;
                    OK.Visibility = Visibility.Visible;
                    break;
            }

            this.Message.Text = message;
            this.Title = caption;
            this.Topmost = topMost;
            bool? result = this.ShowDialog();

            return _result;
        }

        /// <summary>
        /// Modeless dialog message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="type"></param>
        /// <param name="topMost"></param>
        [STAThread]
        private void BaseShow(string message, string caption, CustomMessageBoxType type, bool topMost = true)
        {
            _type = type;
            switch (_type)
            {
                case CustomMessageBoxType.YesNo:
                    Yes.Visibility = Visibility.Visible;
                    No.Visibility = Visibility.Visible;
                    OK.Visibility = Visibility.Collapsed;
                    break;
                case CustomMessageBoxType.OK:
                    Yes.Visibility = Visibility.Collapsed;
                    No.Visibility = Visibility.Collapsed;
                    OK.Visibility = Visibility.Visible;
                    break;
            }

            this.Message.Text = message;
            this.Title = caption;
            this.Topmost = topMost;
            this.Show();
        }

        [STAThread]
        public static CustomMessageBoxResult ShowDialog(string message)
        {
            if (_instance == null)
            {
                _instance = new TopmostMessageBox();
                _instance.Closed += InstanceClosed;
            }

            return _instance.BaseShowDialog(message, string.Empty, CustomMessageBoxType.OK, true);
        }

        [STAThread]
        public static CustomMessageBoxResult ShowDialog(string message, CustomMessageBoxType type)
        {
            if (_instance == null)
            {
                _instance = new TopmostMessageBox();
                _instance.Closed += InstanceClosed;
            }

            return _instance.BaseShowDialog(message, string.Empty, type, true);
        }

        [STAThread]
        public static CustomMessageBoxResult ShowDialog(string message, bool topMost = true)
        {
            if (_instance == null)
            {
                _instance = new TopmostMessageBox();
                _instance.Closed += InstanceClosed;
            }

            return _instance.BaseShowDialog(message, string.Empty, CustomMessageBoxType.OK, topMost);
        }

        [STAThread]
        public static CustomMessageBoxResult ShowDialog(string message, CustomMessageBoxType type, bool topMost = true)
        {
            if (_instance == null)
            {
                _instance = new TopmostMessageBox();
                _instance.Closed += InstanceClosed;
            }

            return _instance.BaseShowDialog(message, string.Empty, type, topMost);
        }

        [STAThread]
        public static CustomMessageBoxResult ShowDialog(string message, string caption, CustomMessageBoxType type, bool topMost = true)
        {
            if (_instance == null)
            {
                _instance = new TopmostMessageBox();
                _instance.Closed += InstanceClosed;
            }

            return _instance.BaseShowDialog(message, caption, type, topMost);
        }

        [STAThread]
        public static void Show(string message)
        {
            _instance = new TopmostMessageBox();
            _instance.Closed += InstanceClosed;

            _instance.BaseShow(message, string.Empty, CustomMessageBoxType.OK, true);
        }

        [STAThread]
        public static void Show(string message, bool topMost = true)
        {
            _instance = new TopmostMessageBox();
            _instance.Closed += InstanceClosed;

            _instance.BaseShow(message, string.Empty, CustomMessageBoxType.OK, topMost);
        }

        [STAThread]
        public static void Show(string message, CustomMessageBoxType type)
        {
            _instance = new TopmostMessageBox();
            _instance.Closed += InstanceClosed;

            _instance.BaseShow(message, string.Empty, type, true);
        }

        [STAThread]
        public static void Show(string message, CustomMessageBoxType type, bool topMost = true)
        {
            _instance = new TopmostMessageBox();
            _instance.Closed += InstanceClosed;

            _instance.BaseShow(message, string.Empty, type, topMost);
        }

        [STAThread]
        public static void Show(string message, string caption, CustomMessageBoxType type, bool topMost = true)
        {
            _instance = new TopmostMessageBox();
            _instance.Closed += InstanceClosed;

            _instance.BaseShow(message, caption, type, topMost);
        }

        private static void InstanceClosed(object sender, EventArgs e)
        {
            _instance = null;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;
            string name = clicked.Name;
            switch (name.ToUpper())
            {
                case "YES":
                    _result = CustomMessageBoxResult.Yes;
                    break;
                case "NO":
                    _result = CustomMessageBoxResult.No;
                    break;
                case "OK":
                    _result = CustomMessageBoxResult.OK;
                    break;
            }

            Close();
        }
    }
}
