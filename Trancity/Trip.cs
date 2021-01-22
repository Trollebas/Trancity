using System.Collections.Generic;
using System;

namespace Trancity
{
    public class Trip
    {
        public bool inPark;
        public int inParkIndex;
        public double �����_�����������;
        public double �����_��������;
        public Road[] pathes = new Road[0];
        public List<TripStop> tripStopList = null;

        public Trip Clone(double �����_�����������)
        {
            Trip trip = new Trip();
            trip.pathes = this.pathes;
            trip.inPark = this.inPark;
            trip.inParkIndex = this.inParkIndex;
            trip.pathes = this.pathes;
            trip.�����_����������� = �����_�����������;
            trip.�����_�������� = �����_����������� + this.�����_��������;
            trip.tripStopList = this.tripStopList;
            return trip;
        }

        public string str_�����_�����������
        {
            get
            {
                int num = (((int)this.�����_�����������) / 0xe10) % 0x18;
                int num2 = (((int)this.�����_�����������) / 60) % 60;
                return (num.ToString("0") + ":" + num2.ToString("00"));
            }
        }

        public string str_�����_��������
        {
            get
            {
                int num = (((int)this.�����_��������) / 0xe10) % 0x18;
                int num2 = (((int)this.�����_��������) / 60) % 60;
                return (num.ToString("0") + ":" + num2.ToString("00"));
            }
        }

        public double �����_����
        {
            get
            {
                double num = 0.0;
                foreach (Road ������ in this.pathes)
                {
                    num += ������.�����;
                }
                return num;
            }
        }

        public Road ������_�����������
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

        public Road ������_��������
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
                    		{//TODO: ���������� ���������� ���������!!!
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