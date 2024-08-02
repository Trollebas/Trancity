namespace Trancity
{
    using System;

    public class Пересечка
    {
        public double расстояние;
        public Рельс рельс;

        public Пересечка(Рельс рельс, double расстояние)
        {
            this.рельс = рельс;
            this.расстояние = расстояние;
        }
    }
}

