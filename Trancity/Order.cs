namespace Trancity
{
    public class Order
    {
        public Route �������;
        public string �����;
        public ���� ����;
        public bool ���������� = true;
        public bool ��������� = true;
        public Trip[] ����� = new Trip[0];
        public string transport; // = 0;

        public Order(���� ����, Route �������, string �����, string transport)
        {
            this.���� = ����;
            this.������� = �������;
            this.����� = �����;
            this.transport = transport;
        }

/*
        public bool ���������
        {
            get
            {
                return (��������� && ����������);
            }
        }
*/
    }
}