using System;
using System.Diagnostics;

namespace CommonLibrary
{
    public class ContinuousStopwatch : Stopwatch
    {
        public TimeSpan Startoffset
        {
            get;
            private set;
        }

        public ContinuousStopwatch(TimeSpan timeSpan)
        {
            Startoffset = timeSpan;
        }

        public new TimeSpan Elapsed
        {
            get
            {
                return Startoffset + base.Elapsed;
            }
        }

        public new long ElapsedMilliseconds
        {
            get
            {
                return (long)Startoffset.TotalMilliseconds + base.ElapsedMilliseconds;
            }
        }
    }
}
