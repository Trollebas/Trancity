using System;
using System.Collections.Generic;
using System.Text;

namespace Trancity
{
    public class TripStop
    {
        public Stop stop;
        public bool flag;
//        private Stop stop_2;
//        private bool p;

        public TripStop(Stop stop, bool active)
        {
            this.stop = stop;
            this.flag = active;
        }
    }
}
