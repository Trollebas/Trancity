using System.Collections;
using System;

namespace Trancity
{
    public class TypeOfTransport
    {
        public const int Tramway = 0;
        public const int Trolleybus = 1;
        public const int Bus = 2;

        private bool[] type = new bool[3];

        public TypeOfTransport()
        {
            // TODO: Complete member initialization
        }

        public TypeOfTransport(int p)
        {
            // TODO: Complete member initialization
            this[p] = true;
        }

        public bool this[int index]
        {
            get
            {
                if (index < Tramway && index > Bus)
                {
                    throw new IndexOutOfRangeException("Invalid type of transport");
                }
                return type[index];
            }
            set
            {
                if (index < Tramway && index > Bus)
                {
                    throw new IndexOutOfRangeException("Invalid type of transport");
                }
                switch (index)
                {
                    case Tramway:
                        if (value)
                        {
                            type[Tramway] = true;
                            type[Trolleybus] = false;
                            type[Bus] = false;
                        }
                        else
                        {
                            type[Tramway] = false;
                            type[Trolleybus] = true;
                            type[Bus] = true;
                        }
                        break;
                    case Trolleybus:
                        if (value)
                        {
                            type[Tramway] = false;
                            type[Trolleybus] = true;
                        }
                        else
                        {
                            type[Trolleybus] = false;
                            type[Tramway] = !type[Bus];//?false:true;
                        }
                        break;
                    case Bus:
                        if (value)
                        {
                            type[Tramway] = false;
                            type[Bus] = true;
                        }
                        else
                        {
                            type[Bus] = false;
                            type[Tramway] = !type[Trolleybus];// ? false : true;
                        }
                        break;
                }
            }
        }
    }
}