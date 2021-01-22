namespace Trancity
{
    using Common;
    using Microsoft.DirectX;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Drawing;

    public abstract class Троллейбус : Безрельсовый_Транспорт
    {
        private Random rand = new Random();
        public bool sound_замедляется;
        public bool sound_ускоряется;
        public Двери[] двери;
        public Колесо[] колёса;
        public int количество_дверей = 1;
        public double поворот_руля;
        public Положение положение;
        public double радиус_колёс;
        public Дорога следующая_дорога;
        public Штанга[] штанги;
        double r1;
        double num1;

        protected Троллейбус()
        {
        }

        public override void автоматически_управлять(Мир мир)
        {
            double num = 2000.0;
            double num2 = 2000.0;
            Базовая_остановка _остановка = null;
            double num3 = 2000.0;
            double num4 = 2000.0;
            if (this.положение.дорога != null)
            {
                //Console.WriteLine();  // HACK: для отладки
                num = this.положение.дорога.длина - this.положение.расстояние;
                if ((this.следующая_дорога == null) && (this.положение.дорога.следующие_дороги.Length > 0))
                {
                    base.Обновить_рейс();
                    if (((base.рейс != null) && (base.рейс_index < (base.рейс.путь.Length - 1))) && ((base.рейс_index > 0) || (this.положение.дорога == base.рейс.путь[0])))
                    {
                        this.следующая_дорога = base.рейс.путь[base.рейс_index + 1];
                    }
                    else
                    {
                        this.следующая_дорога = this.положение.дорога.следующие_дороги[this.rand.Next(this.положение.дорога.следующие_дороги.Length)];
                    }
                }
                if ((this.следующая_дорога != null) && (!this.следующая_дорога.кривая || (this.следующая_дорога.радиус_abs >= 80.0)))
                {
                    num += this.следующая_дорога.длина;
                    num1 = 0;
                }
                //Console.Write(" S: "); Console.Write(num);
                if (this.положение.дорога.кривая && (this.положение.дорога.радиус_abs < 80.0))
                {
                    // HACK: num = 10.0;
                    r1 = положение.дорога.радиус_abs;
                }
                if (!this.положение.дорога.кривая || (this.положение.дорога.радиус_abs > 80.0))
                {
                    r1 = 0;
                }
                //Console.Write(" r1: "); Console.Write(r1);
                if ((this.следующая_дорога != null) && (this.следующая_дорога.кривая) && (this.следующая_дорога.радиус_abs < 80.0))
                {
                    num1 = следующая_дорога.радиус_abs;
                }
                //Console.Write(" r2: "); Console.Write(r2);
                int num5 = (int)Math.Round((double)(this.положение.дорога.ширина[0] / 6.0));
                int num6 = (int)Math.Round((double)(this.положение.дорога.ширина[1] / 6.0));
                if (num5 < 1)
                {
                    num5 = 1;
                }
                if (num6 < 1)
                {
                    num6 = 1;
                }
                double num7 = this.положение.дорога.найти_ширину(this.положение.расстояние);
                int num8 = (int)Math.Floor((double)((num5 * (this.положение.отклонение + (num7 / 2.0))) / num7));
                int num9 = (int)Math.Floor((double)((num6 * (this.положение.отклонение + (num7 / 2.0))) / num7));
                if (num8 < 0)
                {
                    num8 = 0;
                }
                if (num8 >= num5)
                {
                    num8 = num5 - 1;
                }
                if (num9 < 0)
                {
                    num9 = 0;
                }
                if (num9 >= num6)
                {
                    num9 = num6 - 1;
                }
                double[] numArray = new double[num6];
                for (int i = 0; i < numArray.Length; i++)
                {
                    numArray[i] = 2000.0;
                }
                foreach (object obj2 in this.положение.дорога.объекты)
                {
                    if (obj2 is Остановка)
                    {
                        Остановка остановка = (Остановка)obj2;
                        if ((((остановка.расстояние - this.положение.расстояние) > 5.0) && ((base.рейс == null) || остановка.путь_подходит(base.рейс.путь))) && ((base.маршрут != null) && (base.маршрут.вид_транспорта == остановка.вид_транспорта)))
                        {
                            num2 = Math.Min(num2, остановка.расстояние - this.положение.расстояние);
                            _остановка = остановка;
                        }
                    }
                }
                if (this.следующая_дорога != null)
                {
                    foreach (object obj3 in this.следующая_дорога.объекты)
                    {
                        if (obj3 is Остановка)
                        {
                            Остановка остановка2 = (Остановка)obj3;
                            if (((base.рейс == null) || остановка2.путь_подходит(base.рейс.путь)) && ((base.маршрут != null) && (base.маршрут.вид_транспорта == остановка2.вид_транспорта)))
                            {
                                num2 = Math.Min(num2, (this.положение.дорога.длина - this.положение.расстояние) + остановка2.расстояние);
                            }
                        }
                    }
                }
                List<Положение> list = new List<Положение>(this.положение.дорога.занятые_положения);
                List<Положение> list2 = new List<Положение>();
                if (this.следующая_дорога != null)
                {
                    list2 = new List<Положение>(this.следующая_дорога.занятые_положения);
                }
                foreach (Положение положение in list)
                {
                    if ((положение.comment != this) && (положение.расстояние > this.положение.расстояние))
                    {
                        double num11 = this.положение.дорога.найти_ширину(положение.расстояние);
                        int num12 = (int)Math.Floor((double)((num6 * (положение.отклонение + (num11 / 2.0))) / num11));
                        if (num12 == num9)
                        {
                            num3 = Math.Min(num3, положение.расстояние - this.положение.расстояние);
                        }
                        if ((num12 >= 0) && (num12 < numArray.Length))
                        {
                            numArray[num12] = Math.Min(numArray[num12], положение.расстояние - this.положение.расстояние);
                        }
                    }
                }
                foreach (Положение положение2 in list2)
                {
                    if (положение2.comment != this)
                    {
                        double num13 = this.следующая_дорога.найти_ширину(положение2.расстояние);
                        int num14 = (int)Math.Floor((double)((num6 * (положение2.отклонение + (num13 / 2.0))) / num13));
                        if (num14 == num9)
                        {
                            num3 = Math.Min(num3, (this.положение.дорога.длина - this.положение.расстояние) + положение2.расстояние);
                        }
                        if ((num14 >= 0) && (num14 < numArray.Length))
                        {
                            numArray[num14] = Math.Min(numArray[num14], (this.положение.дорога.длина - this.положение.расстояние) + положение2.расстояние);
                        }
                    }
                }
                foreach (object obj4 in this.положение.дорога.объекты)
                {
                    if (obj4 is Сигнальная_система.Сигнал)
                    {
                        Сигнальная_система.Сигнал сигнал = (Сигнальная_система.Сигнал)obj4;
                        if ((сигнал.система.сигнал == Сигналы.Красный) && ((сигнал.расстояние - this.положение.расстояние) > 10.0))
                        {
                            num4 = Math.Min(num4, (сигнал.расстояние - this.положение.расстояние) - 30.0);
                        }
                    }
                    if (obj4 is Светофорный_сигнал)
                    {
                        Светофорный_сигнал _сигнал = (Светофорный_сигнал)obj4;
                        double num15 = _сигнал.расстояние - this.положение.расстояние;
                        if (((_сигнал.сигнал == Сигналы.Красный) && (num15 > 0.0)) || ((_сигнал.сигнал == Сигналы.Жёлтый) && (num15 > 10.0)))
                        {
                            num4 = Math.Min(num4, num15 - 5.0);
                        }
                    }
                }
                if (this.следующая_дорога != null)
                {
                    foreach (object obj5 in this.следующая_дорога.объекты)
                    {
                        if (obj5 is Сигнальная_система.Сигнал)
                        {
                            Сигнальная_система.Сигнал сигнал2 = (Сигнальная_система.Сигнал)obj5;
                            if (сигнал2.система.сигнал == Сигналы.Красный)
                            {
                                num4 = Math.Min(num4, ((this.положение.дорога.длина - this.положение.расстояние) + сигнал2.расстояние) - 30.0);
                            }
                        }
                        if (obj5 is Светофорный_сигнал)
                        {
                            Светофорный_сигнал _сигнал2 = (Светофорный_сигнал)obj5;
                            double num16 = (this.положение.дорога.длина - this.положение.расстояние) + _сигнал2.расстояние;
                            if ((_сигнал2.сигнал == Сигналы.Красный) || ((_сигнал2.сигнал == Сигналы.Жёлтый) && (num16 > 10.0)))
                            {
                                num4 = Math.Min(num4, num16 - 5.0);
                            }
                        }
                    }
                }
                if (num3 < 100.0)
                {
                    double num17 = num3;
                    for (int j = 0; j < numArray.Length; j++)
                    {
                        if (numArray[j] > num17)
                        {
                            num17 = numArray[j];
                            num9 = j;
                            if (num5 == num6)
                            {
                                num8 = j;
                            }
                        }
                    }
                }
                double num19 = ((this.положение.дорога.ширина[0] * (num8 + 0.5)) / ((double)num5)) - (this.положение.дорога.ширина[0] / 2.0);
                double num20 = ((this.положение.дорога.ширина[1] * (num9 + 0.5)) / ((double)num6)) - (this.положение.дорога.ширина[1] / 2.0);
                double num21 = num19 + (((num20 - num19) * this.положение.расстояние) / this.положение.дорога.длина);
                double num22 = 0.3;
                if ((num2 < 80.0) || (base.осталось_стоять > 0.0))
                {
                    num21 = 0.5 - (num7 / 2.0);
                    if (this.положение.отклонение < (2.0 - (num7 / 2.0)))
                    {
                        num21 = 2.0 - (num7 / 2.0);
                        num22 = 0.2;
                    }
                }
                Double_Point point2 = new Double_Point(this.положение.дорога.найти_направление(this.положение.расстояние) - this.направление);
                double num23 = -point2.угол;
                if (this.положение.отклонение > (num21 + num22))
                {
                    num23 += 0.08 * (this.положение.отклонение - num21);
                }
                else if (this.положение.отклонение < (num21 - num22))
                {
                    num23 -= 0.08 * (num21 - this.положение.отклонение);
                }
                if (this.положение.отклонение < ((-num7 / 2.0) + 1.0))
                {
                    num23 -= 0.3;
                }
                if (this.положение.отклонение > ((num7 / 2.0) - 1.0))
                {
                    num23 += 0.3;
                }
                if (Math.Abs(num23) < 0.001)
                {
                    num23 = 0.0;
                }
                if (this.поворот_руля < num23)
                {
                    this.поворот_руля += 0.3 * Мир.прошло_времени;
                    if (this.поворот_руля > num23)
                    {
                        this.поворот_руля = num23;
                    }
                    if (this.поворот_руля > 0.78539816339744828)
                    {
                        this.поворот_руля = 0.78539816339744828;
                    }
                }
                else if (this.поворот_руля > num23)
                {
                    this.поворот_руля -= 0.3 * Мир.прошло_времени;
                    if (this.поворот_руля < num23)
                    {
                        this.поворот_руля = num23;
                    }
                    if (this.поворот_руля < -0.78539816339744828)
                    {
                        this.поворот_руля = -0.78539816339744828;
                    }
                }
            }
            bool flag = false;
            bool flag2 = false;
            if ((base.рейс != null) && (base.рейс.в_парк || ((base.рейс.дорога_отправления == base.парк.выезд) && (мир.время < base.рейс.время_отправления))))
            {
                flag2 = true;
            }
            if (flag2)
            {
                foreach (Дорога дорога in base.парк.пути_стоянки)
                {
                    if (дорога == this.положение.дорога)
                    {
                        if (base.скорость == 0.0)
                        {
                            flag = true;
                        }
                        double num24 = (дорога.длина - this.положение.расстояние) - 20.0;
                        num4 = Math.Min(num4, num24);
                        if (num > num24)
                        {
                            num = num24;
                        }
                        break;
                    }
                }
            }
            bool flag3 = true;
            int index = -1;
            foreach (Штанга штанга in this.штанги)
            {
                if (flag && (this.ускорение <= 0.0))
                {
                    штанга.поднимается = false;
                    штанга.провод = null;
                    flag3 = false;
                }
                else if ((штанга.провод != null) && (!штанга.провод.обесточенный || (base.скорость != 0.0)))
                {
                    if (штанга.поднята)
                    {
                        double num26 = штанга.провод.длина - штанга.пройденное_расстояние_по_проводу;
                        if (штанга.провод.следующие_провода.Length == 1)
                        {
                            num26 += штанга.провод.следующие_провода[0].длина;
                        }
                        if (num > num26)
                        {
                            num = num26;
                        }
                        if ((штанга.провод.следующие_провода.Length > 1) && (штанга.пройденное_расстояние_по_проводу > (штанга.провод.длина - 2.0)))
                        {
                            List<Контактный_провод> collection = new List<Контактный_провод>(штанга.провод.следующие_провода);
                            if ((this.положение.дорога != null) && (this.следующая_дорога != null))
                            {
                                Дорога дорога2 = null;
                                if (this.следующая_дорога.следующие_дороги.Length == 1)
                                {
                                    дорога2 = this.следующая_дорога.следующие_дороги[0];
                                }
                                List<Контактный_провод> list4 = new List<Контактный_провод>(collection);
                                List<double> list5 = new List<double>();
                                for (int k = 0; k < list4.Count; k++)
                                {
                                    list5.Add(0.0);
                                }
                                for (int m = 0; (m < 50) && (list4.Count >= 2); m++)
                                {
                                    for (int n = 0; n < list4.Count; n++)
                                    {
                                        List<double> list6;
                                        int num33;
                                        bool flag4 = true;
                                        (list6 = list5)[num33 = n] = list6[num33] + 10.0;
                                        while (list5[n] > list4[n].длина)
                                        {
                                            (list6 = list5)[num33 = n] = list6[num33] - list4[n].длина;
                                            if (list4[n].следующие_провода.Length > 0)
                                            {
                                                for (int num30 = 1; num30 < list4[n].следующие_провода.Length; num30++)
                                                {
                                                    collection.Add(collection[n]);
                                                    list4.Add(list4[n].следующие_провода[num30]);
                                                    list5.Add(list5[n] - 10.0);
                                                }
                                                list4[n] = list4[n].следующие_провода[0];
                                            }
                                            else
                                            {
                                                flag4 = false;
                                                break;
                                            }
                                        }
                                        if (flag4)
                                        {
                                            Double_Point pos = list4[n].найти_координаты(list5[n], 0.0);
                                            if (((мир.Найти_положение(pos, this.положение.дорога).дорога == null) && (мир.Найти_положение(pos, this.следующая_дорога).дорога == null)) && ((дорога2 == null) || (мир.Найти_положение(pos, дорога2).дорога == null)))
                                            {
                                                flag4 = false;
                                            }
                                        }
                                        if (!flag4)
                                        {
                                            collection.RemoveAt(n);
                                            list4.RemoveAt(n);
                                            list5.RemoveAt(n);
                                            n--;
                                        }
                                    }
                                }
                            }
                            if (collection.Count == 0)
                            {
                                collection.AddRange(штанга.провод.следующие_провода);
                            }
                            Контактный_провод _провод = collection[this.rand.Next(collection.Count)];
                            if (_провод == штанга.провод.следующие_провода[0])
                            {
                                index = 0;
                            }
                            else
                            {
                                index = 1;
                            }
                        }
                        foreach (object obj6 in штанга.провод.объекты)
                        {
                            if (obj6 is Штанга)
                            {
                                Штанга штанга2 = (Штанга)obj6;
                                if (штанга2.пройденное_расстояние_по_проводу > штанга.пройденное_расстояние_по_проводу)
                                {
                                    num3 = Math.Min(num3, штанга2.пройденное_расстояние_по_проводу - штанга.пройденное_расстояние_по_проводу);
                                }
                            }
                        }
                        Контактный_провод _провод2 = null;
                        if (штанга.провод.следующие_провода.Length == 1)
                        {
                            _провод2 = штанга.провод.следующие_провода[0];
                        }
                        else if (index >= 0)
                        {
                            _провод2 = штанга.провод.следующие_провода[index];
                        }
                        if (_провод2 != null)
                        {
                            foreach (object obj7 in _провод2.объекты)
                            {
                                if (obj7 is Штанга)
                                {
                                    Штанга штанга3 = (Штанга)obj7;
                                    num3 = Math.Min(num3, (штанга.провод.длина - штанга.пройденное_расстояние_по_проводу) + штанга3.пройденное_расстояние_по_проводу);
                                }
                            }
                        }
                    }
                    else
                    {
                        flag3 = false;
                    }
                }
                else
                {
                    Контактный_провод _провод3 = штанга.провод;
                    штанга.Найти_провод(мир.контактные_провода);
                    if (штанга.провод != null)
                    {
                        if (штанга.опущена)
                        {
                            штанга.поднимается = true;
                        }
                        else
                        {
                            штанга.поднимается = false;
                            штанга.провод = _провод3;
                        }
                        flag3 = false;
                    }
                }
            }
            if (((this.штанги.Length == 0) && flag) && (this.ускорение <= 0.0))
            {
                flag3 = false;
            }
            base.включен = flag3;
            if (num > num2)
            {
                num = num2;
            }
            if (num > (num3 - 10.0))
            {
                num = num3 - 10.0;
            }
            if (num > num4)
            {
                num = num4 - 10.0;
            }
            double num31 = 16.0;
            this.открыть_двери(false);
            bool flag5 = (base.рейс == null) || (мир.время >= base.рейс.время_отправления);
            if (!flag5 && (this.положение.дорога != null))
            {
                num4 = Math.Min(num4, (this.положение.дорога.длина - this.положение.расстояние) - 15.0);
                if (num > num4)
                {
                    num = num4;
                }
            }
            if ((base.осталось_стоять > 0.0) && (num2 > 20.0))
            {
                num = 0.0;
                num31 = 0.0;
                if (base.скорость == 0.0)
                {
                    if (base.стоим_с_закрытыми_дверями)
                    {
                        if (!flag && flag5)
                        {
                            base.осталось_стоять -= Мир.прошло_времени;
                            if (base.осталось_стоять <= 0.0)
                            {
                                base.стоим_с_закрытыми_дверями = false;
                            }
                        }
                    }
                    else
                    {
                        this.открыть_двери(true);
                        if (this.двери_открыты && flag5)
                        {
                            base.осталось_стоять -= Мир.прошло_времени;
                        }
                    }
                }
            }
            else
            {
                // HACK: Определяем скорость движения
                if (r1 > 0.0)
                {
                    num31 = r1 / 4.905;
                    //num31 = num1;
                }
                num1 = num1 / 4.905;
                if (num1 < num31)
                {
                    double дистанция_торможения = (num31 * num31 - num1 * num1) / (2 * 0.8);
                    if ((num < дистанция_торможения))// && (num > 11.0))
                    {
                        num31 = Math.Sqrt(2 * num * 0.8);
                    }
                    //else if (num <= 11.0)
                    //{
                    //    num31 = 2.8;
                    //}
                    if (num31 < num1)
                    {
                        num31 = num1;
                    }
                }
                //if (num < 256.0)
                //{
                //    num31 = 14.0;
                //}
                //if (num < 196.0)
                //{
                //    num31 = 12.0;
                //}
                //if (num < 144.0)
                //{
                //    num31 = 10.0;
                //}
                //if (num < 100.0)
                //{
                //    num31 = 8.0;
                //}
                //if (num < 64.0)
                //{
                //    num31 = 6.0;
                //}
                //if (num < 32.0)
                //{
                //    num31 = 4.0;
                //}
                //if (num < 16.0)
                //{
                //    num31 = 2.7;
                //}
                // HACK: вывод в консоль результатов
                if (num2 < 20.0)
                {
                    base.осталось_стоять = 8.0 + (this.rand.NextDouble() * 5.0);
                    if ((_остановка != null) && _остановка.служебная)
                    {
                        base.осталось_стоять = 1.0 + (this.rand.NextDouble() * 3.0);
                        base.стоим_с_закрытыми_дверями = true;
                    }
                }
                if (num4 < 10.0)
                {
                    num31 = 0.0;
                }
                if (num3 < 10.0)
                {
                    num31 = 0.0;
                }
            }
            if (!this.двери_закрыты)
            {
                num31 = 0.0;
            }
            base.система_управления.автоматически_управлять(num31, num, index);
        }

        public bool дверь_закрыта(int номер)
        {
            foreach (Двери двери in this.двери)
            {
                if ((двери.номер == номер) && !двери.закрыты)
                {
                    return false;
                }
            }
            return true;
        }

        public bool дверь_открыта(int номер)
        {
            foreach (Двери двери in this.двери)
            {
                if ((двери.номер == номер) && !двери.открыты)
                {
                    return false;
                }
            }
            return true;
        }

        public void открыть_двери(bool открыть)
        {
            for (int i = 0; i < this.количество_дверей; i++)
            {
                this.открыть_двери(i, открыть);
            }
        }

        public void открыть_двери(int номер, bool открыть)
        {
            foreach (Двери двери in this.двери)
            {
                if (двери.номер == номер)
                {
                    двери.открываются = открыть;
                }
            }
        }

        public void открыть_двери_водителя(bool открыть)
        {
            foreach (Двери двери in this.двери)
            {
                if (двери.дверь_водителя)
                {
                    двери.открываются = открыть;
                }
            }
        }

        public bool двери_водителя_закрыты
        {
            get
            {
                foreach (Двери двери in this.двери)
                {
                    if (двери.дверь_водителя && !двери.закрыты)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool двери_водителя_открыты
        {
            get
            {
                foreach (Двери двери in this.двери)
                {
                    if (двери.дверь_водителя && !двери.открыты)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool двери_закрыты
        {
            get
            {
                foreach (Двери двери in this.двери)
                {
                    if ((двери.номер >= 0) && !двери.закрыты)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool двери_открыты
        {
            get
            {
                foreach (Двери двери in this.двери)
                {
                    if ((двери.номер >= 0) && !двери.открыты)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool обесточен
        {
            get
            {
                foreach (Штанга штанга in this.штанги)
                {
                    if (!штанга.поднята || штанга.провод.обесточенный)
                    {
                        return true;
                    }
                }
                return !base.включен;
            }
        }

        public override double ускорение
        {
            get
            {
                return base.система_управления.ускорение;
            }
        }

        public bool штанги_подняты
        {
            get
            {
                foreach (Штанга штанга in this.штанги)
                {
                    if (!штанга.поднята)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class Колесо : Mesh_Object, Mesh_Object.IFromFile, IMatrixObject
        {
            private string file = "";
            public Double_Point базовое_направление;
            public Double_3D_Point координаты;
            public bool левое;
            public double поворот;
            public double пройденное_расстояние;
            public double радиус = 0.628;
            public Положение текущее_положение;

            public Колесо(bool левое, string dir, string filename, double радиус)
            {
                this.левое = левое;
                base.mesh_dir = dir;
                this.file = filename;
                this.радиус = радиус;
            }

            public Matrix get_matrix(int index)
            {
                Matrix matrix;
                if (!this.левое)
                {
                    matrix = ((Matrix.RotationZ(-((float)(this.пройденное_расстояние / this.радиус))) * Matrix.RotationY(-((float)this.поворот))) * Matrix.RotationZ((float)this.базовое_направление.y)) * Matrix.RotationY(-((float)this.базовое_направление.x));
                }
                else
                {
                    matrix = ((Matrix.RotationZ((float)(this.пройденное_расстояние / this.радиус)) * Matrix.RotationY(-((float)this.поворот))) * Matrix.RotationZ((float)this.базовое_направление.y)) * Matrix.RotationY(-((float)(this.базовое_направление.x + 3.1415926535897931)));
                }
                Double_3D_Point point = this.координаты;
                return (matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
            }

            public string filename
            {
                get
                {
                    return this.file;
                }
            }

            public int matrices_count
            {
                get
                {
                    return 1;
                }
            }

            public Double_Point направление
            {
                get
                {
                    return new Double_Point(this.базовое_направление.x + this.поворот, this.базовое_направление.y);
                }
            }
        }

        public class Обычный_троллейбус : Троллейбус, Объект_привязки_3D, Объект_привязки
        {
            private int бывший_указатель_поворота;
            private bool была_аварийная_сигнализация;
            public double время_поворотников;
            public double время_поворотников_max = 1.0;
            public double время_поворотников_выкл = 0.5;
            public Дополнение[] дополнения = new Дополнение[0];
            public Кузов кузов;
            public Модель_троллейбуса модель;
            public Сочленение[] сочленения = new Сочленение[0];
            public Хвост[] хвосты = new Хвост[0];

            public Обычный_троллейбус(Модель_троллейбуса модель, Double_3D_Point координаты, Double_Point направление, Управление управление, Парк парк, Маршрут маршрут)
            {
                this.модель = модель;
                base.основная_папка = модель.dir;
                this.кузов = new Кузов(this);
                this.кузов.координаты = координаты;
                this.кузов.направление = направление;
                base.управление = управление;
                base.маршрут = маршрут;
                base.парк = парк;
                this.хвосты = new Хвост[модель.количество_хвостов];
                this.сочленения = new Сочленение[модель.количество_хвостов];
                double num = 0.0;
                for (int i = 0; i < модель.количество_хвостов; i++)
                {
                    num -= модель.хвост_dist_1[i] + модель.хвост_dist_2[i];
                    this.хвосты[i] = new Хвост(this);
                    this.хвосты[i].координаты = this.кузов.координаты + ((Double_3D_Point)(new Double_3D_Point(this.кузов.направление) * num));
                    this.хвосты[i].направление = this.кузов.направление;
                    this.сочленения[i] = new Сочленение(this);
                }
                this.дополнения = new Дополнение[модель.дополнения.Length];
                for (int j = 0; j < модель.дополнения.Length; j++)
                {
                    this.дополнения[j] = new Дополнение(this.Найти_часть(модель.дополнения[j].часть), модель.дополнения[j].filename, модель.дополнения[j].тип);
                }
                base.радиус_колёс = модель.радиус_колёс;
                base.колёса = new Троллейбус.Колесо[2 * модель.колёсные_пары.Length];
                for (int k = 0; k < модель.колёсные_пары.Length; k++)
                {
                    base.колёса[2 * k] = new Троллейбус.Колесо(true, модель.колёсные_пары[k].dir, модель.колёсные_пары[k].filename, base.радиус_колёс);
                    base.колёса[(2 * k) + 1] = new Троллейбус.Колесо(false, модель.колёсные_пары[k].dir, модель.колёсные_пары[k].filename, base.радиус_колёс);
                }
                base.штанги = new Троллейбус.Штанга[модель.штанги.Length];
                for (int m = 0; m < base.штанги.Length; m++)
                {
                    base.штанги[m] = new Троллейбус.Штанга(m == 0, модель.штанги_dir, модель.штанги_filename, модель.штанги_полная_длина, модель.штанги_угол_min);
                }
                this.Обновить_колёса_и_штанги();
                foreach (Троллейбус.Штанга штанга in base.штанги)
                {
                    штанга.направление = штанга.базовое_направление;
                }
                base.количество_дверей = модель.количество_дверей;
                base.двери = new Двери[модель.двери.Length];
                for (int n = 0; n < модель.двери.Length; n++)
                {
                    base.двери[n] = Двери.Построить(модель.двери[n].модель, this.Найти_часть(модель.двери[n].часть), модель.двери[n].p1, модель.двери[n].p2, модель.двери[n].правые);
                    base.двери[n].дверь_водителя = модель.двери[n].дверь_водителя;
                    base.двери[n].номер = модель.двери[n].номер;
                }
                base.указатель_наряда = new Указатель_наряда();
                base.система_управления = Система_управления.Parse(модель.система_управления, this);
            }

            public override void CreateMesh(Мир мир)
            {
                if (мир.filename != null)
                {
                    string[] strArray = new string[] { @"..\Cities\" + Path.GetFileNameWithoutExtension(мир.filename) + @"\" + base.парк.название + @"\", @"..\Cities\" + Path.GetFileNameWithoutExtension(мир.filename) + @"\" };
                    this.кузов.extra_mesh_dirs = strArray;
                    foreach (Хвост хвост in this.хвосты)
                    {
                        хвост.extra_mesh_dirs = strArray;
                    }
                    foreach (Сочленение сочленение in this.сочленения)
                    {
                        сочленение.extra_mesh_dirs = strArray;
                    }
                    foreach (Троллейбус.Колесо колесо in base.колёса)
                    {
                        колесо.extra_mesh_dirs = strArray;
                    }
                    foreach (Троллейбус.Штанга штанга in base.штанги)
                    {
                        штанга.extra_mesh_dirs = strArray;
                    }
                    foreach (Двери двери in base.двери)
                    {
                        двери.extra_mesh_dirs = strArray;
                    }
                    base.указатель_наряда.extra_mesh_dirs = strArray;
                }
                this.кузов.CreateMesh();
                this.кузов.Обновить_маршрутный_указатель(base.маршрут.номер);
                foreach (Хвост хвост2 in this.хвосты)
                {
                    хвост2.CreateMesh();
                    хвост2.Обновить_маршрутный_указатель(base.маршрут.номер);
                }
                foreach (Сочленение сочленение2 in this.сочленения)
                {
                    сочленение2.CreateMesh();
                }
                foreach (Дополнение дополнение in this.дополнения)
                {
                    дополнение.CreateMesh();
                }
                foreach (Троллейбус.Колесо колесо2 in base.колёса)
                {
                    колесо2.CreateMesh();
                }
                foreach (Троллейбус.Штанга штанга2 in base.штанги)
                {
                    штанга2.CreateMesh();
                }
                foreach (Двери двери2 in base.двери)
                {
                    двери2.CreateMesh();
                }
                base.указатель_наряда.CreateMesh();
                if (base.наряд != null)
                {
                    base.указатель_наряда.Обновить_картинку(base.наряд);
                }
            }

            public override void CreateSoundBuffers()
            {
                base.система_управления.CreateSoundBuffers();
            }

            public override void Render()
            {
                Double_3D_Point point = this.кузов.координаты - MyDirect3D.Camera_Position;
                if (point.модуль < 250.0)
                {
                    this.кузов.Render();
                    foreach (Хвост хвост in this.хвосты)
                    {
                        хвост.Render();
                    }
                    foreach (Сочленение сочленение in this.сочленения)
                    {
                        сочленение.Render();
                    }
                    foreach (Дополнение дополнение in this.дополнения)
                    {
                        if ((дополнение.тип == Дополнение.Тип.фары) && base.включены_фары)
                        {
                            дополнение.Render();
                        }
                        if (this.время_поворотников < this.время_поворотников_выкл)
                        {
                            if ((дополнение.тип == Дополнение.Тип.влево) && ((base.указатель_поворота < 0) || base.аварийная_сигнализация))
                            {
                                дополнение.Render();
                            }
                            if ((дополнение.тип == Дополнение.Тип.вправо) && ((base.указатель_поворота > 0) || base.аварийная_сигнализация))
                            {
                                дополнение.Render();
                            }
                        }
                        if ((дополнение.тип == Дополнение.Тип.тормоз) && (base.система_управления.ход_или_тормоз < 0))
                        {
                            дополнение.Render();
                        }
                        if ((дополнение.тип == Дополнение.Тип.назад) && (base.система_управления.направление < 0))
                        {
                            дополнение.Render();
                        }
                    }
                    foreach (Троллейбус.Колесо колесо in base.колёса)
                    {
                        колесо.Render();
                    }
                    foreach (Троллейбус.Штанга штанга in base.штанги)
                    {
                        штанга.Render();
                    }
                    foreach (Двери двери in base.двери)
                    {
                        двери.Render();
                    }
                    if (base.наряд != null)
                    {
                        base.указатель_наряда.matrix = Matrix.Translation((float)this.модель.наряд_pos.x, (float)this.модель.наряд_pos.y, (float)this.модель.наряд_pos.z) * this.кузов.get_matrix(0);
                        base.указатель_наряда.Render();
                    }
                }
            }

            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
                base.система_управления.UpdateSound(игроки, игра_активна);
            }

            public override Положение[] Найти_все_положения(Мир мир)
            {
                List<Положение> list = new List<Положение>();
                Double_3D_Point point = new Double_3D_Point(this.кузов.направление);
                Double_3D_Point point2 = Double_3D_Point.поворот(this.кузов.направление, 1.5707963267948966);
                int length = this.модель.занятые_положения.Length;
                for (int i = 0; i < this.модель.занятые_положения_хвостов.Length; i++)
                {
                    length += this.модель.занятые_положения_хвостов[i].Length;
                }
                Double_3D_Point[] pos = new Double_3D_Point[length];
                int index = 0;
                int num4 = 0;
                while (num4 < this.модель.занятые_положения.Length)
                {
                    pos[index] = (Double_3D_Point)((this.кузов.координаты + (point * this.модель.занятые_положения[num4].x)) + (point2 * this.модель.занятые_положения[num4].y));
                    num4++;
                    index++;
                }
                for (int j = 0; j < this.модель.занятые_положения_хвостов.Length; j++)
                {
                    point = new Double_3D_Point(this.хвосты[j].направление);
                    point2 = Double_3D_Point.поворот(this.хвосты[j].направление, 1.5707963267948966);
                    int num6 = 0;
                    while (num6 < this.модель.занятые_положения_хвостов[j].Length)
                    {
                        pos[index] = (Double_3D_Point)((this.хвосты[j].координаты + (point * this.модель.занятые_положения_хвостов[j][num6].x)) + (point2 * this.модель.занятые_положения_хвостов[j][num6].y));
                        num6++;
                        index++;
                    }
                }
                Положение[] collection = мир.Найти_все_положения(pos);
                for (int k = 0; k < collection.Length; k++)
                {
                    collection[k].comment = this;
                }
                list.AddRange(collection);
                base.найденные_положения = list;
                return list.ToArray();
            }

            public Часть_троллейбуса Найти_часть(int index)
            {
                if (index > 0)
                {
                    return this.хвосты[index - 1];
                }
                if (index < 0)
                {
                    return this.сочленения[-index - 1];
                }
                return this.кузов;
            }

            public override void Обновить(Мир мир, Игрок[] игроки_в_игре)
            {
                ArrayList list = new ArrayList();
                if (игроки_в_игре != null)
                {
                    for (int i = 0; i < игроки_в_игре.Length; i++)
                    {
                        if (игроки_в_игре[i].объект_привязки == this)
                        {
                            list.Add(игроки_в_игре[i]);
                        }
                    }
                }
                double num2 = this.направление;
                if (list.Count > 0)
                {
                    int num3;
                    Объект_привязки_3D[] и_dArray = new Объект_привязки_3D[list.Count];
                    int index = 0;
                    foreach (Игрок игрок in list)
                    {
                        double[] numArray = new double[this.хвосты.Length];
                        num3 = 0;
                        while (num3 < this.хвосты.Length)
                        {
                            Double_Point point = игрок.Camera_Position.xz_point - this.хвосты[num3].координаты.xz_point;
                            numArray[num3] = point.модуль;
                            num3++;
                        }
                        double[] numArray2 = new double[this.сочленения.Length];
                        num3 = 0;
                        while (num3 < this.сочленения.Length)
                        {
                            Double_Point point2 = игрок.Camera_Position.xz_point - this.сочленения[num3].координаты.xz_point;
                            numArray2[num3] = point2.модуль;
                            num3++;
                        }
                        bool flag = false;
                        num3 = 0;
                        while (num3 < this.сочленения.Length)
                        {
                            if (numArray2[num3] < 1.3)
                            {
                                и_dArray[index] = this.сочленения[num3];
                                flag = true;
                                break;
                            }
                            num3++;
                        }
                        if (!flag)
                        {
                            и_dArray[index] = this.кузов;
                            Double_Point point3 = игрок.Camera_Position.xz_point - this.кузов.координаты.xz_point;
                            double num5 = point3.модуль;
                            for (num3 = 0; num3 < this.хвосты.Length; num3++)
                            {
                                if (numArray[num3] < num5)
                                {
                                    num5 = numArray[num3];
                                    и_dArray[index] = this.хвосты[num3];
                                }
                            }
                        }
                        index++;
                    }
                    Double_3D_Point[] pointArray = new Double_3D_Point[list.Count];
                    Double_3D_Point[] pointArray2 = new Double_3D_Point[list.Count];
                    Double_3D_Point[] pointArray3 = new Double_3D_Point[list.Count];
                    Double_Point[] pointArray4 = new Double_Point[list.Count];
                    Double_Point[] pointArray5 = new Double_Point[list.Count];
                    num3 = 0;
                    foreach (Игрок игрок2 in list)
                    {
                        pointArray[num3] = игрок2.Camera_Position - и_dArray[num3].координаты_3D;
                        pointArray[num3].xz_point *= new Double_Point(-и_dArray[num3].направление);
                        pointArray[num3].xy_point *= new Double_Point(-и_dArray[num3].направление_y);
                        pointArray2[num3] = игрок2.поворачивать_камеру ? игрок2.Camera_Position : и_dArray[num3].координаты_3D;
                        pointArray4[num3] = new Double_Point(и_dArray[num3].направление, и_dArray[num3].направление_y);
                        num3++;
                    }
                    this.Передвинуть(base.скорость * Мир.прошло_времени, мир);
                    num3 = 0;
                    foreach (Игрок игрок3 in list)
                    {
                        pointArray[num3].xy_point *= new Double_Point(и_dArray[num3].направление_y);
                        pointArray[num3].xz_point *= new Double_Point(и_dArray[num3].направление);
                        pointArray[num3] += и_dArray[num3].координаты_3D;
                        pointArray3[num3] = игрок3.поворачивать_камеру ? pointArray[num3] : и_dArray[num3].координаты_3D;
                        pointArray5[num3] = new Double_Point(и_dArray[num3].направление, и_dArray[num3].направление_y);
                        игрок3.Camera_Position += pointArray3[num3] - pointArray2[num3];
                        if (игрок3.поворачивать_камеру)
                        {
                            игрок3.Camera_Rotation += pointArray5[num3] - pointArray4[num3];
                        }
                        num3++;
                    }
                }
                else
                {
                    this.Передвинуть(base.скорость * Мир.прошло_времени, мир);
                }
                if ((this.направление != num2) && (base.поворот_руля != 0.0))
                {
                    double num6 = this.направление - num2;
                    if (num6 < -3.1415926535897931)
                    {
                        num6 += 6.2831853071795862;
                    }
                    if (num6 > 3.1415926535897931)
                    {
                        num6 -= 6.2831853071795862;
                    }
                    int num7 = Math.Sign((double)(-num6 * base.поворот_руля));
                    if (num7 > 0)
                    {
                        base.поворот_руля += num7 * num6;
                        if (Math.Abs(base.поворот_руля) < 0.001)
                        {
                            base.поворот_руля = 0.0;
                        }
                    }
                }
                this.время_поворотников += Мир.прошло_времени;
                while (this.время_поворотников > this.время_поворотников_max)
                {
                    this.время_поворотников -= this.время_поворотников_max;
                }
                if ((base.указатель_поворота != this.бывший_указатель_поворота) || (base.аварийная_сигнализация != this.была_аварийная_сигнализация))
                {
                    this.время_поворотников = 0.0;
                }
                this.бывший_указатель_поворота = base.указатель_поворота;
                this.была_аварийная_сигнализация = base.аварийная_сигнализация;
                base.sound_ускоряется = false;
                base.sound_замедляется = false;
                if (Math.Abs(base.скорость) < 1E-06)
                {
                    base.скорость = 0.0;
                }
                double num8 = base.скорость;
                base.скорость += this.ускорение * Мир.прошло_времени;
                if (((this.ускорение * base.скорость) < 0.0) && ((base.скорость * num8) < 0.0))
                {
                    base.скорость = 0.0;
                }
                if ((base.система_управления.ход_или_тормоз > 0) && !base.обесточен)
                {
                    base.sound_ускоряется = true;
                }
                if ((base.система_управления.ход_или_тормоз < 0) && !base.обесточен)
                {
                    base.sound_замедляется = true;
                }
                this.Обновить_положение(мир);
                foreach (Двери двери in base.двери)
                {
                    двери.обновить();
                }
                this.Обновить_колёса_и_штанги();
                foreach (Троллейбус.Штанга штанга in base.штанги)
                {
                    if (!штанга.поднимается)
                    {
                        штанга.направление += this.направление - num2;
                    }
                    штанга.Обновить(base.система_управления.переключение);
                }
                base.скорость_abs -= 0.1 * Мир.прошло_времени;
                base.Обновить_рейс();
            }

            private void Обновить_колёса_и_штанги()
            {
                Double_3D_Point point = new Double_3D_Point(this.кузов.направление);
                point.угол_y += 1.5707963267948966;
                for (int i = 0; i < this.модель.колёсные_пары.Length; i++)
                {
                    Часть_троллейбуса _троллейбуса = this.Найти_часть(this.модель.колёсные_пары[i].часть);
                    point = new Double_3D_Point(_троллейбуса.направление);
                    point.угол_y += 1.5707963267948966;
                    base.колёса[2 * i].координаты = (Double_3D_Point)(((_троллейбуса.координаты + (new Double_3D_Point(_троллейбуса.направление) * this.модель.колёсные_пары[i].pos.x)) + (Double_3D_Point.поворот(_троллейбуса.направление, -1.5707963267948966) * this.модель.колёсные_пары[i].pos.y)) + (point * base.радиус_колёс));
                    base.колёса[2 * i].базовое_направление = _троллейбуса.направление;
                    base.колёса[(2 * i) + 1].координаты = (Double_3D_Point)(((_троллейбуса.координаты + (new Double_3D_Point(_троллейбуса.направление) * this.модель.колёсные_пары[i].pos.x)) + (Double_3D_Point.поворот(_троллейбуса.направление, 1.5707963267948966) * this.модель.колёсные_пары[i].pos.y)) + (point * base.радиус_колёс));
                    base.колёса[(2 * i) + 1].базовое_направление = _троллейбуса.направление;
                }
                base.колёса[0].поворот = -base.поворот_руля;
                base.колёса[1].поворот = -base.поворот_руля;
                Double_3D_Point point2 = this.кузов.координаты;
                Double_Point angle = this.кузов.направление;
                if (this.хвосты.Length > 0)
                {
                    point2 = this.хвосты[this.хвосты.Length - 1].координаты;
                    angle = this.хвосты[this.хвосты.Length - 1].направление;
                }
                point = new Double_3D_Point(angle);
                point.угол_y += 1.5707963267948966;
                if (base.штанги.Length > 1)
                {
                    base.штанги[0].основание = (Double_3D_Point)(((point2 + (new Double_3D_Point(angle) * this.модель.штанги[0].pos.x)) + (Double_3D_Point.поворот(angle, -1.5707963267948966) * this.модель.штанги[0].pos.z)) + (point * this.модель.штанги[0].pos.y));
                    base.штанги[1].основание = (Double_3D_Point)(((point2 + (new Double_3D_Point(angle) * this.модель.штанги[1].pos.x)) + (Double_3D_Point.поворот(angle, 1.5707963267948966) * this.модель.штанги[1].pos.z)) + (point * this.модель.штанги[1].pos.y));
                    base.штанги[0].базовое_направление = angle.x + 3.1415926535897931;
                    base.штанги[0].направление_y = -angle.y;
                    base.штанги[1].базовое_направление = angle.x + 3.1415926535897931;
                    base.штанги[1].направление_y = -angle.y;
                }
            }

            public override void Обновить_маршрутные_указатели()
            {
                this.кузов.Обновить_маршрутный_указатель(base.маршрут.номер);
                foreach (Хвост хвост in this.хвосты)
                {
                    хвост.Обновить_маршрутный_указатель(base.маршрут.номер);
                }
            }

            public void Обновить_положение(Мир мир)
            {
                Double_3D_Point pos = (Double_3D_Point)((base.колёса[0].координаты + base.колёса[1].координаты) / 2.0);
                if ((this.положение.дорога != null) && (мир.Найти_положение(pos, this.положение.дорога).дорога != null))
                {
                    base.положение = мир.Найти_положение(pos, this.положение.дорога);
                }
                else if ((base.следующая_дорога != null) && (мир.Найти_положение(pos, base.следующая_дорога).дорога != null))
                {
                    this.положение.дорога.объекты.Remove(this);
                    base.положение = мир.Найти_положение(pos, base.следующая_дорога);
                    this.положение.дорога.объекты.Add(this);
                    base.следующая_дорога = null;
                }
                else
                {
                    if (this.положение.дорога != null)
                    {
                        this.положение.дорога.объекты.Remove(this);
                    }
                    base.положение = мир.Найти_ближайшее_положение(pos.xz_point, мир.дороги);
                    if (this.положение.дорога != null)
                    {
                        this.положение.дорога.объекты.Add(this);
                    }
                    base.следующая_дорога = null;
                }
            }

            public override void Передвинуть(double расстояние, Мир мир)
            {
                Double_3D_Point[] pointArray = new Double_3D_Point[1 + this.хвосты.Length];
                Double_3D_Point[] pointArray2 = new Double_3D_Point[1 + this.хвосты.Length];
                double[] numArray = new double[1 + this.хвосты.Length];
                pointArray[0] = (Double_3D_Point)((base.колёса[2].координаты + base.колёса[3].координаты) / 2.0);
                pointArray2[0] = (Double_3D_Point)((base.колёса[0].координаты + base.колёса[1].координаты) / 2.0);
                Double_3D_Point point8 = pointArray2[0] - pointArray[0];
                numArray[0] = point8.модуль;
                for (int i = 1; i < pointArray.Length; i++)
                {
                    pointArray[i] = (Double_3D_Point)((base.колёса[(2 * i) + 2].координаты + base.колёса[(2 * i) + 3].координаты) / 2.0);
                    Double_3D_Point point9 = pointArray2[i - 1] - pointArray[i - 1];
                    pointArray2[i] = pointArray[i - 1] + ((Double_3D_Point)(new Double_3D_Point(point9.угол) * -this.модель.хвост_dist_1[i - 1]));
                    Double_3D_Point point10 = pointArray2[i] - pointArray[i];
                    numArray[i] = point10.модуль;
                }
                foreach (Троллейбус.Колесо колесо in base.колёса)
                {
                    колесо.координаты += (Double_3D_Point)(new Double_3D_Point(колесо.направление) * расстояние);
                    колесо.пройденное_расстояние += расстояние;
                }
                double[] numArray2 = new double[base.колёса.Length / 2];
                for (int j = 0; j < base.колёса.Length; j++)
                {
                    Троллейбус.Колесо колесо2 = base.колёса[j];
                    Double_3D_Point pos = колесо2.координаты;
                    pos.y -= колесо2.радиус;
                    if (колесо2.текущее_положение.дорога != null)
                    {
                        колесо2.текущее_положение = мир.Найти_положение(pos, колесо2.текущее_положение.дорога);
                    }
                    if (колесо2.текущее_положение.дорога == null)
                    {
                        Double_3D_Point[] pointArray5 = new Double_3D_Point[] { pos };
                        Положение[] положениеArray = мир.Найти_все_положения(pointArray5);
                        if (положениеArray.Length > 0)
                        {
                            колесо2.текущее_положение = положениеArray[0];
                        }
                    }
                    if (колесо2.текущее_положение.дорога != null)
                    {
                        numArray2[j / 2] = Math.Max(numArray2[j / 2], колесо2.координаты.y - колесо2.текущее_положение.высота);
                        double num3 = колесо2.текущее_положение.дорога.найти_направление_y(колесо2.текущее_положение.расстояние);
                        if (num3 != 0.0)
                        {
                            base.скорость += Math.Cos(num3 + 1.5707963267948966) * 0.03;
                        }
                    }
                    else
                    {
                        numArray2[j / 2] = Math.Max(numArray2[j / 2], колесо2.радиус - 0.1);
                    }
                }
                for (int k = 0; k < base.колёса.Length; k++)
                {
                    base.колёса[k].координаты.y = numArray2[k / 2];
                }
                Double_3D_Point[] pointArray3 = new Double_3D_Point[1 + this.хвосты.Length];
                Double_3D_Point[] pointArray4 = new Double_3D_Point[1 + this.хвосты.Length];
                pointArray3[0] = (Double_3D_Point)((base.колёса[2].координаты + base.колёса[3].координаты) / 2.0);
                pointArray4[0] = (Double_3D_Point)((base.колёса[0].координаты + base.колёса[1].координаты) / 2.0);
                for (int m = 0; m < pointArray.Length; m++)
                {
                    if (m > 0)
                    {
                        pointArray3[m] = (Double_3D_Point)((base.колёса[(2 * m) + 2].координаты + base.колёса[(2 * m) + 3].координаты) / 2.0);
                        Double_3D_Point point11 = pointArray4[m - 1] - pointArray3[m - 1];
                        pointArray4[m] = pointArray3[m - 1] + ((Double_3D_Point)(new Double_3D_Point(point11.угол) * -this.модель.хвост_dist_1[m - 1]));
                    }
                    Double_3D_Point point12 = pointArray4[m] - pointArray3[m];
                    if (Math.Abs((double)(point12.модуль - numArray[m])) > 0.001)
                    {
                        try
                        {
                            Double_3D_Point point13 = pointArray4[m] - pointArray2[m];
                            double num6 = point13.модуль;
                            Double_3D_Point point14 = pointArray4[m] - pointArray2[m];
                            Double_3D_Point point15 = pointArray3[m] - pointArray2[m];
                            Double_3D_Point point2 = new Double_3D_Point(point14.угол - point15.угол);
                            Double_3D_Point point16 = point2 - new Double_3D_Point(point2.x, 0.0, 0.0);
                            double y = point16.модуль;
                            Double_Point point17 = new Double_Point(point2.x, y);
                            double d = point17.угол;
                            double num9 = (num6 * Math.Cos(d)) + Math.Sqrt((numArray[m] * numArray[m]) - (((num6 * num6) * Math.Sin(d)) * Math.Sin(d)));
                            Double_3D_Point point3 = pointArray3[m] - pointArray2[m];
                            pointArray3[m] = pointArray2[m] + ((Double_3D_Point)(new Double_3D_Point(point3.угол) * num9));
                            Троллейбус.Колесо колесо1 = base.колёса[(2 * m) + 2];
                            колесо1.пройденное_расстояние += point3.модуль - num9;
                            Троллейбус.Колесо колесо3 = base.колёса[(2 * m) + 3];
                            колесо3.пройденное_расстояние += point3.модуль - num9;
                        }
                        catch
                        {
                        }
                    }
                    if (m == 0)
                    {
                        Double_3D_Point point18 = pointArray4[m] - pointArray3[m];
                        this.кузов.координаты = pointArray3[m] + ((Double_3D_Point)(new Double_3D_Point(point18.угол) * -this.модель.колёсные_пары[m + 1].pos.x));
                        Double_3D_Point point19 = pointArray4[m] - pointArray3[m];
                        this.кузов.направление = point19.угол;
                        Double_3D_Point point4 = new Double_3D_Point(this.кузов.направление);
                        point4.угол_y += 1.5707963267948966;
                        this.кузов.координаты -= (Double_3D_Point)(point4 * base.радиус_колёс);
                    }
                    else
                    {
                        Double_3D_Point point20 = pointArray4[m] - pointArray3[m];
                        pointArray3[m] = pointArray4[m] + ((Double_3D_Point)(new Double_3D_Point(point20.угол) * -this.модель.хвост_dist_2[m - 1]));
                        Double_3D_Point point21 = pointArray4[m] - pointArray3[m];
                        this.хвосты[m - 1].координаты = pointArray3[m] + ((Double_3D_Point)(new Double_3D_Point(point21.угол) * -this.модель.колёсные_пары[m + 1].pos.x));
                        Double_3D_Point point22 = pointArray4[m] - pointArray3[m];
                        this.хвосты[m - 1].направление = point22.угол;
                        Double_3D_Point point5 = new Double_3D_Point(this.хвосты[m - 1].направление);
                        point5.угол_y += 1.5707963267948966;
                        Хвост хвост1 = this.хвосты[m - 1];
                        хвост1.координаты -= (Double_3D_Point)(point5 * base.радиус_колёс);
                        this.сочленения[m - 1].координаты = pointArray4[m];
                        Double_3D_Point point23 = pointArray4[m - 1] - pointArray4[m];
                        Double_Point point6 = point23.угол;
                        Double_3D_Point point24 = pointArray4[m] - pointArray3[m];
                        Double_Point point7 = point24.угол;
                        if (Math.Abs((double)(point6.x - point7.x)) < 3.1415926535897931)
                        {
                            this.сочленения[m - 1].направление.x = (point6.x + point7.x) / 2.0;
                        }
                        else
                        {
                            this.сочленения[m - 1].направление.x = ((point6.x + point7.x) / 2.0) + 3.1415926535897931;
                        }
                        if (Math.Abs((double)(point6.y - point7.y)) < 3.1415926535897931)
                        {
                            this.сочленения[m - 1].направление.y = (point6.y + point7.y) / 2.0;
                        }
                        else
                        {
                            this.сочленения[m - 1].направление.y = ((point6.y + point7.y) / 2.0) + 3.1415926535897931;
                        }
                        point5 = new Double_3D_Point(this.сочленения[m - 1].направление);
                        point5.угол_y += 1.5707963267948966;
                        Сочленение сочленение1 = this.сочленения[m - 1];
                        сочленение1.координаты -= (Double_3D_Point)(point5 * base.радиус_колёс);
                    }
                }
            }

            public override Double_Point координаты
            {
                get
                {
                    return this.кузов.координаты.xz_point;
                }
            }

            public Double_3D_Point координаты_3D
            {
                get
                {
                    return this.кузов.координаты;
                }
            }

            public override double направление
            {
                get
                {
                    return this.кузов.направление.x;
                }
            }

            public double направление_y
            {
                get
                {
                    return this.кузов.направление.y;
                }
            }

            public override Положение текущее_положение
            {
                get
                {
                    return base.положение;
                }
            }

            public class Дополнение : Mesh_Object, Mesh_Object.IFromFile, IMatrixObject
            {
                public string file;
                public Тип тип;
                public Троллейбус.Обычный_троллейбус.Часть_троллейбуса часть_троллейбуса;

                public Дополнение(Троллейбус.Обычный_троллейбус.Часть_троллейбуса часть_троллейбуса, string filename, Тип тип)
                {
                    this.часть_троллейбуса = часть_троллейбуса;
                    this.file = filename;
                    this.тип = тип;
                }

                public Matrix get_matrix(int index)
                {
                    return this.часть_троллейбуса.get_matrix(0);
                }

                public string filename
                {
                    get
                    {
                        base.mesh_dir = this.часть_троллейбуса.mesh_dir;
                        return this.file;
                    }
                }

                public int matrices_count
                {
                    get
                    {
                        return 1;
                    }
                }

                public enum Тип
                {
                    фары,
                    влево,
                    вправо,
                    тормоз,
                    назад
                }
            }

            public class Кузов : Троллейбус.Обычный_троллейбус.Часть_троллейбуса, Mesh_Object.IFromFile, IMatrixObject
            {
                public Кузов(Троллейбус.Обычный_троллейбус троллейбус)
                {
                    base.троллейбус = троллейбус;
                }

                public string filename
                {
                    get
                    {
                        base.mesh_dir = base.троллейбус.модель.dir;
                        return base.троллейбус.модель.filename;
                    }
                }
            }

            public class Сочленение : Троллейбус.Обычный_троллейбус.Часть_троллейбуса, Mesh_Object.IFromFile, IMatrixObject
            {
                public Сочленение(Троллейбус.Обычный_троллейбус троллейбус)
                {
                    base.троллейбус = троллейбус;
                }

                public string filename
                {
                    get
                    {
                        base.mesh_dir = base.троллейбус.модель.dir;
                        int index = 0;
                        for (int i = 0; i < base.троллейбус.сочленения.Length; i++)
                        {
                            if (base.троллейбус.сочленения[i] == this)
                            {
                                index = i;
                                break;
                            }
                        }
                        return base.троллейбус.модель.сочленение_filename[index];
                    }
                }
            }

            public class Хвост : Троллейбус.Обычный_троллейбус.Часть_троллейбуса, Mesh_Object.IFromFile, IMatrixObject
            {
                public Хвост(Троллейбус.Обычный_троллейбус троллейбус)
                {
                    base.троллейбус = троллейбус;
                }

                public string filename
                {
                    get
                    {
                        base.mesh_dir = base.троллейбус.модель.dir;
                        int index = 0;
                        for (int i = 0; i < base.троллейбус.хвосты.Length; i++)
                        {
                            if (base.троллейбус.хвосты[i] == this)
                            {
                                index = i;
                                break;
                            }
                        }
                        return base.троллейбус.модель.хвост_filename[index];
                    }
                }
            }

            public class Часть_троллейбуса : Mesh_Object, Объект_привязки_3D, Объект_привязки, IMatrixObject
            {
                public Double_3D_Point координаты;
                public Double_Point направление;
                public Троллейбус.Обычный_троллейбус троллейбус;

                public Matrix get_matrix(int index)
                {
                    Matrix matrix = Matrix.RotationZ((float)this.направление.y) * Matrix.RotationY(-((float)this.направление.x));
                    Double_3D_Point point = this.координаты;
                    return (matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
                }

                public void Обновить_маршрутный_указатель(string маршрут)
                {
                    if (base.mesh_texture_filenames != null)
                    {
                        for (int i = 0; i < base.mesh_texture_filenames.Length; i++)
                        {
                            if ((base.mesh_texture_filenames[i] != null) && (base.mesh_texture_filenames[i].Length > 0))
                            {
                                string str = base.mesh_texture_filenames[i];
                                string str2 = "";
                                int startIndex = str.LastIndexOf('.');
                                if (startIndex > 0)
                                {
                                    str2 = str.Substring(startIndex);
                                    str = str.Substring(0, startIndex);
                                }
                                bool flag = false;
                                foreach (string str3 in base.extra_mesh_dirs)
                                {
                                    if (File.Exists(str3 + str + маршрут + str2))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    base.LoadTexture(i, str + маршрут + str2);
                                }
                                else
                                {
                                    base.LoadTexture(i, base.mesh_texture_filenames[i]);
                                }
                            }
                        }
                    }
                }

                public int matrices_count
                {
                    get
                    {
                        return 1;
                    }
                }

                Double_Point Объект_привязки.координаты
                {
                    get
                    {
                        return this.координаты.xz_point;
                    }
                }

                double Объект_привязки.направление
                {
                    get
                    {
                        return this.направление.x;
                    }
                }

                Double_3D_Point Объект_привязки_3D.координаты_3D
                {
                    get
                    {
                        return this.координаты;
                    }
                }

                double Объект_привязки_3D.направление_y
                {
                    get
                    {
                        return this.направление.y;
                    }
                }
            }
        }

        public class Штанга : Mesh_Object, Mesh_Object.IFromFile, IMatrixObject
        {
            private string file = "";
            private Контактный_провод fпровод;
            public double базовое_направление;
            public double длина = 6.066;
            public double направление;
            public double направление_y;
            public Double_3D_Point основание;
            public bool поднимается;
            public double полная_длина = 6.46;
            public bool правая;
            public double скорость_подъёма;
            public double угол;
            public double угол_max = 0.3;
            public double угол_min = -0.351;
            public double угол_normal;

            public Штанга(bool правая, string dir, string filename, double полная_длина, double угол_min)
            {
                this.правая = правая;
                base.mesh_dir = dir;
                this.file = filename;
                this.полная_длина = полная_длина;
                this.угол_min = угол_min;
                this.угол = угол_min;
            }

            public Matrix get_matrix(int index)
            {
                Double_3D_Point point = this.основание;
                Matrix matrix = ((Matrix.RotationZ((float)this.угол) * Matrix.RotationY(-((float)(this.направление - this.базовое_направление)))) * Matrix.RotationZ((float)this.направление_y)) * Matrix.RotationY(-((float)this.базовое_направление));
                return (matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
            }

            public void Найти_провод(Контактный_провод[] контактные_провода)
            {
                double num = 1000.0;
                foreach (Контактный_провод _провод in контактные_провода)
                {
                    if ((_провод.правый == this.правая) && !_провод.обесточенный)
                    {
                        Double_Point point = this.основание.xz_point - _провод.начало;
                        point.угол -= _провод.направление;
                        if (Math.Abs(point.y) <= this.длина)
                        {
                            double num2 = Math.Sqrt((this.длина * this.длина) - (point.y * point.y));
                            if (((point.x + num2) >= 0.0) && ((point.x + num2) < _провод.длина))
                            {
                                Double_Point point2 = _провод.начало + ((Double_Point)(new Double_Point(_провод.направление) * (point.x + num2)));
                                Double_Point point3 = point2 - this.основание.xz_point;
                                point3.угол -= this.базовое_направление;
                                if (Math.Abs(point3.угол) <= 1.5707963267948966)
                                {
                                    Double_Point point6 = point2 - this.координаты;
                                    if (point6.модуль < num)
                                    {
                                        this.провод = _провод;
                                        Double_Point point7 = point2 - this.координаты;
                                        num = point7.модуль;
                                        goto Label_022F;
                                    }
                                }
                            }
                            if (((point.x - num2) >= 0.0) && ((point.x - num2) < _провод.длина))
                            {
                                Double_Point point4 = _провод.начало + ((Double_Point)(new Double_Point(_провод.направление) * (point.x - num2)));
                                Double_Point point5 = point4 - this.основание.xz_point;
                                point5.угол -= this.базовое_направление;
                                if (Math.Abs(point5.угол) <= 1.5707963267948966)
                                {
                                    Double_Point point8 = point4 - this.координаты;
                                    if (point8.модуль < num)
                                    {
                                        this.провод = _провод;
                                        Double_Point point9 = point4 - this.координаты;
                                        num = point9.модуль;
                                    }
                                }
                            }
                        Label_022F: ;
                        }
                    }
                }
            }

            public void Обновить(bool включен_тэд)
            {
                if (this.провод != null)
                {
                    bool flag = this.поднята;
                    Vector3 vector = new Vector3(6.65928f, 2.2f, 0f);
                    double num = this.угол;
                    this.угол = this.угол_normal;
                    vector.TransformCoordinate(this.get_matrix(0));
                    this.угол = num;
                    Double_3D_Point point = new Double_3D_Point((double)vector.X, (double)vector.Y, (double)vector.Z);
                    Double_Point point6 = point.xz_point - this.провод.начало;
                    double num2 = point6.модуль;
                    double num3 = this.провод.найти_высоту(num2) + Контактный_провод.высота_контактной_сети;
                    double num4 = this.полная_длина * Math.Sin(this.угол_normal - this.угол_min);
                    num4 += num3 - point.y;
                    if (num4 < 0.0)
                    {
                        num4 = 0.0;
                    }
                    if (num4 > this.полная_длина)
                    {
                        num4 = this.полная_длина;
                    }
                    this.угол_normal = Math.Asin(num4 / this.полная_длина) + this.угол_min;
                    this.длина = this.полная_длина * Math.Cos(this.угол_normal - this.угол_min);
                    if (flag)
                    {
                        this.угол = this.угол_normal;
                    }
                }
                Double_Point point2 = new Double_Point(this.направление - this.базовое_направление);
                if (point2.угол > 1.5707963267948966)
                {
                    this.направление = this.базовое_направление + 1.5707963267948966;
                }
                else if (point2.угол < -1.5707963267948966)
                {
                    this.направление = this.базовое_направление - 1.5707963267948966;
                }
                if (this.поднимается)
                {
                    if ((this.угол < this.угол_normal) || (this.провод == null))
                    {
                        if (this.угол < this.угол_normal)
                        {
                            this.скорость_подъёма = 0.005;
                        }
                        else
                        {
                            this.скорость_подъёма += (this.угол_max - this.угол) * 0.02;
                            this.скорость_подъёма *= 0.98;
                        }
                        this.угол += this.скорость_подъёма;
                        if ((this.угол > this.угол_normal) && (this.провод != null))
                        {
                            this.угол = this.угол_normal;
                        }
                    }
                    if (this.провод != null)
                    {
                        Double_Point point3 = this.основание.xz_point - this.провод.начало;
                        point3.угол -= this.провод.направление;
                        if (Math.Abs(point3.y) > this.длина)
                        {
                            this.провод = null;
                        }
                        else
                        {
                            double num5 = Math.Sqrt((this.длина * this.длина) - (point3.y * point3.y));
                            Double_Point point4 = this.провод.начало + ((Double_Point)(new Double_Point(this.провод.направление) * (point3.x + num5)));
                            Double_Point point5 = this.провод.начало + ((Double_Point)(new Double_Point(this.провод.направление) * (point3.x - num5)));
                            double num6 = point3.x + num5;
                            Double_Point point7 = point5 - this.координаты;
                            Double_Point point8 = point4 - this.координаты;
                            if (point7.модуль < point8.модуль)
                            {
                                point4 = point5;
                                num6 = point3.x - num5;
                            }
                            point5 = point4 - this.основание.xz_point;
                            point5.угол -= this.базовое_направление;
                            if (Math.Abs(point5.угол) > 1.5707963267948966)
                            {
                                this.провод = null;
                            }
                            else
                            {
                                Double_Point point9 = new Double_Point((point5.угол + this.базовое_направление) - this.направление);
                                double num7 = point9.угол;
                                if (this.угол < this.угол_normal)
                                {
                                    this.направление += (num7 * this.скорость_подъёма) / ((this.угол_normal - this.угол) + this.скорость_подъёма);
                                }
                                else
                                {
                                    this.направление += num7;
                                }
                                if (num6 >= this.провод.длина)
                                {
                                    if (this.провод.следующие_провода.Length > 0)
                                    {
                                        int index = включен_тэд ? 0 : (this.провод.следующие_провода.Length - 1);
                                        this.провод = this.провод.следующие_провода[index];
                                    }
                                    else
                                    {
                                        this.провод = null;
                                    }
                                }
                                else if (num6 < 0.0)
                                {
                                    if (this.провод.предыдущие_провода.Length > 0)
                                    {
                                        int num9 = new Random().Next(this.провод.предыдущие_провода.Length);
                                        this.провод = this.провод.предыдущие_провода[num9];
                                    }
                                    else
                                    {
                                        this.провод = null;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.провод = null;
                    Double_Point point10 = new Double_Point(this.направление - this.базовое_направление);
                    this.направление = this.базовое_направление + point10.угол;
                    this.угол -= 0.005;
                    if (this.угол < this.угол_min)
                    {
                        this.угол = this.угол_min;
                    }
                    if (this.угол == this.угол_min)
                    {
                        this.направление = this.базовое_направление;
                    }
                    else
                    {
                        this.направление += ((this.базовое_направление - this.направление) * 0.005) / ((this.угол + 0.005) - this.угол_min);
                    }
                }
            }

            public string filename
            {
                get
                {
                    return this.file;
                }
            }

            public int matrices_count
            {
                get
                {
                    return 1;
                }
            }

            public Double_Point координаты
            {
                get
                {
                    return (this.основание.xz_point + ((Double_Point)(new Double_Point(this.направление) * this.длина)));
                }
            }

            public bool опущена
            {
                get
                {
                    return ((this.угол == this.угол_min) && (this.направление == this.базовое_направление));
                }
            }

            public bool поднята
            {
                get
                {
                    return ((this.провод != null) && (this.угол == this.угол_normal));
                }
            }

            public Контактный_провод провод
            {
                get
                {
                    return this.fпровод;
                }
                set
                {
                    if (this.fпровод != null)
                    {
                        this.fпровод.объекты.Remove(this);
                    }
                    this.fпровод = value;
                    if (value != null)
                    {
                        value.объекты.Add(this);
                    }
                }
            }

            public double пройденное_расстояние_по_проводу
            {
                get
                {
                    if (this.провод != null)
                    {
                        Double_Point point = this.координаты - this.провод.начало;
                        return point.модуль;
                    }
                    return 0.0;
                }
            }
        }
    }
}

