using System.Runtime.InteropServices;

namespace Trancity
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ������������
    {
        public ��� ���;
        public string dir;
        public string filename;
        public double �����;
        public double ������;
        public double ������;
        public ������������(��� ���, string dir, string filename, double �����, double ������, double ������)
        {
            this.��� = ���;
            this.dir = dir;
            this.filename = filename;
            this.����� = �����;
            this.������ = ������;
            this.������ = ������;
        }
        
        public enum ���
        {
            �������������,
            ������������������,
            ��������,
            Custom
        }
    }
}