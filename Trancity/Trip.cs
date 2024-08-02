using System.Collections.Generic;
using System;

namespace Trancity
{
    public class Trip
    {
        public bool inPark;
        public int inParkIndex;
        public double время_отправления;
        public double время_прибытия;
        public Road[] pathes = new Road[0];
        public List<TripStop> tripStopList = null;

        public Trip Clone(double время_отправления)
        {
            Trip trip = new Trip();
            trip.pathes = this.pathes;
            trip.inPark = this.inPark;
            trip.inParkIndex = this.inParkIndex;
            trip.pathes = this.pathes;
            trip.время_отправления = время_отправления;
            trip.время_прибытия = время_отправления + this.время_прибытия;
            trip.tripStopList = this.tripStopList;
            return trip;
        }

        public string str_время_отправления
        {
            get
            {
                int num = (((int)this.время_отправления) / 0xe10) % 0x18;
                int num2 = (((int)this.время_отправления) / 60) % 60;
                return (num.ToString("0") + ":" + num2.ToString("00"));
            }
        }

        public string str_время_прибытия
        {
            get
            {
                int num = (((int)this.время_прибытия) / 0xe10) % 0x18;
                int num2 = (((int)this.время_прибытия) / 60) % 60;
                return (num.ToString("0") + ":" + num2.ToString("00"));
            }
        }

        public double длина_пути
        {
            get
            {
                double num = 0.0;
                foreach (Road дорога in this.pathes)
                {
                    num += дорога.Длина;
                }
                return num;
            }
        }

        public Road дорога_отправления
        {
            get
            {
                if (this.pathes.Length > 0)
                {
                    return this.pathes[0];
                }
                return null;
            }
        }

        public Road дорога_прибытия
        {
            get
            {
                if (this.pathes.Length > 0)
                {
                    return this.pathes[this.pathes.Length - 1];
                }
                return null;
            }
        }

        public void InitTripStopList(Route route)
        {
            tripStopList = new List<TripStop>();
            this.AddToTripStopList(route);
        }
        
        public void UpdateTripStopList(Route route)
        {
			tripStopList.Clear();
			this.AddToTripStopList(route);
        }
        
        private void AddToTripStopList(Route route)
        {
        	bool flag = false;
        	foreach (Road path in pathes)
            {
                foreach (var obj in path.objects)
                {
                    if (!(obj is Stop)) continue;
                    Stop stop = (Stop)obj;
                    if (stop.typeOfTransport[route.typeOfTransport])
                    {
                    	if (flag)
                    	{
                    		int i = 0;
                    		while (((i < tripStopList.Count - 1) || ((tripStopList.Count == 1) && (i == 0))) && (tripStopList[tripStopList.Count - 1 - i].stop.distance > stop.distance) && (tripStopList[tripStopList.Count - 1 - i].stop.road == stop.road))
                    		{//TODO: НОРМАЛЬНАЯ СОРТИРОВКА ОСТАНОВОК!!!
                    			i++;
                    		}
                    		tripStopList.Insert(tripStopList.Count - i, new TripStop(stop, true));
                    		continue;
                    	}
                    	else
                    	{
                    		tripStopList.Add(new TripStop(stop, true));
                    		flag = true;
                    	}
                    }
                }
                flag = false;
            }
        }
    }
}