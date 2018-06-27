using System.ComponentModel.Composition;
using System.Windows;

namespace UILibrary.Calendars
{
    /// <summary>
    /// CalendarView.xaml에 대한 상호 작용 논리
    /// </summary>
    [Export(typeof(CalendarView))]
    public partial class CalendarView : Window
    {
        public CalendarViewModel ViewModel
        {
            get
            {
                return DataContext as CalendarViewModel;
            }
        }
        
        public CalendarView()
        {
            InitializeComponent();
        }
    }
}
