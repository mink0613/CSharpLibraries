﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UILibrary.Common;

namespace UILibrary.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<GenericEventArgs<object>> OKClicked;

        public event EventHandler Closing;

        public void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (property != null)
            {
                if (property.Equals(value)) return;
            }

            OnPropertyChanged(propertyName);
            property = value;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
