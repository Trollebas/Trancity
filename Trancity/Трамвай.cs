using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Common;
using Engine;
using SlimDX;
using System.Windows.Forms;

namespace Trancity
{
    /// <summary>
    /// new
    /// </summary>
    public abstract class ������� : ���������_���������//Transport
    {
//        public �������[] ������ = new �������[0];
//        private ������� _�����������;
        public �����������_new �����������;
        public �������[] �������;

        public override void ����������������������(World ���)
        {
            var ost_dist = 2000.0;
            var stops_dist = 2000.0;
            var some_other_fucking_distance = 2000.0;
            var signals_dist = 2000.0;
            ����� next_rail = null;
            base.stand_brake = false;
            if (��������_���.�������_�����.���������_������.Length > 0)
                next_rail = ��������_���.�������_�����.���������_������[��������_���.�������_�����.���������_�����];
            else
                signals_dist = ��������_���.�������_�����.����� - ��������_���.����������_����������_��_������;
            base.UpdateTripStops();
            ost_dist = ��������_���.�������_�����.����� - ��������_���.����������_����������_��_������;
            if (((��������_���.�������_�����.���������_������.Length == 1) && !��������_���.�������_�����.������) && !next_rail.������)
            {
                ost_dist += next_rail.�����;
            }
            foreach (var obj2 in ��������_���.�������_�����.objects)
            {
                if (obj2 is Stop)
                {
                    var ��������� = (Stop)obj2;
                    if ((((���������.distance - ��������_���.����������_����������_��_������) <= 10.0) || //10.0
                        ((���� != null) && !���������.������������(����.pathes))) ||
                        ((������� == null) || (!���������.typeOfTransport[�������.typeOfTransport])) || ((currentStop != null) && (��������� != currentStop))) continue;// || (���������!=nextStop)) continue;
                    base.SearchForCurrentStop(���������);
                    if (��������� != nextStop) continue;
                    stops_dist = Math.Min(stops_dist, ���������.distance - ��������_���.����������_����������_��_������);
                    ���������������� = ���������;
                    currentStop = ���������;
                }
                if (obj2 is Visual_Signal/*����������_�������.������*/)
                {
                    var ������ = (Visual_Signal/*����������_�������.������*/)obj2;
                    if ((������.�������.������ == �������.�������) && ((������.���������.���������� - ��������_���.����������_����������_��_������) > 10.0))
                    {
                        signals_dist = Math.Min(signals_dist, (������.���������.���������� - ��������_���.����������_����������_��_������) - 30.0);
                    }
                }
                if (obj2 is �����������_������)
                {
                    var ������2 = (�����������_������)obj2;
                    var num5 = ������2.���������� - ��������_���.����������_����������_��_������;
                    if (((������2.������ == �������.�������) && (num5 > 5.0)) || ((������2.������ == �������.Ƹ����) && (num5 > 15.0)))
                    {
                        signals_dist = Math.Min(signals_dist, num5 - 10.0);
                    }
                }
                //���������� ������ �� �������, ����� 1:
                if ((��������_���.�������_�����.���������_������.Length <= 1) || (!(obj2 is ���))) continue;
                var axis = (���)obj2;
                if ((axis.������) && (axis.������� != this) && (axis.����������_����������_��_������ > axis.�������_�����.����� - axis.�������_�����.����������_����������_��������))//����� ��� ������ ���?
                {
                    some_other_fucking_distance = Math.Min(some_other_fucking_distance, (��������_���.�������_�����.����� - ��������_���.����������_����������_��_������) - (��������_���.�������_�����.����������_����������_��������));
                }
            }
            if (next_rail != null)
            {
                foreach (var obj3 in next_rail.objects)
                {
                    if (obj3 is Stop)
                    {
                        var ���������2 = (Stop)obj3;
                        if (((���� == null) || ���������2.������������(����.pathes)) && ((������� != null) && (���������2.typeOfTransport[�������.typeOfTransport]) && (���������2==nextStop) ))
                        {
                            stops_dist = Math.Min(stops_dist,
                                            (��������_���.�������_�����.����� -
                                             ��������_���.����������_����������_��_������) + ���������2.distance);
                        }
                    }
                    if (obj3 is Visual_Signal/*����������_�������.������*/)
                    {
                        var ������2 = (Visual_Signal/*����������_�������.������*/)obj3;
                        if (������2.�������.������ == �������.�������)
                        {
                            signals_dist = Math.Min(signals_dist, ((��������_���.�������_�����.����� - ��������_���.����������_����������_��_������) + ������2.���������.����������) - 30.0);
                        }
                    }
                    if (obj3 is �����������_������)
                    {
                        var ������21 = (�����������_������)obj3;
                        var num6 = (��������_���.�������_�����.����� - ��������_���.����������_����������_��_������) + ������21.����������;
                        if (((������21.������ == �������.�������) && (num6 > 5.0)) || ((������21.������ == �������.Ƹ����) && (num6 > 15.0)))
                        {
                            signals_dist = Math.Min(signals_dist, num6 - 10.0);
                        }
                    }
                    //���������� ������ �� �������, ����� 2:
                    if (��������_���.�������_�����.���������_������.Length <=1) continue;
                    if (obj3 is ���)
                    {
                        var ��� = (���)obj3;
                        if (!(���.��������) || (���.������� == this)) continue;//<- �� ����
                        if (���.�������.������_���.�������_����� != ���.�������_�����)
                        {
                            some_other_fucking_distance = Math.Min(some_other_fucking_distance, ��������_���.�������_�����.����� - ��������_���.����������_����������_��_������ - (��������_���.�������_�����.����������_����������_��������));
                        }
                    }
                }
                if (next_rail.����������_������.Length > 1)
                {
                    int j = -1;
                    for (int i = 0; i < next_rail.����������_������.Length; i++)
                    {
                        /*if (��������_���.�������_����� != �����.����������_������[i])
                        {
                            ind = i;
                            break;
                        }*/
                        //��������� ���������������:
                        if (��������_���.�������_����� == next_rail.����������_������[i])
                        {
                            j = i;
                            continue;
                        }
                        foreach (var obj4 in next_rail.����������_������[i].objects)
                        {
                            if (!(obj4 is ���)) continue;
                            var ��� = (���)obj4;
    //                        if ((!���.��������) && (!���.������)) continue;
                            //���������� ������ �� �������, ����� 3:
                            if (((���.����������_����������_��_������ > (���.�������_�����.����� - ���.�������_�����.����������_����������_�������� - 10.0)) &&
                                 (j < 0/*���.�������.�������� > this.��������*/)) || (���.������ && ���.�������_����� != ���.�������.��������_���.�������_�����))// && (��������_���.����������_����������_��_������ < ��������_���.�������_�����.����������_����������_��������))
                            {
                                some_other_fucking_distance = Math.Min(some_other_fucking_distance, ��������_���.�������_�����.����� - ��������_���.����������_����������_��_������ - (��������_���.�������_�����.����������_����������_��������));
                            }
                        }
                    }
                }
                List<���������> list2 = new List<���������>(next_rail.����������������);
                foreach (var ���������2 in list2)
                {
                    if (���������2.comment == this) continue;
                    some_other_fucking_distance = Math.Min(some_other_fucking_distance, (�������_���������.������.����� - �������_���������.����������) + ���������2.���������� - 2.0);
                }
            }
            /*if ((����� != null) && (�����.����������_������.Length > 1))// && (��������_���.�������_�����.���������_������.Length > 1))
            {
//                var ind = -10;
                for (int i = 0; i < �����.����������_������.Length; i++)
                {
                    /*if (��������_���.�������_����� != �����.����������_������[i])
                    {
                        ind = i;
                        break;
                    }*
                    //��������� ���������������:
                    if (��������_���.�������_����� == �����.����������_������[i]) continue;
                    foreach (var obj4 in �����.����������_������[i].objects)
                    {
                        if (!(obj4 is ���)) continue;
                        var ��� = (���)obj4;
//                        if ((!���.��������) && (!���.������)) continue;
                        //���������� ������ �� �������, ����� 3:
                        if (((���.����������_����������_��_������ > (���.�������_�����.����� - ���.�������_�����.����������_����������_�������� - 10.0)) &&
                             (���.�������.�������� > this.��������)) || (���.������ && ���.�������_����� != ���.�������.��������_���.�������_�����))// && (��������_���.����������_����������_��_������ < ��������_���.�������_�����.����������_����������_��������))
                        {
                            some_other_fucking_distance = Math.Min(some_other_fucking_distance, ��������_���.�������_�����.����� - ��������_���.����������_����������_��_������ - (��������_���.�������_�����.����������_����������_��������));
                        }
                    }
                }
                /*foreach (var obj4 in �����.����������_������[ind].objects)
                {
                    if (!(obj4 is ���)) continue;
                    var ��� = (���)obj4;
                    if ((���.��������) || (���.������))
                    {
                        //���������� ������ �� �������, ����� 3:
                        if (((���.����������_����������_��_������ > (���.�������_�����.����� - ���.�������_�����.����������_����������_�������� - 10.0)) &&
                             (���.�������.�������� != 0.0)) || (���.������ && ���.�������_����� != ���.�������.��������_���.�������_�����))// && (��������_���.����������_����������_��_������ < ��������_���.�������_�����.����������_����������_��������))
                        {
                            some_other_fucking_distance = Math.Min(some_other_fucking_distance, ��������_���.�������_�����.����� - ��������_���.����������_����������_��_������ - (��������_���.�������_�����.����������_����������_�������� + 5.0));
                        }
                    }
                }*
            }*/
            var list = new List<���������>(�������_���������.������.����������������);
//            List<���������> list2 = new List<���������>();
//            if (����� != null) list2 = new List<���������>(�����.����������������);
            foreach (var ��������� in list)
            {
                if ((���������.comment == this) || (���������.���������� <= �������_���������.����������))
                    continue;
                if ((���������.comment != null) && (���������.comment is �������))
                {
                    var comment = (�������)���������.comment;
                    if (((comment.�������_���������.����� != ���������.�����) || (comment.������_���.�������_����� != ���������.�����)) && (comment.�������� == 0.0))
                        continue;
                }
                some_other_fucking_distance = Math.Min(some_other_fucking_distance, ���������.���������� - �������_���������.���������� - 2.0);
            }
            /*foreach (var ���������2 in list2)
            {
                if (���������2.comment != this)
                {
                    some_other_fucking_distance = Math.Min(some_other_fucking_distance, (�������_���������.������.����� - �������_���������.����������) + ���������2.���������� - 2.0);
                }
            }*/
            /*foreach (var obj5 in ��������_���.�������_�����.objects)
            {
                if (obj5 is ����������_�������.������)
                {
                    var ������ = (����������_�������.������)obj5;
                    if ((������.�������.������ == �������.�������) && ((������.���������� - ��������_���.����������_����������_��_������) > 10.0))
                    {
                        signals_dist = Math.Min(signals_dist, (������.���������� - ��������_���.����������_����������_��_������) - 30.0);
                    }
                }
                if (!(obj5 is �����������_������)) continue;
                var ������2 = (�����������_������)obj5;
                var num5 = ������2.���������� - ��������_���.����������_����������_��_������;
                if (((������2.������ == �������.�������) && (num5 > 5.0)) || ((������2.������ == �������.Ƹ����) && (num5 > 15.0)))
                {
                    signals_dist = Math.Min(signals_dist, num5 - 10.0);
                }
            }
            foreach (var obj6 in ��������_���.�������_�����.���������_������[��������_���.�������_�����.���������_�����].objects)
            {
                if (obj6 is ����������_�������.������)
                {
                    var ������2 = (����������_�������.������)obj6;
                    if (������2.�������.������ == �������.�������)
                    {
                        signals_dist = Math.Min(signals_dist, ((��������_���.�������_�����.����� - ��������_���.����������_����������_��_������) + ������2.����������) - 30.0);
                    }
                }
                if (!(obj6 is �����������_������)) continue;
                var ������21 = (�����������_������)obj6;
                var num6 = (��������_���.�������_�����.����� - ��������_���.����������_����������_��_������) + ������21.����������;
                if (((������21.������ == �������.�������) && (num6 > 5.0)) || ((������21.������ == �������.Ƹ����) && (num6 > 15.0)))
                {
                    signals_dist = Math.Min(signals_dist, num6 - 10.0);
                }
            }*/
            var rec_speed7 = 15.0;
            var flag = false;
            /*var flag2 = false;
            if ((���� != null) && (����.inPark || ((����.������_����������� == ����.�����) && (���.time < ����.�����_�����������))))
            {
                flag2 = true;
            }*/
            if ((���� != null) && (����.inPark || ((����.������_����������� == ����.�����) && (���.time < ����.�����_�����������))))//(flag2)
            {
                foreach (����� �����2 in ����.����_�������)
                {
                    if (�����2 != ��������_���.�������_�����) continue;
                    flag = (�������� == 0.0);
                    var num8 = (�����2.����� - ��������_���.����������_����������_��_������) - 20.0;
                    signals_dist = Math.Min(signals_dist, num8);
                    ost_dist = Math.Min(ost_dist, num8);
                    break;
                }
            }
            if (flag && (base.�������_����������.���_���_������ <= 0))
            {
                �����������.����������� = false;
            }
            else
            {
                if (�����������.������ == null)
                {
                    �����������.�����������(���.�����������������2);
                }
                else if (!�����������.�����������)
                {
                    �����������.����������� = true;
                }
            }
            ������������(false);
            var flag3 = (���� == null) || (���.time >= ����.�����_�����������);
            if (!flag3)
            {
                signals_dist = Math.Min(signals_dist, (��������_���.�������_�����.����� - ��������_���.����������_����������_��_������) - 15.0);
            }
            base.stand_brake = flag;
            /*if (stops_dist < ost_dist)
            {
                ost_dist = stops_dist;
            }*/
            ost_dist = Math.Min(ost_dist, Math.Min(stops_dist, signals_dist));
            /*if (signals_dist < ost_dist)
            {
                ost_dist = signals_dist;
            }*/
            if (some_other_fucking_distance < ost_dist)
            {
                ost_dist = some_other_fucking_distance - 10.0;
            }
            if ((��������_������ > 0.0) && (stops_dist > 20.0))
            {
                rec_speed7 = 0.0;//-2.0;
                if (��������_abs < 0.1)//(�������� == 0.0)
                {
                    base.stand_brake = true;
                    if (�����_�_���������_�������)
                    {
                        if (!flag && flag3)
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
                        if (�����_������� && flag3)
                        {
                            ��������_������ -= World.�������������;
                        }
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
                if (ost_dist < 200.0)
                {
                    rec_speed7 = 12.0;
                }
                if (ost_dist < 80.0)
                {
                    rec_speed7 = 7.0;
                }
                if (ost_dist < 30.0)
                {
                    rec_speed7 = 4.0;
                }
                /*if (ost_dist < 10.0)
                {
                    rec_speed7 = 0.0;
                }*/
                if (stops_dist < 20.0)
                {
                    rec_speed7 = 1.5;
                    ��������_������ = 8.0 + (Cheats._random.NextDouble() * 5.0);
                    if ((���������������� != null) && ����������������.serviceStop)
                    {
                        ��������_������ = 1.0 + (Cheats._random.NextDouble() * 3.0);
                        �����_�_���������_������� = true;
                    }
                }
                if (signals_dist < 10.0)
                {
                    rec_speed7 = -2.0;
                }
                if (some_other_fucking_distance < 15.0)
                {
                    rec_speed7 = -2.0;
                }
                /*if (some_other_fucking_distance < 0.0)
                {
                    if (���������������� > 0)
                    {
                        rec_speed7 = -2.0;
                        if (�������� == 0.0)
                        {
                            ���������������� = -����������������;
                        }
                    }
                    else
                    {
                        rec_speed7 = 7.0;
                    }
                }
                if ((���������������� < 0) && (some_other_fucking_distance > 0.0))
                {
                    rec_speed7 = -2.0;
                    if (�������� == 0.0)
                    {
                        ���������������� = -����������������;
                    }
                }*/
            }
            if (!�����_�������)
            {
                rec_speed7 = 0.0;
            }
            var index = 0;
            var sw = false;
            if (��������_���.�������_�����.���������_������.Length > 1)
            {
//                var num11 = -1;
                index = Cheats._random.Next(2);
                if (���� != null)
                {
                    for (var i = ����_index; i < (����.pathes.Length - 1); i++)
                    {
                        if (����.pathes[i] != ��������_���.�������_�����) continue;
                        if (����.pathes[i + 1] == ��������_���.�������_�����.���������_������[0])
                        {
                            index = 0;
                        }
                        else if (����.pathes[i + 1] == ��������_���.�������_�����.���������_������[1])
                        {
                            index = 1;
                        }
                        break;
                    }
                }
                /*if (num11 == -1)
                {
                    num11 = Cheats._random.Next(2);
                }
                index = num11;*/
                var num13 = ��������_���.�������_�����.����� - ��������_���.����������_����������_��_������;
                var �����3 = ��������_���.�������_�����.���������_������[index];
                if ((num13 < 30.0) && �����3.������)
                {
                    ���������_�������� = (�����3.���������������0 > 0.0) ? 1 : -1;
                    sw = true;
                }
                /*else if (��������_���.�������_����� == ������_���.�������_�����)
                {
                    ���������_�������� = 0;
                }*/
            }
            if (((flag || (��������_������ > 20.0)) || (!flag3 && (�������� == 0.0))) || ((��������_���.�������_����� == ������_���.�������_�����) && (!sw)))
            {
                ���������_�������� = 0;
            }
            �������_����������.�������������_���������(rec_speed7, ost_dist, index);
        }

        public bool ��������_������������
        {
            get
            {
                /*if (�����������.������ && (��������_���.�������_�����.���������_������.Length > 1))
                {
                    DoublePoint point = ����������_������������ - ��������_���.�������_�����.����������_�������.����������;
                    if (point.������ < 0.5)
                    {
                        return true;
                    }
                }
                return false;*/
                return base.�������_����������.������������;
            }
        }

        public abstract ���[] ���_��� { get; }

        public abstract ��� ������_��� { get; }
        
        public abstract ��� ��������_��� { get; }

        public abstract Double3DPoint ����������_������������ { get; }

//        public abstract double �����������_������������ { get; }

        public abstract Matrix ��������������_������������ { get; }

        public override ��������� �������_���������
        {
            get
            {
                return new ���������(��������_���.�������_�����, ��������_���.����������_����������_��_������);
            }
        }

        /// <summary>
        /// �� ������ ��-68�
        /// </summary>
        public class �������������� : �������, IVector, I��������������3D
        {
//            private int[] frequency = new int[3];
//            private int[] volume = new int[3];
//            private int volume_muting;
            public ����� �����;
            public ������������[] ������;
            private double axis_radius;
//            public double �����������������;
//            public double �����������������Max = 1.0;
//            public double ��������������������� = 0.5;
//            public �������.����������_��������� ����������_���������;
//            public double ����������_�����_�������� = 16.5;
//            public double ����������_�����_��������� = 7.5;// 7.5;
            public double ����������_�����_����� = 1.94;//1.94
//            public �������.������������� ��������_�_����;
//            public ����������[] ����������;
            public ����������_new[] ����������2 = new ����������_new[0];
            public ����������[] ���������� = new ����������[0];
            
            public ��������������(���������������� ������, ����� �����, double ����������_��_������, ���������� ����������, ���� ����, Route �������, Order �����)//, bool test)//, int ����������_�������)
            {
                 this.������ = ������;
                ��������_����� = ������.dir;
                this.����� = �����;
//                this.����������_�����_��������� = ������.����������_�����_���������;
                this.����������_�����_����� = ������.����������_�����_�����;
                this.axis_radius = ������.axis_radius;
                var num = 0.0;
                this.������� = new �������.�������[������.�������.Length];
                for (int i = 0; i < this.�������.Length; i++)
                {
                    if ((������.�������[i].index > 0) && (������.�������[i].index != ������.�������[i-1].index)) num += ������.tails[������.�������[i].index - 1].dist;
                    this.�������[i] = new �������.�������(�����, ����������_��_������, ������.�������[0].dist - ������.�������[i].dist + ((������.�������[i].index > 0) ? /*������.tails[������.�������[i].index - 1].dist*/num : 0), axis_radius, this);
                }
/*                this.����������_�����_�������� = ������.����������_�����_��������;
                this.������� = new �������.�������[������.����������_������� * 2];
                for (int i = 0; i < this.�������.Length; i++)
                {
                    this.�������[i] = new �������.�������(�����, (����������_��_������ - (����������_�����_�������� * i)) - ����������_�����_���������, this);
                }
 */                ����� = new �����(this);//, new �������.�������(�����, ����������_��_������, 0.0f, axis_radius, this), new �������.�������(�����, ����������_��_������, ����������_�����_���������, axis_radius, this));
//                this.���������� = new ����������[������.�����������������];
                this.������ = new ������������[������.tails.Length];//�����������������];
                for (int z = 0; z < this.������.Length; z++)
                {
                    this.������[z] = new ������_�����(this);//, new �������.�������(�����, ����������_��_������, (������.tails[z].dist * (z + 1)), axis_radius, this), new �������.�������(�����, ����������_��_������, (������.tails[z].dist * (z + 1)) + ������.tails[z].t_dist, axis_radius, this));
                     /*if (z == 0)
                     {
                         this.����������[z] = new ����������(this, �����.��������_�������, ������[z].������_�������, �����.������_�������, ������.tails[z].have);
                     }
                     else
                     {
                         this.����������[z] = new ����������(this, ������[z - 1].��������_�������, ������[z].������_�������, ������[z - 1].������_�������, ������.tails[z].have);
                    }*/
                }
                try {
                this.����������2 = new ����������_new[������.����������.Length];//��������������������];
                for (int k = 0; k < this.����������2.Length; k++)
                {
                    this.����������2[k] = new ����������_new(/*k,*/ this, ������.����������[k].index, ������.����������[k].target, ������.����������[k].dist);
                }}
                catch {};
//                this.����������2[0] = new ����������_new(this, 0, 1, 3.456);
//                vs = ������.���������.pos.y + 1.5;
//                ����������_��������� = new �������.����������_���������();
                ���������_������ = new ���������������();
                ��������_�_���� = new �������������(this);
                this.���������� = ����������;
                this.������� = �������;
                this.���� = ����;
//                ����������� = �����������.Build(������.���������);
                ����������� = new �����������_new.���������(this, ������.���������.pos.y, ������.���������.pos.y + ������.���������.min_height, ������.���������.pos.y + ������.���������.max_height);//������.���������.pos, this.�����_�����(0));
                /*�����������.������� = this;
                �����������.����� = new �������.�����������_new.�����[������.���������.parts.Length];
                for (int i = 0; i < �����������.�����.Length; i++)
                {
                    �����������.�����[i] = new �������.�����������_new.�����(�����������, ������.���������.parts[i].filename, ������.���������.parts[i].index, ������.���������.parts[i].height, ������.���������.parts[i].width, ������.���������.parts[i].length);
                }
                �����������.������_��������� = ������.���������.pos.y;
                �����������.������_min = �����������.������_��������� + �����������.�����[0].height;//0.65;
                �����������.width = �����������.�����[�����������.�����.Length - 1].width / 2.0;*/
                ���������� = new ����������[������.����������.Length];
                for (int j = 0; j < ������.����������.Length; j++)
                {
                    this.����������[j] = new ����������(this.�����_�����(������.����������[j].�����), ������.����������[j].filename, ������.����������[j].���);
                }                 
                _���������������� = ������.����������������;
                _����� = new �����[������.�����.Length];
                for (var n = 0; n < ������.�����.Length; n++)
                {
                    _�����[n] = �����.���������(������.�����[n].������, �����_�����(������.�����[n].�����), ������.�����[n].p1, ������.�����[n].p2, ������.�����[n].������);
                    _�����[n].������������� = ������.�����[n].�������������;
                    _�����[n].����� = ������.�����[n].�����;
                }
                �������_���������� = �������_����������.Parse(������.�����������������, this);
                if (!������.hasnt_bbox)
                {
                    /*�����.bounding_box = new AABB(������.bbox[0], ������.bbox[1]);//new Double3DPoint(-(����������_�����_��������� / 2.0 + 4.0), 0.0, -2.5), new Double3DPoint(����������_�����_��������� / 2.0 + 4.0, 3.0, 2.5));
                    for (int k = 0; k < this.������.Length; k++)
                    {
                        ������[k].bounding_box = new AABB(������.tails_bbox[k][0], ������.tails_bbox[k][1]);// new Double3DPoint(-(tail_telegi_dist / 2.0 + 4.0), 0.0, -2.5), new Double3DPoint(tail_telegi_dist / 2.0 + 4.0, 3.0, 2.5));
                    }*/
                    �����.bounding_sphere = new Sphere(������.bsphere.pos, ������.bsphere.radius);
                    for (var i = 0; i < ������.Length; i++)
                    {
                        ������[i].bounding_sphere = new Sphere(������.tails_bsphere[i].pos, ������.tails_bsphere[i].radius);
                    }
                }
                else
                {
                    /*�����.bounding_box = new AABB(new Double3DPoint(-(����������_�����_��������� / 2.0 + 4.0), 0.0, -2.5), new Double3DPoint(����������_�����_��������� / 2.0 + 4.0, 3.0, 2.5));
                    for (int k = 0; k < this.������.Length; k++)
                    {
                         ������[k].bounding_box = new AABB(new Double3DPoint(-(������.tails[k].t_dist / 2.0 + 4.0), 0.0, -2.5), new Double3DPoint(������.tails[k].t_dist / 2.0 + 4.0, 3.0, 2.5));
                     }*/
                    �����.bounding_sphere = new Sphere(Double3DPoint.Zero, 10.0);
                    for (var i = 0; i < ������.Length; i++)
                    {
                        ������[i].bounding_sphere = new Sphere(Double3DPoint.Zero, 10.0);
                    }
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
                    length1 += num;
                }
                LoadCameras();
            }
 
            public override void CreateMesh(World ���)
            {
                if (���.filename != null)
                {
                    //var strArray = ����==null?
                    var strArray = new[]
                            {
                                            Application.StartupPath + @"\Cities\" +
                                            Path.GetFileNameWithoutExtension(���.filename) + @"\" + ����.�������� + @"\",
                                            Application.StartupPath + @"\Cities\" +
                                            Path.GetFileNameWithoutExtension(���.filename) + @"\"
                            };
                    �����.extraMeshDirs = strArray;
                    //����������_���������.extraMeshDirs = strArray;
                    ���������_������.extraMeshDirs = strArray;
                    ��������_�_����.extraMeshDirs = strArray;
    //                �����������.extraMeshDirs = strArray;
    //                �����������.����.extraMeshDirs = strArray;
                    foreach (����� ����� in _�����)
                    {
                        �����.ExtraMeshDirs = strArray;
                    }
                    foreach (�������.��������������.������������ tail in ������)
                    {
                          tail.extraMeshDirs = strArray;
                    }
                    /*foreach (�������.��������������.���������� middle in ����������)
                    {
                           if (!middle._has) continue;
                           middle.extraMeshDirs = strArray;
                    }*/
                }
                foreach (var ������� in this.�������)
                {
                    �������.CreateMesh();
                    foreach (var axis in �������.���)
                    {
                        axis.CreateMesh();
                    }
                }
                foreach (�������.��������������.������������ tail in this.������)
                {
                    tail.CreateMesh();
                    //
                    /*tail.��������_�������.CreateMesh();
                    tail.������_�������.CreateMesh();
                       tail.��������_�������.���[0].CreateMesh();
                    tail.��������_�������.���[1].CreateMesh();
                    tail.������_�������.���[0].CreateMesh();
                    tail.������_�������.���[1].CreateMesh();*/
                    //
                    /*foreach (var telega in tail.�������)
                    {
                        if (telega == null) continue;
                        telega.CreateMesh();
                        telega.���[0].CreateMesh();
                        telega.���[1].CreateMesh();
                    }*/
                    tail.��������_����������_���������(�������.number, �����.�����);
                }
                /*foreach (�������.��������������.���������� middle in ����������)
                {
                    if (!middle._has) continue;
                    middle.CreateMesh();
                }*/
                foreach (var middle2 in ����������2)
                {
                    middle2.CreateMesh();
                }
                �����.CreateMesh();
                //
                /*�����.��������_�������.CreateMesh();
                �����.������_�������.CreateMesh();
                �����.��������_�������.���[0].CreateMesh();
                �����.��������_�������.���[1].CreateMesh();
                �����.������_�������.���[0].CreateMesh();
                �����.������_�������.���[1].CreateMesh();*/
                //
                /*foreach (var telega in �����.�������)
                {
                    if (telega == null) continue;
                    telega.CreateMesh();
                    telega.���[0].CreateMesh();
                    telega.���[1].CreateMesh();
                }*/
                �����������.CreateMesh();
    //            �����������.����.CreateMesh();
                foreach (var ���������� in ����������)
                {
                    ����������.CreateMesh();
                }
    //            ����������_���������.CreateMesh();
    //            ����������_���������.��������(�������.number);
                �����.��������_����������_���������(�������.number, �����.�����);
                ���������_������.CreateMesh();
                if (����� != null)
                {
                    ���������_������.����������������(�����);
                }
                ��������_�_����.CreateMesh();
                foreach (����� �����2 in _�����)
                {
                    �����2.CreateMesh();
                }
    /*          foreach (������� ������� in ������)
                  {
                      �������.CreateMesh(���);
                  }
    */        }
        
            protected override void CheckCondition()
            {
                var cnd = !base.condition;
                �����.IsNear = cnd;
                foreach (var tail in this.������)
                {
                    tail.IsNear = cnd;
                }
                if (!cnd) return;
                �����������.CheckCondition();
                foreach (var ������� in this.�������)
                {
                    �������.IsNear = cnd;
                    foreach (var axis in �������.���)
                    {
                        axis.IsNear = cnd;
                    }
                }
                foreach (var middle2 in ����������2)
                {
                    middle2.IsNear = cnd;
                }
                foreach (var ���������� in ����������)
                {
                    ����������.IsNear = cnd;
                }
                if (����� != null)
                {
                    ���������_������.IsNear = cnd;
                }
                ��������_�_����.IsNear = cnd;
                foreach (var ����� in _�����)
                {
                    �����.CheckCondition();
                }
            }
        
            public override void Render()
            {
                CheckCondition();
                if (condition) return;
                var visible = false;
                var lod = 2;
                if (MyDirect3D.SphereInFrustum(�����.bounding_sphere))//(MyDirect3D.AABBInFrustum(�����.bounding_box))
                {
                    visible = true;
                    �����.Render();
                    lod = Math.Min(�����.bounding_sphere.LODnum, lod);
                    //
                    /*�����.��������_�������.Render();
                    �����.������_�������.Render();
                    �����.��������_�������.���[0].Render();
                    �����.��������_�������.���[1].Render();
                    �����.������_�������.���[0].Render();
                    �����.������_�������.���[1].Render();*/
                    //
                    /*foreach (var telega in �����.�������)
                    {
//                    if (telega == null) continue;
                        telega.Render();
                        telega.���[0].Render();
                        telega.���[1].Render();
                    }*/
                }
                foreach (�������.��������������.������������ tail in this.������)
                {
                    //if (!MyDirect3D.AABBInFrustum(tail.bounding_box)) continue;
                    if (!MyDirect3D.SphereInFrustum(tail.bounding_sphere)) continue;
                    visible = true;
                    tail.Render();
                    lod = Math.Min(tail.bounding_sphere.LODnum, lod);
                    //
                    /*tail.��������_�������.Render();
                    tail.������_�������.Render();
                       tail.��������_�������.���[0].Render();
                    tail.��������_�������.���[1].Render();
                    tail.������_�������.���[0].Render();
                    tail.������_�������.���[1].Render();*/
                    //
                    /*foreach (var telega in tail.�������)
                    {
//                        if (telega == null) continue;
                        telega.Render();
                        telega.���[0].Render();
                        telega.���[1].Render();
                    }*/
                }
                if ((!visible) || (lod > 0)) return;
                �����������.Render();
                /*�����������.����.Render();
                foreach (�������.��������������.���������� middle in ����������)
                {
                    if (!middle._has) continue;
                    middle.Render();
                }*/
                foreach (var ������� in this.�������)
                {
                    �������.Render();
                    foreach (var axis in �������.���)
                    {
                        axis.Render();
                    }
                }
                foreach (var middle2 in ����������2)
                {
                    middle2.Render();
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
                    if ((����������.��� == ���_����������.�����) && (�������_����������.����������� < 0))
                    {
                        ����������.Render();
                    }
                }
//                ����������_���������.matrix = Matrix.Translation(1.756f, 1.05f, -1.3f) * �����.GetMatrix(0);
//                ����������_���������.Render();
                if (����� != null)
                {
                    ���������_������.matrix = Matrix.Translation((float)������.�����Pos.x, (float)������.�����Pos.y, (float)������.�����Pos.z) * �����.last_matrix;//.GetMatrix(0);
                    ���������_������.Render();
                }
                ��������_�_����.matrix = Matrix.Translation((float)������.��������.pos.x, (float)������.��������.pos.y, (float)������.��������.pos.z) * �����.last_matrix;//.GetMatrix(0); //(7.65f, 2.26f, -0.42f) * �����.GetMatrix(0);
                ��������_�_����.Render();
                foreach (var ����� in _�����)
                {
                    �����.Render();
                }
            }
            
            public override void UpdateBoundigBoxes(World world)
            {
//                if (!world.simple_timer.flag) return;
                /*foreach (var ����� in ������)
                {
                    �����.bounding_box.Update(�����.����������3D, �����.�����������);
                }
                �����.bounding_box.Update(�����.����������3D, �����.�����������);*/
                �����.bounding_sphere.Update(�����.����������3D, �����.�����������);
                foreach (var ����� in ������)
                {
                    �����.bounding_sphere.Update(�����.����������3D, �����.�����������);
                }
            }
            
            /*public override void UpdateSound(�����[] ������, bool ����_�������)
            {
                base.�������_����������.UpdateSound(������, ����_�������);
            }*/
              
            public ������������ �����_�����(int index)
            {
                if (index > 0)
                {
                    return ������[index - 1];
                }
                if (index < 0)
                {
                    return this.����������2[-index - 1];
                }
                return this.�����;
            }
              
            public override ���������[] �����������������(World ���)
            {
//                var list = new List<���������>();//
                base.���������_���������.Clear();
                var point = new Double3DPoint(this.�����.�����������);
                var point2 = Double3DPoint.Rotate(this.�����.�����������, (Math.PI / 2.0));
                int length = this.������.����������������.Length;
                for (int i = 0; i < this.������.�����������������������.Length; i++)
                {
                    length += this.������.�����������������������[i].Length;
                }
                var pos = new Double3DPoint[length];
                int index = 0;
                int num4 = 0;
                while (num4 < this.������.����������������.Length)
                {
                    pos[index] = (Double3DPoint)((this.�����.����������3D + (point * this.������.����������������[num4].x)) + (point2 * this.������.����������������[num4].y));
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
                        pos[index] = (Double3DPoint)((this.������[j].����������3D + (point * this.������.�����������������������[j][num6].x)) + (point2 * this.������.�����������������������[j][num6].y));
                        num6++;
                        index++;
                    }
                }
                var collection = ���.�����_���_���������(pos);
                for (var i = 0; i < collection.Length; i++)
                {
                    collection[i].comment = this;
                }
                base.���������_���������.AddRange(collection);
//                ���������_��������� = list;
                return base.���������_���������.ToArray();
            }
                           
            public override void ��������(World ���, �����[] ������_�_����)
            {
                var list = new List<�����>();
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
                var num2 = ��������;
                if (list.Count > 0)
                {
                    I��������������3D[] �DArray = new I��������������3D[list.Count];
                    int index = 0;
                    int num3;
                    foreach (����� ����� in list)
                    {
                        double[] numArray = new double[this.������.Length];
                        num3 = 0;
                        while (num3 < this.������.Length)
                        {
                            DoublePoint point = �����.cameraPosition.XZPoint - this.������[num3].position;
                            numArray[num3] = point.Modulus;
                            num3++;
                        }
                        double[] numArray2 = new double[this.����������2.Length];
                        num3 = 0;
                        while (num3 < this.����������2.Length)
                        {
                            DoublePoint point2 = �����.cameraPosition.XZPoint - this.����������2[num3].position;
                            numArray2[num3] = point2.Modulus;
                            num3++;
                        }
                        bool flag = false;
                        num3 = 0;
                        while (num3 < this.����������2.Length)
                        {
                            if (numArray2[num3] < 1.0)
                            {
                                �DArray[index] = this.����������2[num3];
                                flag = true;
                                break;
                            }
                            num3++;
                        }
                        if (!flag)
                        {
                            �DArray[index] = this.�����;
                            var point3 = �����.cameraPosition.XZPoint - this.�����.����������3D.XZPoint;
                            var num5 = point3.Modulus;
                            for (num3 = 0; num3 < this.������.Length; num3++)
                            {
                                if (numArray[num3] < num5)
                                {
                                    num5 = numArray[num3];
                                    �DArray[index] = this.������[num3];
                                }
                            }
                        }
                        index++;
                    }
                    var pointArray = new Double3DPoint[list.Count];
                    var pointArray2 = new Double3DPoint[list.Count];
                    var pointArray3 = new Double3DPoint[list.Count];
                    var pointArray4 = new DoublePoint[list.Count];
                    var pointArray5 = new DoublePoint[list.Count];
                    index = 0;
                    foreach (var �����2 in list)
                    {
                        pointArray[index] = �����2.cameraPosition - �DArray[index].����������3D;
                        pointArray[index].XZPoint = pointArray[index].XZPoint.Multyply(new DoublePoint(-�DArray[index].direction));
                        pointArray[index].XYPoint = pointArray[index].XYPoint.Multyply(new DoublePoint(-�DArray[index].�����������Y));
                        pointArray2[index] = �����2.������������������ ? �����2.cameraPosition : �DArray[index].����������3D;
                        pointArray4[index] = new DoublePoint(�DArray[index].direction, �DArray[index].�����������Y);
                        index++;
                    }
                    �����������(�������� * World.�������������, ���);
                    index = 0;
                    foreach (����� �����3 in list)
                    {
                        pointArray[index].XYPoint = pointArray[index].XYPoint.Multyply(new DoublePoint(�DArray[index].�����������Y));
                        pointArray[index].XZPoint = pointArray[index].XZPoint.Multyply(new DoublePoint(�DArray[index].direction));
                        pointArray[index].Add(�DArray[index].����������3D);
                        pointArray3[index] = �����3.������������������ ? pointArray[index] : �DArray[index].����������3D;
                        pointArray5[index] = new DoublePoint(�DArray[index].direction, �DArray[index].�����������Y);
                        �����3.cameraPosition.Add(pointArray3[index] - pointArray2[index]);
                        if (�����3.������������������)
                        {
                            �����3.cameraRotation.Add(pointArray5[index] - pointArray4[index]);
                        }
                        index++;
                    }
                }
                else
                {
                    �����������(�������� * World.�������������, ���);
                }
//                �����������.position = �����.����������3D + ((Double3DPoint)(new Double3DPoint(�����.�����������) * ������.���������.pos.x));
                �����������.��������(���);
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
                _sound���������� = false;
                _sound����������� = false;
                if ((base.�������_����������.���_���_������ != 0) && �����������.������)
                {
                    double last_speed = ��������;
                    �������� += ��������� * World.�������������;
                    if (base.�������_����������.���_���_������ > 0)
                    {
                        _sound���������� = true;
                    }
                    else if (base.�������_����������.���_���_������ < 0)// && (�������� >= 1.0))
                    {
                        if ((�������� * last_speed) < 0.0)
                        {
                            �������� = 0.0;
                        }
                        _sound����������� = true;
                    }
                }
                if (��������_������������)
                {
                    var cond = ((base.�������_����������.���_���_������ <= 0) != �����.�������_��������);
                    /*if ((base.�������_����������.���_���_������ <= 0) != �����.�������_��������)// && (������������������ > -5)
                    {
                        ��������_���.�������_�����.���������_����� = 0;
                        foreach (����� ����� in ��������_���.�������_�����.��������_������)
                        {
                            �����.���������_����� = 0;
                        }
                    }
                    else
                    {
                        ��������_���.�������_�����.���������_����� = 1;
                        foreach (����� �����2 in ��������_���.�������_�����.��������_������)
                        {
                            �����2.���������_����� = 1;
                        }
                    }*/
                    ��������_���.�������_�����.���������_����� = cond ? 0 : 1;
                    foreach (����� �����2 in ��������_���.�������_�����.��������_������)
                    {
                        �����2.���������_����� = cond ? 0 : 1;
                    }
                }
                if ((��������_���.�������_�����.������[0] == ��������_���.�������_�����.������[1]) && !��������_���.�_��������_�������)
                {
                    var �����3 = ��������_���.�������_�����;
                    var num5 = ��������_���.����������_����������_��_������;
                    for (int i = 0; i < �������.Length; i++)
                    {
                        if (num5 < �������[i].default_dist + ����������_�����_�����) continue;
                        �������[i].���[0].c������_�������_�����(�����3);
                        �������[i].���[0].����������_����������_��_������ = num5 - �������[i].default_dist;
                        �������[i].���[1].c������_�������_�����(�����3);
                        �������[i].���[1].����������_����������_��_������ = num5 - �������[i].default_dist - ����������_�����_�����;
                    }
                }
                foreach (var ����� in base._�����)
                {
                    �����.��������();
                }
                base.��������_abs -= 0.1 * World.�������������;
                base.������������();
                this.UpdateBoundigBoxes(���);
            }

            protected override void ���������������������������()
            {
                �����.��������_����������_���������(�������.number, ����� == null ? "" : �����.�����);
                foreach (var tail in ������)
                {
                    tail.��������_����������_���������(�������.number, ����� == null ? "" : �����.�����);
                }
//                ����������_���������.��������(�������.number);
            }
            
            public override void SetPosition(Road road, double distance, double shift, Double3DPoint pos, DoublePoint rot, World world)
            {
                for (int i = 0; i < this.�������.Length; i++)
                {
                    this.�������[i].���[0].�������_����� = (�����)road;
                    this.�������[i].���[1].�������_����� = (�����)road;
                    this.�������[i].���[0].����������_����������_��_������ = distance - this.�������[i].default_dist;
                    this.�������[i].���[1].����������_����������_��_������ = distance - this.�������[i].default_dist - ����������_�����_�����;
                }
            }

            public override void �����������(double ����������, World ���)
            {
                /*�����.��������_�������.���[0].�����������(����������);
                �����.��������_�������.���[1].�����������(����������);
                �����.������_�������.���[0].�����������(����������);
                �����.������_�������.���[1].�����������(����������);
                for (int i = 0; i < this.������.Length; i++)
                {
                    ������[i].��������_�������.���[0].�����������(����������);
                    ������[i].��������_�������.���[1].�����������(����������);
                    ������[i].������_�������.���[0].�����������(����������);
                    ������[i].������_�������.���[1].�����������(����������);
                }*/
                /**/
                var pos = new Double3DPoint[1 + this.������.Length];
                var dir = new DoublePoint[1 + this.������.Length];
//                var flag = false;
                for (int i = 0; i < �������.Length; i++)
                {
                    for (int j = 0; j < �������[i].���.Length; j++)
                    {
                        �������[i].���[j].�����������(����������);
                    }
                }
                for (int i = 0; i < �������.Length; i++)
                {
                    if ((i < �������.Length - 1) && (������.�������[i].index == ������.�������[i+1].index))
                    {
//                        var p = �������[i].����������_3D - �������[i+1].����������_3D;
                        dir[������.�������[i].index] = (�������[i].����������_3D - �������[i+1].����������_3D).Angle;
                        pos[������.�������[i].index] = �������[i].����������_3D + (new Double3DPoint(dir[������.�������[i].index]).Multyply(-������.�������[i].dist));
                        i++;
                        continue;
                    }
                    dir[������.�������[i].index] = new DoublePoint(������.�������[i].dist, 0.0);
                    pos[������.�������[i].index] = �������[i].����������_3D;
                }
                if (�������.Length == 1) dir[0] = �������[0].����������_3D.Angle;
                
                Double3DPoint point1;
                DoublePoint point01;
                Double3DPoint point2;
                DoublePoint point02;
                for (int k = 0; k < this.����������2.Length; k++)
                {
                    ����������2[k]._����������3D = pos[����������2[k]._index] + (new Double3DPoint(dir[����������2[k]._index]).Multyply(-����������2[k].dist));
                    var x = dir[����������2[k]._target].x;
                    if (����������2[k]._index > ����������2[k]._target)
                    {
                        var p = pos[����������2[k]._target] - ����������2[k]._����������3D;
                        dir[����������2[k]._target] = p.Angle;
                        pos[����������2[k]._target].Add(new Double3DPoint(dir[����������2[k]._target]).Multyply(-x));
                    }
                    else
                    {
                        var p = ����������2[k]._����������3D - pos[����������2[k]._target];
                        dir[����������2[k]._target] = p.Angle;
                        pos[����������2[k]._target].Add(new Double3DPoint(dir[����������2[k]._target]).Multyply(-x));
                    }
                    point1 = pos[����������2[k]._index] - ����������2[k].����������3D;
                    point01 = point1.Angle;
                    point2 = ����������2[k].����������3D - pos[����������2[k]._target];
                    point02 = point2.Angle;
                    ����������2[k]._direction.x = (point01.x + point02.x) / 2.0;
                    ����������2[k]._direction.y = (point01.y + point02.y) / 2.0;
                    if (Math.Abs(point01.x - point02.x) >= Math.PI)
                    {
                        ����������2[k]._direction.x += Math.PI;
                    }
                    if (Math.Abs(point01.y - point02.y) >= Math.PI)
                    {
                        ����������2[k]._direction.y += Math.PI;
                    }
//                    point2 = new Double3DPoint(this.����������2[k].�����������);
//                    point2.����_y += 1.5707963267948966;
//                    ����������2[k].���������� -= (Double3DPoint)(point2 * (axis_radius * 2));
                }
                �����._����������3D = pos[0];
                �����._direction = dir[0];
                for (int j = 1; j < pos.Length; j++)
                {
                    ������[j-1]._����������3D = pos[j];
                    ������[j-1]._direction = dir[j];
                }
                /*///���������� ������
                foreach (var telega in �����.�������)
                {
                    telega.���[0].�����������(����������);
                    telega.���[1].�����������(����������);
                }
                for (int i = 0; i < this.������.Length; i++)
                {
                    foreach (var telega2 in ������[i].�������)
                    {
                        telega2.���[0].�����������(����������);
                        telega2.���[1].�����������(����������);
                    }
                }
                if (this.����������2.Length == 0) return;
                Double3DPoint point1;
                DoublePoint point01;
                Double3DPoint point2;
                DoublePoint point02;
                for (int k = 0; k < this.����������2.Length; k++)
                {
                    ����������2[k].���������� = ����������2[k].index.����������3D + ((Double3DPoint)(new Double3DPoint(����������2[k].index.�����������) * -����������2[k].dist));
                    point1 = ����������2[k].index.����������3D - ����������2[k].����������3D;//part.������_�������.����������_3D;
                    point01 = point1.����;
                    point2 = ����������2[k].����������3D - ����������2[k].target.����������3D;// - ����������2[k].����������3D;
                    point02 = point2.����;
                    ����������2[k].�����������.x = (point01.x + point02.x) / 2.0;
                    ����������2[k].�����������.y = (point01.y + point02.y) / 2.0;
                    if (Math.Abs(point01.x - point02.x) >= Math.PI)
                    {
                        ����������2[k].�����������.x += Math.PI;
                    }
                    if (Math.Abs(point01.y - point02.y) >= Math.PI)
                    {
                        ����������2[k].�����������.y += Math.PI;
                    }
//                    var point3 = new Double3DPoint(this.����������2[k].�����������);
//                    point3.����_y += 1.5707963267948966;
//                    ����������2[k].���������� -= (Double3DPoint)(point3 * (axis_radius * 2));
                }/**/
            }
          
            
        public class ������������ : MeshObject, I��������������3D, IVector, IMatrixObject
        {
            public �������������� �������;
            public Double3DPoint _����������3D;
            public DoublePoint _direction;
            
            public virtual Matrix GetMatrix(int index)
            {
                  Matrix matrix = Matrix.RotationZ((float)�����������Y) * Matrix.RotationY(-((float)direction));
                last_matrix = (matrix * Matrix.Translation((float)����������3D.x, (float)����������3D.y, (float)����������3D.z));
                return last_matrix;//(matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
            }
            
            public virtual int MatricesCount
            {
                get
                {
                    return 1;
                }
            }

            public virtual DoublePoint position
            {
                get
                {
                    /*if (middle_index != -1)
                    {
                        DoublePoint point0;
                        DoublePoint point1;
                        if (middle_front)
                        {
                            point0 = �������.����������2[middle_index].position;
                            point1 = �������[0].position;
                        }
                        else
                        {
                            point0 = �������[0].position;
                            point1 = �������.����������2[middle_index].position;
                        }
                        return (DoublePoint)((point0 + point1) / 2.0);
                    }
                    return (DoublePoint)((�������[0].position + �������[1].position) / 2.0);*/
                    return ����������3D.XZPoint;
                }
            }

            public virtual Double3DPoint ����������3D
            {
                get
                {
                    /*if (middle_index != -1)
                    {
                        Double3DPoint point0;
                        Double3DPoint point1;
                        if (middle_front)
                        {
                            point0 = �������.����������2[middle_index].����������3D;
                            point1 = �������[0].����������_3D;
                        }
                        else
                        {
                            point0 = �������[0].����������_3D;
                            point1 = �������.����������2[middle_index].����������3D;
                        }
                        return (Double3DPoint)((point1 + point0) / 2.0);
                    }
                    return (Double3DPoint)((�������[1].����������_3D + �������[0].����������_3D) / 2.0);*/
                    return _����������3D;
                }
            }

            public virtual double direction
            {
                get
                {
                    /*DoublePoint point;
                    if (middle_index != -1)
                    {
                        DoublePoint point0;
                        DoublePoint point1;
                        if (middle_front)
                        {
                            point0 = �������.����������2[middle_index].position;
                            point1 = �������[0].position;
                        }
                        else
                        {
                            point0 = �������[0].position;
                            point1 = �������.����������2[middle_index].position;
                        }
                        point = point0 - point1;
                        return point.����;
                    }
                    point = �������[0].position - �������[1].position;
                    return point.����;*/
                    return _direction.x;
                }
            }

            public DoublePoint �����������_3D
            {
                get
                {
                    return new DoublePoint(direction, �����������Y);
                }
            }

            public DoublePoint �����������
            {
                get
                {
                    /*DoublePoint point3 = new DoublePoint(direction, �����������Y);
                    return point3;*/
                    return _direction;
                }
            }
                
            public virtual double �����������Y
            {
                get
                {
                    /*Double3DPoint point;
                    DoublePoint point3;
                    if (middle_index != -1)
                    {
                        Double3DPoint point0;
                        Double3DPoint point1;
                        if (middle_front)
                        {
                            point0 = �������.����������2[middle_index].����������3D;
                            point1 = �������[0].����������_3D;
                        }
                        else
                        {
                            point0 = �������[0].����������_3D;
                            point1 = �������.����������2[middle_index].����������3D;
                        }
                        point = point0 - point1;
                        point3 = new DoublePoint(point.xz_point.������, point.y);
                        return point3.����;
                    }
                    point = �������[0].����������_3D - �������[1].����������_3D;
                    point3 = new DoublePoint(point.xz_point.������, point.y);
                    return point3.����;*/
                    return _direction.y;
                }
            }
        }
        
        public class ���������� : MeshObject, MeshObject.IFromFile, IMatrixObject
        {
            public string file;
            public ���_���������� ���;
            public ������������ ������������;

            public ����������(������������ ������������, string filename, ���_���������� ���)
            {
                this.������������ = ������������;
                this.file = filename;
                this.��� = ���;
            }

            public Matrix GetMatrix(int index)
            {
                return ������������.last_matrix;//.GetMatrix(0);
            }

            public string Filename
            {
                get
                {
                    base.meshDir = this.������������.meshDir;
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
        
        public class ����� : ������������
        {
            public �����(�������.�������������� �������)//, �������.������� ��������_�������, �������.������� ������_�������)
            {
                base.������� = �������;
//                base.�������[0] = ��������_�������;
//                base.�������[1] = ������_�������;
            }
            public override string Filename
            {
                get
                {
                    base.meshDir = �������.������.dir;
                    return base.�������.������.filename;
                }
            }
        }
        
        public class ������_����� : ������������
        {
            public ������_�����(�������.�������������� �������)//, �������.������� ��������_�������, �������.������� ������_�������)
            {
                base.������� = �������;
//                base.�������[0] = ��������_�������;
//                base.�������[1] = ������_�������;
            }
            public override string Filename
            {
                get
                {
                    base.meshDir = �������.������.dir;
                    int index = 0;
                    for (int i=0; i < �������.������.Length; i++)
                    {
                        if (this.�������.������[i] == this)
                        {
                            index = i;
                            break;
                        }
                    }
                    return this.�������.������.tails[index].filename;
                }
            }
        }
        
        public abstract class ������������ : �������.��������������.������������, MeshObject.IFromFile, IMatrixObject
        {
            protected ������������()
            {
            }
            public abstract string Filename { get; }
            
                public void ��������_����������_���������(string �������, string �����)
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
        }

            /*public class ���������� : ������������, MeshObject.IFromFile //MeshObject, MeshObject.IFromFile, IMatrixObject, IVector, I��������������3D
            {
                public bool _has;
                public �������.������� �������;
//                public �������.������� ������_�������;
//                public �������.������� ��������_�������;
//                public �������������� _�������;

                public ����������(�������������� �������, �������.������� ��������_�������, �������.������� ������_�������, �������.������� �������, bool ����)
                {
                    this.������� = �������;
                    this.�������[1] = ������_�������;
                    this.�������[0] = ��������_�������;
                    this.������� = �������;
                    this._has = ����;
                }

                public override Matrix GetMatrix(int index)
                {
                    Matrix matrix = Matrix.RotationZ((float) this.�����������Y) * Matrix.RotationY(-((float) this.direction));
                    Double3DPoint point = this.����������3D;
                    last_matrix = (matrix * Matrix.Translation((float) point.x, (float) point.y, (float) point.z));
                    return last_matrix;
                }

                public string Filename
                {
                    get
                    {
                        /*if (_has)
                        {
                            base.meshDir = �������.������.dir;
                            var index = 0;
                            for (var i = 0; i < �������.����������.Length; i++)
                            {
                                if (�������.����������[i] == this)
                                {
                                    index = i;
                                    break;
                                }
                            }                        
                            return �������.������.tails[index].m_f;
                        }//
                        return null;
                    }
                }

                public override int MatricesCount
                {
                    get
                    {
                        return !this._has ? 0 : 1;
                    }
                }

                public override DoublePoint position
                {
                    get
                    {
                        return this.�������.position;
                    }
                }

                public override Double3DPoint ����������3D
                {
                    get
                    {
                        return this.�������.����������_3D;
                    }
                }

                public override double direction
                {
                    get
                    {
                        DoublePoint point = this.�������[0].position - this.�������.position;
                        double num = point.����;
                        DoublePoint point2 = this.�������.position - this.�������[1].position;
                        double num2 = point2.����;
                        if (Math.Abs((double) (num - num2)) < 3.1415926535897931)
                        {
                            return ((num + num2) / 2.0);
                        }
                        return (((num + num2) / 2.0) + 3.1415926535897931);                        
                    }
                }

                public override double �����������Y
                {
                    get
                    {
                        Double3DPoint point = this.�������[0].����������_3D - this.�������.����������_3D;
                        DoublePoint point2 = new DoublePoint(point.xz_point.������, point.y);
                        double num = point2.����;
                        Double3DPoint point3 = this.�������.����������_3D - this.�������[1].����������_3D;
                        DoublePoint point4 = new DoublePoint(point3.xz_point.������, point3.y);
                        double num2 = point4.����;
                        if (Math.Abs((double) (num - num2)) < 3.1415926535897931)
                        {
                            return ((num + num2) / 2.0);
                        }
                        return (((num + num2) / 2.0) + 3.1415926535897931);
                    }
                }
            }*/
            
            public class ����������_new : ������������, MeshObject.IFromFile //MeshObject, MeshObject.IFromFile, I��������������3D, IVector, IMatrixObject
            {
                /*public Double3DPoint _����������3D;
                public DoublePoint _direction;
                public �������������� �������;*/
//                public int index = 0;
//                public int target = 1;
                public double dist = 0;//3.456;
                public int _index = 0;
                public int _target = 0;
//                public ������������ index = null;
//                public ������������ target = null;
                
                public ����������_new(/*int _index, */�������������� �������, int ind, int tar, double distance)
                {
                    this.������� = �������;
//                    this.index = �������.�����_�����(ind);
//                    this.target = �������.�����_�����(tar);
                    _index = ind;
                    _target = tar;
                    this.dist = distance;
//                    this.target.middle_index = _index;
//                    this.target.middle_front = (ind < tar);
//                    this.target.�������������();
                }

                public override Matrix GetMatrix(int index)
                {
                    var matrix = Matrix.RotationZ((float)�����������Y) * Matrix.RotationY(-((float)this.direction));
                    last_matrix = (matrix * Matrix.Translation((float)_����������3D.x, (float)_����������3D.y, (float)_����������3D.z));
                    return last_matrix;
                }
                
                public string Filename
                {
                    get
                    {
                        base.meshDir = �������.������.dir;
                        var index = 0;
                        for (var i = 0; i < �������.����������2.Length; i++)
                        {
                            if (�������.����������2[i] == this)
                            {
                                index = i;
                                break;
                            }
                        }
                        return �������.������.����������[index].filename;
//                        return "����������2.x";
                    }
                }

                /*public int MatricesCount
                {
                    get
                    {
                        return 1;
                    }
                }

                /*public DoublePoint position
                {
                    get
                    {
                        return this._����������3D.xz_point;
                    }
                }

                /*public double direction
                {
                    get
                    {
                        return this._direction.x;
                    }
                }

                public Double3DPoint ����������3D
                {
                    get
                    {
                        return this._����������3D;
                    }
                }

                public double �����������Y
                {
                    get
                    {
                        return _direction.y;
                    }
                }*/
            }            
        
            public override �������.���[] ���_���
            {
                get
                {
                    List<���> list = new List<���>();
                    foreach (var telega in this.�������)
                    {
                        list.AddRange(new �������.���[] { telega.���[0], telega.���[1] });
                    }
                    /*foreach (var telega in �����.�������)
                    {
                        list.AddRange(new �������.���[] { telega.���[0], telega.���[1] });
                    }
                    foreach (var tail in this.������)
                    {
                        foreach (var telega in tail.�������)
                        {
                            list.AddRange(new �������.���[] { telega.���[0], telega.���[1] });
                        }
                    }*/
                    return list.ToArray();
                }
            }
            
            public override �������.��� ������_���
            {
                get
                {
                    /*if (������.Length > 0)
                    {
                        return ������[������.Length - 1].�������[������[������.Length - 1].�������.Length - 1].���[1];//.������_�������.���[1];
                    }
                    return �����.�������[1].���[1];//.������_�������.���[1];*/
                    return �������[�������.Length - 1].���[�������[�������.Length - 1].���.Length - 1];
                }
            }
            
            public override �������.��� ��������_���
            {
                get
                {
//                    return �����.�������[0].���[0];//.��������_�������.���[0];
                    return �������[0].���[0];
                }
            }
            
            public override DoublePoint position
            {
                get
                {
                    return �����.position;
                }
            }

            public override Double3DPoint ����������_������������
            {
                get
                {
//                    var point = (�����.middle_index == -1) ? �����.�������[1].position : ����������2[�����.middle_index].position;
//                    var point2 = �����.�������[0].position - point;
//                    return (point2 != Double3DPoint.Zero.xz_point) ? (point + ((DoublePoint)((point2 / point2.������) * ������.����������_�����_���������))) : point;//������.����������_�����_���������)));
                    return �����.����������3D + (new Double3DPoint(�����.�����������).Multyply(������.���������.pos.x - ������.���������.dist));
                }
            }

            public override double direction
            {
                get
                {
                    return �����.direction;
                }
            }
            
            public override Double3DPoint ����������3D
            {
                get
                {
                    return this.�����.����������3D;
                }
            }
            
            public override double �����������Y
            {
                get
                {
                    return this.�����.�����������Y;
                }
            }

            public override Matrix ��������������_������������
            {
                get
                {
                    return (Matrix.Translation((float)������.���������.pos.x,(float)������.���������.pos.y, (float)������.���������.pos.z) * �����.GetMatrix(0));
                }
            }
            
            public int �����������
            {
                get
                {
                    return base.�������_����������.�����������;
                }
            }
            public override double ���������
            {
                get
                {
                    return base.�������_����������.���������;
                }
            }
        }
        /// <summary>
        /// END_NEW
        /// </summary>

     
/*        public class ����������_��������� : MeshObject, MeshObject.IFromFile, IMatrixObject
        {
            public Matrix matrix;

            public Matrix GetMatrix(int index)
            {
                return matrix;
            }

            public void ��������(string �������)
            {
                if (_meshTextureFilenames != null)
                {
                    for (int i = 0; i < _meshTextureFilenames.Length; i++)
                    {
                        if (_meshTextureFilenames[i] == "�������.PNG")
                        {
                            try
                            {
                                LoadTexture(i, "�������" + ������� + ".png");
                            }
                            catch
                            {
                                LoadTexture(i, "�������.png");
                            }
                        }
                    }
                }
            }

            public string Filename
            {
                get
                {
                    return "�������.x";
                }
            }

            public int MatricesCount
            {
                get
                {
                    return 1;
                }
            }
        }*/

        public class ��� : MeshObject, MeshObject.IFromFile, IMatrixObject, IVector
        {
            public bool �_��������_�������;
            public double ����������_����������_�����;
            public double ����������_����������_��_������;
            public double ������ = 0.35;
            public ����� �������_�����;
            public readonly �������������� �������;

            public ���(����� �����, double ����������_��_������, double _������, �������������� �������)
            {
                this.������� = �������;
                ������ = _������;
                �������_����� = �����;
                ����������_����������_��_������ = ����������_��_������;
//                �������_�����.objects.Add(this);
            }

            public void c������_�������_�����(����� �����)
            {
                if (�������_����� != null)
                {
                    �������_�����.objects.Remove(this);
                }
                �������_����� = �����;
                �������_�����.objects.Add(this);
            }

            ~���()
            {
                if (�������_����� != null)
                {
                    �������_�����.objects.Remove(this);
                }
            }

            public Matrix GetMatrix(int index)
            {
                Matrix matrix = Matrix.RotationZ(-((float)(����������_����������_����� / ������))) * Matrix.RotationY(-((float)direction));
                Double3DPoint point = ����������_3D;
                return (matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
            }

            public void �����������(double ����������)
            {
                double dist = ����������_����������_��_������;
                int reverse_dir = �_��������_������� ? -1 : 1;
                ����������_����������_��_������ += reverse_dir * ����������;
                if (�������_�����.������[0] != �������_�����.������[1])
                {
                    if (���������� != 0.0)
                    {
                        double num3 = �������_�����.�����������(����������_����������_��_������) - �������_�����.�����������(dist);
                        double num4 = (���������� * ����������) / ((���������� * ����������) + (num3 * num3));
                        ���������� = (����������_����������_��_������ - dist) * num4;
                        ����������_����������_��_������ = dist + ����������;
                        ���������� *= reverse_dir;
                    }
//                    double d = �������_�����.����������������Y(����������_����������_��_������) + 1.5707963267948966;
                    �������.�������� -= Math.Sin(�������_�����.����������������Y(����������_����������_��_������)) * Road.uklon_koef * World.�������������;//* 0.03;
//                    �������.�������� += Math.Cos(�������_�����.����������������Y(����������_����������_��_������) + MyFeatures.halfPI) * 0.03;
                }
                ����������_����������_����� += ����������;
                ���������_��������(dist, ����������_����������_��_������);
                while ((����������_����������_��_������ < 0.0) && (�������_�����.����������_������.Length > 0))
                {
                    c������_�������_�����(�������_�����.����������_������[�������_�����.����������_�����]);
                    ����������_����������_��_������ += �������_�����.�����;
                    ���������_��������(�������_�����.����� + 1.0, ����������_����������_��_������);
                }
                while ((����������_����������_��_������ > �������_�����.�����) && (�������_�����.���������_������.Length > 0))
                {
                    ����������_����������_��_������ -= �������_�����.�����;
                    c������_�������_�����(�������_�����.���������_������[�������_�����.���������_�����]);
                    ���������_��������(-1.0, ����������_����������_��_������);
                }
            }

            private void ���������_��������(double ����������_old, double ����������_new)
            {
                if (!(��������)) return;
                    foreach (object obj2 in �������_�����.objects)
                    {
                        if (!(obj2 is ����������_�������.�������)) continue;
                        var ������� = (����������_�������.�������)obj2;
                        if ((�������.���������� < ����������_new) && (�������.���������� >= ����������_old))
                        {
                            if (!�������.�����)
                            {
                                �������.�������.���������++;
                            }
                            else
                            {
                                �������.�������.���������--;
                            }
                        }
                        if ((�������.���������� >= ����������_old) || (�������.���������� < ����������_new)) continue;
                        if (�������.�����)
                        {
                            �������.�������.���������++;
                            continue;
                        }
                        �������.�������.���������--;
                    }
            }

            public string Filename
            {
                get
                {
//                    return "���.x";
                    base.meshDir = �������.������.dir;
                    return this.�������.������.axisfilename;
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
                    return �������_�����.���������������(����������_����������_��_������, 0.0);
                }
            }

            public Double3DPoint ����������_3D
            {
                get
                {
                    var point = new Double3DPoint();
                    point.XZPoint = position;
                    point.y = �������_�����.�����������(����������_����������_��_������) + 0.05;
                    double d = �������_�����.����������������Y(����������_����������_��_������) + (Math.PI / 2.0);
//                    point.xz_point += (DoublePoint)((new DoublePoint(direction) * ������) * Math.Cos(d));
                    point.XZPoint = point.XZPoint.Add(new DoublePoint(direction).Multyply(������ * Math.Cos(d)));
                    point.y += ������ * Math.Sin(d);
                    return point;
                }
            }

            public double direction
            {
                get
                {
                    return �������_�����.����������������(����������_����������_��_������);
                }
            }

            public bool ��������
            {
                get
                {
                    return ((������� != null) && (this == �������.��������_���));
                }
            }
            
            public bool ������
            {
                get
                {
                    return ((������� != null) && (this == �������.������_���));
                }
            }
        }

        public class ������� : MeshObject, MeshObject.IFromFile, IMatrixObject, IVector
        {
            public ���[] ���;
            public readonly �������������� �������;
            public double default_dist;
 
            public �������(����� �����, double ������������������, double dist, double axis_radius, �������������� �������)
            {
                ��� = new[] { new ���(�����, ������������������ - dist, axis_radius, �������), new ���(�����, ������������������ - dist - �������.����������_�����_�����, axis_radius, �������) };
                this.������� = �������;
                this.default_dist = dist;
            }

            public Matrix GetMatrix(int index)
            {
                var matrix = Matrix.RotationZ((float)�����������_y) * Matrix.RotationY(-((float)direction));
                var point = ����������_3D;
                return (matrix * Matrix.Translation((float)point.x, (float)point.y, (float)point.z));
            }

            public string Filename
            {
                get
                {
                    base.meshDir = �������.������.dir;
                    return this.�������.������.telegafilename;
//                    return "�������.x";
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
                    return (���[0].position + ���[1].position) / 2.0;
                }
            }

            public Double3DPoint ����������_3D
            {
                get
                {
                    return (���[0].����������_3D + ���[1].����������_3D) / 2.0;
                }
            }

            public double direction
            {
                get
                {
                    DoublePoint point = ���[1].position - ���[0].position;
                    return point.Angle;
                }
            }

            public double �����������_y
            {
                get
                {
                    Double3DPoint point = ���[1].����������_3D - ���[0].����������_3D;
                    DoublePoint point3 = new DoublePoint(point.XZPoint.Modulus, point.y);
                    return point3.Angle;
                }
            }
        }
        
        public abstract class �����������_new : ITest2
        {
            private ����������_����������_������ _f������;
            public double ������ = 0.0;
            public double ������_max = 5.0;
            public double ������_min = 4.0;
//            public double ������_���� = 0.2;
            public double ������_��������� = 3.35;
//            public double lenght = 0.2;
            public double width = 0.53;
            protected double dist = 0.0;
            
            public Double3DPoint position;
            public �����[] �����;
            public bool ����������� = true;
            public ������� �������;
            
            public abstract Matrix GetMatrix(int index);
            public abstract void ��������(World ���);
            
            public virtual void Render()
            {
                foreach (var ����� in �����)
                {
                    �����.Render();
                }
            }
            
            public virtual void CreateMesh()
            {
                foreach (var ����� in �����)
                {
                    �����.CreateMesh();
                }
            }
            
            public void CheckCondition()
            {
                foreach (var ����� in �����)
                {
                    �����.IsNear = true;
                }
            }
            
            public void �����������(����������_������[] �����������������)
            {
                double minHeight = 1000.0;
                double curHeight = -1000.0;
                
                var position2D = position.XZPoint;
                foreach (var ������ in �����������������)
                {
                    if (������.������������)
                        continue;
                    
                    var pantRelativePos = position2D - ������.������;
                    pantRelativePos.Angle -= ������.�����������;
                    if ((Math.Abs(pantRelativePos.y) <= width) && ((pantRelativePos.x >= 0.0) && (pantRelativePos.x <= ������.�����)))
                    {
                        var projectedPantPos = new DoublePoint(������.�����������).Multyply(pantRelativePos.x).Add(ref ������.������);
                        var normal = DoublePoint.Distance(ref projectedPantPos, ref position2D);
                        curHeight = ������.FindHeight(pantRelativePos.x);
                        if ((normal <= width) && (curHeight < minHeight))
                        {
                            ������ = (����������_����������_������)������;
                            minHeight = curHeight;
                            continue;
                        }
                    }
                }
            }
            
            public double �������_������_max
            {
                get
                {
                    if (������ != null)
                    {
                        return ((������.FindHeight(�����������������������������) + ����������_������.������_����������_����) - position.y - 0.03);
                    }
                    return ������_min;
                }
            }

            public bool ������
            {
                get
                {
                    return (������ <= ������_min);
                }
            }

            public bool ������
            {
                get
                {
                    return ((������ != null) && (������ >= �������_������_max));
                }
            }
            
            public ����������_����������_������ ������
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
//                        var point = position.XZPoint - ������.������;
//                        return point.Modulus;
                        var positionXZ = position.XZPoint;
                        return DoublePoint.Distance(ref positionXZ, ref ������.������);
                    }
                    return 0.0;
                }
            }
            
            public class ����� : MeshObject, MeshObject.IFromFile, IMatrixObject
            {
                private readonly �����������_new �����������;
                private int _index;
                private string filename;
                public double height;
                public double width;
                public double length;
                public double norm_ang;

                public �����(�����������_new �����������, string filename, int ind, double _height, double _width, double _length, double ang)
                {
                    this.����������� = �����������;
                    this._index = ind;
                    this.filename = filename;
                    this.height = _height;
                    this.width = _width;
                    this.length = _length;
                    this.norm_ang = ang;
                }

                public Matrix GetMatrix(int index)
                {
                    return �����������.GetMatrix(_index);
                }

                public string Filename
                {
                    get
                    {
                        base.meshDir = �����������.�������.������.���������.dir;
                        return this.filename;//�����������.�������.������.���������.part_filename;
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
            
            public class ��������� : �����������_new
            {
//                private const double num4 = 1.5;
                
                public ���������(������� tramway, double base_height, double min_height, double max_height)
                {
                    base.������� = tramway;
                    base.������_��������� = base_height;
                    base.������_min = min_height;
                    base.������_max = max_height;
                    base.dist = �������.������.���������.dist;
                    base.����� = new �����[�������.������.���������.parts.Length];
                    for (int i = 0; i < base.�����.Length; i++)
                    {
                        base.�����[i] = new �����(this, �������.������.���������.parts[i].filename, i, �������.������.���������.parts[i].height, �������.������.���������.parts[i].width, �������.������.���������.parts[i].length, �������.������.���������.parts[i].ang);//�������.������.���������.parts[i].index
                    }
                }
                
                public override void ��������(World ���)
                {
                    position = �������.����������_������������;//Trash.PointFromMatrix(�������.��������������_������������);//tr.�����.����������3D + ((Double3DPoint)(new Double3DPoint(tr.�����.�����������) * tr.������.���������.pos.x));
                    if (������ != null)
                    {
                        var dist = �����������������������������;
                        if ((dist > ������.�����) && (������.���������_�������2.Length > 0))
                        {
                            ������ = ������.���������_�������2[0];
                        }
                        else if ((dist < 0.0) && (������.����������_�������2.Length > 0))
                        {
                            ������ = ������.����������_�������2[0];
                        }
                        else
                        {
                            dist = Math.Max(0, Math.Min(dist, ������.�����));
                            var point = ������.FindCoords(dist, 0.0);
                            var point2 = position.XZPoint - point;
                            if (point2.Modulus > width)
                            {
                                ������ = null;
                                �����������(���.�����������������2);
                            }
                        }
                    }
                    else
                    {
                        ����������� = false;
                    }
                    if (����������� && !������)
                    {
                        ������ += 0.8 * World.�������������;
                    }
                    else if (!����������� && !������)
                    {
                        ������ -= 0.8 * World.�������������;
                    }
                    if (������ > ������_max)
                    {
                        ������ = ������_max;
                        ����������� = false;
                        return;
                    }
                    if (������)
                    {
                        ������ = ������_min;
                    }
                    if (������)
                    {
                        ������ = �������_������_max;//������_max;
                    }
                }
                
                public override Matrix GetMatrix(int index)
                {
                    var matrix = �������.��������������_������������;
                    var _height = ������ - ������_���������;
                    if (index == 0)
                    {
                        return matrix;
                    }
                    if (index == (�����.Length - 1))
                    {
                        return (Matrix.Translation(-(float)dist, (float)_height, 0f) * matrix);
                    }
                    //c^2=a^2+b^2-2*a*b*cosA!!!
                    //2*a*b*cosA=a^2+b^2-c^2
                    //cosA=(a^2+b^2-c^2)/2*a*b
                    //--------------��� ��� ����� ���� ������???
                    //2/some_sin=_height/add_sin
                    //2*add_sin=_height*some_sin
                    //some_sin=2*add_sin/_height
                    double add_cos = 0.0;
                    double length1 = index > 2 ? �����[3].length : �����[1].length;
                    double length2 = index > 2 ? �����[4].length : �����[2].length;
                    _height -= �����[�����.Length - 1].height;
                    if (dist != 0.0)
                    {
                        add_cos = Math.Sqrt(1 / (1 + Math.Pow(_height / dist, 2.0)));
                        _height = dist / add_cos;
                    }
                    double target_cos = (Math.Pow(length1, 2.0) + Math.Pow(length2, 2.0) - Math.Pow(_height, 2.0)) / (2.0 * length1 * length2);
                    double add_sin = Math.Sqrt(1 - Math.Pow(target_cos, 2.0));
                    add_sin = length2 * add_sin / _height;
                    double rot1 = (Math.PI / 2.0) - Math.Asin(add_sin);
                    if (length2 * target_cos > length1)
                    {
                        rot1 = -rot1;//Math.Asin(add_sin) - Math.PI / 2.0;
                    }
                    if (dist != 0.0)
                    {
                        rot1 += (Math.PI / 2.0) - Math.Acos(add_cos);
                    }
                    switch (index)
                    {
                        case 1:
                            {
                                rot1 += �����[1].norm_ang;
                                return (Matrix.RotationZ((float)rot1) * matrix);
                            }

                        case 2:
                            {
                                return (Matrix.RotationZ(-(float)(Math.Acos(target_cos) - �����[2].norm_ang)) * Matrix.Translation((float)�����[1].length, 0.0f, 0.0f) * Matrix.RotationZ((float)rot1) * matrix);
                            }

                        case 3:
                            {
                                rot1 -= �����[3].norm_ang;
                                return (Matrix.RotationZ(-(float)rot1) * Matrix.Translation((float)(dist * -2.0), 0f, 0f) * matrix);
                            }

                        case 4:
                            {
                                return (Matrix.RotationZ((float)(Math.Acos(target_cos) + �����[4].norm_ang)) * Matrix.Translation(-(float)�����[3].length, 0.0f, 0.0f) * Matrix.RotationZ(-(float)rot1) * Matrix.Translation((float)(dist * -2.0), 0f, 0f) * matrix);
                            }
                    }
                    /*var num = ������ - ������_����;
                    var num2 = ������_��������� + ((num - ������_���������) / 2.0);
                    var y = num - num2;
                    if (y < 0.0)
                    {
                        y = 0.0;
                    }
                    else if (y > num4)
                    {
                        y = num4;
                    }
                    var x = Math.Sqrt((num4 * num4) - (y * y));
                    var point = new Double3DPoint(x, y, 0.0);
                    var point1 = new Double3DPoint(0.0, 2.0 * y, 0.0);
                    switch (index)
                    {
//                        case 0:
//                            return matrix;

                        case 1:
                            return (GetMatrix(Double3DPoint.Zero, point) * matrix);

                        case 2:
                            return (GetMatrix(point, point1) * matrix);

//                        case 3:
//                            return (Matrix.Translation(0f, (float)(������ - ������_���������), 0f) * matrix);
                    }*/
                    return Matrix.Identity;
                }
            }
        }//
    }
}