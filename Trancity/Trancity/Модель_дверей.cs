namespace Trancity
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Модель_дверей
    {
        public Тип тип;
        public string dir;
        public string filename;
        public double длина;
        public double высота;
        public double ширина;
        public Модель_дверей(Тип тип, string dir, string filename, double длина, double высота, double ширина)
        {
            this.тип = тип;
            this.dir = dir;
            this.filename = filename;
            this.длина = длина;
            this.высота = высота;
            this.ширина = ширина;
        }
        public enum Тип
        {
            Двустворчатые,
            Шарнирно_поворотные
        }
    }
}

