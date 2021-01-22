using System.Runtime.InteropServices;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public struct МодельДверей
    {
        public Тип тип;
        public string dir;
        public string filename;
        public double длина;
        public double высота;
        public double ширина;
        public МодельДверей(Тип тип, string dir, string filename, double длина, double высота, double ширина)
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
            ШарнирноПоворотные,
            Сдвижные,
            Custom
        }
    }
}