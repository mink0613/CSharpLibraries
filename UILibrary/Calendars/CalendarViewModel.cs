using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using UILibrary.Base;

namespace UILibrary.Calendars
{
    [Export(typeof(CalendarViewModel))]
    public class CalendarViewModel : BaseViewModel
    {
        private ICommand _okClick;

        private ICommand _cancelClick;

        private DateTime _selected;

        public event Action<DateTime> RequestSelect;

        public event Action RequestClose;

        public ICommand OKClick
        {
            get
            {
                return _okClick;
            }
        }

        public ICommand CancelClick
        {
            get
            {
                return _cancelClick;
            }
        }

        public DateTime Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        private void OnOKClick()
        {
            if (RequestClose != null)
            {
                RequestSelect(Selected);
                RequestClose();
            }
        }

        private void OnCancelClick()
        {
            if (RequestClose != null)
            {
                RequestClose();
            }
        }

        private void Initialize()
        {
            Selected = DateTime.Today;
            _okClick = new RelayCommand((param) => OnOKClick(), true);
            _cancelClick = new RelayCommand((param) => OnCancelClick(), true);
        }

        public void SetSelectedDate(DateTime date)
        {
            Selected = date;
        }

        [ImportingConstructor]
        public CalendarViewModel()
        {
            Initialize();
        }
    }
}
