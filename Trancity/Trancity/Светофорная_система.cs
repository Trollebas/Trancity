namespace Trancity
{
    using System;
    using System.Collections.Generic;

    public class Светофорная_система
    {
        public double время_зелёного;
        public double время_переключения_на_зелёный;
        public double начало_работы;
        public double окончание_работы;
        public List<Светофорный_сигнал> светофорные_сигналы = new List<Светофорный_сигнал>();
        public List<Светофор> светофоры = new List<Светофор>();
        public double цикл = 1.0;
        private bool green = false;
        private bool yellow = false;
        private bool red = false;

        public void CreateMesh()
        {
            foreach (Светофор светофор in this.светофоры)
            {
                светофор.CreateMesh();
            }
        }

        public void Render()
        {
            foreach (Светофор светофор in this.светофоры)
            {
            	светофор.Custom_render(green, yellow, red);
//                светофор.Render();
            }
        }

        public void Обновить(World мир)
        {
            green = false;
            yellow = false;
            red = false;
            Сигналы сигналы = Сигналы.Нет;
            if ((this.начало_работы == 0.0) && (this.окончание_работы == 86400.0))
            {
                this.начало_работы = 10800.0;
                this.окончание_работы = 97200.0;
            }
            if ((мир.time < this.начало_работы) || (мир.time >= this.окончание_работы))
            {
                yellow = (мир.time % 1.0) < 0.5;
            }
            else
            {
                double num = ((мир.time - this.начало_работы) - this.время_переключения_на_зелёный) % this.цикл;
                if (num < this.время_зелёного)
                {
                    сигналы = Сигналы.Зелёный;
                    green = (num < (this.время_зелёного - 4.0)) || ((num % 1.0) >= 0.5);
                }
                else if (num < (this.время_зелёного + 2.0))
                {
                    сигналы = Сигналы.Жёлтый;
                    yellow = true;
                }
                else
                {
                    сигналы = Сигналы.Красный;
                    red = true;
                    if (num >= (this.цикл - 2.0))
                    {
                        yellow = true;
                    }
                }
            }
            foreach (Светофорный_сигнал _сигнал in this.светофорные_сигналы)
            {
                _сигнал.сигнал = сигналы;
            }
            /*foreach (Светофор светофор in this.светофоры)
            {
                светофор.Обновить_материалы(green, yellow, red);
            }*/
        }
    }
}

