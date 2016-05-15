using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace SerialTerminal
{
    /// <summary>
    /// Counts total amount of data and also data rate. Event is fired every time inverval value passes
    /// </summary>
    class DataTraficCounter
    {
        public class MyEventArgs
        {
            public double Rate = 0;
            public double Total = 0;
        }


        private int Counter;
        private double Total;
        Timer IntervalTimer;

        public delegate void ValueUpdatedEventHandler(object sender, MyEventArgs e);
        public event ValueUpdatedEventHandler ValueUpdated;


        /// <summary>
        /// Creates new instance of data trafic counter
        /// </summary>
        /// <param name="updateIntervalMs">Specifies update period for data rates</param>
        public DataTraficCounter(int updateIntervalMs)
        {
            IntervalTimer = new Timer(updateIntervalMs);
            IntervalTimer.Elapsed += new ElapsedEventHandler(IntervalCounter_Elapsed);
            IntervalTimer.Start();
            Counter = 0;
            Total = 0;
        }


        /// <summary>
        /// Increments data counter
        /// </summary>
        /// <param name="val">Increment value</param>
        public void IncrementCounter(int val)
        {
            Counter += val;
            Total += val;
        }


        public void ClearTotal()
        {
            Total = 0;
        }

        void IntervalCounter_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (ValueUpdated != null)
            {
                var Args = new MyEventArgs();
                Args.Rate = Counter / (IntervalTimer.Interval * 0.001);
                Args.Total = Total;
                ValueUpdated(sender, Args);         //fire event with updated custom eventargs
            }
            Counter = 0;                            //reset data counter
        }
    }
}
