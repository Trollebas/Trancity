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
    public abstract class ���������� : ������������_���������
    {
        private ������[] _�����;
        public double �����������;
        public double �����������;
        public ��������� ���������;
        private double _����������;
        private Road _���������������;
        public ������[] ������;
        public ���� ���� = null;
        public �� ��;
        //public double ��������;

        private double abs_r;
        private double nr_abs_r;

        private const double stop = 20.0;
        private const double spd = 4.0;
        private const double tg = (16.0 - spd) / (140.0 - stop);

        //TODO: ��������� � ���� ���� ����������
        public override void ����������������������(World ���)
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
            var �� = false;

            base.stand_brake = false;
            if (���������.������ != null)
            {
                var ������ = ���������.������;
                uklon = (������.������[1] - ������.������[0]) / ������.�����;
                ost_dist = ������.����� - ���������.����������;
                if ((_��������������� == null) && (������.���������������.Length > 0))
                {
                    ������������();
                    if (((���� != null) && (����_index < (����.pathes.Length - 1)) && ((����_index > 0) || (������ == ����.pathes[0]))))//  && (����_index < (����.pathes.Length - 1)
                    {
                        _��������������� = ����.pathes[����_index + 1];
                    }
                    else
                    {
                        _��������������� = ������.���������������[Cheats._random.Next(������.���������������.Length)];
                    }
                }
                base.UpdateTripStops();
                ���������_�������� = 0;
                if (_��������������� != null)
                {
                    nr_uklon = (_���������������.������[1] - _���������������.������[0]) / _���������������.�����;
                    if ((������.���������������.Length > 1) && (ost_dist <= 40.0) && (_���������������.������))
                    {
                        ���������_�������� = (_���������������.���������������0 > 0.0) ? 1 : -1;
                    }
                }
                else
                {
                    signals_dist = ost_dist - 5.0;
                }
                abs_r = 0.0;
                nr_abs_r = 0.0;
                if (������.������ && (������.���������������� <= 80.0))
                {
                    // HACK: num = 10.0;
                    abs_r = ������.����������������;
                }
                if ((_��������������� != null) && (_���������������.������) && (_���������������.���������������� <= 80.0))
                {
                    nr_abs_r = _���������������.����������������;
                }
                if ((_��������������� != null) && (nr_abs_r == 0.0))
                {
                    nr_lenght = _���������������.�����;
                    ost_dist += nr_lenght;
                    nr_abs_r = 0;
                }
                // TODO: why div 5.0? <-- ������ �������� ���������!
                int width0m5 = Math.Max((int)Math.Round(������.������[0] / Road.������������), 1);
                int width1m5 = Math.Max((int)Math.Round(������.������[1] / Road.������������), 1);
                double current_width = ������.�����������(���������.����������);
                int shiftLine0 = Math.Max((int)Math.Floor((width0m5 * (���������.���������� + (current_width / 2.0))) / current_width), 0);
                int shiftLine1 = Math.Max((int)Math.Floor((width1m5 * (���������.���������� + (current_width / 2.0))) / current_width), 0);
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
                //TODO: ���� ��������� � �� ���������
                foreach (var obj2 in ������.objects)
                {
                    if (obj2 is Stop)
                    {
                        var ��������� = (Stop)obj2;
                        if ((((���������.distance - ���������.����������) <= 50.0) || //10.0
                             ((���� != null) && !���������.������������(����.pathes))) ||
                             ((������� == null) || (!���������.typeOfTransport[�������.typeOfTransport])))// || (���������!=nextStop))
                            continue;
                        base.SearchForCurrentStop(���������);
                        if (��������� != nextStop) continue;
                        stops_dist = Math.Min(stops_dist, ���������.distance - ���������.����������);
                        ���������������� = ���������;
                        currentStop = ���������;
                        continue;
                    }
                    else if (obj2 is Visual_Signal/*����������_�������.������*/)
                    {
                        var ������ = (Visual_Signal/*����������_�������.������*/)obj2;
                        if ((������.�������.������ == �������.�������) && ((������.���������.���������� - ���������.����������) > 10.0))
                        {
                            signals_dist = Math.Min(signals_dist, (������.���������.���������� - ���������.����������) - 10.0);
                        }
                        continue;
                    }
                    if (!(obj2 is �����������_������)) continue;
                    var ������2 = (�����������_������)obj2;
                    var num15 = ������2.���������� - ���������.����������;
                    if (((������2.������ == �������.�������) && (num15 > 0.0)) || ((������2.������ == �������.Ƹ����) && (num15 > 10.0)))
                    {
                        signals_dist = Math.Min(signals_dist, num15 - 5.0);
                    }
                }
                if (_��������������� != null)
                {
                    foreach (var obj3 in _���������������.objects)
                    {
                        if (obj3 is Stop)
                        {// continue;
                            var ���������2 = (Stop)obj3;
                            if (((���������2 == nextStop) && (���������2.typeOfTransport[�������.typeOfTransport])) && (((���� == null) || ���������2.������������(����.pathes)) && (������� != null)))
                            {
                                stops_dist = Math.Min(stops_dist, (������.����� - ���������.����������) + ���������2.distance);
                            }
                            continue;
                        }
                        else if (obj3 is Visual_Signal/*����������_�������.������*/)
                        {
                            var ������2 = (Visual_Signal/*����������_�������.������*/)obj3;
                            if (������2.�������.������ == �������.�������)
                            {
                                signals_dist = Math.Min(signals_dist, ((������.����� - ���������.����������) + ������2.���������.����������) - 30.0);
                            }
                            continue;
                        }
                        if (!(obj3 is �����������_������)) continue;
                        var ������21 = (�����������_������)obj3;
                        var num16 = (������.����� - ���������.����������) + ������21.����������;
                        if ((������21.������ == �������.�������) || ((������21.������ == �������.Ƹ����) && (num16 > 10.0)))//10
                        {
                            signals_dist = Math.Min(signals_dist, num16 - 5.0);
                        }
                    }
                }
                /*var list = new List<���������>(������.����������������);
                var list2 = new List<���������>();
                if (_��������������� != null)
                {
                    list2 = new List<���������>(_���������������.����������������);
                }*/
                foreach (var ���������1 in ������.����������������)//list)
                {
                    if ((���������1.comment == this) || (���������1.���������� <= ���������.����������)) continue;
                    var new_width = ������.�����������(���������1.����������);
                    var shiftLineTmp = (int)Math.Floor((width1m5 * (���������1.���������� + (new_width / 2.0))) / new_width);
                    if (shiftLineTmp == shiftLine1)
                    {
                        some_other_fucking_distance = Math.Min(some_other_fucking_distance, ���������1.���������� - ���������.����������);
                    }
                    if ((shiftLineTmp >= 0) && (shiftLineTmp < dist_by_lines.Length))
                    {
                        dist_by_lines[shiftLineTmp] = Math.Min(dist_by_lines[shiftLineTmp], ���������1.���������� - ���������.����������);
                    }
                }
                if (_��������������� != null)
                {
                    foreach (var ���������2 in _���������������.����������������)//list2)
                    {
                        if (���������2.comment == this) continue;
                        if (_��������������� == null) continue;
                        var num13 = _���������������.�����������(���������2.����������);
                        var shiftLineTmp = (int)Math.Floor((width1m5 * (���������2.���������� + (num13 / 2.0))) / num13);
                        if (shiftLineTmp == shiftLine1)
                        {
                            some_other_fucking_distance = Math.Min(some_other_fucking_distance, (������.����� - ���������.����������) + ���������2.����������);
                        }
                        if ((shiftLineTmp >= 0) && (shiftLineTmp < dist_by_lines.Length))
                        {
                            dist_by_lines[shiftLineTmp] = Math.Min(dist_by_lines[shiftLineTmp], (������.����� - ���������.����������) + ���������2.����������);
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
                var newShift0 = ((������.������[0] * (shiftLine0 + 0.5)) / width0m5) - (������.������[0] / 2.0);
                var newShift1 = ((������.������[1] * (shiftLine1 + 0.5)) / width1m5) - (������.������[1] / 2.0);
                var rec_shift = newShift0 + (((newShift1 - newShift0) * ���������.����������) / ������.�����);
                var shiftAcc = 0.3;
                if ((stops_dist < 40.0) || (��������_������ > 0.0))
                {
                    rec_shift = 0.5 - (current_width / 2.0);
                    if (���������.���������� < (2.0 - (current_width / 2.0)))
                    {
                        rec_shift = 2.0 - (current_width / 2.0);
                        shiftAcc = 0.2;
                    }
                    ���������_�������� = 1;
                    signals_dist = Math.Min(signals_dist, stops_dist + 20.0);
                }
                var point2 = new DoublePoint(������.����������������(���������.����������) - direction);
                var target_direction = -point2.Angle;
                if (Math.Abs(���������.���������� - rec_shift) > shiftAcc)
                {
                    target_direction += 0.08 * (���������.���������� - rec_shift);
                }
                if (���������.���������� < ((-current_width / 2.0) + 1.0))
                {
                    target_direction -= 0.3;
                }
                else if (���������.���������� > ((current_width / 2.0) - 1.0))
                {
                    target_direction += 0.3;
                }
                if (Math.Abs(target_direction) < 0.001)
                {
                    target_direction = 0.0;
                }
                if (MainForm.in_editor)
                {
                    ����������� = target_direction;
                    ����������� = target_direction;
                }
                else
                {
                    if (����������� < target_direction)
                    {
                        ����������� += 0.3 * World.�������������;
                        if (����������� > target_direction)
                        {
                            ����������� = target_direction;
                        }
                    }
                    else if (����������� > target_direction)
                    {
                        ����������� -= 0.3 * World.�������������;
                        if (����������� < target_direction)
                        {
                            ����������� = target_direction;
                        }
                    }
                    /* if (����������� < target_direction)
                     {
                         ����������� += 0.3 * World.�������������;
                         if (����������� > target_direction)
                         {
                             ����������� = target_direction;
                         }
                     }
                     else if (����������� > target_direction)
                     {
                         ����������� -= 0.3 * World.�������������;
                         if (����������� < target_direction)
                         {
                             ����������� = target_direction;
                         }
                     }*/
                }

            }
            var flag = false;
            /*var flag2 = false;
            if ((���� != null) && (����.inPark || ((����.������_����������� == ����.�����) && (���.time < ����.�����_�����������))))
            {
                flag2 = true;
            }*/
            if ((���� != null) && (����.inPark || ((����.������_����������� == ����.�����) && (���.time < ����.�����_�����������))))//(flag2)
            {
                foreach (var ������ in ����.����_�������)
                {
                    if (������ != ���������.������) continue;
                    flag = (�������� == 0.0);
                    var num24 = (������.����� - ���������.����������) - 20.0;
                    signals_dist = Math.Min(signals_dist, num24);
                    //                    ost_dist = Math.Min(ost_dist, num24);
                    break;
                }
            }
            var flag3 = true;
            var index = -1;
            //TODO:� ��� ����... ���� ��������� �� ������������, ������, ���� ���� ��� �������� (��������, ���� ������ ����� �������� ��� �� �� ��������)
            foreach (var ������ in ������)
            {
                if (flag && (��������� <= 0.0))
                {
                    ������.����������� = false;
                    ������.������ = null;
                    flag3 = false;
                }
                else if ((������.������ != null) && (!������.������.������������ || (�������� != 0.0)))
                {
                    if (������.�������)
                    {
                        shtangi_ost_dist = ������.������.����� - ������.�����������������������������;
                        if (������.������.���������_�������.Length == 1)
                        {
                            shtangi_ost_dist += ������.������.���������_�������[0].�����;
                        }
                        if (ost_dist > shtangi_ost_dist)
                        {
                            ost_dist = shtangi_ost_dist;
                            dont_stop = false;
                        }
                        if ((������.������.���������_�������.Length > 1) && (������.����������������������������� > (������.������.����� - 2.0)))
                        {
                            var next_wires = new List<����������_������>(������.������.���������_�������);
                            if ((���������.������ != null) && (_��������������� != null))
                            {
                                Road ������2 = null;
                                if (_���������������.���������������.Length == 1)
                                {
                                    ������2 = _���������������.���������������[0];
                                }
                                var list4 = new List<����������_������>(next_wires);
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
                                        while (list5[n] > list4[n].�����)
                                        {
                                            (list6 = list5)[num33 = n] = list6[num33] - list4[n].�����;
                                            if (list4[n].���������_�������.Length > 0)
                                            {
                                                for (var num30 = 1; num30 < list4[n].���������_�������.Length; num30++)
                                                {
                                                    next_wires.Add(next_wires[n]);
                                                    list4.Add(list4[n].���������_�������[num30]);
                                                    list5.Add(list5[n] - 10.0);
                                                }
                                                list4[n] = list4[n].���������_�������[0];
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
                                            if (((���.�����_���������(pos, ���������.������).������ == null)
                                                 && (���.�����_���������(pos, _���������������).������ == null))
                                                 && ((������2 == null) || (���.�����_���������(pos, ������2).������ == null)))
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
                                next_wires.AddRange(������.������.���������_�������);
                            }
                            var ������ = next_wires[Cheats._random.Next(next_wires.Count)];
                            index = ������ == ������.������.���������_�������[0] ? 0 : 1;
                        }
                        foreach (var obj6 in ������.������.objects)
                        {
                            if (!(obj6 is ������)) continue;
                            var ������2 = (������)obj6;
                            if (������2.����������������������������� > ������.�����������������������������)
                            {
                                some_other_fucking_distance = Math.Min(some_other_fucking_distance, ������2.����������������������������� - ������.�����������������������������);
                            }
                        } //TODO:
                        ����������_������ ������2 = null;
                        if (������.������.���������_�������.Length == 1)
                        {
                            ������2 = ������.������.���������_�������[0];
                        }
                        else if ((index >= 0) && (index <= ������.������.���������_�������.Length - 1))
                        {
                            ������2 = ������.������.���������_�������[index];
                        }
                        if (������2 != null)
                        {
                            foreach (var obj7 in ������2.objects)
                            {
                                if (!(obj7 is ������)) continue;
                                var ������3 = (������)obj7;
                                some_other_fucking_distance = Math.Min(some_other_fucking_distance, (������.������.����� - ������.�����������������������������) + ������3.�����������������������������);
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
                    var ������3 = ������.������;
                    ������.�����������(���.�����������������);
                    if (������.������ != null)
                    {
                        if (������.�������)
                        {
                            ������.����������� = true;
                        }
                        else
                        {
                            ������.����������� = false;
                            ������.��������������� = 0;
                            ������.������ = ������3;
                            �� = true;
                        }
                        flag3 = false;
                    }
                }
            }
            if (((������.Length == 0) && flag) && (��������� <= 0.0))
            {
                flag3 = false;
            }
            ������� = flag3;
            base.stand_brake = flag;
            var ��_����� = (���� == null) || (���.time >= ����.�����_�����������);
            if (!��_����� && (���������.������ != null))
            {
                signals_dist = Math.Min(signals_dist, (���������.������.����� - ���������.����������) - 15.0);
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
            ������������(false);
            if ((��������_������ > 0.0) && (stops_dist > 20.0))
            {
                ost_dist = 0.0;
                recomend_speed = 0.0;
                if (��������_abs < 0.1)
                {
                    base.stand_brake = true;
                    if (�����_�_���������_�������)
                    {
                        if (!flag && ��_�����)
                        {
                            ��������_������ -= World.�������������;
                            if (��������_������ <= 0.0)
                            {
                                �����_�_���������_������� = false;
                            }
                        }
                    }
                    else
                    {
                        ������������(true);
                        if (�����_������� && ��_�����)
                        {
                            ��������_������ -= World.�������������;
                        }
                        //TO_DO: ����� ��������� ���������
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

                s1 = (��������_abs - nr_rec_speed) / tg;
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
                if ((uklon < 0.0) && (��������_abs - recomend_speed > 1.5))
                {
                    recomend_speed = 0.0;
                }
                //                if (recomend_speed > 16.0) recomend_speed = 16.0;
                if (stops_dist <= 20)
                {
                    ��������_������ = 8.0 + (Cheats._random.NextDouble() * 5.0);
                    if ((���������������� != null) && ����������������.serviceStop)
                    {
                        ��������_������ = 1.0 + (Cheats._random.NextDouble() * 3.0);
                        �����_�_���������_������� = true;
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
            if ((!�����_�������) || (signals_dist < 10.0) || (some_other_fucking_distance < 10.0))
            {
                recomend_speed = 0.0;
            }
            �������_����������.�������������_���������(recomend_speed, ost_dist, index);
        }

        public bool ���������
        {
            get
            {
                if ((!������_�������) || (������_����������))
                {
                    if (�� == null)
                    {
                        return ((!base.�������) || (�� == null) || (!��.�������));//true;
                    }
                    if (��.����)
                    {
                        return ((!������_�������) && (��.�������) && (base.�������));
                    }
                }

                return !base.�������;
            }
        }

        public override double ���������
        {
            get
            {
                return base.�������_����������.���������;
            }
        }

        public bool ������_�������
        {
            get
            {
                foreach (������ ������ in this.������)
                {
                    if (!������.�������)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool ������_����������
        {
            get
            {
                if (!������_�������) return true;
                foreach (������ ������ in this.������)
                {
                    if (������.������.������������)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public class ��
        {
            private const double r = 0.20;
            private const double z = 0.17;
            public bool ������� = false;
            public bool ���� = false;
            public double ������_�������;
            public double �������_�������;
            public double ���������;
            public double ������;
            public ���������� ����������;

            public ��(���������� ����������, double ������_�������, double ���������, double ������)
            {
                this.���������� = ����������;
                this.��������� = ���������;
                this.������ = ������;
                this.������_������� = ������_�������;
                this.�������_������� = ������_�������;

                if (this.��������� > 0.0)
                {
                    this.��������� = Math.Min(1.0, this.���������);
                }
                else this.��������� = Cheats._random.NextDouble();
            }

            public bool ����������
            {
                get
                {
                    return ((((!�������) && (����������.�������)) &&
                         ((����������.�������_���������� is �������_����������.����_����������) &&
                         (����������.������_������� && !����������.������_����������))) &&
                         (�������_������� < ������_�������));
                }
            }

            public void Simulation()
            {
                if ((�������) && (����������.������_������� || !����������.������_����������)) ������� = false;
                if ((�������) && (������_������� > 0.0))
                {
                    �������_������� -= ������ * Math.Abs(����������.�������_����������.���������) * World.�������������;
                    if (�������_������� <= 0.0)
                    {
                        �������_������� = 0.0;
                        ������� = false;

                    }
                }
                else if (����������)
                {
                    �������_������� += z * World.�������������;
                    �������_������� = Math.Min(�������_�������, ������_�������);//if (�������_������� >= e) �������_������� = e;
                }
            }

        }

        public class ������ : MeshObject, MeshObject.IFromFile, IMatrixObject
        {
            private string _file = "";
            public DoublePoint ������������������;
            public Double3DPoint ����������;
            public bool �����;
            public double �������;
            public double ��������������������;
            public double ������ = 0.628;
            public ��������� ����������������;

            public ������(bool �����, string dir, string filename, double ������)
            {
                this.����� = �����;
                meshDir = dir;
                _file = filename;
                this.������ = ������;
            }

            public Matrix GetMatrix(int index)
            {
                Matrix matrix;
                if (!�����)
                {
                    matrix = ((Matrix.RotationZ(-((float)(�������������������� / ������))) * Matrix.RotationY(-((float)�������))) * Matrix.RotationZ((float)������������������.y)) * Matrix.RotationY(-((float)������������������.x));
                }
                else
                {
                    matrix = ((Matrix.RotationZ((float)(�������������������� / ������)) * Matrix.RotationY(-((float)�������))) * Matrix.RotationZ((float)������������������.y)) * Matrix.RotationY(-((float)(������������������.x + Math.PI)));
                }
                return (matrix * Matrix.Translation((float)����������.x, (float)����������.y, (float)����������.z));
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

            public DoublePoint �����������
            {
                get
                {
                    return new DoublePoint(������������������.x + �������, ������������������.y);
                }
            }
        }

        public class ����������������� : ����������, I��������������3D, IVector
        {
            public ����������[] ���������� = new ����������[0];
            public ����� �����;
            public ����������[] ���������� = new ����������[0];
            public �����[] ������ = new �����[0];

            public �����������������(���������������� ������, Double3DPoint ����������, DoublePoint �����������, ���������� ����������, ���� ����, Route �������, Order �����)
            {
                this.������ = ������;
                ��������_����� = ������.dir;
                ����� = new �����(this) { ���������� = ����������, ����������� = ����������� };
                base.���������� = ����������;
                base.������� = �������;
                this.����� = �����;
                base.���� = ����;
                ������ = new �����[(int)((������.�����Dist1.Length + ������.�����Dist2.Length) / 2)];//[������.�����������������];
                ���������� = new ����������[������.Length];//������.�����������������];
                var num = 0.0;
                for (var i = 0; i < ������.Length; i++)
                {
                    num -= ������.�����Dist1[i] + ������.�����Dist2[i];
                    ������[i] = new �����(this)
                    {
                        ���������� =
                                            �����.���������� +
                                            new Double3DPoint(�����.�����������) * num,//�����.�����������
                        ����������� = �����.�����������
                    };
                    ����������[i] = new ����������(this);
                }
                for (int i = 0; i < ������.����������������.Length; i++)
                {
                    width = Math.Max(width, Math.Abs(������.����������������[i].y));
                    length0 = Math.Max(length0, Math.Abs(������.����������������[i].x));
                    length1 = Math.Min(length1, ������.����������������[i].x);
                }
                length1 = -length1;
                if (������.�����������������������.Length > 0)
                {
                    length1 = 0.0;
                    for (int i = 0; i < ������.�����������������������[������.�����������������������.Length - 1].Length; i++)
                    {
                        length1 = Math.Max(length1, Math.Abs(������.�����������������������[������.�����������������������.Length - 1][i].x));
                    }
                    length1 -= num;
                }
                ���������� = new ����������[������.����������.Length];
                for (int j = 0; j < ������.����������.Length; j++)
                {
                    this.����������[j] = new ����������(this.�����_�����(������.����������[j].�����), ������.����������[j].filename, ������.����������[j].���);
                }
                _���������� = ������.����������;
                _����� = new ������[2 * ������.�����������.Length];
                for (var k = 0; k < ������.�����������.Length; k++)
                {
                    _�����[2 * k] = new ������(true, ������.�����������[k].dir, ������.�����������[k].filename, base._����������);
                    _�����[(2 * k) + 1] = new ������(false, ������.�����������[k].dir, ������.�����������[k].filename, base._����������);
                }
                ������ = new ������[������.������.Length];
                for (var m = 0; m < ������.Length; m++)
                {
                    ������[m] = new ������(m == 0, ������.������Dir, ������.������Filename, ������.�����������������, ������.����������Min);
                }
                if (������.���� != null)
                {
                    ���� = new ����(
                        ������.����.dir,
                        ������.����.filename,
                        ������.����.pos,
                        ������.����.angle,
                        this.�����);
                }
                if (������.�� != null)
                {
                    �� = new ��(this, ������.��.������_�������, ������.��.���������, ������.��.������);
                }
                if (������.�������� != null)
                {
                    ��������_�_���� = new �������������(this);
                }
                �����������������������();
                foreach (var ������ in ������)
                {
                    ������.����������� = ������.������������������;
                }
                _���������������� = ������.����������������;
                _����� = new �����[������.�����.Length];
                for (var n = 0; n < ������.�����.Length; n++)
                {
                    _�����[n] = �����.���������(������.�����[n].������, �����_�����(������.�����[n].�����), ������.�����[n].p1, ������.�����[n].p2, ������.�����[n].������);
                    _�����[n].������������� = ������.�����[n].�������������;
                    _�����[n].����� = ������.�����[n].�����;
                }
                ���������_������ = new ���������������();
                �������_���������� = �������_����������.Parse(������.�����������������, this);
                if (!������.hasnt_bbox)
                {
                    �����.bounding_sphere = new Sphere(������.bsphere.pos, ������.bsphere.radius);
                    for (var i = 0; i < ������.Length; i++)
                    {
                        ������[i].bounding_sphere = new Sphere(������.tails_bsphere[i].pos, ������.tails_bsphere[i].radius);
                    }
                }
                else
                {
                    �����.bounding_sphere = new Sphere(Double3DPoint.Zero, 8.0);
                    for (var i = 0; i < ������.Length; i++)
                    {
                        ������[i].bounding_sphere = new Sphere(Double3DPoint.Zero, 8.0);
                    }
                }
                LoadCameras();
            }

            public override void CreateMesh(World ���)
            {
                if (���.filename != null)
                {
                    var strArray = new[]
                                       {
                                           Application.StartupPath + @"\Cities\" +
                                           Path.GetFileNameWithoutExtension(���.filename) + @"\" + ����.�������� + @"\",
                                           Application.StartupPath + @"\Cities\" +
                                           Path.GetFileNameWithoutExtension(���.filename) + @"\"
                                       };
                    �����.extraMeshDirs = strArray;
                    foreach (var ����� in ������)
                    {
                        �����.extraMeshDirs = strArray;
                    }
                    foreach (var ���������� in ����������)
                    {
                        ����������.extraMeshDirs = strArray;
                    }
                    foreach (var ������ in _�����)
                    {
                        ������.extraMeshDirs = strArray;
                    }
                    foreach (var ������ in ������)
                    {
                        ������.extraMeshDirs = strArray;
                    }
                    foreach (var ����� in _�����)
                    {
                        �����.ExtraMeshDirs = strArray;
                    }
                    ���������_������.extraMeshDirs = strArray;
                    //                    if (�������� != null)
                    //                      {
                    //                        ��������.extraMeshDirs = strArray;
                    //                    }
                }
                �����.CreateMesh();
                �����.���������������������������(�������.number, �����.�����);
                foreach (var �����2 in ������)
                {
                    �����2.CreateMesh();
                    �����2.���������������������������(�������.number, �����.�����);
                }
                foreach (var ����������2 in ����������)
                {
                    ����������2.CreateMesh();
                }
                foreach (var ���������� in ����������)
                {
                    ����������.CreateMesh();
                }
                foreach (var ������2 in _�����)
                {
                    ������2.CreateMesh();
                }
                foreach (var ������2 in ������)
                {
                    ������2.CreateMesh();
                }
                foreach (var �����2 in _�����)
                {
                    �����2.CreateMesh();
                }
                ���������_������.CreateMesh();
                if (����� != null)
                {
                    ���������_������.����������������(base.�����);
                }
                if (���� != null)
                {
                    ����.CreateMesh();
                }
                if (��������_�_���� != null)
                {
                    ��������_�_����.CreateMesh();
                }
            }

            protected override void CheckCondition()
            {
                var cnd = !base.condition;
                �����.IsNear = cnd;
                foreach (var ����� in ������)
                {
                    �����.IsNear = cnd;
                }
                if (!cnd) return;
                foreach (var ���������� in ����������)
                {
                    ����������.IsNear = true;
                }
                foreach (var ������ in _�����)
                {
                    ������.IsNear = true;
                }
                foreach (var ������ in ������)
                {
                    ������.IsNear = true;
                }
                if (���� != null)
                {
                    ����.IsNear = true;
                }
                foreach (var ����� in _�����)
                {
                    �����.CheckCondition();
                }
                foreach (var ���������� in ����������)
                {
                    ����������.IsNear = true;
                }
                if (����� != null)
                {
                    ���������_������.IsNear = true;
                }
                if (��������_�_���� != null)
                {
                    ��������_�_����.IsNear = true;
                }
            }

            public override void Render()
            {
                CheckCondition();
                if (condition) return;
                var visible = false;
                var lod = 2;
                if (MyDirect3D.SphereInFrustum(�����.bounding_sphere))
                {
                    visible = true;
                    lod = Math.Min(�����.bounding_sphere.LODnum, lod);
                    �����.Render();
                }
                foreach (var ����� in ������)
                {
                    //                       if (!(MyDirect3D.AABBInFrustum(�����.bounding_box))) continue;
                    if (!(MyDirect3D.SphereInFrustum(�����.bounding_sphere))) continue;
                    visible = true;
                    lod = Math.Min(�����.bounding_sphere.LODnum, lod);
                    �����.Render();
                }
                if ((!visible) || (lod > 0)) return;
                foreach (var ���������� in ����������)
                {
                    ����������.Render();
                }
                foreach (var ������ in _�����)
                {
                    ������.Render();
                }
                foreach (var ������ in ������)
                {
                    ������.Render();
                }
                if (���� != null)
                {
                    ����.Render();
                }
                foreach (var ����� in _�����)
                {
                    �����.Render();
                }
                foreach (var ���������� in ����������)
                {
                    if ((����������.��� == ���_����������.����) && ��������_����)
                    {
                        ����������.Render();
                    }
                    if (����������������� < ���������������������)
                    {
                        if ((����������.��� == ���_����������.�����) && ((���������_�������� < 0) || ���������_������������))
                        {
                            ����������.Render();
                        }
                        if ((����������.��� == ���_����������.������) && ((���������_�������� > 0) || ���������_������������))
                        {
                            ����������.Render();
                        }
                    }
                    if ((����������.��� == ���_����������.������) && (�������_����������.���_���_������ < 0))
                    {
                        ����������.Render();
                    }
                    if ((����������.��� == ���_����������.�����) && (base.�������_����������.����������� < 0))
                    {
                        ����������.Render();
                    }
                }
                if (����� != null)
                {
                    ���������_������.matrix = Matrix.Translation((float)������.�����Pos.x, (float)������.�����Pos.y, (float)������.�����Pos.z) * �����.last_matrix;//.GetMatrix(0);//(float)������.�����Pos.x, (float)������.�����Pos.y, (float)������.�����Pos.z  �����.GetMatrix(0)
                    ���������_������.Render();
                }
                if (��������_�_���� != null)
                {
                    ��������_�_����.matrix = Matrix.Translation((float)������.��������.pos.x, (float)������.��������.pos.y, (float)������.��������.pos.z) * �����.last_matrix;//.GetMatrix(0); //((float)������.��������.pos.x, (float)������.��������.pos.y, (float)������.��������.pos.z) * �����.GetMatrix(0);
                    ��������_�_����.Render();
                }
            }

            public override void UpdateBoundigBoxes(World world)
            {
                �����.bounding_sphere.Update(�����.����������, �����.�����������);
                foreach (var ����� in ������)
                {
                    �����.bounding_sphere.Update(�����.����������, �����.�����������);
                }
            }

            public override ���������[] �����������������(World ���)
            {
                //                List<���������> list = new List<���������>();
                base.���������_���������.Clear();
                Double3DPoint point = new Double3DPoint(this.�����.�����������);
                Double3DPoint point2 = Double3DPoint.Rotate(this.�����.�����������, (Math.PI / 2.0));
                int length = this.������.����������������.Length;
                for (int i = 0; i < this.������.�����������������������.Length; i++)
                {
                    length += this.������.�����������������������[i].Length;
                }
                Double3DPoint[] pos = new Double3DPoint[length];
                int index = 0;
                int num4 = 0;
                while (num4 < this.������.����������������.Length)
                {
                    pos[index] = (Double3DPoint)((this.�����.���������� + (point * this.������.����������������[num4].x)) + (point2 * this.������.����������������[num4].y));
                    num4++;
                    index++;
                }
                for (int j = 0; j < this.������.�����������������������.Length; j++)
                {
                    point = new Double3DPoint(this.������[j].�����������);
                    point2 = Double3DPoint.Rotate(this.������[j].�����������, (Math.PI / 2.0));
                    int num6 = 0;
                    while (num6 < this.������.�����������������������[j].Length)
                    {
                        pos[index] = (Double3DPoint)((this.������[j].���������� + (point * this.������.�����������������������[j][num6].x)) + (point2 * this.������.�����������������������[j][num6].y));
                        num6++;
                        index++;
                    }
                }
                ���������[] collection = ���.�����_���_���������(pos);
                for (int k = 0; k < collection.Length; k++)
                {
                    collection[k].comment = this;
                }
                base.���������_���������.AddRange(collection);
                //                base.���������_��������� = list;
                return base.���������_���������.ToArray();
            }

            public ���������������� �����_�����(int index)
            {
                if (index > 0)
                {
                    return this.������[index - 1];
                }
                if (index < 0)
                {
                    return this.����������[-index - 1];
                }
                return this.�����;
            }

            public override void ��������(World ���, �����[] ������_�_����)
            {
                ArrayList list = new ArrayList();
                double direction_prev = this.direction;
                base.����������� = Math.Min(Math.Max(base.�����������, -0.78539816339744828), 0.78539816339744828);
                if (������_�_���� != null)
                {
                    for (int i = 0; i < ������_�_����.Length; i++)
                    {
                        if (������_�_����[i].�������������� == this)
                        {
                            list.Add(������_�_����[i]);
                        }
                    }
                }
                if (list.Count > 0)
                {
                    int num3;
                    I��������������3D[] �_dArray = new I��������������3D[list.Count];
                    int index = 0;
                    foreach (����� ����� in list)
                    {
                        double[] numArray = new double[this.������.Length];
                        num3 = 0;
                        while (num3 < this.������.Length)
                        {
                            //                            DoublePoint point = �����.cameraPosition.xz_point - this.������[num3].����������.xz_point;
                            numArray[num3] = (�����.cameraPosition.XZPoint - this.������[num3].����������.XZPoint).Modulus;//point.������;
                            num3++;
                        }
                        double[] numArray2 = new double[this.����������.Length];
                        num3 = 0;
                        while (num3 < this.����������.Length)
                        {
                            DoublePoint point2 = �����.cameraPosition.XZPoint - this.����������[num3].����������.XZPoint;
                            numArray2[num3] = (�����.cameraPosition.XZPoint - this.����������[num3].����������.XZPoint).Modulus;//point2.������;
                            num3++;
                        }
                        bool flag = false;
                        num3 = 0;
                        while (num3 < this.����������.Length)
                        {
                            if (numArray2[num3] < 1.3)
                            {
                                �_dArray[index] = this.����������[num3];
                                flag = true;
                                break;
                            }
                            num3++;
                        }
                        if (!flag)
                        {
                            �_dArray[index] = this.�����;
                            //                            var point3 = �����.cameraPosition.xz_point - this.�����.����������.xz_point;
                            var num5 = (�����.cameraPosition.XZPoint - this.�����.����������.XZPoint).Modulus;//point3.������;
                            for (num3 = 0; num3 < this.������.Length; num3++)
                            {
                                if (numArray[num3] >= num5) continue;
                                num5 = numArray[num3];
                                �_dArray[index] = ������[num3];
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
                    foreach (����� �����2 in list)
                    {
                        pointArray[num3] = �����2.cameraPosition - �_dArray[num3].����������3D;
                        pointArray[num3].XZPoint = pointArray[num3].XZPoint.Multyply(new DoublePoint(-�_dArray[num3].direction));// *= new DoublePoint(-�_dArray[num3].direction);
                        pointArray[num3].XYPoint = pointArray[num3].XYPoint.Multyply(new DoublePoint(-�_dArray[num3].�����������Y));// *= new DoublePoint(-�_dArray[num3].�����������Y);
                        pointArray2[num3] = �����2.������������������ ? �����2.cameraPosition : �_dArray[num3].����������3D;
                        pointArray4[num3] = new DoublePoint(�_dArray[num3].direction, �_dArray[num3].�����������Y);
                        num3++;
                    }
                    this.�����������(base.�������� * World.�������������, ���);
                    num3 = 0;
                    foreach (����� �����3 in list)
                    {
                        pointArray[num3].XYPoint = pointArray[num3].XYPoint.Multyply(new DoublePoint(�_dArray[num3].�����������Y));// *= new DoublePoint(�_dArray[num3].�����������Y);
                        pointArray[num3].XZPoint = pointArray[num3].XZPoint.Multyply(new DoublePoint(�_dArray[num3].direction));// *= new DoublePoint(�_dArray[num3].direction);
                        pointArray[num3].Add(�_dArray[num3].����������3D);// += �_dArray[num3].����������3D;
                        pointArray3[num3] = �����3.������������������ ? pointArray[num3] : �_dArray[num3].����������3D;
                        pointArray5[num3] = new DoublePoint(�_dArray[num3].direction, �_dArray[num3].�����������Y);
                        �����3.cameraPosition.Add(pointArray3[num3] - pointArray2[num3]);// += pointArray3[num3] - pointArray2[num3];
                        if (�����3.������������������)
                        {
                            �����3.cameraRotation.Add(pointArray5[num3] - pointArray4[num3]);// += pointArray5[num3] - pointArray4[num3];
                        }
                        num3++;
                    }
                }
                else
                {
                    this.�����������(base.�������� * World.�������������, ���);
                }

                if ((this.direction != direction_prev) && (base.����������� != 0.0))
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

                    int num7 = Math.Sign((double)(-num6 * base.�����������));
                    if (num7 > 0)
                    {
                        if (Game.space)
                        {
                            base.����������� += num7 * num6;
                            if (Math.Abs(base.�����������) < 0.001)
                            {
                                base.����������� = 0.0;
                            }
                        }
                    }

                }
                this.����������������� += World.�������������;
                while (this.����������������� > this.�����������������Max)
                {
                    this.����������������� -= this.�����������������Max;
                }
                if ((base.���������_�������� != this._�����������������������) || (base.���������_������������ != this._�������������������������))
                {
                    this.����������������� = 0.0;
                }
                this._����������������������� = base.���������_��������;
                this._������������������������� = base.���������_������������;
                base._sound���������� = ((base.�������_����������.���_���_������ > 0) && !base.���������);//false;
                base._sound����������� = ((base.�������_����������.���_���_������ < 0) && !base.���������);//false;
                if (�� != null)
                {
                    ��.Simulation();
                }
                if (Math.Abs(base.��������) < 1E-06)
                {
                    base.�������� = 0.0;
                }
                var num8 = base.��������;
                base.�������� += this.��������� * World.�������������;
                if (((this.��������� * base.��������) < 0.0) && ((base.�������� * num8) < 0.0))
                {
                    base.�������� = 0.0;
                }
                this.�����������������(���);
                foreach (����� ����� in base._�����)
                {
                    �����.��������();
                }
                this.�����������������������();
                foreach (����������.������ ������ in base.������)
                {
                    if (!������.�����������)
                    {
                        ������.����������� += this.direction - direction_prev;
                    }
                    ������.��������(base.�������_����������.������������);//, (float)base.��������_abs);
                }
                base.��������_abs -= 0.1 * World.�������������;
                base.������������();
                UpdateBoundigBoxes(���);
            }

            public override void SetPosition(Road road, double distance, double shift, Double3DPoint pos, DoublePoint rot, World world)
            {
                if (road != null)
                {
                    pos = new Double3DPoint
                    {
                        XZPoint = road.���������������(distance, shift),
                        y = road.�����������(distance)
                    };
                    var pos2 = new Double3DPoint
                    {
                        XZPoint = road.���������������(distance + ������.�����������[0].pos.x, shift),
                        y = road.�����������(distance + ������.�����������[0].pos.x)
                    };
                    rot = (pos2 - pos).Angle;//new DoublePoint(road.����������������(distance), road.����������������Y(distance));
                }
                �����.���������� = pos;
                �����.����������� = rot;
                Double3DPoint point;
                Double3DPoint point2;
                for (var i = 0; i < ������.Length; i++)
                {
                    distance -= ������.�����Dist1[i] + ������.�����Dist2[i];
                    while ((distance < 0.0) && (road.����������������.Length > 0))
                    {
                        road = road.����������������[Cheats._random.Next(0, road.����������������.Length)];
                        distance += road.�����;
                    }

                    point = new Double3DPoint
                    {
                        XZPoint = road.���������������(distance + ������.�����Dist2[i], shift),
                        y = road.�����������(distance + ������.�����Dist2[i])
                    };
                    point2 = new Double3DPoint
                    {
                        XZPoint = road.���������������(distance, shift),
                        y = road.�����������(distance)
                    };
                    pos = ((i == 0) ? �����.���������� : ������[i - 1].����������) + new Double3DPoint((i == 0) ? �����.����������� : ������[i - 1].�����������) * -������.�����Dist1[i];
                    ������[i].���������� = pos + new Double3DPoint((point - point2).Angle) * -������.�����Dist2[i];
                    ������[i].����������� = (pos - ������[i].����������).Angle;
                }
                this.�����������������������();
                this.�����������������(world);
            }

            private void �����������������������()
            {
                Double3DPoint point = new Double3DPoint(�����.�����������);
                point.AngleY += (Math.PI / 2.0);
                for (var i = 0; i < ������.�����������.Length; i++)
                {
                    var ����������� = �����_�����(������.�����������[i].�����);
                    //                    point = new Double3DPoint(�����������.�����������);
                    point.CopyFromAngle(�����������.�����������);
                    point.AngleY += (Math.PI / 2.0);
                    point.Multyply(_����������);
                    var point3 = new Double3DPoint(�����������.�����������).Multyply(������.�����������[i].pos.x);
                    //                    _�����[2 * i].���������� = ((�����������.���������� + (new Double3DPoint(�����������.�����������) * ������.�����������[i].pos.x)) + (Double3DPoint.�������(�����������.�����������, -MyFeatures.halfPI) * ������.�����������[i].pos.y)) + (point * _����������);
                    _�����[2 * i].���������� = �����������.���������� + point3 + Double3DPoint.Rotate(�����������.�����������, -(Math.PI / 2.0)).Multyply(������.�����������[i].pos.y) + point;
                    _�����[2 * i].������������������ = �����������.�����������;
                    //                    _�����[(2 * i) + 1].���������� = ((�����������.���������� + (new Double3DPoint(�����������.�����������) * ������.�����������[i].pos.x)) + (Double3DPoint.�������(�����������.�����������, MyFeatures.halfPI) * ������.�����������[i].pos.y)) + (point * _����������);
                    _�����[(2 * i) + 1].���������� = �����������.���������� + point3 + Double3DPoint.Rotate(�����������.�����������, (Math.PI / 2.0)).Multyply(������.�����������[i].pos.y) + point;
                    _�����[(2 * i) + 1].������������������ = �����������.�����������;
                }
                _�����[0].������� = -�����������;
                _�����[1].������� = -�����������;
                if (���� != null)
                {
                    ����.������� = �����������;
                }
                if (������.Length <= 1) return;
                var point2 = �����.����������;
                var angle = �����.�����������;
                if (������.Length > 0)
                {
                    point2 = ������[������.Length - 1].����������;
                    angle = ������[������.Length - 1].�����������;
                }
                point = new Double3DPoint(angle);
                point.AngleY += (Math.PI / 2.0);
                ������[0].��������� = point2 + new Double3DPoint(angle).Multyply(������.������[0].pos.x) + Double3DPoint.Rotate(angle, -(Math.PI / 2.0)).Multyply(������.������[0].pos.z) + point.Multyply(������.������[0].pos.y);
                ������[1].��������� = Double3DPoint.Multiply(������.������[1].pos, point2, angle);//((point2 + (new Double3DPoint(angle) * ������.������[1].pos.x)) + (Double3DPoint.�������(angle, 1.5707963267948966) * ������.������[1].pos.z)) + (point * ������.������[1].pos.y);
                ������[0].������������������ = angle.x + Math.PI;
                ������[0].�����������Y = -angle.y;
                ������[1].������������������ = angle.x + Math.PI;
                ������[1].�����������Y = -angle.y;
            }

            protected override void ���������������������������()
            {
                �����.���������������������������(�������.number, ����� == null ? "" : �����.�����);
                foreach (var ����� in ������)
                {
                    �����.���������������������������(�������.number, ����� == null ? "" : �����.�����);
                }
            }

            public void �����������������(World ���)
            {
                var pos = (_�����[0].���������� + _�����[1].����������) / 2.0;
                if ((���������.������ != null) && (���.�����_���������(pos, ���������.������).������ != null))
                {
                    ��������� = ���.�����_���������(pos, ���������.������);
                }
                else if ((_��������������� != null) && (���.�����_���������(pos, _���������������).������ != null))
                {
                    this.���������.������.objects.Remove(this);
                    base.��������� = ���.�����_���������(pos, base._���������������);
                    this.���������.������.objects.Add(this);
                    base._��������������� = null;
                }
                else
                {
                    if (this.���������.������ != null)
                    {
                        this.���������.������.objects.Remove(this);
                    }
                    base.��������� = ���.�����_���������_���������(pos.XZPoint, ���.������);
                    if (this.���������.������ != null)
                    {
                        this.���������.������.objects.Add(this);
                    }
                    base._��������������� = null;
                }
            }

            public override void �����������(double ����������, World ���)
            {
                //some new:
                Double3DPoint pos = new Double3DPoint();
                DoublePoint direction;
                //original vars
                Double3DPoint[] posArrayBack0 = new Double3DPoint[1 + this.������.Length];
                Double3DPoint[] posArrayFront0 = new Double3DPoint[1 + this.������.Length];
                double[] numArray = new double[1 + this.������.Length];
                //                posArrayBack0[0] = (Double3DPoint)((base._�����[2].���������� + base._�����[3].����������) / 2.0);
                base._�����[2].����������.CopyTo(ref posArrayBack0[0]);
                posArrayBack0[0].Add(base._�����[3].����������);
                posArrayBack0[0].Divide(2.0);
                //                posArrayFront0[0] = (Double3DPoint)((base._�����[0].���������� + base._�����[1].����������) / 2.0);
                base._�����[0].����������.CopyTo(ref posArrayFront0[0]);
                posArrayFront0[0].Add(base._�����[1].����������);
                posArrayFront0[0].Divide(2.0);
                Double3DPoint point8 = new Double3DPoint();
                Double3DPoint point9 = new Double3DPoint();
                posArrayFront0[0].CopyTo(ref point8);
                point8.Subtract(posArrayBack0[0]);
                numArray[0] = point8.Modulus;
                for (int i = 1; i < posArrayBack0.Length; i++)
                {
                    //                    posArrayBack0[i] = (Double3DPoint)((base._�����[(2 * i) + 2].���������� + base._�����[(2 * i) + 3].����������) / 2.0);
                    base._�����[(2 * i) + 2].����������.CopyTo(ref posArrayBack0[i]);
                    posArrayBack0[i].Add(base._�����[(2 * i) + 3].����������);
                    posArrayBack0[i].Divide(2.0);
                    //                    posArrayFront0[i] = posArrayBack0[i - 1] + (new Double3DPoint(point8.����).Multiply(-this.������.�����Dist1[i - 1]));
                    posArrayBack0[i - 1].CopyTo(ref posArrayFront0[i]);
                    point9.CopyFromAngle(point8.Angle.x, point8.Angle.y);
                    point9.Multyply(-this.������.�����Dist1[i - 1]);
                    posArrayFront0[i].Add(point9);
                    //                    point8 = posArrayFront0[i] - posArrayBack0[i];
                    posArrayFront0[i].CopyTo(ref point8);
                    point8.Subtract(posArrayBack0[i]);
                    numArray[i] = point8.Modulus;
                }
                double[] numArray2 = new double[base._�����.Length / 2];
                for (int j = 0; j < base._�����.Length; j++)
                {
                    //                    base._�����[j].���������� += (Double3DPoint)(new Double3DPoint(base._�����[j].�����������) * ����������);
                    //��� ����������:
                    //                    base._�����[j].����������.Add(new Double3DPoint(base._�����[j].�����������).Multiply(����������));
                    //� ��� ��� ������:
                    direction = base._�����[j].�����������;
                    pos.CopyFromAngle(direction);
                    pos.Multyply(����������);
                    base._�����[j].����������.Add(pos);
                    //
                    base._�����[j].�������������������� += ����������;
                    base._�����[j].����������.CopyTo(ref pos);
                    pos.y -= base._�����[j].������;
                    if (base._�����[j].����������������.������ != null)
                    {
                        base._�����[j].���������������� = ���.�����_���������(pos, base._�����[j].����������������.������);
                    }
                    if (base._�����[j].����������������.������ == null)
                    {
                        ���������[] ���������Array = ���.�����_���_���������(new Double3DPoint[] { pos });//(pointArray5);
                        if (���������Array.Length > 0)
                        {
                            base._�����[j].���������������� = ���������Array[0];
                        }
                    }
                    if (base._�����[j].����������������.������ != null)
                    {
                        numArray2[j / 2] = Math.Max(numArray2[j / 2], base._�����[j].����������.y - base._�����[j].����������������.������);
                        double num3 = base._�����[j].����������������.������.����������������Y(base._�����[j].����������������.����������);
                        if (num3 != 0.0)
                        {
                            //                            base.�������� += Math.Cos(num3 + MyFeatures.halfPI) * 0.03;//* 10 * World.�������������;
                            base.�������� -= Math.Sin(num3) * Math.Cos(_�����[j].����������������.������.����������������(_�����[j].����������������.����������)
                                           - (_�����[j].������������������.x + _�����[j].�������)) * Road.uklon_koef * World.�������������;
                        }
                    }
                    else
                    {
                        numArray2[j / 2] = Math.Max(numArray2[j / 2], base._�����[j].������ + ���.GetHeight(base._�����[j].����������.XZPoint));
                    }
                    base._�����[j].����������.y = numArray2[j / 2];
                }
                Double3DPoint[] posArrayBack = new Double3DPoint[1 + this.������.Length];
                Double3DPoint[] posArrayFront = new Double3DPoint[1 + this.������.Length];
                posArrayBack[0] = (Double3DPoint)((base._�����[2].���������� + base._�����[3].����������) / 2.0);
                posArrayFront[0] = (Double3DPoint)((base._�����[0].���������� + base._�����[1].����������) / 2.0);
                DoublePoint ang43 = new DoublePoint();
                Double3DPoint point12 = new Double3DPoint();
                Double3DPoint point5 = new Double3DPoint();
                for (int m = 0; m < posArrayBack0.Length; m++)
                {
                    if (m > 0)
                    {
                        //                        posArrayBack[m] = (Double3DPoint)((base._�����[(2 * m) + 2].���������� + base._�����[(2 * m) + 3].����������) / 2.0);
                        base._�����[(2 * m) + 2].����������.CopyTo(ref posArrayBack[m]);
                        posArrayBack[m].Add(base._�����[(2 * m) + 3].����������);
                        posArrayBack[m].Divide(2.0);
                        //                        Double3DPoint point11 = pointArray4[m - 1] - pointArray3[m - 1];
                        //                        pointArray4[m] = pointArray3[m - 1] + ((Double3DPoint)(new Double3DPoint((pointArray4[m - 1] - pointArray3[m - 1]).����) * -this.������.�����Dist1[m - 1]));
                        //                        posArrayFront[m] = posArrayBack[m - 1] + (new Double3DPoint(ang43).Multiply(-this.������.�����Dist1[m - 1]));
                        posArrayBack[m - 1].CopyTo(ref posArrayFront[m]);
                        point5.CopyFromAngle(ang43);
                        point5.Multyply(-this.������.�����Dist1[m - 1]);
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
                        double y = point16.������;
                        DoublePoint point17 = new DoublePoint(point2.x, y);
                        double d = point17.����;*/
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
                        base._�����[(2 * m) + 2].�������������������� += md - num9; //������1.�������������������� += point3.������ - num9;
                        base._�����[(2 * m) + 3].�������������������� += md - num9; //������3.�������������������� += point3.������ - num9;

                        posArrayFront[m].CopyTo(ref point12);
                        point12.Subtract(posArrayBack[m]);
                    }
                    point12.Angle.CopyTo(ref ang43);
                    point5.CopyFromAngle(ang43);
                    if (m == 0)
                    {
                        this.�����.���������� = posArrayBack[m] + (new Double3DPoint(ang43).Multyply(-this.������.�����������[m + 1].pos.x));
                        this.�����.����������� = ang43;
                        Double3DPoint point4 = point5;
                        point4.AngleY += (Math.PI / 2.0);
                        this.�����.����������.Subtract(point4.Multyply(base._����������));
                    }
                    else
                    {
                        //�� � ����� ���� � �� �� ����?
                        posArrayBack[m] = posArrayFront[m] + (new Double3DPoint(ang43).Multyply(-this.������.�����Dist2[m - 1]));
                        this.������[m - 1].���������� = posArrayBack[m] + (new Double3DPoint(ang43).Multyply(-this.������.�����������[m + 1].pos.x));
                        this.������[m - 1].����������� = ang43;
                        //                        point5 = new Double3DPoint(this.������[m - 1].�����������);
                        point5.CopyFromAngle(this.������[m - 1].�����������);
                        point5.AngleY += (Math.PI / 2.0);
                        this.������[m - 1].����������.Subtract(point5.Multyply(base._����������));
                        this.����������[m - 1].���������� = posArrayFront[m];
                        DoublePoint point6 = (posArrayFront[m - 1] - posArrayFront[m]).Angle;
                        DoublePoint point7 = ang43;//point24.����;
                        this.����������[m - 1].�����������.x = (point6.x + point7.x) / 2.0;
                        this.����������[m - 1].�����������.y = (point6.y + point7.y) / 2.0;
                        if (Math.Abs((double)(point6.x - point7.x)) >= Math.PI)
                        {
                            this.����������[m - 1].�����������.x += Math.PI;
                        }
                        if (Math.Abs((double)(point6.y - point7.y)) >= Math.PI)
                        {
                            this.����������[m - 1].�����������.y += Math.PI;
                        }
                        //                        point5 = new Double3DPoint(this.����������[m - 1].�����������);
                        point5.CopyFromAngle(this.����������[m - 1].�����������);
                        point5.AngleY += (Math.PI / 2.0);
                        this.����������[m - 1].����������.Subtract(point5.Multyply(base._����������));
                    }
                }
            }

            public override DoublePoint position
            {
                get
                {
                    return this.�����.����������.XZPoint;
                }
            }

            public override Double3DPoint ����������3D
            {
                get
                {
                    return this.�����.����������;
                }
            }

            public override double direction
            {
                get
                {
                    return this.�����.�����������.x;
                }
            }

            public override double �����������Y
            {
                get
                {
                    return this.�����.�����������.y;
                }
            }

            public override ��������� �������_���������
            {
                get
                {
                    return base.���������;
                }
            }



            public class ���������� : MeshObject, MeshObject.IFromFile, IMatrixObject
            {
                public string file;
                public ���_���������� ���;
                public ���������������� ����������������;

                public ����������(���������������� ����������������, string filename, ���_���������� ���)
                {
                    this.���������������� = ����������������;
                    this.file = filename;
                    this.��� = ���;
                }

                public Matrix GetMatrix(int index)
                {
                    return ����������������.last_matrix;//.GetMatrix(0);
                }

                public string Filename
                {
                    get
                    {
                        base.meshDir = this.����������������.meshDir;
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

            public class ����� : ����������.�����������������.����������������, MeshObject.IFromFile, IMatrixObject
            {
                public �����(����������.����������������� ����������)
                {
                    base.���������� = ����������;
                }

                public string Filename
                {
                    get
                    {
                        base.meshDir = base.����������.������.dir;
                        return base.����������.������.filename;
                    }
                }
            }

            public class ���������� : ����������.�����������������.����������������, MeshObject.IFromFile, IMatrixObject
            {
                public ����������(����������������� ����������)
                {
                    base.���������� = ����������;
                }

                public string Filename
                {
                    get
                    {
                        meshDir = ����������.������.dir;
                        var index = 0;
                        for (var i = 0; i < ����������.����������.Length; i++)
                        {
                            if (����������.����������[i] == this)
                            {
                                index = i;
                                break;
                            }
                        }
                        return ����������.������.����������Filename[index];
                    }
                }
            }

            public class ����� : ����������������, MeshObject.IFromFile, IMatrixObject
            {
                public �����(����������������� ����������)
                {
                    base.���������� = ����������;
                }

                public string Filename
                {
                    get
                    {
                        base.meshDir = base.����������.������.dir;
                        int index = 0;
                        for (int i = 0; i < base.����������.������.Length; i++)
                        {
                            if (base.����������.������[i] == this)
                            {
                                index = i;
                                break;
                            }
                        }
                        return base.����������.������.�����Filename[index];
                    }
                }
            }

            public class ���������������� : MeshObject, I��������������3D, IVector, IMatrixObject
            {
                public Double3DPoint ����������;
                public DoublePoint �����������;
                public ����������������� ����������;

                public Matrix GetMatrix(int index)
                {
                    var matrix = Matrix.RotationZ((float)�����������Y) * Matrix.RotationY(-((float)this.direction));//.�����������.x));
                    last_matrix = (matrix * Matrix.Translation((float)����������.x, (float)����������.y, (float)����������.z));
                    return last_matrix;//(matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
                }

                public void ���������������������������(string �������, string �����)
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
                            if (File.Exists(str3 + transport + filename + ������� + "-" + ����� + ext))
                            {
                                flag = false;
                                LoadTexture(i, transport + filename + ������� + "-" + ����� + ext);
                                break;
                            }
                            if (!File.Exists(str3 + transport + filename + ������� + ext)) continue;
                            flag = false;
                            LoadTexture(i, transport + filename + ������� + ext);
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
                        return this.����������.XZPoint;
                    }
                }

                public double direction
                {
                    get
                    {
                        return this.�����������.x;
                    }
                }

                public Double3DPoint ����������3D
                {
                    get
                    {
                        return this.����������;
                    }
                }

                public double �����������Y
                {
                    get
                    {
                        return �����������.y;
                    }
                }
            }
        }

        public class ������ : MeshObject, MeshObject.IFromFile, IMatrixObject, IVector
        {
            private string _file = "";
            private ����������_������ _f������;
            public double ������������������;
            public double ����� = 6.066;
            public double �����������;
            public double �����������Y;
            public Double3DPoint _���������;
            public DoublePoint _���������XZ;
            public bool �����������;
            public double ����������� = 6.46;
            public bool ������;
            public double ���������������;
            public double ����;
            public double ����Max = 0.3;
            public double ����Min = -0.351;
            public double ����Normal;
            Transport _trransport;

            public ������(bool ������, string dir, string filename, double �����������, double ����Min)
            {
                this.������ = ������;
                meshDir = dir;
                _file = filename;
                this.����������� = �����������;
                this.����Min = ����Min;
                ���� = ����Min;
            }

            public Matrix GetMatrix(int index)
            {
                return (((Matrix.RotationZ((float)����) * Matrix.RotationY((float)(������������������ - �����������))) * Matrix.RotationZ((float)�����������Y)) * Matrix.RotationY(-((float)������������������)) * Matrix.Translation((float)���������.x, (float)���������.y, (float)���������.z));
            }

            public void �����������(����������_������[] �����������������)
            {
                double num = 1000.0;
                DoublePoint point = _���������XZ;

                double num2;
                // FIXME: � ��� �� ����� �����, ���...
                foreach (var ������1 in �����������������)
                {
                    if ((������1.������ == ������) && !������1.������������)
                    {
                        point = _���������XZ - ������1.������;
                        point.Angle -= ������1.�����������;
                        if (Math.Abs(point.y) <= �����)
                        {
                            num2 = Math.Sqrt((����� * �����) - (point.y * point.y));
                            if (((point.x + num2) >= 0.0) && ((point.x + num2) < ������1.�����))
                            {
                                var point2 = ������1.������ + new DoublePoint(������1.�����������) * (point.x + num2);
                                var point3 = point2 - _���������XZ;
                                point3.Angle -= ������������������;
                                if (Math.Abs(point3.Angle) <= (Math.PI / 2.0))
                                {
                                    var point6 = point2 - position;
                                    if (point6.Modulus < num)
                                    {
                                        ������ = ������1;
                                        var point7 = point2 - position;
                                        num = point7.Modulus;
                                        continue;
                                    }
                                }
                            }
                            if (((point.x - num2) >= 0.0) && ((point.x - num2) < ������1.�����))
                            {
                                var point4 = ������1.������ + new DoublePoint(������1.�����������) * (point.x - num2);
                                var point5 = point4 - _���������XZ;
                                point5.Angle -= ������������������;
                                if (Math.Abs(point5.Angle) <= (Math.PI / 2.0))
                                {
                                    var point8 = point4 - position;
                                    if (point8.Modulus < num)
                                    {
                                        ������ = ������1;
                                        var point9 = point4 - position;
                                        num = point9.Modulus;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public void ��������(bool ����������)
            {
                var dt = 0.5 * World.�������������;
                if (������ != null)
                {
                    var flag = �������;
                    var num = ����;
                    //var troll = Transport();
                    //var Transport = new ����������();
                    ���� = ����Normal;
                    // HACK: ������ �������� ��� �������, �?
                    var point = MyFeatures.ToDouble3DPoint(Vector3.TransformCoordinate(new Vector3(6.65928f, 2.2f, 0f), GetMatrix(0)));
                    ���� = num;
                    var point6 = point.XZPoint - ������.������;
                    var wiredDistance = point6.Modulus;
                    var num3 = ������.FindHeight(wiredDistance) + ����������_������.������_����������_����;
                    var num4 = ����������� * Math.Sin(����Normal - ����Min);
                    num4 += num3 - point.y;
                    if (num4 < 0.0)
                    {
                        num4 = 0.0;
                    }
                    if (num4 > �����������)
                    {
                        num4 = �����������;
                    }
                    ����Normal = Math.Asin(num4 / �����������) + ����Min;
                    ����� = ����������� * Math.Cos(����Normal - ����Min);
                    if (flag)
                    {
                        ���� = ����Normal;
                    }
                }
                var point2 = new DoublePoint(����������� - ������������������);
                if (point2.Angle > (Math.PI / 2.0))
                {
                    ����������� = ������������������ + Math.PI / 2.0;
                }
                else if (point2.Angle < -(Math.PI / 2.0))
                {
                    ����������� = ������������������ - Math.PI / 2.0;
                }
                if (�����������)
                {
                    if ((���� < ����Normal) || (������ == null))
                    {
                        if (���� < ����Normal)
                        {
                            ��������������� = dt;//0.005;
                        }
                        else
                        {
                            ��������������� += (����Max - ����) * dt;
                            ��������������� *= Math.Max(1.0 - dt, 0.0);
                        }
                        ���� += ���������������;
                        if ((���� > ����Normal) && (������ != null))
                        {
                            ���� = ����Normal;
                        }
                    }
                    if (������ != null)
                    {
                        var point3 = _���������XZ - ������.������;
                        point3.Angle -= ������.�����������;
                        if (Math.Abs(point3.y) > �����)
                        {
                            ������ = null;
                        }
                        else
                        {
                            var num5 = Math.Sqrt((����� * �����) - (point3.y * point3.y));
                            var point4 = ������.������ + new DoublePoint(������.�����������) * (point3.x + num5);
                            var point5 = ������.������ + new DoublePoint(������.�����������) * (point3.x - num5);
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
                            point5 = point4 - _���������XZ;
                            point5.Angle -= ������������������;
                            if (Math.Abs(point5.Angle) > (Math.PI / 2.0))
                            {
                                ������ = null;
                            }
                            else
                            {
                                var point9 = new DoublePoint((point5.Angle + ������������������) - �����������);
                                var num7 = point9.Angle;
                                if (���� < ����Normal)
                                {
                                    ����������� += (num7 * ���������������) / ((����Normal - ����) + ���������������);
                                }
                                else
                                {
                                    ����������� += num7;
                                }
                                if (num6 >= ������.�����)
                                {
                                    // TODO: ������� ��� �����
                                    //if ((������ != null) && ((������.����������_�������.Length > 1) && (Transport.�������� > 6.95)))
                                    if (������.���������_�������.Length > 1) //&& (_trransport.�������� >= 6.95))
                                    {
                                        var index = ���������� ? 0 : (������.���������_�������.Length - 1);
                                        ������ = ������.���������_�������[index];
                                    }
                                    else if (������.���������_�������.Length == 1)
                                    {
                                        ������ = ������.���������_�������[0];
                                    }
                                    else
                                    {
                                        ������ = null;
                                    }

                                }
                                else if (num6 < 0.0)
                                {
                                    if (������.����������_�������.Length > 0)
                                    {
                                        var num9 = Cheats._random.Next(������.����������_�������.Length);
                                        ������ = ������.����������_�������[num9];
                                    }
                                    else
                                    {
                                        ������ = null;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // � �����, ���� � ��� null?
                    //                    ������ = null;
                    // � ��� ��� ������ �� �����?
                    //                    var point10 = new DoublePoint(����������� - ������������������);
                    //                    ����������� = ������������������ + point10.Angle;
                    ���� -= dt;
                    if (���� < ����Min)
                    {
                        ���� = ����Min;
                        ����������� = ������������������;
                    }
                    // ����� ���� ������ ��������: ������� ���������� ������ ���� ��������� ���������� =>
                    // ������ � �� ���������
                    //                    if (���� == ����Min)
                    //                    {
                    //                        ����������� = ������������������;
                    //                    }
                    else
                    {
                        ����������� += ((������������������ - �����������) * dt) / ((���� + dt) - ����Min);
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

            public Double3DPoint ���������
            {
                get
                {
                    return _���������;
                }
                set
                {
                    value.CopyTo(ref _���������);
                    if (value == null)
                        return;
                    value.XZPoint.CopyTo(ref _���������XZ);
                }
            }

            public DoublePoint position
            {
                get
                {
                    // �������?
                    // ������������ (���������.XZPoint + new DoublePoint(�����������) * �����)
                    DoublePoint result = new DoublePoint(�����������).Multyply(�����);
                    return result.Add(ref _���������XZ);
                }
            }

            public double direction
            {
                get
                {
                    return �����������;
                }
            }

            public bool �������
            {
                get
                {
                    return ((���� == ����Min) && (����������� == ������������������));
                }
            }

            public bool �������
            {
                get
                {
                    return ((������ != null) && (���� == ����Normal));
                }
            }

            public ����������_������ ������
            {
                get
                {
                    return _f������;
                }
                set
                {
                    if (_f������ != null)
                    {
                        _f������.objects.Remove(this);
                    }
                    _f������ = value;
                    if (value != null)
                    {
                        value.objects.Add(this);
                    }
                }
            }

            public double �����������������������������
            {
                get
                {
                    if (������ != null)
                    {
                        //                        var point = position - ������.������;
                        //                        return point.Modulus;
                        var point = position;
                        return DoublePoint.Distance(ref point, ref ������.������);
                    }
                    return 0.0;
                }
            }
        }

        public class ���� : MeshObject, MeshObject.IFromFile, IMatrixObject
        {
            private string _filename;
            public double angle;
            public Double3DPoint point;
            public double �������;
            private MeshObject obj;

            public ����(string dir, string filename, Double3DPoint pos, double ang, MeshObject obj)
            {
                meshDir = dir;
                _filename = filename;
                angle = ang;
                point = pos;
                this.obj = obj;
            }

            public Matrix GetMatrix(int index)
            {
                var matrix1 = (Matrix.RotationY((float)������� * 16) * Matrix.RotationZ((float)angle));
                // ��, ��� ������������� � 0.6.2
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