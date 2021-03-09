using System.Collections.Generic;

namespace Trancity
{
    public class Route
    {
        public int typeOfTransport;
        public Order[] orders = new Order[0];
        public string number = "-";
        public List<Trip> parkTrips = new List<Trip>();
        public List<Trip> trips = new List<Trip>();

        public Route(int typeOfTransport, string number)
        {
            this.typeOfTransport = typeOfTransport;
            this.number = number;
        }

        public List<Trip> AllTrips
        {
            get
            {
                List<Trip> list = new List<Trip>(trips);
                list.AddRange(parkTrips);
                return list;
            }
        }
    }
}