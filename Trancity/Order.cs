namespace Trancity
{
    public class Order
    {
        public Route маршрут;
        public string номер;
        public ѕарк парк;
        public bool по¬ыходным = true;
        public bool по–абочим = true;
        public Trip[] рейсы = new Trip[0];
        public string transport; // = 0;

        public Order(ѕарк парк, Route маршрут, string номер, string transport)
        {
            this.парк = парк;
            this.маршрут = маршрут;
            this.номер = номер;
            this.transport = transport;
        }

/*
        public bool ≈жедневно
        {
            get
            {
                return (по–абочим && по¬ыходным);
            }
        }
*/
    }
}