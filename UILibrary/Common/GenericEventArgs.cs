using System;

namespace UILibrary.Common
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T EventData { get; private set; }

        public GenericEventArgs(T eventData)
        {
            this.EventData = eventData;
        }
    }
}
