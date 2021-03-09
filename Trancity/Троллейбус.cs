using Common;
using Engine;
using SlimDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Trancity
{
    public abstract class Троллейбус : Безрельсовый_Транспорт
    {
        private Колесо[] _колёса;
        public double поворотРуля;
        public double возвратРуля;
        public Положение положение;
        private double _радиусКолёс;
        private Road _следующаяДорога;
        public Штанга[] штанги;
        public Руль руль = null;
        public АХ ах;
        //public double скорость;

        private double abs_r;
        private double nr_abs_r;

        private const double stop = 20.0;
        private const double spd = 4.0;
        private const double tg = (16.0 - spd) / (140.0 - stop);

        //TODO: осмотреть и этот ужас переделать
        public override void АвтоматическиУправлять(World мир)
        {
            var ost_dist = 20000.0;
            var nr_lenght = 20000.0;
            var stops_dist = 20000.0;
            var some_other_fucking_distance = 20000.0;
            var signals_dist = 20000.0;
            var uklon = 0.0;
            var nr_uklon = 0.0;
            var shtangi_ost_dist = 20000.0;
            var b1 = false;
            var dont_stop = true;
            var ах = false;

            base.stand_brake = false;
            if (положение.Дорога != null)
            {
                var дорога = положение.Дорога;
                uklon = (дорога.высота[1] - дорога.высота[0]) / дорога.Длина;
                ost_dist = дорога.Длина - положение.расстояние;
                if ((_следующаяДорога == null) && (дорога.следующиеДороги.Length > 0))
                {
                    ОбновитьРейс();
                    if (((рейс != null) && (рейс_index < (рейс.pathes.Length - 1)) && ((рейс_index > 0) || (дорога == рейс.pathes[0]))))//  && (рейс_index < (рейс.pathes.Length - 1)
                    {
                        _следующаяДорога = рейс.pathes[рейс_index + 1];
                    }
                    else
                    {
                        _следующаяДорога = дорога.следующиеДороги[Cheats._random.Next(дорога.следующиеДороги.Length)];
                    }
                }
                base.UpdateTripStops();
                указатель_поворота = 0;
                if (_следующаяДорога != null)
                {
                    nr_uklon = (_следующаяДорога.высота[1] - _следующаяДорога.высота[0]) / _следующаяДорога.Длина;
                    if ((дорога.следующиеДороги.Length > 1) && (ost_dist <= 40.0) && (_следующаяДорога.кривая))
                    {
                        указатель_поворота = (_следующаяДорога.СтепеньПоворота0 > 0.0) ? 1 : -1;
                    }
                }
                else
                {
                    signals_dist = ost_dist - 5.0;
                }
                abs_r = 0.0;
                nr_abs_r = 0.0;
                if (дорога.кривая && (дорога.АбсолютныйРадиус <= 80.0))
                {
                    // HACK: num = 10.0;
                    abs_r = дорога.АбсолютныйРадиус;
                }
                if ((_следующаяДорога != null) && (_следующаяДорога.кривая) && (_следующаяДорога.АбсолютныйРадиус <= 80.0))
                {
                    nr_abs_r = _следующаяДорога.АбсолютныйРадиус;
                }
                if ((_следующаяДорога != null) && (nr_abs_r == 0.0))
                {
                    nr_lenght = _следующаяДорога.Длина;
                    ost_dist += nr_lenght;
                    nr_abs_r = 0;
                }
                // TODO: why div 5.0? <-- полосы движения вспомнить!
                int width0m5 = Math.Max((int)Math.Round(дорога.ширина[0] / Road.ширинаПолосы), 1);
                int width1m5 = Math.Max((int)Math.Round(дорога.ширина[1] / Road.ширинаПолосы), 1);
                double current_width = дорога.НайтиШирину(положение.расстояние);
                int shiftLine0 = Math.Max((int)Math.Floor((width0m5 * (положение.отклонение + (current_width / 2.0))) / current_width), 0);
                int shiftLine1 = Math.Max((int)Math.Floor((width1m5 * (положение.отклонение + (current_width / 2.0))) / current_width), 0);
                if (shiftLine0 >= width0m5)
                {
                    shiftLine0 = width0m5 - 1;
                }
                if (shiftLine1 >= width1m5)
                {
                    shiftLine1 = width1m5 - 1;
                }
                var dist_by_lines = new double[width1m5];
                for (var i = 0; i < dist_by_lines.Length; i++)
                {
                    dist_by_lines[i] = 2000.0;
                }
                //TODO: Ищем остановку и всё остальное
                foreach (var obj2 in дорога.objects)
                {
                    if (obj2 is Stop)
                    {
                        var остановка = (Stop)obj2;
                        if ((((остановка.distance - положение.расстояние) <= 50.0) || //10.0
                             ((рейс != null) && !остановка.ПутьПодходит(рейс.pathes))) ||
                             ((маршрут == null) || (!остановка.typeOfTransport[маршрут.typeOfTransport])))// || (остановка!=nextStop))
                            continue;
                        base.SearchForCurrentStop(остановка);
                        if (остановка != nextStop) continue;
                        stops_dist = Math.Min(stops_dist, остановка.distance - положение.расстояние);
                        базоваяОстановка = остановка;
                        currentStop = остановка;
                        continue;
                    }
                    else if (obj2 is Visual_Signal/*Сигнальная_система.Сигнал*/)
                    {
                        var сигнал = (Visual_Signal/*Сигнальная_система.Сигнал*/)obj2;
                        if ((сигнал.система.сигнал == Сигналы.Красный) && ((сигнал.положение.расстояние - положение.расстояние) > 10.0))
                        {
                            signals_dist = Math.Min(signals_dist, (сигнал.положение.расстояние - положение.расстояние) - 10.0);
                        }
                        continue;
                    }
                    if (!(obj2 is Светофорный_сигнал)) continue;
                    var сигнал2 = (Светофорный_сигнал)obj2;
                    var num15 = сигнал2.расстояние - положение.расстояние;
                    if (((сигнал2.сигнал == Сигналы.Красный) && (num15 > 0.0)) || ((сигнал2.сигнал == Сигналы.Жёлтый) && (num15 > 10.0)))
                    {
                        signals_dist = Math.Min(signals_dist, num15 - 5.0);
                    }
                }
                if (_следующаяДорога != null)
                {
                    foreach (var obj3 in _следующаяДорога.objects)
                    {
                        if (obj3 is Stop)
                        {// continue;
                            var остановка2 = (Stop)obj3;
                            if (((остановка2 == nextStop) && (остановка2.typeOfTransport[маршрут.typeOfTransport])) && (((рейс == null) || остановка2.ПутьПодходит(рейс.pathes)) && (маршрут != null)))
                            {
                                stops_dist = Math.Min(stops_dist, (дорога.Длина - положение.расстояние) + остановка2.distance);
                            }
                            continue;
                        }
                        else if (obj3 is Visual_Signal/*Сигнальная_система.Сигнал*/)
                        {
                            var сигнал2 = (Visual_Signal/*Сигнальная_система.Сигнал*/)obj3;
                            if (сигнал2.система.сигнал == Сигналы.Красный)
                            {
                                signals_dist = Math.Min(signals_dist, ((дорога.Длина - положение.расстояние) + сигнал2.положение.расстояние) - 30.0);
                            }
                            continue;
                        }
                        if (!(obj3 is Светофорный_сигнал)) continue;
                        var сигнал21 = (Светофорный_сигнал)obj3;
                        var num16 = (дорога.Длина - положение.расстояние) + сигнал21.расстояние;
                        if ((сигнал21.сигнал == Сигналы.Красный) || ((сигнал21.сигнал == Сигналы.Жёлтый) && (num16 > 10.0)))//10
                        {
                            signals_dist = Math.Min(signals_dist, num16 - 5.0);
                        }
                    }
                }
                /*var list = new List<Положение>(дорога.занятыеПоложения);
                var list2 = new List<Положение>();
                if (_следующаяДорога != null)
                {
                    list2 = new List<Положение>(_следующаяДорога.занятыеПоложения);
                }*/
                foreach (var положение1 in дорога.занятыеПоложения)//list)
                {
                    if ((положение1.comment == this) || (положение1.расстояние <= положение.расстояние)) continue;
                    var new_width = дорога.НайтиШирину(положение1.расстояние);
                    var shiftLineTmp = (int)Math.Floor((width1m5 * (положение1.отклонение + (new_width / 2.0))) / new_width);
                    if (shiftLineTmp == shiftLine1)
                    {
                        some_other_fucking_distance = Math.Min(some_other_fucking_distance, положение1.расстояние - положение.расстояние);
                    }
                    if ((shiftLineTmp >= 0) && (shiftLineTmp < dist_by_lines.Length))
                    {
                        dist_by_lines[shiftLineTmp] = Math.Min(dist_by_lines[shiftLineTmp], положение1.расстояние - положение.расстояние);
                    }
                }
                if (_следующаяДорога != null)
                {
                    foreach (var положение2 in _следующаяДорога.занятыеПоложения)//list2)
                    {
                        if (положение2.comment == this) continue;
                        if (_следующаяДорога == null) continue;
                        var num13 = _следующаяДорога.НайтиШирину(положение2.расстояние);
                        var shiftLineTmp = (int)Math.Floor((width1m5 * (положение2.отклонение + (num13 / 2.0))) / num13);
                        if (shiftLineTmp == shiftLine1)
                        {
                            some_other_fucking_distance = Math.Min(some_other_fucking_distance, (дорога.Длина - положение.расстояние) + положение2.расстояние);
                        }
                        if ((shiftLineTmp >= 0) && (shiftLineTmp < dist_by_lines.Length))
                        {
                            dist_by_lines[shiftLineTmp] = Math.Min(dist_by_lines[shiftLineTmp], (дорога.Длина - положение.расстояние) + положение2.расстояние);
                        }
                    }
                }
                if (some_other_fucking_distance < 100.0)
                {
                    var max_dist = some_other_fucking_distance;
                    for (var j = 0; j < dist_by_lines.Length; j++)
                    {
                        if (dist_by_lines[j] <= max_dist) continue;
                        max_dist = dist_by_lines[j];
                        shiftLine1 = j;
                        if (width0m5 == width1m5)
                        {
                            shiftLine0 = j;
                        }
                    }
                }
                var newShift0 = ((дорога.ширина[0] * (shiftLine0 + 0.5)) / width0m5) - (дорога.ширина[0] / 2.0);
                var newShift1 = ((дорога.ширина[1] * (shiftLine1 + 0.5)) / width1m5) - (дорога.ширина[1] / 2.0);
                var rec_shift = newShift0 + (((newShift1 - newShift0) * положение.расстояние) / дорога.Длина);
                var shiftAcc = 0.3;
                if ((stops_dist < 40.0) || (осталось_стоять > 0.0))
                {
                    rec_shift = 0.5 - (current_width / 2.0);
                    if (положение.отклонение < (2.0 - (current_width / 2.0)))
                    {
                        rec_shift = 2.0 - (current_width / 2.0);
                        shiftAcc = 0.2;
                    }
                    указатель_поворота = 1;
                    signals_dist = Math.Min(signals_dist, stops_dist + 20.0);
                }
                var point2 = new DoublePoint(дорога.НайтиНаправление(положение.расстояние) - direction);
                var target_direction = -point2.Angle;
                if (Math.Abs(положение.отклонение - rec_shift) > shiftAcc)
                {
                    target_direction += 0.08 * (положение.отклонение - rec_shift);
                }
                if (положение.отклонение < ((-current_width / 2.0) + 1.0))
                {
                    target_direction -= 0.3;
                }
                else if (положение.отклонение > ((current_width / 2.0) - 1.0))
                {
                    target_direction += 0.3;
                }
                if (Math.Abs(target_direction) < 0.001)
                {
                    target_direction = 0.0;
                }
                if (MainForm.in_editor)
                {
                    поворотРуля = target_direction;
                    возвратРуля = target_direction;
                }
                else
                {
                    if (поворотРуля < target_direction)
                    {
                        поворотРуля += 0.3 * World.прошлоВремени;
                        if (поворотРуля > target_direction)
                        {
                            поворотРуля = target_direction;
                        }
                    }
                    else if (поворотРуля > target_direction)
                    {
                        поворотРуля -= 0.3 * World.прошлоВремени;
                        if (поворотРуля < target_direction)
                        {
                            поворотРуля = target_direction;
                        }
                    }
                    /* if (возвратРуля < target_direction)
                     {
                         возвратРуля += 0.3 * World.прошлоВремени;
                         if (возвратРуля > target_direction)
                         {
                             возвратРуля = target_direction;
                         }
                     }
                     else if (возвратРуля > target_direction)
                     {
                         возвратРуля -= 0.3 * World.прошлоВремени;
                         if (возвратРуля < target_direction)
                         {
                             возвратРуля = target_direction;
                         }
                     }*/
                }

            }
            var flag = false;
            /*var flag2 = false;
            if ((рейс != null) && (рейс.inPark || ((рейс.дорога_отправления == парк.выезд) && (мир.time < рейс.время_отправления))))
            {
                flag2 = true;
            }*/
            if ((рейс != null) && (рейс.inPark || ((рейс.дорога_отправления == парк.выезд) && (мир.time < рейс.время_отправления))))//(flag2)
            {
                foreach (var дорога in парк.пути_стоянки)
                {
                    if (дорога != положение.Дорога) continue;
                    flag = (скорость == 0.0);
                    var num24 = (дорога.Длина - положение.расстояние) - 20.0;
                    signals_dist = Math.Min(signals_dist, num24);
                    //                    ost_dist = Math.Min(ost_dist, num24);
                    break;
                }
            }
            var flag3 = true;
            var index = -1;
            //TODO:а тут ужас... надо заставить АХ использовать, всегда, даже если нет маршрута (заставил, пока штанги перед участком без КС не опускает)
            foreach (var штанга in штанги)
            {
                if (flag && (ускорение <= 0.0))
                {
                    штанга.поднимается = false;
                    штанга.Провод = null;
                    flag3 = false;
                }
                else if ((штанга.Провод != null) && (!штанга.Провод.обесточенный || (скорость != 0.0)))
                {
                    if (штанга.Поднята)
                    {
                        shtangi_ost_dist = штанга.Провод.длина - штанга.ПройденноеРасстояниеПоПроводу;
                        if (штанга.Провод.следующие_провода.Length == 1)
                        {
                            shtangi_ost_dist += штанга.Провод.следующие_провода[0].длина;
                        }
                        if (ost_dist > shtangi_ost_dist)
                        {
                            ost_dist = shtangi_ost_dist;
                            dont_stop = false;
                        }
                        if ((штанга.Провод.следующие_провода.Length > 1) && (штанга.ПройденноеРасстояниеПоПроводу > (штанга.Провод.длина - 2.0)))
                        {
                            var next_wires = new List<Контактный_провод>(штанга.Провод.следующие_провода);
                            if ((положение.Дорога != null) && (_следующаяДорога != null))
                            {
                                Road дорога2 = null;
                                if (_следующаяДорога.следующиеДороги.Length == 1)
                                {
                                    дорога2 = _следующаяДорога.следующиеДороги[0];
                                }
                                var list4 = new List<Контактный_провод>(next_wires);
                                var list5 = new List<double>();
                                for (var k = 0; k < list4.Count; k++)
                                {
                                    list5.Add(0.0);
                                }
                                for (var m = 0; (m < 50) && (list4.Count >= 2); m++)
                                {
                                    for (var n = 0; n < list4.Count; n++)
                                    {
                                        List<double> list6;
                                        int num33;
                                        var flag4 = true;
                                        (list6 = list5)[num33 = n] = list6[num33] + 10.0;
                                        while (list5[n] > list4[n].длина)
                                        {
                                            (list6 = list5)[num33 = n] = list6[num33] - list4[n].длина;
                                            if (list4[n].следующие_провода.Length > 0)
                                            {
                                                for (var num30 = 1; num30 < list4[n].следующие_провода.Length; num30++)
                                                {
                                                    next_wires.Add(next_wires[n]);
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
                                            var pos = list4[n].FindCoords(list5[n], 0.0);
                                            if (((мир.Найти_положение(pos, положение.Дорога).Дорога == null)
                                                 && (мир.Найти_положение(pos, _следующаяДорога).Дорога == null))
                                                 && ((дорога2 == null) || (мир.Найти_положение(pos, дорога2).Дорога == null)))
                                            {
                                                flag4 = false;
                                            }
                                        }
                                        if (flag4) continue;
                                        next_wires.RemoveAt(n);
                                        list4.RemoveAt(n);
                                        list5.RemoveAt(n);
                                        n--;
                                    }
                                }
                            }
                            if (next_wires.Count == 0)
                            {
                                next_wires.AddRange(штанга.Провод.следующие_провода);
                            }
                            var провод = next_wires[Cheats._random.Next(next_wires.Count)];
                            index = провод == штанга.Провод.следующие_провода[0] ? 0 : 1;
                        }
                        foreach (var obj6 in штанга.Провод.objects)
                        {
                            if (!(obj6 is Штанга)) continue;
                            var штанга2 = (Штанга)obj6;
                            if (штанга2.ПройденноеРасстояниеПоПроводу > штанга.ПройденноеРасстояниеПоПроводу)
                            {
                                some_other_fucking_distance = Math.Min(some_other_fucking_distance, штанга2.ПройденноеРасстояниеПоПроводу - штанга.ПройденноеРасстояниеПоПроводу);
                            }
                        } //TODO:
                        Контактный_провод провод2 = null;
                        if (штанга.Провод.следующие_провода.Length == 1)
                        {
                            провод2 = штанга.Провод.следующие_провода[0];
                        }
                        else if ((index >= 0) && (index <= штанга.Провод.следующие_провода.Length - 1))
                        {
                            провод2 = штанга.Провод.следующие_провода[index];
                        }
                        if (провод2 != null)
                        {
                            foreach (var obj7 in провод2.objects)
                            {
                                if (!(obj7 is Штанга)) continue;
                                var штанга3 = (Штанга)obj7;
                                some_other_fucking_distance = Math.Min(some_other_fucking_distance, (штанга.Провод.длина - штанга.ПройденноеРасстояниеПоПроводу) + штанга3.ПройденноеРасстояниеПоПроводу);
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
                    var провод3 = штанга.Провод;
                    штанга.НайтиПровод(мир.контактныеПровода);
                    if (штанга.Провод != null)
                    {
                        if (штанга.Опущена)
                        {
                            штанга.поднимается = true;
                        }
                        else
                        {
                            штанга.поднимается = false;
                            штанга.скоростьПодъёма = 0;
                            штанга.Провод = провод3;
                            ах = true;
                        }
                        flag3 = false;
                    }
                }
            }
            if (((штанги.Length == 0) && flag) && (ускорение <= 0.0))
            {
                flag3 = false;
            }
            включен = flag3;
            base.stand_brake = flag;
            var на_рейсе = (рейс == null) || (мир.time >= рейс.время_отправления);
            if (!на_рейсе && (положение.Дорога != null))
            {
                signals_dist = Math.Min(signals_dist, (положение.Дорога.Длина - положение.расстояние) - 15.0);
            }
            if (ost_dist > stops_dist)
            {
                ost_dist = stops_dist;
                dont_stop = false;
            }
            if (ost_dist > some_other_fucking_distance)
            {
                ost_dist = some_other_fucking_distance;
                dont_stop = false;
            }
            if (ost_dist > signals_dist)
            {
                ost_dist = signals_dist;
                dont_stop = false;
            }
            var recomend_speed = 16.0;
            ОткрытьДвери(false);
            if ((осталось_стоять > 0.0) && (stops_dist > 20.0))
            {
                ost_dist = 0.0;
                recomend_speed = 0.0;
                if (скорость_abs < 0.1)
                {
                    base.stand_brake = true;
                    if (стоим_с_закрытыми_дверями)
                    {
                        if (!flag && на_рейсе)
                        {
                            осталось_стоять -= World.прошлоВремени;
                            if (осталось_стоять <= 0.0)
                            {
                                стоим_с_закрытыми_дверями = false;
                            }
                        }
                    }
                    else
                    {
                        ОткрытьДвери(true);
                        if (двери_открыты && на_рейсе)
                        {
                            осталось_стоять -= World.прошлоВремени;
                        }
                        //TO_DO: Поиск следующей остановки
                        if (nextStop == currentStop)
                        {
                            stopIndex++;
                            nextStop = null;
                            currentStop = null;
                        }
                    }
                }
            }
            else
            {
                var nr_rec_speed = dont_stop ? (1.3 * nr_abs_r / 4.905) : 0;
                var s1 = 0.0;
                var dist = 0.0;
                var v3 = tg * nr_lenght;

                //                if (((nr_abs_r == 0) && ((ost_dist < nr_lenght / 2) && (ost_dist < 10.0))) && (ost_dist != some_other_fucking_distance))
                //                {
                //                    ost_dist += nr_lenght;
                //                    recomend_speed = 1.3 * ost_dist / 4.905;
                //                }

                if (abs_r > 0.0)
                {
                    recomend_speed = 1.3 * abs_r / 4.905;
                }

                /*if ((nr_abs_r == 0) && ((ost_dist < nr_lenght / 2) && (ost_dist < 10.0)))
                {
                        ost_dist += nr_lenght;
                        recomend_speed = 1.3 * ost_dist / 4.905;
                }*/

                if (uklon < 0)// && recomend_speed > 6.0)
                {
                    recomend_speed = Math.Min(recomend_speed, -0.2 / uklon);//6.0;
                }
                if (nr_uklon < 0.0)// && nr_rec_speed > 6.0)
                {
                    nr_rec_speed = Math.Min(nr_rec_speed, -0.25 / nr_uklon);//6.0;
                }

                s1 = (скорость_abs - nr_rec_speed) / tg;
                if (!b1 && ost_dist < s1)
                {
                    dist = s1;
                }

                if (ost_dist < dist)
                {
                    if (!dont_stop)
                    {
                        if (ost_dist > 40)
                        {
                            recomend_speed = tg * (ost_dist - stop) / 2 + spd;
                        }
                        else if (abs_r > 0.0)
                        {
                            recomend_speed = 1.3 * abs_r / 4.905;
                        }
                        else
                        {
                            recomend_speed = spd;
                        }
                    }
                    else
                    {
                        if ((nr_rec_speed > 0) && (nr_rec_speed < recomend_speed))
                        {
                            if (ost_dist > 30)
                            {
                                recomend_speed = tg * ost_dist / 2 + nr_rec_speed;
                            }
                            else
                            {
                                recomend_speed = nr_rec_speed;
                                if (nr_lenght < 30.0) recomend_speed *= 0.8;
                            }
                        }
                        else if (nr_rec_speed == 0)
                        {
                            if ((nr_abs_r == 0) && (abs_r == 0))
                            {
                                ost_dist += nr_lenght / 2;
                                recomend_speed = tg * ost_dist * 2;// + v2;
                            }
                            else
                            {
                                recomend_speed = tg * ost_dist;
                            }
                            if (nr_uklon > 0.0)
                            {
                                recomend_speed = Math.Min(nr_uklon * 100.0, recomend_speed);//v3 > recomend_speed ? recomend_speed : v3;
                            }
                        }
                    }
                }
                if ((uklon < 0.0) && (скорость_abs - recomend_speed > 1.5))
                {
                    recomend_speed = 0.0;
                }
                //                if (recomend_speed > 16.0) recomend_speed = 16.0;
                if (stops_dist <= 20)
                {
                    осталось_стоять = 8.0 + (Cheats._random.NextDouble() * 5.0);
                    if ((базоваяОстановка != null) && базоваяОстановка.serviceStop)
                    {
                        осталось_стоять = 1.0 + (Cheats._random.NextDouble() * 3.0);
                        стоим_с_закрытыми_дверями = true;
                    }
                }
                /*if (signals_dist < 10.0)
                {
                    recomend_speed = 0.0;
                }
                if (some_other_fucking_distance < 10.0)
                {
                    recomend_speed = 0.0;
                }*/
            }
            if ((!двери_закрыты) || (signals_dist < 10.0) || (some_other_fucking_distance < 10.0))
            {
                recomend_speed = 0.0;
            }
            система_управления.автоматически_управлять(recomend_speed, ost_dist, index);
        }

        public bool обесточен
        {
            get
            {
                if ((!штанги_подняты) || (штанги_обесточены))
                {
                    if (ах == null)
                    {
                        return ((!base.включен) || (ах == null) || (!ах.включён));//true;
                    }
                    if (ах.есть)
                    {
                        return ((!штанги_подняты) && (ах.включён) && (base.включен));
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
                    if (!штанга.Поднята)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool штанги_обесточены
        {
            get
            {
                if (!штанги_подняты) return true;
                foreach (Штанга штанга in this.штанги)
                {
                    if (штанга.Провод.обесточенный)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public class АХ
        {
            private const double r = 0.20;
            private const double z = 0.17;
            public bool включён = false;
            public bool есть = false;
            public double полная_ёмкость;
            public double текущая_ёмкость;
            public double ускорение;
            public double расход;
            public Троллейбус троллейбус;

            public АХ(Троллейбус троллейбус, double полная_ёмкость, double ускорение, double расход)
            {
                this.троллейбус = троллейбус;
                this.ускорение = ускорение;
                this.расход = расход;
                this.полная_ёмкость = полная_ёмкость;
                this.текущая_ёмкость = полная_ёмкость;

                if (this.ускорение > 0.0)
                {
                    this.ускорение = Math.Min(1.0, this.ускорение);
                }
                else this.ускорение = Cheats._random.NextDouble();
            }

            public bool заряжается
            {
                get
                {
                    return ((((!включён) && (троллейбус.включен)) &&
                         ((троллейбус.система_управления is Система_управления.РКСУ_Троллейбус) &&
                         (троллейбус.штанги_подняты && !троллейбус.штанги_обесточены))) &&
                         (текущая_ёмкость < полная_ёмкость));
                }
            }

            public void Simulation()
            {
                if ((включён) && (троллейбус.штанги_подняты || !троллейбус.штанги_обесточены)) включён = false;
                if ((включён) && (полная_ёмкость > 0.0))
                {
                    текущая_ёмкость -= расход * Math.Abs(троллейбус.система_управления.ускорение) * World.прошлоВремени;
                    if (текущая_ёмкость <= 0.0)
                    {
                        текущая_ёмкость = 0.0;
                        включён = false;

                    }
                }
                else if (заряжается)
                {
                    текущая_ёмкость += z * World.прошлоВремени;
                    текущая_ёмкость = Math.Min(текущая_ёмкость, полная_ёмкость);//if (текущая_ёмкость >= e) текущая_ёмкость = e;
                }
            }

        }

        public class Колесо : MeshObject, MeshObject.IFromFile, IMatrixObject
        {
            private string _file = "";
            public DoublePoint базовоеНаправление;
            public Double3DPoint координаты;
            public bool левое;
            public double поворот;
            public double пройденноеРасстояние;
            public double радиус = 0.628;
            public Положение текущееПоложение;

            public Колесо(bool левое, string dir, string filename, double радиус)
            {
                this.левое = левое;
                meshDir = dir;
                _file = filename;
                this.радиус = радиус;
            }

            public Matrix GetMatrix(int index)
            {
                Matrix matrix;
                if (!левое)
                {
                    matrix = ((Matrix.RotationZ(-((float)(пройденноеРасстояние / радиус))) * Matrix.RotationY(-((float)поворот))) * Matrix.RotationZ((float)базовоеНаправление.y)) * Matrix.RotationY(-((float)базовоеНаправление.x));
                }
                else
                {
                    matrix = ((Matrix.RotationZ((float)(пройденноеРасстояние / радиус)) * Matrix.RotationY(-((float)поворот))) * Matrix.RotationZ((float)базовоеНаправление.y)) * Matrix.RotationY(-((float)(базовоеНаправление.x + Math.PI)));
                }
                return (matrix * Matrix.Translation((float)координаты.x, (float)координаты.y, (float)координаты.z));
            }

            public string Filename
            {
                get
                {
                    return this._file;
                }
            }

            public int MatricesCount
            {
                get
                {
                    return 1;
                }
            }

            public DoublePoint Направление
            {
                get
                {
                    return new DoublePoint(базовоеНаправление.x + поворот, базовоеНаправление.y);
                }
            }
        }

        public class ОбычныйТроллейбус : Троллейбус, IОбъектПривязки3D, IVector
        {
            public Дополнение[] дополнения = new Дополнение[0];
            public Кузов кузов;
            public Сочленение[] сочленения = new Сочленение[0];
            public Хвост[] хвосты = new Хвост[0];

            public ОбычныйТроллейбус(МодельТранспорта модель, Double3DPoint координаты, DoublePoint направление, Управление управление, Парк парк, Route маршрут, Order наряд)
            {
                this.модель = модель;
                основная_папка = модель.dir;
                кузов = new Кузов(this) { координаты = координаты, направление = направление };
                base.управление = управление;
                base.маршрут = маршрут;
                this.наряд = наряд;
                base.парк = парк;
                хвосты = new Хвост[(int)((модель.хвостDist1.Length + модель.хвостDist2.Length) / 2)];//[модель.количествоХвостов];
                сочленения = new Сочленение[хвосты.Length];//модель.количествоХвостов];
                var num = 0.0;
                for (var i = 0; i < хвосты.Length; i++)
                {
                    num -= модель.хвостDist1[i] + модель.хвостDist2[i];
                    хвосты[i] = new Хвост(this)
                    {
                        координаты =
                                            кузов.координаты +
                                            new Double3DPoint(кузов.направление) * num,//кузов.направление
                        направление = кузов.направление
                    };
                    сочленения[i] = new Сочленение(this);
                }
                for (int i = 0; i < модель.занятыеПоложения.Length; i++)
                {
                    width = Math.Max(width, Math.Abs(модель.занятыеПоложения[i].y));
                    length0 = Math.Max(length0, Math.Abs(модель.занятыеПоложения[i].x));
                    length1 = Math.Min(length1, модель.занятыеПоложения[i].x);
                }
                length1 = -length1;
                if (модель.занятыеПоложенияХвостов.Length > 0)
                {
                    length1 = 0.0;
                    for (int i = 0; i < модель.занятыеПоложенияХвостов[модель.занятыеПоложенияХвостов.Length - 1].Length; i++)
                    {
                        length1 = Math.Max(length1, Math.Abs(модель.занятыеПоложенияХвостов[модель.занятыеПоложенияХвостов.Length - 1][i].x));
                    }
                    length1 -= num;
                }
                дополнения = new Дополнение[модель.дополнения.Length];
                for (int j = 0; j < модель.дополнения.Length; j++)
                {
                    this.дополнения[j] = new Дополнение(this.Найти_часть(модель.дополнения[j].часть), модель.дополнения[j].filename, модель.дополнения[j].тип);
                }
                _радиусКолёс = модель.радиусКолёс;
                _колёса = new Колесо[2 * модель.колёсныеПары.Length];
                for (var k = 0; k < модель.колёсныеПары.Length; k++)
                {
                    _колёса[2 * k] = new Колесо(true, модель.колёсныеПары[k].dir, модель.колёсныеПары[k].filename, base._радиусКолёс);
                    _колёса[(2 * k) + 1] = new Колесо(false, модель.колёсныеПары[k].dir, модель.колёсныеПары[k].filename, base._радиусКолёс);
                }
                штанги = new Штанга[модель.штанги.Length];
                for (var m = 0; m < штанги.Length; m++)
                {
                    штанги[m] = new Штанга(m == 0, модель.штангиDir, модель.штангиFilename, модель.штангиПолнаяДлина, модель.штангиУголMin);
                }
                if (модель.руль != null)
                {
                    руль = new Руль(
                        модель.руль.dir,
                        модель.руль.filename,
                        модель.руль.pos,
                        модель.руль.angle,
                        this.кузов);
                }
                if (модель.ах != null)
                {
                    ах = new АХ(this, модель.ах.полная_ёмкость, модель.ах.ускорение, модель.ах.расход);
                }
                if (модель.табличка != null)
                {
                    табличка_в_парк = new ТабличкаВПарк(this);
                }
                ОбновитьКолёсаШтангиРуль();
                foreach (var штанга in штанги)
                {
                    штанга.направление = штанга.базовоеНаправление;
                }
                _количествоДверей = модель.количествоДверей;
                _двери = new Двери[модель.двери.Length];
                for (var n = 0; n < модель.двери.Length; n++)
                {
                    _двери[n] = Двери.Построить(модель.двери[n].модель, Найти_часть(модель.двери[n].часть), модель.двери[n].p1, модель.двери[n].p2, модель.двери[n].правые);
                    _двери[n].дверьВодителя = модель.двери[n].дверьВодителя;
                    _двери[n].номер = модель.двери[n].номер;
                }
                указатель_наряда = new УказательНаряда();
                система_управления = Система_управления.Parse(модель.системаУправления, this);
                if (!модель.hasnt_bbox)
                {
                    кузов.bounding_sphere = new Sphere(модель.bsphere.pos, модель.bsphere.radius);
                    for (var i = 0; i < хвосты.Length; i++)
                    {
                        хвосты[i].bounding_sphere = new Sphere(модель.tails_bsphere[i].pos, модель.tails_bsphere[i].radius);
                    }
                }
                else
                {
                    кузов.bounding_sphere = new Sphere(Double3DPoint.Zero, 8.0);
                    for (var i = 0; i < хвосты.Length; i++)
                    {
                        хвосты[i].bounding_sphere = new Sphere(Double3DPoint.Zero, 8.0);
                    }
                }
                LoadCameras();
            }

            public override void CreateMesh(World мир)
            {
                if (мир.filename != null)
                {
                    var strArray = new[]
                                       {
                                           Application.StartupPath + @"\Cities\" +
                                           Path.GetFileNameWithoutExtension(мир.filename) + @"\" + парк.название + @"\",
                                           Application.StartupPath + @"\Cities\" +
                                           Path.GetFileNameWithoutExtension(мир.filename) + @"\"
                                       };
                    кузов.extraMeshDirs = strArray;
                    foreach (var хвост in хвосты)
                    {
                        хвост.extraMeshDirs = strArray;
                    }
                    foreach (var сочленение in сочленения)
                    {
                        сочленение.extraMeshDirs = strArray;
                    }
                    foreach (var колесо in _колёса)
                    {
                        колесо.extraMeshDirs = strArray;
                    }
                    foreach (var штанга in штанги)
                    {
                        штанга.extraMeshDirs = strArray;
                    }
                    foreach (var двери in _двери)
                    {
                        двери.ExtraMeshDirs = strArray;
                    }
                    указатель_наряда.extraMeshDirs = strArray;
                    //                    if (табличка != null)
                    //                      {
                    //                        табличка.extraMeshDirs = strArray;
                    //                    }
                }
                кузов.CreateMesh();
                кузов.ОбновитьМаршрутныйУказатель(маршрут.number, наряд.номер);
                foreach (var хвост2 in хвосты)
                {
                    хвост2.CreateMesh();
                    хвост2.ОбновитьМаршрутныйУказатель(маршрут.number, наряд.номер);
                }
                foreach (var сочленение2 in сочленения)
                {
                    сочленение2.CreateMesh();
                }
                foreach (var дополнение in дополнения)
                {
                    дополнение.CreateMesh();
                }
                foreach (var колесо2 in _колёса)
                {
                    колесо2.CreateMesh();
                }
                foreach (var штанга2 in штанги)
                {
                    штанга2.CreateMesh();
                }
                foreach (var двери2 in _двери)
                {
                    двери2.CreateMesh();
                }
                указатель_наряда.CreateMesh();
                if (наряд != null)
                {
                    указатель_наряда.ОбновитьКартинку(base.наряд);
                }
                if (руль != null)
                {
                    руль.CreateMesh();
                }
                if (табличка_в_парк != null)
                {
                    табличка_в_парк.CreateMesh();
                }
            }

            protected override void CheckCondition()
            {
                var cnd = !base.condition;
                кузов.IsNear = cnd;
                foreach (var хвост in хвосты)
                {
                    хвост.IsNear = cnd;
                }
                if (!cnd) return;
                foreach (var сочленение in сочленения)
                {
                    сочленение.IsNear = true;
                }
                foreach (var колесо in _колёса)
                {
                    колесо.IsNear = true;
                }
                foreach (var штанга in штанги)
                {
                    штанга.IsNear = true;
                }
                if (руль != null)
                {
                    руль.IsNear = true;
                }
                foreach (var двери in _двери)
                {
                    двери.CheckCondition();
                }
                foreach (var дополнение in дополнения)
                {
                    дополнение.IsNear = true;
                }
                if (наряд != null)
                {
                    указатель_наряда.IsNear = true;
                }
                if (табличка_в_парк != null)
                {
                    табличка_в_парк.IsNear = true;
                }
            }

            public override void Render()
            {
                CheckCondition();
                if (condition) return;
                var visible = false;
                var lod = 2;
                if (MyDirect3D.SphereInFrustum(кузов.bounding_sphere))
                {
                    visible = true;
                    lod = Math.Min(кузов.bounding_sphere.LODnum, lod);
                    кузов.Render();
                }
                foreach (var хвост in хвосты)
                {
                    //                       if (!(MyDirect3D.AABBInFrustum(хвост.bounding_box))) continue;
                    if (!(MyDirect3D.SphereInFrustum(хвост.bounding_sphere))) continue;
                    visible = true;
                    lod = Math.Min(хвост.bounding_sphere.LODnum, lod);
                    хвост.Render();
                }
                if ((!visible) || (lod > 0)) return;
                foreach (var сочленение in сочленения)
                {
                    сочленение.Render();
                }
                foreach (var колесо in _колёса)
                {
                    колесо.Render();
                }
                foreach (var штанга in штанги)
                {
                    штанга.Render();
                }
                if (руль != null)
                {
                    руль.Render();
                }
                foreach (var двери in _двери)
                {
                    двери.Render();
                }
                foreach (var дополнение in дополнения)
                {
                    if ((дополнение.тип == Тип_дополнения.фары) && включены_фары)
                    {
                        дополнение.Render();
                    }
                    if (времяПоворотников < времяПоворотниковВыкл)
                    {
                        if ((дополнение.тип == Тип_дополнения.влево) && ((указатель_поворота < 0) || аварийная_сигнализация))
                        {
                            дополнение.Render();
                        }
                        if ((дополнение.тип == Тип_дополнения.вправо) && ((указатель_поворота > 0) || аварийная_сигнализация))
                        {
                            дополнение.Render();
                        }
                    }
                    if ((дополнение.тип == Тип_дополнения.тормоз) && (система_управления.ход_или_тормоз < 0))
                    {
                        дополнение.Render();
                    }
                    if ((дополнение.тип == Тип_дополнения.назад) && (base.система_управления.направление < 0))
                    {
                        дополнение.Render();
                    }
                }
                if (наряд != null)
                {
                    указатель_наряда.matrix = Matrix.Translation((float)модель.нарядPos.x, (float)модель.нарядPos.y, (float)модель.нарядPos.z) * кузов.last_matrix;//.GetMatrix(0);//(float)модель.нарядPos.x, (float)модель.нарядPos.y, (float)модель.нарядPos.z  кузов.GetMatrix(0)
                    указатель_наряда.Render();
                }
                if (табличка_в_парк != null)
                {
                    табличка_в_парк.matrix = Matrix.Translation((float)модель.табличка.pos.x, (float)модель.табличка.pos.y, (float)модель.табличка.pos.z) * кузов.last_matrix;//.GetMatrix(0); //((float)модель.табличка.pos.x, (float)модель.табличка.pos.y, (float)модель.табличка.pos.z) * вагон.GetMatrix(0);
                    табличка_в_парк.Render();
                }
            }

            public override void UpdateBoundigBoxes(World world)
            {
                кузов.bounding_sphere.Update(кузов.координаты, кузов.направление);
                foreach (var хвост in хвосты)
                {
                    хвост.bounding_sphere.Update(хвост.координаты, хвост.направление);
                }
            }

            public override Положение[] НайтиВсеПоложения(World мир)
            {
                //                List<Положение> list = new List<Положение>();
                base.найденные_положения.Clear();
                Double3DPoint point = new Double3DPoint(this.кузов.направление);
                Double3DPoint point2 = Double3DPoint.Rotate(this.кузов.направление, (Math.PI / 2.0));
                int length = this.модель.занятыеПоложения.Length;
                for (int i = 0; i < this.модель.занятыеПоложенияХвостов.Length; i++)
                {
                    length += this.модель.занятыеПоложенияХвостов[i].Length;
                }
                Double3DPoint[] pos = new Double3DPoint[length];
                int index = 0;
                int num4 = 0;
                while (num4 < this.модель.занятыеПоложения.Length)
                {
                    pos[index] = (Double3DPoint)((this.кузов.координаты + (point * this.модель.занятыеПоложения[num4].x)) + (point2 * this.модель.занятыеПоложения[num4].y));
                    num4++;
                    index++;
                }
                for (int j = 0; j < this.модель.занятыеПоложенияХвостов.Length; j++)
                {
                    point = new Double3DPoint(this.хвосты[j].направление);
                    point2 = Double3DPoint.Rotate(this.хвосты[j].направление, (Math.PI / 2.0));
                    int num6 = 0;
                    while (num6 < this.модель.занятыеПоложенияХвостов[j].Length)
                    {
                        pos[index] = (Double3DPoint)((this.хвосты[j].координаты + (point * this.модель.занятыеПоложенияХвостов[j][num6].x)) + (point2 * this.модель.занятыеПоложенияХвостов[j][num6].y));
                        num6++;
                        index++;
                    }
                }
                Положение[] collection = мир.Найти_все_положения(pos);
                for (int k = 0; k < collection.Length; k++)
                {
                    collection[k].comment = this;
                }
                base.найденные_положения.AddRange(collection);
                //                base.найденные_положения = list;
                return base.найденные_положения.ToArray();
            }

            public ЧастьТроллейбуса Найти_часть(int index)
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

            public override void Обновить(World мир, Игрок[] игроки_в_игре)
            {
                ArrayList list = new ArrayList();
                double direction_prev = this.direction;
                base.поворотРуля = Math.Min(Math.Max(base.поворотРуля, -0.78539816339744828), 0.78539816339744828);
                if (игроки_в_игре != null)
                {
                    for (int i = 0; i < игроки_в_игре.Length; i++)
                    {
                        if (игроки_в_игре[i].объектПривязки == this)
                        {
                            list.Add(игроки_в_игре[i]);
                        }
                    }
                }
                if (list.Count > 0)
                {
                    int num3;
                    IОбъектПривязки3D[] и_dArray = new IОбъектПривязки3D[list.Count];
                    int index = 0;
                    foreach (Игрок игрок in list)
                    {
                        double[] numArray = new double[this.хвосты.Length];
                        num3 = 0;
                        while (num3 < this.хвосты.Length)
                        {
                            //                            DoublePoint point = игрок.cameraPosition.xz_point - this.хвосты[num3].координаты.xz_point;
                            numArray[num3] = (игрок.cameraPosition.XZPoint - this.хвосты[num3].координаты.XZPoint).Modulus;//point.модуль;
                            num3++;
                        }
                        double[] numArray2 = new double[this.сочленения.Length];
                        num3 = 0;
                        while (num3 < this.сочленения.Length)
                        {
                            DoublePoint point2 = игрок.cameraPosition.XZPoint - this.сочленения[num3].координаты.XZPoint;
                            numArray2[num3] = (игрок.cameraPosition.XZPoint - this.сочленения[num3].координаты.XZPoint).Modulus;//point2.модуль;
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
                            //                            var point3 = игрок.cameraPosition.xz_point - this.кузов.координаты.xz_point;
                            var num5 = (игрок.cameraPosition.XZPoint - this.кузов.координаты.XZPoint).Modulus;//point3.модуль;
                            for (num3 = 0; num3 < this.хвосты.Length; num3++)
                            {
                                if (numArray[num3] >= num5) continue;
                                num5 = numArray[num3];
                                и_dArray[index] = хвосты[num3];
                            }
                        }
                        index++;
                    }
                    var pointArray = new Double3DPoint[list.Count];
                    var pointArray2 = new Double3DPoint[list.Count];
                    var pointArray3 = new Double3DPoint[list.Count];
                    var pointArray4 = new DoublePoint[list.Count];
                    var pointArray5 = new DoublePoint[list.Count];
                    num3 = 0;
                    foreach (Игрок игрок2 in list)
                    {
                        pointArray[num3] = игрок2.cameraPosition - и_dArray[num3].Координаты3D;
                        pointArray[num3].XZPoint = pointArray[num3].XZPoint.Multyply(new DoublePoint(-и_dArray[num3].direction));// *= new DoublePoint(-и_dArray[num3].direction);
                        pointArray[num3].XYPoint = pointArray[num3].XYPoint.Multyply(new DoublePoint(-и_dArray[num3].НаправлениеY));// *= new DoublePoint(-и_dArray[num3].НаправлениеY);
                        pointArray2[num3] = игрок2.поворачиватьКамеру ? игрок2.cameraPosition : и_dArray[num3].Координаты3D;
                        pointArray4[num3] = new DoublePoint(и_dArray[num3].direction, и_dArray[num3].НаправлениеY);
                        num3++;
                    }
                    this.Передвинуть(base.скорость * World.прошлоВремени, мир);
                    num3 = 0;
                    foreach (Игрок игрок3 in list)
                    {
                        pointArray[num3].XYPoint = pointArray[num3].XYPoint.Multyply(new DoublePoint(и_dArray[num3].НаправлениеY));// *= new DoublePoint(и_dArray[num3].НаправлениеY);
                        pointArray[num3].XZPoint = pointArray[num3].XZPoint.Multyply(new DoublePoint(и_dArray[num3].direction));// *= new DoublePoint(и_dArray[num3].direction);
                        pointArray[num3].Add(и_dArray[num3].Координаты3D);// += и_dArray[num3].Координаты3D;
                        pointArray3[num3] = игрок3.поворачиватьКамеру ? pointArray[num3] : и_dArray[num3].Координаты3D;
                        pointArray5[num3] = new DoublePoint(и_dArray[num3].direction, и_dArray[num3].НаправлениеY);
                        игрок3.cameraPosition.Add(pointArray3[num3] - pointArray2[num3]);// += pointArray3[num3] - pointArray2[num3];
                        if (игрок3.поворачиватьКамеру)
                        {
                            игрок3.cameraRotation.Add(pointArray5[num3] - pointArray4[num3]);// += pointArray5[num3] - pointArray4[num3];
                        }
                        num3++;
                    }
                }
                else
                {
                    this.Передвинуть(base.скорость * World.прошлоВремени, мир);
                }

                if ((this.direction != direction_prev) && (base.поворотРуля != 0.0))
                {
                    var num6 = this.direction - direction_prev;
                    if (num6 < -Math.PI)
                    {
                        num6 += Math.PI * 2.0;
                    }
                    if (num6 > Math.PI)
                    {
                        num6 -= Math.PI * 2.0;
                    }

                    int num7 = Math.Sign((double)(-num6 * base.поворотРуля));
                    if (num7 > 0)
                    {
                        if (Game.space)
                        {
                            base.поворотРуля += num7 * num6;
                            if (Math.Abs(base.поворотРуля) < 0.001)
                            {
                                base.поворотРуля = 0.0;
                            }
                        }
                    }

                }
                this.времяПоворотников += World.прошлоВремени;
                while (this.времяПоворотников > this.времяПоворотниковMax)
                {
                    this.времяПоворотников -= this.времяПоворотниковMax;
                }
                if ((base.указатель_поворота != this._бывшийУказательПоворота) || (base.аварийная_сигнализация != this._былаАварийнаяСигнализация))
                {
                    this.времяПоворотников = 0.0;
                }
                this._бывшийУказательПоворота = base.указатель_поворота;
                this._былаАварийнаяСигнализация = base.аварийная_сигнализация;
                base._soundУскоряется = ((base.система_управления.ход_или_тормоз > 0) && !base.обесточен);//false;
                base._soundЗамедляется = ((base.система_управления.ход_или_тормоз < 0) && !base.обесточен);//false;
                if (ах != null)
                {
                    ах.Simulation();
                }
                if (Math.Abs(base.скорость) < 1E-06)
                {
                    base.скорость = 0.0;
                }
                var num8 = base.скорость;
                base.скорость += this.ускорение * World.прошлоВремени;
                if (((this.ускорение * base.скорость) < 0.0) && ((base.скорость * num8) < 0.0))
                {
                    base.скорость = 0.0;
                }
                this.ОбновитьПоложение(мир);
                foreach (Двери двери in base._двери)
                {
                    двери.Обновить();
                }
                this.ОбновитьКолёсаШтангиРуль();
                foreach (Троллейбус.Штанга штанга in base.штанги)
                {
                    if (!штанга.поднимается)
                    {
                        штанга.направление += this.direction - direction_prev;
                    }
                    штанга.Обновить(base.система_управления.переключение);//, (float)base.скорость_abs);
                }
                base.скорость_abs -= 0.1 * World.прошлоВремени;
                base.ОбновитьРейс();
                UpdateBoundigBoxes(мир);
            }

            public override void SetPosition(Road road, double distance, double shift, Double3DPoint pos, DoublePoint rot, World world)
            {
                if (road != null)
                {
                    pos = new Double3DPoint
                    {
                        XZPoint = road.НайтиКоординаты(distance, shift),
                        y = road.НайтиВысоту(distance)
                    };
                    var pos2 = new Double3DPoint
                    {
                        XZPoint = road.НайтиКоординаты(distance + модель.колёсныеПары[0].pos.x, shift),
                        y = road.НайтиВысоту(distance + модель.колёсныеПары[0].pos.x)
                    };
                    rot = (pos2 - pos).Angle;//new DoublePoint(road.НайтиНаправление(distance), road.НайтиНаправлениеY(distance));
                }
                кузов.координаты = pos;
                кузов.направление = rot;
                Double3DPoint point;
                Double3DPoint point2;
                for (var i = 0; i < хвосты.Length; i++)
                {
                    distance -= модель.хвостDist1[i] + модель.хвостDist2[i];
                    while ((distance < 0.0) && (road.предыдущиеДороги.Length > 0))
                    {
                        road = road.предыдущиеДороги[Cheats._random.Next(0, road.предыдущиеДороги.Length)];
                        distance += road.Длина;
                    }

                    point = new Double3DPoint
                    {
                        XZPoint = road.НайтиКоординаты(distance + модель.хвостDist2[i], shift),
                        y = road.НайтиВысоту(distance + модель.хвостDist2[i])
                    };
                    point2 = new Double3DPoint
                    {
                        XZPoint = road.НайтиКоординаты(distance, shift),
                        y = road.НайтиВысоту(distance)
                    };
                    pos = ((i == 0) ? кузов.координаты : хвосты[i - 1].координаты) + new Double3DPoint((i == 0) ? кузов.направление : хвосты[i - 1].направление) * -модель.хвостDist1[i];
                    хвосты[i].координаты = pos + new Double3DPoint((point - point2).Angle) * -модель.хвостDist2[i];
                    хвосты[i].направление = (pos - хвосты[i].координаты).Angle;
                }
                this.ОбновитьКолёсаШтангиРуль();
                this.ОбновитьПоложение(world);
            }

            private void ОбновитьКолёсаШтангиРуль()
            {
                Double3DPoint point = new Double3DPoint(кузов.направление);
                point.AngleY += (Math.PI / 2.0);
                for (var i = 0; i < модель.колёсныеПары.Length; i++)
                {
                    var троллейбуса = Найти_часть(модель.колёсныеПары[i].часть);
                    //                    point = new Double3DPoint(троллейбуса.направление);
                    point.CopyFromAngle(троллейбуса.направление);
                    point.AngleY += (Math.PI / 2.0);
                    point.Multyply(_радиусКолёс);
                    var point3 = new Double3DPoint(троллейбуса.направление).Multyply(модель.колёсныеПары[i].pos.x);
                    //                    _колёса[2 * i].координаты = ((троллейбуса.координаты + (new Double3DPoint(троллейбуса.направление) * модель.колёсныеПары[i].pos.x)) + (Double3DPoint.Поворот(троллейбуса.направление, -MyFeatures.halfPI) * модель.колёсныеПары[i].pos.y)) + (point * _радиусКолёс);
                    _колёса[2 * i].координаты = троллейбуса.координаты + point3 + Double3DPoint.Rotate(троллейбуса.направление, -(Math.PI / 2.0)).Multyply(модель.колёсныеПары[i].pos.y) + point;
                    _колёса[2 * i].базовоеНаправление = троллейбуса.направление;
                    //                    _колёса[(2 * i) + 1].координаты = ((троллейбуса.координаты + (new Double3DPoint(троллейбуса.направление) * модель.колёсныеПары[i].pos.x)) + (Double3DPoint.Поворот(троллейбуса.направление, MyFeatures.halfPI) * модель.колёсныеПары[i].pos.y)) + (point * _радиусКолёс);
                    _колёса[(2 * i) + 1].координаты = троллейбуса.координаты + point3 + Double3DPoint.Rotate(троллейбуса.направление, (Math.PI / 2.0)).Multyply(модель.колёсныеПары[i].pos.y) + point;
                    _колёса[(2 * i) + 1].базовоеНаправление = троллейбуса.направление;
                }
                _колёса[0].поворот = -поворотРуля;
                _колёса[1].поворот = -поворотРуля;
                if (руль != null)
                {
                    руль.поворот = поворотРуля;
                }
                if (штанги.Length <= 1) return;
                var point2 = кузов.координаты;
                var angle = кузов.направление;
                if (хвосты.Length > 0)
                {
                    point2 = хвосты[хвосты.Length - 1].координаты;
                    angle = хвосты[хвосты.Length - 1].направление;
                }
                point = new Double3DPoint(angle);
                point.AngleY += (Math.PI / 2.0);
                штанги[0].основание = point2 + new Double3DPoint(angle).Multyply(модель.штанги[0].pos.x) + Double3DPoint.Rotate(angle, -(Math.PI / 2.0)).Multyply(модель.штанги[0].pos.z) + point.Multyply(модель.штанги[0].pos.y);
                штанги[1].основание = Double3DPoint.Multiply(модель.штанги[1].pos, point2, angle);//((point2 + (new Double3DPoint(angle) * модель.штанги[1].pos.x)) + (Double3DPoint.Поворот(angle, 1.5707963267948966) * модель.штанги[1].pos.z)) + (point * модель.штанги[1].pos.y);
                штанги[0].базовоеНаправление = angle.x + Math.PI;
                штанги[0].направлениеY = -angle.y;
                штанги[1].базовоеНаправление = angle.x + Math.PI;
                штанги[1].направлениеY = -angle.y;
            }

            protected override void ОбновитьМаршрутныеУказатели()
            {
                кузов.ОбновитьМаршрутныйУказатель(маршрут.number, наряд == null ? "" : наряд.номер);
                foreach (var хвост in хвосты)
                {
                    хвост.ОбновитьМаршрутныйУказатель(маршрут.number, наряд == null ? "" : наряд.номер);
                }
            }

            public void ОбновитьПоложение(World мир)
            {
                var pos = (_колёса[0].координаты + _колёса[1].координаты) / 2.0;
                if ((положение.Дорога != null) && (мир.Найти_положение(pos, положение.Дорога).Дорога != null))
                {
                    положение = мир.Найти_положение(pos, положение.Дорога);
                }
                else if ((_следующаяДорога != null) && (мир.Найти_положение(pos, _следующаяДорога).Дорога != null))
                {
                    this.положение.Дорога.objects.Remove(this);
                    base.положение = мир.Найти_положение(pos, base._следующаяДорога);
                    this.положение.Дорога.objects.Add(this);
                    base._следующаяДорога = null;
                }
                else
                {
                    if (this.положение.Дорога != null)
                    {
                        this.положение.Дорога.objects.Remove(this);
                    }
                    base.положение = мир.Найти_ближайшее_положение(pos.XZPoint, мир.Дороги);
                    if (this.положение.Дорога != null)
                    {
                        this.положение.Дорога.objects.Add(this);
                    }
                    base._следующаяДорога = null;
                }
            }

            public override void Передвинуть(double расстояние, World мир)
            {
                //some new:
                Double3DPoint pos = new Double3DPoint();
                DoublePoint direction;
                //original vars
                Double3DPoint[] posArrayBack0 = new Double3DPoint[1 + this.хвосты.Length];
                Double3DPoint[] posArrayFront0 = new Double3DPoint[1 + this.хвосты.Length];
                double[] numArray = new double[1 + this.хвосты.Length];
                //                posArrayBack0[0] = (Double3DPoint)((base._колёса[2].координаты + base._колёса[3].координаты) / 2.0);
                base._колёса[2].координаты.CopyTo(ref posArrayBack0[0]);
                posArrayBack0[0].Add(base._колёса[3].координаты);
                posArrayBack0[0].Divide(2.0);
                //                posArrayFront0[0] = (Double3DPoint)((base._колёса[0].координаты + base._колёса[1].координаты) / 2.0);
                base._колёса[0].координаты.CopyTo(ref posArrayFront0[0]);
                posArrayFront0[0].Add(base._колёса[1].координаты);
                posArrayFront0[0].Divide(2.0);
                Double3DPoint point8 = new Double3DPoint();
                Double3DPoint point9 = new Double3DPoint();
                posArrayFront0[0].CopyTo(ref point8);
                point8.Subtract(posArrayBack0[0]);
                numArray[0] = point8.Modulus;
                for (int i = 1; i < posArrayBack0.Length; i++)
                {
                    //                    posArrayBack0[i] = (Double3DPoint)((base._колёса[(2 * i) + 2].координаты + base._колёса[(2 * i) + 3].координаты) / 2.0);
                    base._колёса[(2 * i) + 2].координаты.CopyTo(ref posArrayBack0[i]);
                    posArrayBack0[i].Add(base._колёса[(2 * i) + 3].координаты);
                    posArrayBack0[i].Divide(2.0);
                    //                    posArrayFront0[i] = posArrayBack0[i - 1] + (new Double3DPoint(point8.угол).Multiply(-this.модель.хвостDist1[i - 1]));
                    posArrayBack0[i - 1].CopyTo(ref posArrayFront0[i]);
                    point9.CopyFromAngle(point8.Angle.x, point8.Angle.y);
                    point9.Multyply(-this.модель.хвостDist1[i - 1]);
                    posArrayFront0[i].Add(point9);
                    //                    point8 = posArrayFront0[i] - posArrayBack0[i];
                    posArrayFront0[i].CopyTo(ref point8);
                    point8.Subtract(posArrayBack0[i]);
                    numArray[i] = point8.Modulus;
                }
                double[] numArray2 = new double[base._колёса.Length / 2];
                for (int j = 0; j < base._колёса.Length; j++)
                {
                    //                    base._колёса[j].координаты += (Double3DPoint)(new Double3DPoint(base._колёса[j].Направление) * расстояние);
                    //так интереснее:
                    //                    base._колёса[j].координаты.Add(new Double3DPoint(base._колёса[j].Направление).Multiply(расстояние));
                    //а так ещё больше:
                    direction = base._колёса[j].Направление;
                    pos.CopyFromAngle(direction);
                    pos.Multyply(расстояние);
                    base._колёса[j].координаты.Add(pos);
                    //
                    base._колёса[j].пройденноеРасстояние += расстояние;
                    base._колёса[j].координаты.CopyTo(ref pos);
                    pos.y -= base._колёса[j].радиус;
                    if (base._колёса[j].текущееПоложение.Дорога != null)
                    {
                        base._колёса[j].текущееПоложение = мир.Найти_положение(pos, base._колёса[j].текущееПоложение.Дорога);
                    }
                    if (base._колёса[j].текущееПоложение.Дорога == null)
                    {
                        Положение[] положениеArray = мир.Найти_все_положения(new Double3DPoint[] { pos });//(pointArray5);
                        if (положениеArray.Length > 0)
                        {
                            base._колёса[j].текущееПоложение = положениеArray[0];
                        }
                    }
                    if (base._колёса[j].текущееПоложение.Дорога != null)
                    {
                        numArray2[j / 2] = Math.Max(numArray2[j / 2], base._колёса[j].координаты.y - base._колёса[j].текущееПоложение.высота);
                        double num3 = base._колёса[j].текущееПоложение.Дорога.НайтиНаправлениеY(base._колёса[j].текущееПоложение.расстояние);
                        if (num3 != 0.0)
                        {
                            //                            base.скорость += Math.Cos(num3 + MyFeatures.halfPI) * 0.03;//* 10 * World.прошлоВремени;
                            base.скорость -= Math.Sin(num3) * Math.Cos(_колёса[j].текущееПоложение.Дорога.НайтиНаправление(_колёса[j].текущееПоложение.расстояние)
                                           - (_колёса[j].базовоеНаправление.x + _колёса[j].поворот)) * Road.uklon_koef * World.прошлоВремени;
                        }
                    }
                    else
                    {
                        numArray2[j / 2] = Math.Max(numArray2[j / 2], base._колёса[j].радиус + мир.GetHeight(base._колёса[j].координаты.XZPoint));
                    }
                    base._колёса[j].координаты.y = numArray2[j / 2];
                }
                Double3DPoint[] posArrayBack = new Double3DPoint[1 + this.хвосты.Length];
                Double3DPoint[] posArrayFront = new Double3DPoint[1 + this.хвосты.Length];
                posArrayBack[0] = (Double3DPoint)((base._колёса[2].координаты + base._колёса[3].координаты) / 2.0);
                posArrayFront[0] = (Double3DPoint)((base._колёса[0].координаты + base._колёса[1].координаты) / 2.0);
                DoublePoint ang43 = new DoublePoint();
                Double3DPoint point12 = new Double3DPoint();
                Double3DPoint point5 = new Double3DPoint();
                for (int m = 0; m < posArrayBack0.Length; m++)
                {
                    if (m > 0)
                    {
                        //                        posArrayBack[m] = (Double3DPoint)((base._колёса[(2 * m) + 2].координаты + base._колёса[(2 * m) + 3].координаты) / 2.0);
                        base._колёса[(2 * m) + 2].координаты.CopyTo(ref posArrayBack[m]);
                        posArrayBack[m].Add(base._колёса[(2 * m) + 3].координаты);
                        posArrayBack[m].Divide(2.0);
                        //                        Double3DPoint point11 = pointArray4[m - 1] - pointArray3[m - 1];
                        //                        pointArray4[m] = pointArray3[m - 1] + ((Double3DPoint)(new Double3DPoint((pointArray4[m - 1] - pointArray3[m - 1]).угол) * -this.модель.хвостDist1[m - 1]));
                        //                        posArrayFront[m] = posArrayBack[m - 1] + (new Double3DPoint(ang43).Multiply(-this.модель.хвостDist1[m - 1]));
                        posArrayBack[m - 1].CopyTo(ref posArrayFront[m]);
                        point5.CopyFromAngle(ang43);
                        point5.Multyply(-this.модель.хвостDist1[m - 1]);
                        posArrayFront[m].Add(point5);
                    }
                    posArrayFront[m].CopyTo(ref point12);
                    point12.Subtract(posArrayBack[m]);
                    double md43 = point12.Modulus;
                    if (Math.Abs(md43 - numArray[m]) > 0.001)
                    {

                        Double3DPoint point13 = posArrayFront[m] - posArrayFront0[m];
                        double num6 = point13.Modulus;

                        Double3DPoint point3 = posArrayBack[m] - posArrayFront0[m];
                        var ang1 = point3.Angle;
                        Double3DPoint point2 = new Double3DPoint(point13.Angle - ang1);
                        /*Double3DPoint point16 = point2 - new Double3DPoint(point2.x, 0.0, 0.0);
                        double y = point16.модуль;
                        DoublePoint point17 = new DoublePoint(point2.x, y);
                        double d = point17.угол;*/
                        //
                        double x1 = point2.x;
                        point2.x = 0.0;
                        double d = new DoublePoint(x1, point2.Modulus).Angle;
                        //
                        double num9 = (num6 * Math.Cos(d)) + Math.Sqrt((numArray[m] * numArray[m]) - (((num6 * num6) * Math.Sin(d)) * Math.Sin(d)));

                        double md = point3.Modulus;
                        //                            posArrayBack[m] = posArrayFront0[m] + (new Double3DPoint(ang1).Multiply(num9));
                        posArrayFront0[m].CopyTo(ref posArrayBack[m]);
                        posArrayBack[m].Add(new Double3DPoint(ang1).Multyply(num9));
                        base._колёса[(2 * m) + 2].пройденноеРасстояние += md - num9; //колесо1.пройденноеРасстояние += point3.модуль - num9;
                        base._колёса[(2 * m) + 3].пройденноеРасстояние += md - num9; //колесо3.пройденноеРасстояние += point3.модуль - num9;

                        posArrayFront[m].CopyTo(ref point12);
                        point12.Subtract(posArrayBack[m]);
                    }
                    point12.Angle.CopyTo(ref ang43);
                    point5.CopyFromAngle(ang43);
                    if (m == 0)
                    {
                        this.кузов.координаты = posArrayBack[m] + (new Double3DPoint(ang43).Multyply(-this.модель.колёсныеПары[m + 1].pos.x));
                        this.кузов.направление = ang43;
                        Double3DPoint point4 = point5;
                        point4.AngleY += (Math.PI / 2.0);
                        this.кузов.координаты.Subtract(point4.Multyply(base._радиусКолёс));
                    }
                    else
                    {
                        //ну и зачем одно и то же было?
                        posArrayBack[m] = posArrayFront[m] + (new Double3DPoint(ang43).Multyply(-this.модель.хвостDist2[m - 1]));
                        this.хвосты[m - 1].координаты = posArrayBack[m] + (new Double3DPoint(ang43).Multyply(-this.модель.колёсныеПары[m + 1].pos.x));
                        this.хвосты[m - 1].направление = ang43;
                        //                        point5 = new Double3DPoint(this.хвосты[m - 1].направление);
                        point5.CopyFromAngle(this.хвосты[m - 1].направление);
                        point5.AngleY += (Math.PI / 2.0);
                        this.хвосты[m - 1].координаты.Subtract(point5.Multyply(base._радиусКолёс));
                        this.сочленения[m - 1].координаты = posArrayFront[m];
                        DoublePoint point6 = (posArrayFront[m - 1] - posArrayFront[m]).Angle;
                        DoublePoint point7 = ang43;//point24.угол;
                        this.сочленения[m - 1].направление.x = (point6.x + point7.x) / 2.0;
                        this.сочленения[m - 1].направление.y = (point6.y + point7.y) / 2.0;
                        if (Math.Abs((double)(point6.x - point7.x)) >= Math.PI)
                        {
                            this.сочленения[m - 1].направление.x += Math.PI;
                        }
                        if (Math.Abs((double)(point6.y - point7.y)) >= Math.PI)
                        {
                            this.сочленения[m - 1].направление.y += Math.PI;
                        }
                        //                        point5 = new Double3DPoint(this.сочленения[m - 1].направление);
                        point5.CopyFromAngle(this.сочленения[m - 1].направление);
                        point5.AngleY += (Math.PI / 2.0);
                        this.сочленения[m - 1].координаты.Subtract(point5.Multyply(base._радиусКолёс));
                    }
                }
            }

            public override DoublePoint position
            {
                get
                {
                    return this.кузов.координаты.XZPoint;
                }
            }

            public override Double3DPoint Координаты3D
            {
                get
                {
                    return this.кузов.координаты;
                }
            }

            public override double direction
            {
                get
                {
                    return this.кузов.направление.x;
                }
            }

            public override double НаправлениеY
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



            public class Дополнение : MeshObject, MeshObject.IFromFile, IMatrixObject
            {
                public string file;
                public Тип_дополнения тип;
                public ЧастьТроллейбуса частьТроллейбуса;

                public Дополнение(ЧастьТроллейбуса частьТроллейбуса, string filename, Тип_дополнения тип)
                {
                    this.частьТроллейбуса = частьТроллейбуса;
                    this.file = filename;
                    this.тип = тип;
                }

                public Matrix GetMatrix(int index)
                {
                    return частьТроллейбуса.last_matrix;//.GetMatrix(0);
                }

                public string Filename
                {
                    get
                    {
                        base.meshDir = this.частьТроллейбуса.meshDir;
                        return this.file;
                    }
                }

                public int MatricesCount
                {
                    get
                    {
                        return 1;
                    }
                }
            }

            public class Кузов : Троллейбус.ОбычныйТроллейбус.ЧастьТроллейбуса, MeshObject.IFromFile, IMatrixObject
            {
                public Кузов(Троллейбус.ОбычныйТроллейбус троллейбус)
                {
                    base.троллейбус = троллейбус;
                }

                public string Filename
                {
                    get
                    {
                        base.meshDir = base.троллейбус.модель.dir;
                        return base.троллейбус.модель.filename;
                    }
                }
            }

            public class Сочленение : Троллейбус.ОбычныйТроллейбус.ЧастьТроллейбуса, MeshObject.IFromFile, IMatrixObject
            {
                public Сочленение(ОбычныйТроллейбус троллейбус)
                {
                    base.троллейбус = троллейбус;
                }

                public string Filename
                {
                    get
                    {
                        meshDir = троллейбус.модель.dir;
                        var index = 0;
                        for (var i = 0; i < троллейбус.сочленения.Length; i++)
                        {
                            if (троллейбус.сочленения[i] == this)
                            {
                                index = i;
                                break;
                            }
                        }
                        return троллейбус.модель.сочленениеFilename[index];
                    }
                }
            }

            public class Хвост : ЧастьТроллейбуса, MeshObject.IFromFile, IMatrixObject
            {
                public Хвост(ОбычныйТроллейбус троллейбус)
                {
                    base.троллейбус = троллейбус;
                }

                public string Filename
                {
                    get
                    {
                        base.meshDir = base.троллейбус.модель.dir;
                        int index = 0;
                        for (int i = 0; i < base.троллейбус.хвосты.Length; i++)
                        {
                            if (base.троллейбус.хвосты[i] == this)
                            {
                                index = i;
                                break;
                            }
                        }
                        return base.троллейбус.модель.хвостFilename[index];
                    }
                }
            }

            public class ЧастьТроллейбуса : MeshObject, IОбъектПривязки3D, IVector, IMatrixObject
            {
                public Double3DPoint координаты;
                public DoublePoint направление;
                public ОбычныйТроллейбус троллейбус;

                public Matrix GetMatrix(int index)
                {
                    var matrix = Matrix.RotationZ((float)НаправлениеY) * Matrix.RotationY(-((float)this.direction));//.направление.x));
                    last_matrix = (matrix * Matrix.Translation((float)координаты.x, (float)координаты.y, (float)координаты.z));
                    return last_matrix;//(matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
                }

                public void ОбновитьМаршрутныйУказатель(string маршрут, string наряд)
                {
                    if (_meshTextureFilenames == null) return;
                    for (var i = 0; i < _meshTextureFilenames.Length; i++)
                    {
                        if (string.IsNullOrEmpty(_meshTextureFilenames[i])) continue;
                        var fullpath = _meshTextureFilenames[i];
                        var filename = "";
                        var ext = "";
                        var transport = "";

                        var startIndex = fullpath.LastIndexOf('.');
                        if (startIndex > 0)
                        {
                            ext = fullpath.Substring(startIndex);
                            fullpath = fullpath.Substring(0, startIndex);
                            startIndex = fullpath.LastIndexOf('\\');
                            filename = fullpath.Substring(startIndex);
                            fullpath = fullpath.Substring(0, startIndex);
                            startIndex = fullpath.LastIndexOf('\\');
                            transport = fullpath.Substring(startIndex);
                        }
                        var flag = true;
                        foreach (var str3 in extraMeshDirs)
                        {
                            if (File.Exists(str3 + transport + filename + маршрут + "-" + наряд + ext))
                            {
                                flag = false;
                                LoadTexture(i, transport + filename + маршрут + "-" + наряд + ext);
                                break;
                            }
                            if (!File.Exists(str3 + transport + filename + маршрут + ext)) continue;
                            flag = false;
                            LoadTexture(i, transport + filename + маршрут + ext);
                            break;
                        }
                        if (flag)
                        {
                            LoadTexture(i, _meshTextureFilenames[i]);
                        }
                    }
                }

                public int MatricesCount
                {
                    get
                    {
                        return 1;
                    }
                }

                public DoublePoint position
                {
                    get
                    {
                        return this.координаты.XZPoint;
                    }
                }

                public double direction
                {
                    get
                    {
                        return this.направление.x;
                    }
                }

                public Double3DPoint Координаты3D
                {
                    get
                    {
                        return this.координаты;
                    }
                }

                public double НаправлениеY
                {
                    get
                    {
                        return направление.y;
                    }
                }
            }
        }

        public class Штанга : MeshObject, MeshObject.IFromFile, IMatrixObject, IVector
        {
            private string _file = "";
            private Контактный_провод _fпровод;
            public double базовоеНаправление;
            public double длина = 6.066;
            public double направление;
            public double направлениеY;
            public Double3DPoint _основание;
            public DoublePoint _основаниеXZ;
            public bool поднимается;
            public double полнаяДлина = 6.46;
            public bool правая;
            public double скоростьПодъёма;
            public double угол;
            public double уголMax = 0.3;
            public double уголMin = -0.351;
            public double уголNormal;
            Transport _trransport;

            public Штанга(bool правая, string dir, string filename, double полнаяДлина, double уголMin)
            {
                this.правая = правая;
                meshDir = dir;
                _file = filename;
                this.полнаяДлина = полнаяДлина;
                this.уголMin = уголMin;
                угол = уголMin;
            }

            public Matrix GetMatrix(int index)
            {
                return (((Matrix.RotationZ((float)угол) * Matrix.RotationY((float)(базовоеНаправление - направление))) * Matrix.RotationZ((float)направлениеY)) * Matrix.RotationY(-((float)базовоеНаправление)) * Matrix.Translation((float)основание.x, (float)основание.y, (float)основание.z));
            }

            public void НайтиПровод(Контактный_провод[] контактныеПровода)
            {
                double num = 1000.0;
                DoublePoint point = _основаниеXZ;

                double num2;
                // FIXME: и тут всё очень плохо, мда...
                foreach (var провод1 in контактныеПровода)
                {
                    if ((провод1.правый == правая) && !провод1.обесточенный)
                    {
                        point = _основаниеXZ - провод1.начало;
                        point.Angle -= провод1.направление;
                        if (Math.Abs(point.y) <= длина)
                        {
                            num2 = Math.Sqrt((длина * длина) - (point.y * point.y));
                            if (((point.x + num2) >= 0.0) && ((point.x + num2) < провод1.длина))
                            {
                                var point2 = провод1.начало + new DoublePoint(провод1.направление) * (point.x + num2);
                                var point3 = point2 - _основаниеXZ;
                                point3.Angle -= базовоеНаправление;
                                if (Math.Abs(point3.Angle) <= (Math.PI / 2.0))
                                {
                                    var point6 = point2 - position;
                                    if (point6.Modulus < num)
                                    {
                                        Провод = провод1;
                                        var point7 = point2 - position;
                                        num = point7.Modulus;
                                        continue;
                                    }
                                }
                            }
                            if (((point.x - num2) >= 0.0) && ((point.x - num2) < провод1.длина))
                            {
                                var point4 = провод1.начало + new DoublePoint(провод1.направление) * (point.x - num2);
                                var point5 = point4 - _основаниеXZ;
                                point5.Angle -= базовоеНаправление;
                                if (Math.Abs(point5.Angle) <= (Math.PI / 2.0))
                                {
                                    var point8 = point4 - position;
                                    if (point8.Modulus < num)
                                    {
                                        Провод = провод1;
                                        var point9 = point4 - position;
                                        num = point9.Modulus;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public void Обновить(bool включенТэд)
            {
                var dt = 0.5 * World.прошлоВремени;
                if (Провод != null)
                {
                    var flag = Поднята;
                    var num = угол;
                    //var troll = Transport();
                    //var Transport = new Троллейбус();
                    угол = уголNormal;
                    // HACK: откуда значения для вектора, а?
                    var point = MyFeatures.ToDouble3DPoint(Vector3.TransformCoordinate(new Vector3(6.65928f, 2.2f, 0f), GetMatrix(0)));
                    угол = num;
                    var point6 = point.XZPoint - Провод.начало;
                    var wiredDistance = point6.Modulus;
                    var num3 = Провод.FindHeight(wiredDistance) + Контактный_провод.высота_контактной_сети;
                    var num4 = полнаяДлина * Math.Sin(уголNormal - уголMin);
                    num4 += num3 - point.y;
                    if (num4 < 0.0)
                    {
                        num4 = 0.0;
                    }
                    if (num4 > полнаяДлина)
                    {
                        num4 = полнаяДлина;
                    }
                    уголNormal = Math.Asin(num4 / полнаяДлина) + уголMin;
                    длина = полнаяДлина * Math.Cos(уголNormal - уголMin);
                    if (flag)
                    {
                        угол = уголNormal;
                    }
                }
                var point2 = new DoublePoint(направление - базовоеНаправление);
                if (point2.Angle > (Math.PI / 2.0))
                {
                    направление = базовоеНаправление + Math.PI / 2.0;
                }
                else if (point2.Angle < -(Math.PI / 2.0))
                {
                    направление = базовоеНаправление - Math.PI / 2.0;
                }
                if (поднимается)
                {
                    if ((угол < уголNormal) || (Провод == null))
                    {
                        if (угол < уголNormal)
                        {
                            скоростьПодъёма = dt;//0.005;
                        }
                        else
                        {
                            скоростьПодъёма += (уголMax - угол) * dt;
                            скоростьПодъёма *= Math.Max(1.0 - dt, 0.0);
                        }
                        угол += скоростьПодъёма;
                        if ((угол > уголNormal) && (Провод != null))
                        {
                            угол = уголNormal;
                        }
                    }
                    if (Провод != null)
                    {
                        var point3 = _основаниеXZ - Провод.начало;
                        point3.Angle -= Провод.направление;
                        if (Math.Abs(point3.y) > длина)
                        {
                            Провод = null;
                        }
                        else
                        {
                            var num5 = Math.Sqrt((длина * длина) - (point3.y * point3.y));
                            var point4 = Провод.начало + new DoublePoint(Провод.направление) * (point3.x + num5);
                            var point5 = Провод.начало + new DoublePoint(Провод.направление) * (point3.x - num5);
                            var num6 = point3.x + num5;
                            //                            var point7 = point5 - position;
                            var curPos = position;
                            var dist7 = DoublePoint.Distance(ref point5, ref curPos);
                            //                            var point8 = point4 - position;
                            var dist8 = DoublePoint.Distance(ref point4, ref curPos);
                            //                            if (point7.Modulus < point8.Modulus)
                            if (dist7 < dist8)
                            {
                                point4 = point5;
                                num6 = point3.x - num5;
                            }
                            point5 = point4 - _основаниеXZ;
                            point5.Angle -= базовоеНаправление;
                            if (Math.Abs(point5.Angle) > (Math.PI / 2.0))
                            {
                                Провод = null;
                            }
                            else
                            {
                                var point9 = new DoublePoint((point5.Angle + базовоеНаправление) - направление);
                                var num7 = point9.Angle;
                                if (угол < уголNormal)
                                {
                                    направление += (num7 * скоростьПодъёма) / ((уголNormal - угол) + скоростьПодъёма);
                                }
                                else
                                {
                                    направление += num7;
                                }
                                if (num6 >= Провод.длина)
                                {
                                    // TODO: вернуть слёт штанг
                                    //if ((Провод != null) && ((Провод.предыдущие_провода.Length > 1) && (Transport.скорость > 6.95)))
                                    if (Провод.следующие_провода.Length > 1) //&& (_trransport.скорость >= 6.95))
                                    {
                                        var index = включенТэд ? 0 : (Провод.следующие_провода.Length - 1);
                                        Провод = Провод.следующие_провода[index];
                                    }
                                    else if (Провод.следующие_провода.Length == 1)
                                    {
                                        Провод = Провод.следующие_провода[0];
                                    }
                                    else
                                    {
                                        Провод = null;
                                    }

                                }
                                else if (num6 < 0.0)
                                {
                                    if (Провод.предыдущие_провода.Length > 0)
                                    {
                                        var num9 = Cheats._random.Next(Провод.предыдущие_провода.Length);
                                        Провод = Провод.предыдущие_провода[num9];
                                    }
                                    else
                                    {
                                        Провод = null;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // а зачем, если и так null?
                    //                    Провод = null;
                    // а это что вообще за херня?
                    //                    var point10 = new DoublePoint(направление - базовоеНаправление);
                    //                    направление = базовоеНаправление + point10.Angle;
                    угол -= dt;
                    if (угол < уголMin)
                    {
                        угол = уголMin;
                        направление = базовоеНаправление;
                    }
                    // здесь тоже лишняя проверка: условие выполнится только если ывполнено предыдущее =>
                    // перенёс в то выражение
                    //                    if (угол == уголMin)
                    //                    {
                    //                        направление = базовоеНаправление;
                    //                    }
                    else
                    {
                        направление += ((базовоеНаправление - направление) * dt) / ((угол + dt) - уголMin);
                    }
                }

            }

            public string Filename
            {
                get
                {
                    return _file;
                }
            }

            public int MatricesCount
            {
                get
                {
                    return 1;
                }
            }

            public Double3DPoint основание
            {
                get
                {
                    return _основание;
                }
                set
                {
                    value.CopyTo(ref _основание);
                    if (value == null)
                        return;
                    value.XZPoint.CopyTo(ref _основаниеXZ);
                }
            }

            public DoublePoint position
            {
                get
                {
                    // ускорим?
                    // эквивалентно (основание.XZPoint + new DoublePoint(направление) * длина)
                    DoublePoint result = new DoublePoint(направление).Multyply(длина);
                    return result.Add(ref _основаниеXZ);
                }
            }

            public double direction
            {
                get
                {
                    return направление;
                }
            }

            public bool Опущена
            {
                get
                {
                    return ((угол == уголMin) && (направление == базовоеНаправление));
                }
            }

            public bool Поднята
            {
                get
                {
                    return ((Провод != null) && (угол == уголNormal));
                }
            }

            public Контактный_провод Провод
            {
                get
                {
                    return _fпровод;
                }
                set
                {
                    if (_fпровод != null)
                    {
                        _fпровод.objects.Remove(this);
                    }
                    _fпровод = value;
                    if (value != null)
                    {
                        value.objects.Add(this);
                    }
                }
            }

            public double ПройденноеРасстояниеПоПроводу
            {
                get
                {
                    if (Провод != null)
                    {
                        //                        var point = position - Провод.начало;
                        //                        return point.Modulus;
                        var point = position;
                        return DoublePoint.Distance(ref point, ref Провод.начало);
                    }
                    return 0.0;
                }
            }
        }

        public class Руль : MeshObject, MeshObject.IFromFile, IMatrixObject
        {
            private string _filename;
            public double angle;
            public Double3DPoint point;
            public double поворот;
            private MeshObject obj;

            public Руль(string dir, string filename, Double3DPoint pos, double ang, MeshObject obj)
            {
                meshDir = dir;
                _filename = filename;
                angle = ang;
                point = pos;
                this.obj = obj;
            }

            public Matrix GetMatrix(int index)
            {
                var matrix1 = (Matrix.RotationY((float)поворот * 16) * Matrix.RotationZ((float)angle));
                // всё, нет совместимости с 0.6.2
                var matrix2 = Matrix.Translation((float)(point.x), (float)(point.y), (float)(point.z));
                return (matrix1 * matrix2) * obj.last_matrix;
            }

            public int MatricesCount
            {
                get
                {
                    return 1;
                }
            }

            public string Filename
            {
                get
                {
                    return _filename;
                }
            }
        }
    }
}