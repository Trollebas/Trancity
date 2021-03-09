namespace Trancity
{
    public class Светофорный_сигнал
    {
        private Road froad = null;
        public double расстояние;
        public Сигналы сигнал;

        public Светофорный_сигнал(Road дорога, double расстояние)
        {
            this.дорога = дорога;
            this.расстояние = расстояние;
            /*if (дорога != null)
            {
                дорога.objects.Add(this);
            }*/
        }

        public Road дорога
        {
            get
            {
                return this.froad;
            }
            set
            {
                if (this.froad != null)
                {
                    this.froad.objects.Remove(this);
                }
                this.froad = value;
                if (value != null)
                {
                    this.froad.objects.Add(this);
                }
            }
        }
    }
}

