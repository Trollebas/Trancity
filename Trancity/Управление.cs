namespace Trancity
{
    using System;

    public class Управление
    {
        public bool автоматическое;
        public bool ручное;

        private Управление(bool ручное, bool автоматическое)
        {
            this.ручное = ручное;
            this.автоматическое = автоматическое;
        }

        public static Управление Автоматическое
        {
            get
            {
                return new Управление(false, true);
            }
        }

        public static Управление Нет
        {
            get
            {
                return new Управление(false, false);
            }
        }

        public static Управление Ручное
        {
            get
            {
                return new Управление(true, false);
            }
        }
    }
}

