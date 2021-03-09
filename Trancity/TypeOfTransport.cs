using System;

namespace Trancity
{
    public class TypeOfTransport
    {
        public const int Tramway = 0;
        public const int Trolleybus = 1;
        public const int Bus = 2;
        public const int Electrobus = 3;
        public const int Traffic = 4;

        private bool[] type = new bool[5];

        public TypeOfTransport() : this(0)
        {
        }

        public TypeOfTransport(int p)
        {
            this[p] = true;
        }

        public bool this[int index]
        {
            get
            {
                if (index < Tramway && index > Traffic)
                {
                    throw new IndexOutOfRangeException("Недопустимый вид транспорта");
                }
                return type[index];
            }
            set
            {
                if (index < Tramway && index > Traffic)
                {
                    throw new IndexOutOfRangeException("Недопустимый вид транспорта");
                }
                switch (index)
                {
                    case Tramway:
                        type[Tramway] = value;
                        type[Trolleybus] = !value;
                        type[Bus] = !value;
                        break;
                    case Trolleybus:
                        type[Tramway] = value ? false : !type[Bus];
                        type[Trolleybus] = value;
                        break;
                    case Bus:
                        type[Tramway] = value ? false : !type[Trolleybus];
                        type[Bus] = value;
                        break;
                    case Electrobus:
                        type[Tramway] = value ? false : !type[Trolleybus] ? false : !type[Bus];
                        type[Electrobus] = value;
                        break;
                    case Traffic:
                        type[Tramway] = value ? false : !type[Trolleybus] ? false : !type[Bus] ? false : !type[Electrobus];
                        type[Traffic] = value;
                        break;
                }
            }
        }
    }
}