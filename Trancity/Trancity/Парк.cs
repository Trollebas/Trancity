namespace Trancity
{
    using System;

    public class Парк
    {
        public Road въезд;
        public Road выезд;
        public string название = "Парк";
        public Road[] пути_стоянки = new Road[0];

        public Парк(string название)
        {
            this.название = название;
        }
    }
}

