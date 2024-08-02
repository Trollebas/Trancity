/*namespace Common
{
    using System;
    using System.Collections;

    public class MyRandomList : MyList
    {
        private Random r;

        public MyRandomList() : base(new Type[0])
        {
            this.r = new Random();
        }

        public MyRandomList(ICollection c) : base(new Type[0])
        {
            this.r = new Random();
            this.AddRange(c);
        }

        public override void Add(object value)
        {
            int index = this.r.Next(base.Count + 1);
            base.Insert(index, value);
        }

        public override void AddRange(ICollection c)
        {
            foreach (object obj2 in c)
            {
                this.Add(obj2);
            }
        }
    }
}
*/
