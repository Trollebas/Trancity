using Common;
using System;
using System.Reflection;
using Engine;
using Engine.Sound;

namespace Trancity
{
    public abstract class Система_управления
    {
        protected ISound3D[] SoundBuffers;
        protected int[] SoundBuffers_Volume;
        protected int[] frequency = new int[6];
        protected int[] last_frequency = new int[6];
        protected int[] volume = new int[6];
        protected int volume_muting;
        private bool isPlaying = false;

        protected Система_управления()
        {
        }

        public abstract void CreateSoundBuffers();
        public static Система_управления Parse(string text, Transport transport)
        {
            switch (text)
            {
            	
            	   case "РКСУ_Трамвай":
            		return new РКСУ_Трамвай((Трамвай)transport);
            		
            	   case "ТИСУ_Трамвай":
                    return new РКСУ_Трамвай((Трамвай)transport);
                    
                   case "ТРСУ_Трамвай":
                    return new РКСУ_Трамвай((Трамвай)transport);
                    
                   case "НСУ_Трамвай":
                    return new РКСУ_Трамвай((Трамвай)transport);
                    
                   case "КНРКСУ_Трамвай":
                    return new РКСУ_Трамвай((Трамвай)transport);
            		          	
            	   case "РКСУ_Троллейбус":
            		return new РКСУ_Троллейбус((Троллейбус)transport);
            	   
            	   case "ТРСУ_Троллейбус":
                    return new РКСУ_Троллейбус((Троллейбус)transport);
                    
                   case "ТИСУ_Троллейбус":
                    return new РКСУ_Троллейбус((Троллейбус)transport);

                   case "КП_Авто":
            		return new КП_Авто((Троллейбус)transport);
            		
                   case "КП_Авто1":
                    return new КП_Авто1((Троллейбус)transport);
                    
                   case "РКП":
                    return new КП_Авто1((Троллейбус)transport);
                   
                   case "Электробус":
                    return new КП_Авто((Троллейбус)transport);
                   
                   case "Траффик":
                    return new КП_Авто((Троллейбус)transport);
            }
            return null;
        }

        public abstract void UpdateSound(Игрок[] игроки, bool игра_активна);
        public abstract void автоматически_управлять(double рекомендуемая_скорость, double оставшееся_расстояние, int переключение);

        public abstract int направление { get; }

        public abstract bool переключение { get; }

        public abstract double ускорение { get; }

        public abstract int ход_или_тормоз { get; }

        public abstract class Автобусная : Система_управления
        {
        }   
        public abstract class Автобусная1 : Система_управления
        {
        }
        
        // FIXME: включить голову и заменить этот костыль адекватным решением!
        protected bool PreUpdateSound(Transport transport, bool gameActive)
        {
        	var isNear = !transport.condition;
        	if (gameActive)
        	{
        		if ((isNear) && (!isPlaying))
        		{
        			foreach (var snd in SoundBuffers)
        			{
        				snd.Play();
        			}
        			isPlaying = true;
        		}
        		else if ((!isNear) && (isPlaying))
        		{
        			foreach (var snd in SoundBuffers)
        			{
        				snd.Stop();
        			}
        			isPlaying = false;
        		}
        	}
        	else if (isPlaying)
        	{
        		foreach (var snd in SoundBuffers)
        		{
        			snd.Stop();
        		}
        		isPlaying = false;
        	}
        	if (isPlaying)
        	{
        		var point = transport.Координаты3D;
        		foreach (var snd in SoundBuffers)
        		{
        			snd.Update(ref point);
        		}
        	}
        	return isPlaying;
        }

        public class КП_Авто : Система_управления.Автобусная
        {
            private int fрежим;
            private Троллейбус автобус;
            public int передача;
            public double положение_педалей = -1.0;
            public string[] режимы = new string[] { "P", "R", "N", "D", "2", "1" };

            public КП_Авто(Троллейбус автобус)
            {
                this.автобус = автобус;
            }

            public override void CreateSoundBuffers()
            {
            	SoundBuffers = new ISound3D[6];
            	for (int i = 0; i < SoundBuffers.Length; i++)
                {
            		SoundBuffers[i] = MyXAudio2.Device.CreateEmitter(60f, this.автобус.основная_папка + "engine.wav");
            		SoundBuffers[i].Volume = 1f;
            	}
            	SoundBuffers[3].Volume = 0.5f;
            	SoundBuffers[4].Volume = 0.5f;
            	SoundBuffers[5].Volume = 1.0f;
            }

            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
            	if (!PreUpdateSound(this.автобус, игра_активна))
            		return;
            	int num = Math.Max(1, Math.Abs(this.передача));
                double num2 = (this.ход_или_тормоз > 0) ? ((num + 0.5) / 5.0) : 1.0;
                double num3 = 4.3 - ((4.3 - (this.автобус.скорость_abs / ((double)num))) * num2);
                double num4 = (400.0 * this.автобус.скорость_abs) + 1000.0;
                double num5 = (400.0 * num3) + 1000.0;
                if (this.автобус.скорость_abs < 2.0)
                {
                	num4 = Math.Max(((1800.0 * this.автобус.скорость_abs) / 2.0), 1.0);
                }
                if (num3 < 2.0)
                {
                	num5 = ((this.положение_педалей > 0.0) || (this.автобус.скорость_abs >= 2.0)) ? ((90.0 * num3)) : 0.0;
                }
                if (this.передача == 0)
                {
                    num5 = 0.0;
                }
            	SoundBuffers[0].Frequency = (float)Math.Max(num4 * 2.8, 100.0) / 10000f;
                SoundBuffers[1].Frequency = (float)Math.Max(num4 * 4.2, 100.0) / 10000f;
                SoundBuffers[2].Frequency = (float)((this.автобус._soundУскоряется || this.автобус._soundЗамедляется) ? Math.Max(num5 * 6.0, 0x1f40) : 0x1f40) / 10000f;
                SoundBuffers[3].Frequency = (float)Math.Max(num5 * 10.5, 100.0) / 10000f;
                SoundBuffers[4].Frequency = (float)Math.Max(num5 * 15.0, 100.0) / 10000f;
                SoundBuffers[5].Frequency = (float)Math.Max(num5 * 15.0, 100.0) / 10000f;

            }
            //TODO: осмотреть и переделать моменты
            public override void автоматически_управлять(double рекомендуемая_скорость, double оставшееся_расстояние, int переключение)
            {
            	var ped_speed = ((World.прошлоВремени * 5.0) / 3.0);
                if ((this.режим == 0) || (this.режим == 2))
                {
                    if ((this.положение_педалей != -1.0) || (this.автобус.скорость != 0.0))
                    {
                        if (this.положение_педалей > (-1.0 + ped_speed))
                        {
                            this.положение_педалей -= ped_speed;
                            return;
                        }
                        this.положение_педалей = -1.0;
                        return;
                    }
                    this.режим = (рекомендуемая_скорость >= 0.0) ? 3 : 1;
                }
                if (this.режим > 3)
                {
                    this.режим = 3;
                }
                if ((рекомендуемая_скорость * направление) < 0.0)
                {
                    if ((положение_педалей != -1.0) || (автобус.скорость != 0.0))
                    {
                        if (положение_педалей > (-1.0 + ped_speed))
                        {
                            положение_педалей -= ped_speed;
                            return;
                        }
                        положение_педалей = -1.0;
                        return;
                    }
                    режим = направление < 0 ? 3 : 1;
                }
                рекомендуемая_скорость *= направление;
                var num = автобус.скорость * направление;
                var num2 = рекомендуемая_скорость - num;
                if (num < 0.0)
                {
                    num2 = -num2;
                }
                if (рекомендуемая_скорость <= 0.0)
                {
                    num2 -= 2.0;
                }
                var num3 = 0.0;
                if ((Math.Abs(num2) < 0.5) && (num > 0.5))
                {
                	num3 = 0.0;
                    if ((num2 > 0.1) && (num < 4.0))
                    {
                        num3 = 0.2;
                    }
                }
                else if ((num == 0.0) && (рекомендуемая_скорость <= 0.0))
                {
                    num3 = -1.0;
                }
                else if (num2 > 0.0)
                {
                    if ((num > 10.0) && (положение_педалей == 0.0))
                    {
                        num3 = 0.0;
                    }
                    else if ((num >= 0.0) && (num < 2.3))
                    {
                        num3 = 0.4;
                    }
                    else if ((num >= 2.3) && (num < 5.0))
                    {
                        num3 = 0.6;
                    }
                    else if ((num >= 5.0) && (num < 10.0))
                    {
                        num3 = 0.8;
                    }
                    else if (num >= 10.0)
                    {
                        num3 = 1.0;
                    }
                }
                else if (num2 < 0.0)
                {
                    num3 = (((num2 - 3.0) * ((num + рекомендуемая_скорость) / 2.0)) / Math.Max((double)(оставшееся_расстояние - 5.0), (double)0.6)) / 1.8;
                    if (num3 > 0.0)
                    {
                        num3 = 0.0;
                    }
                    if ((num3 < -1.0) || (num < 0.3))
                    {
                        num3 = -1.0;
                    }
                }
                                
                if (положение_педалей > (num3 + ped_speed))
                {
                    положение_педалей -= ped_speed;
                }
                else if (положение_педалей < (num3 - ped_speed))
                {
                    положение_педалей += ped_speed;
                }
                else
                {
                    положение_педалей = num3;
                }
            }

            public override int направление
            {
                get
                {
                    return Math.Sign(this.передача);
                }
            }

            public override bool переключение
            {
                get
                {
                    return false;
                }
            }

            public int режим
            {
                get
                {
                    return this.fрежим;
                }
                set
                {
                    this.fрежим = value;
                    string str = this.текущий_режим;
                    if (str != null)
                    {
                        if (!(str == "P") && !(str == "N"))
                        {
                            if (!(str == "R"))
                            {
                                if (!(str == "D"))
                                {
                                    if (!(str == "2"))
                                    {
                                        if (str == "1")
                                        {
                                            this.передача = 1;
                                        }
                                        return;
                                    }
                                    this.передача = 2;
                                    return;
                                }
                                if (this.передача <= 0)
                                {
                                    this.передача = 1;
                                }
                                return;
                            }
                        }
                        else
                        {
                            this.передача = 0;
                            return;
                        }
                        this.передача = -1;
                    }
                }
            }

            public string текущая_передача
            {
                get
                {
                    if (this.передача > 0)
                    {
                        return this.передача.ToString();
                    }
                    if (this.передача == 0)
                    {
                        return "-";
                    }
                    return "R";
                }
            }

            public string текущий_режим
            {
                get
                {
                    return this.режимы[this.fрежим];
                }
            }

            public override double ускорение
            {
                get
                {
                	if ((this.режим == 0) || (this.автобус.stand_brake))
                    {
                        return (double)(-2 * Math.Sign(this.автобус.скорость));//-50
                    }
                    double num = 0.0;
                    double num2 = 0.0;
                    double num3 = this.автобус.скорость * this.направление;
                    if (this.автобус.включен && (this.режим != 2))
                    {
                        if (num3 < 2.0)
                        {
                            num = 1.0;
                            if (num3 > 0.4)
                            {
                                num *= Math.Pow(0.4 / num3, 2.0);
                            }
                        }
                        if (this.положение_педалей > 0.0)
                        {
                            double num4 = Math.Abs(this.передача);
                            num = (2.0 * this.положение_педалей) / num4;
                            double num5 = ((4.3 * num4) - this.автобус.скорость_abs) / World.прошлоВремени;
                            if (num > num5)
                            {
                                num = num5;
                                if (this.передача < 5)
                                {
                                    this.передача++;
                                }
                                
                                else if (this.передача < 2)
                                {
                                    this.передача++;
                                }
                            }
                            if ((this.передача > 1) && (this.автобус.скорость_abs < (4.0 * (this.передача - 1))))
                        	{
                            	this.передача--;
                        	}
                        }
                    }
                    if (this.положение_педалей < 0.0)
                    {
                        num2 += -this.положение_педалей * 1.8;
                        if ((this.передача > 1) && (this.автобус.скорость_abs < (4.0 * (this.передача - 1))))
                        {
                            this.передача--;
                        }
                    }
                    if (this.автобус.скорость == 0.0)
                    {
                        num -= num2;
                        if (num < 0.0)
                        {
                            num = 0.0;
                        }
                    }
					try
					{
						return (double)((num * this.направление) - (num2 * Math.Sign(this.автобус.скорость)));
					}
					catch
					{
						return 0.0;
					}
                }
            }

            public override int ход_или_тормоз
            {
                get
                {
                    if ((this.режим == 0) || (this.автобус.stand_brake))
                    {
                        return -1;
                    }
                    if ((this.положение_педалей < 0.0) && (Math.Abs(this.автобус.скорость) < 2.0))
                    {
                        return 0;
                    }
                    return Math.Sign(this.положение_педалей);
                }
            }
        }
        
        public class КП_Авто1 : Система_управления.Автобусная1
        {
            private int fпередача;
            private Троллейбус автобус1;
            public int передача;
            public double положение_педалей = -1.0;
            public double максимальная_скорость = 21.6;

            public КП_Авто1(Троллейбус автобус)
            {
                this.автобус1 = автобус;
            }

            public override void CreateSoundBuffers()
            {
                SoundBuffers = new ISound3D[5];
                for (int i = 0; i < SoundBuffers.Length; i++)
                {
                    SoundBuffers[i] = MyXAudio2.Device.CreateEmitter(60f, this.автобус1.основная_папка + "engine.wav");
                    SoundBuffers[i].Volume = 1f;
                }
                SoundBuffers[3].Volume = 0.5f;
                SoundBuffers[4].Volume = 0.5f;
                SoundBuffers[4].Volume = 1.0f;
            }

            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
                if (!PreUpdateSound(this.автобус1, игра_активна))
                    return;
                int num = Math.Max(1, Math.Abs(this.передача));
                double num2 = (this.ход_или_тормоз > 0) ? ((num + 0.5) / 5.0) : 1.0;
                double num3 = 4.3 - ((4.3 - (this.автобус1.скорость_abs / ((double)num))) * num2);
                double num4 = (400.0 * this.автобус1.скорость_abs) + 1000.0;
                double num5 = (400.0 * num3) + 1000.0;
                if (this.автобус1.скорость_abs < 2.0)
                {
                    num4 = Math.Max(((1800.0 * this.автобус1.скорость_abs) / 2.0), 1.0);
                }
                if (num3 < 5.0)
                {
                    num5 = ((this.положение_педалей > 0.0) || (this.автобус1.скорость_abs >= 2.0)) ? ((90.0 * num3)) : 1.0;
                }
                if (this.положение_педалей == 0)
                {
                    num5 = 0.0;
                }
                SoundBuffers[0].Frequency = (float)Math.Max(num4 * 2.8, 100.0) / 10000f;
                SoundBuffers[1].Frequency = (float)Math.Max(num4 * 4.2, 100.0) / 10000f;
                SoundBuffers[2].Frequency = (float)((this.автобус1._soundУскоряется || this.автобус1._soundЗамедляется) ? Math.Max(num5 * 6.0, 0x1f40) : 0x1f40) / 10000f;
                SoundBuffers[3].Frequency = (float)Math.Max(num5 * 10.5, 100.0) / 10000f;
                SoundBuffers[4].Frequency = (float)Math.Max(num5 * 15.0, 100.0) / 10000f;
                SoundBuffers[5].Frequency = (float)Math.Max(num5 * 15.0, 100.0) / 10000f;
            }

            public override void автоматически_управлять(double рекомендуемая_скорость, double оставшееся_расстояние, int переключение)
            {
                var ped_speed = ((World.прошлоВремени * 5.0) / 3.0);
                if ((this.передача == 0) || (this.передача == 2))
                {
                    if ((this.положение_педалей != -1.0) || (this.автобус1.скорость != 0.0))
                    {
                        if (this.положение_педалей > (-1.0 + ped_speed))
                        {
                            this.положение_педалей -= ped_speed;
                            return;
                        }
                        this.положение_педалей = -1.0;
                        return;
                    }
                    this.передача_перевод = (рекомендуемая_скорость >= 0.0) ? 1 : 3;
                }
                if (this.передача > 3)
                {
                    this.передача = 3;
                }
                if ((рекомендуемая_скорость * направление) < 0.0)
                {
                    if ((положение_педалей != -1.0) || (автобус1.скорость != 0.0))
                    {
                        if (положение_педалей > (-1.0 + ped_speed))
                        {
                            положение_педалей -= ped_speed;
                            return;
                        }
                        положение_педалей = -1.0;
                        return;
                    }
                    передача_перевод = направление < 0 ? 3 : 1;
                }
                рекомендуемая_скорость *= направление;
                var num = автобус1.скорость * направление;
                var num2 = рекомендуемая_скорость - num;
                if (num < 0.0)
                {
                    num2 = -num2;
                }
                if (рекомендуемая_скорость <= 0.0)
                {
                    num2 -= 2.0;
                }
                var num3 = 0.0;
                if ((Math.Abs(num2) < 0.5) && (num > 0.5))
                {
                    num3 = 0.0;
                    if ((num2 > 0.1) && (num < 4.0))
                    {
                        num3 = 0.2;
                    }
                }
                else if ((num == 0.0) && (рекомендуемая_скорость <= 0.0))
                {
                    num3 = -1.0;
                }
                else if (num2 > 0.0)
                {
                    if ((num > 10.0) && (положение_педалей >= 0.0))
                    {
                        num3 = 0.0;
                    }
                    else if ((num >= 0.0) && (num < 2.3))
                    {
                        num3 = 0.4;
                    }
                    else if ((num >= 2.3) && (num < 5.0))
                    {
                        num3 = 0.6;
                    }
                    else if ((num >= 5.0) && (num < 10.0))
                    {
                        num3 = 0.8;
                    }
                    else if (num >= 10.0)
                    {
                        num3 = 1.0;
                    }
                }
                else if (num2 < 0.0)
                {
                    num3 = (((num2 - 3.0) * ((num + рекомендуемая_скорость) / 2.0)) / Math.Max((double)(оставшееся_расстояние - 5.0), (double)0.6)) / 1.8;
                    if (num3 > 0.0)
                    {
                        num3 = 0.0;
                    }
                    if ((num3 < -1.0) || (num < 0.3))
                    {
                        num3 = -1.0;
                    }
                }
                                
                if (положение_педалей > (num3 + ped_speed))
                {
                    положение_педалей -= ped_speed;
                }
                else if (положение_педалей < (num3 - ped_speed))
                {
                    положение_педалей += ped_speed;
                }
                else
                {
                    положение_педалей = num3;
                }
            }

            public override int направление
            {
                get
                {
                    return Math.Sign(this.передача);
                }
            }

            public override bool переключение
            {
                get
                {
                    
                    return false;
                }
            }

            public int передача_перевод
            {
                get
                {
                    return fпередача;
                }
                set
                {
                    fпередача = value;
                    string str = this.текущая_передача;
                    if (str != null)
                    {
                        if (!(str == "N"))
                        {
                            if (!(str == "R"))
                            {
                                if (!(str == "1"))
                                {
                                    if (!(str == "2"))
                                {
                                        if (!(str == "3"))
                                {
                                            if (!(str == "4"))
                                {
                                                if (!(str == "5"))
                                {
                                                    if (!(str == "6"))
                                {
                                                      this.передача = 5;
                                                      return;  
                                                    }
                                                 this.передача = 5;
                                                 return;
                                         }
                                             this.передача = 4;
                                             return;
                                        }
                                         this.передача = 3;
                                         return;
                                    }
                                        this.передача = 2;
                                        return;
                                       }
                                    this.передача = 1;
                                    return;
                                }
                                this.передача = -1;
                                return;
                            }
                            this.передача = 0;
                            return;
                      }
                   }
                }
             }
                
            

            public string текущая_передача
            {
                get
                {
                    {
                        {
                        if (this.передача > 0)
                    {
                        return this.передача.ToString();
                    }
                    if (this.передача == 0)
                    {
                        return "N";
                    }
                    if (this.передача == 5)
                    {
                        return "5";
                    }
                    if (this.передача >= 6)
                        {
                        передача--;
                        return "Передача недоступна";
                    }
                    return "R";
                        }
                    }
                }
            }

            /*public string текущий_режим
            {
                get
                {
                    return this.режимы[this.fрежим];
                }
            }*/

            public override double ускорение
            {
                get
                {
                    if (this.автобус1.stand_brake)
                    {
                        return (double)(-2 * Math.Sign(this.автобус1.скорость));//-50
                    }
                    double num = 0.0;
                    double num2 = 0.0;
                    double num3 = this.автобус1.скорость * this.направление;
                    //double num6 = (4.3 * (Math.Abs(this.передача))) / World.прошлоВремени;
                    if (this.автобус1.включен)
                    {
                        if (num3 < 2.0)
                        {
                            num = 1.0;
                            if (num3 > 0.4)
                            {
                                num *= Math.Pow(0.4 / num3, 2.0);
                            }
                        }
                        if (this.положение_педалей > 0.0)
                        {
                            double num4 = Math.Abs(this.передача);
                            num = (2.0 * this.положение_педалей) / num4;
                            double num5 = ((4.3 * num4) - this.автобус1.скорость_abs) / World.прошлоВремени;
                            double num6 = (4.3 * (Math.Abs(this.передача))) / World.прошлоВремени;
                            if (num > num5)
                            {
                                num = num5;
                                if ((this.текущая_передача == "1") && (this.передача < 5))
                                {
                                    this.передача++;
                                }
                                if (this.текущая_передача == "5")
                                {
                                    this.передача = 5;
                                }
                            }
                            if ((this.передача > 1) && (this.автобус1.скорость_abs < (4.0 * (this.передача - 1))))
                            {
                                this.передача--;
                            }
                        }
                    }
                    if (this.положение_педалей < 0.0)
                    {
                        num2 += -this.положение_педалей * 1.8;
                        if ((this.передача > 1) && (this.автобус1.скорость < (4.0 * (this.передача - 1))))
                        {
                            this.передача--;
                        }
                    }
                    if (this.автобус1.скорость == 0.0)
                    {
                        num -= num2;
                        if (num < 0.0)
                        {
                            num = 0.0;
                        }
                    }
                    try
                    {
                        return (double)((num * this.направление) - (num2 * Math.Sign(this.автобус1.скорость)));
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
            }

            public override int ход_или_тормоз
            {
                get
                {
                    if (this.автобус1.stand_brake)
                    {
                        return -1;
                    }
                    if ((this.положение_педалей < 0.0) && (Math.Abs(this.автобус1.скорость) < 0.0))
                    {
                        return 0;
                    }
                    return Math.Sign(this.положение_педалей);
                }
            }
        }

        public class РКСУ_Трамвай : Система_управления
        {
            public readonly int позиция_max = 4;
            public readonly int позиция_min = -5;
            public int позиция_контроллера;
            public int позиция_реверсора = 1;
            private Трамвай трамвай;
            
            public РКСУ_Трамвай(Трамвай трамвай)
            {
            	this.трамвай = трамвай;
            }
            
            public override void CreateSoundBuffers()
            {
            	SoundBuffers = new ISound3D[3];
            	for (int i = 0; i < SoundBuffers.Length; i++)
                {
            		SoundBuffers[i] = MyXAudio2.Device.CreateEmitter(60f, this.трамвай.основная_папка + "Sound 1.wav");
            		SoundBuffers[i].Volume = 1f;
            	}
#if false
            	SoundBufferDescription desc = new SoundBufferDescription();
                var loader = new SoundLoader(this.трамвай.основная_папка + "Sound 1.wav");
				desc.SizeInBytes = loader.OutBytes.Length;
				desc.Format = loader.Format;
                desc.Flags = BufferFlags.ControlVolume | BufferFlags.ControlFrequency;
                base.SoundBuffers = new SlimDX.DirectSound.SecondarySoundBuffer[3];
                base.SoundBuffers_Volume = new int[3];
                for (int i = 0; i < SoundBuffers.Length; i++)
                {
                	base.SoundBuffers[i] = new SlimDX.DirectSound.SecondarySoundBuffer(MyDirectSound.device, desc);//this.автобус.основная_папка + "engine.wav", desc, MyDirectSound.device);
                	base.SoundBuffers[i].Frequency = 100;
                	base.SoundBuffers[i].Volume = -10000;
                	base.SoundBuffers[i].Write(loader.OutBytes, 0, LockFlags.None);
                	base.SoundBuffers[i].Play(0, PlayFlags.Looping);
                }
#endif
            }
            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
            	if (!PreUpdateSound(this.трамвай, игра_активна))
            		return;
            	SoundBuffers[0].Frequency = ((трамвай.скорость_abs > 1.0)
            	                ? (((float)(3000.0 * трамвай.скорость_abs)) + 1000) : /*5*/000) / 10000.0f;
                SoundBuffers[1].Frequency = SoundBuffers[0].Frequency * 2;
                SoundBuffers[2].Frequency = (SoundBuffers[0].Frequency * 25) / 16;// + 153;
                
                SoundBuffers[0].Volume = (трамвай._soundУскоряется || трамвай._soundЗамедляется) ? 0.8f : 0.2f;
                SoundBuffers[1].Volume = SoundBuffers[0].Volume;
                SoundBuffers[2].Volume = (трамвай.скорость_abs < 6.0)
                	? ((((((SoundBuffers[0].Volume * 10000) - 10000 + 2000) * (float)(трамвай.скорость_abs)) / 6.0f) + 8000.0f) / (10000.0f))
                	: SoundBuffers[0].Volume;
                
                
#if false
            	frequency[0] = (трамвай.скорость_abs > 1.0) ? (((int)(3000.0 * трамвай.скорость_abs)) + 0x3e8) : 5000;//(((int)(3000.0 * трамвай.скорость_abs)) + 0x3e8);
                frequency[1] = frequency[0] * 2;
                frequency[2] = (frequency[0] * 0x19) / 0x10 + 0x99;
                volume[0] = -10000;
                volume[1] = -10000;
                volume[2] = -10000;
                while ((трамвай._soundУскоряется || трамвай._soundЗамедляется) && SoundBuffers_Volume[0]<= -200)
                {
                	SoundBuffers_Volume[0] += 200;
                }
                while ((!трамвай._soundУскоряется && !трамвай._soundЗамедляется) && SoundBuffers_Volume[0]>= -1800)
                {
                	SoundBuffers_Volume[0] -= 200;
                }
                SoundBuffers_Volume[1] = SoundBuffers_Volume[0];
                if (трамвай.скорость_abs < 6.0)
                {
                    SoundBuffers_Volume[2] = (int)((((SoundBuffers_Volume[0] + 0x7d0) * (трамвай.скорость_abs - 3.0)) / 3.0) - 2000.0);// - 2000
                }
                else
                {
                    SoundBuffers_Volume[2] = SoundBuffers_Volume[0];
                }
                if (игра_активна && (volume_muting < 0))
                {
                    volume_muting += 500;
                }
                else if (!игра_активна && (volume_muting > -10000))
                {
                    volume_muting -= 500;
                }
                for (int i = 0; i < игроки.Length; i++)
                {
                    Double3DPoint point = new Double3DPoint(трамвай.position.x, 1.0, трамвай.position.y) - игроки[i].cameraPosition;
                    double vdist = point.модуль;
                    if (vdist < 120.0)//120
                    {
                        for (int j = 0; j < SoundBuffers.Length; j++)
                        {
                            int vol_j = SoundBuffers_Volume[j];
                            if (vdist > 15.0)
                            {
                                int vol2 = vol_j - ((int)((vdist - 15.0) * 20.0));
                                vol_j = (vol2 > -10000) ? vol2 : -10000;
                                if (vdist > 100.0)
                                {
                                    vol2 = vol_j - ((int)((vdist - 100.0) * 100.0));
                                    vol_j = (vol2 > -10000) ? vol2 : -10000;
                                }
                            }
                            if (frequency[j] < 0x1388)
                            {
                                int num6 = vol_j - ((0x1388 - frequency[j]) * 2);
                                vol_j = (num6 > -10000) ? num6 : -10000;
                            }
                            if (volume[j] < (vol_j + volume_muting))
                            {
                                volume[j] = vol_j + volume_muting;
                            }
                        }
                    }
                }
                try
                {
                    for (int k = 0; k < SoundBuffers.Length; k++)
                    {
                    	if ((volume[k] > -10000) && (SoundBuffers[k].Status != BufferStatus.Playing))
                        {
                            SoundBuffers[k].Play(0, PlayFlags.Looping);
                        }
                        else if ((volume[k] <= -10000) && (SoundBuffers[k].Status == BufferStatus.Playing))
                        {
                            SoundBuffers[k].Stop();
                        }
                    }
                    if (SoundBuffers[0].Volume != volume[0])
                    {
                        SoundBuffers[0].Volume = volume[0];
                    }
                    if (SoundBuffers[1].Volume != volume[1])
                    {
                        SoundBuffers[1].Volume = volume[1];
                    }
                    if (SoundBuffers[2].Volume != volume[2])
                    {
                        SoundBuffers[2].Volume = volume[2];
                    }
                    if (SoundBuffers[0].Frequency != frequency[0])
                    {
                        SoundBuffers[0].Frequency = frequency[0];
                    }
                    if (SoundBuffers[1].Frequency != frequency[1])
                    {
                        SoundBuffers[1].Frequency = frequency[1];
                    }
                    if (SoundBuffers[2].Frequency != frequency[2])
                    {
                        SoundBuffers[2].Frequency = frequency[2];
                    }
                }
                catch
                {
                }
#endif
            }
            public override int ход_или_тормоз
            {
                get
                {
                	if (this.трамвай.stand_brake) return -1;
                    return Math.Sign(позиция_контроллера);
                }
            }
            public override double ускорение
            {
                get
                {
                    double num = 0.0;
                    double num2 = трамвай.скорость * позиция_реверсора;
                    switch (позиция_контроллера)
                    {
                    	case -5:
                    		{
                    			num = -2.1;
                    		}
                    		break;
                    		
                    	case -4:
                    		{
                    			num = -2.0;
                    		}
                    		break;
                    		
                    	case -3:
                    		{
                    			num = -1.5;
                    		}
                    		break;
                    		
                    	case -2:
                    		{
                    			num = -1.1;
                    		}
                    		break;
                    		
                    	case -1:
                    		{
                    			num = -0.7;
                    		}
                    		break;
                    		
                    	case 1:
                    		{
	                    		num = 0.8;
		                        if (num2 > 2.0)
		                        {
		                            num *= Math.Pow(2.0 / num2, 4.0);
		                        }
                    		}
	                        break;
	                        
	                    case 2:
	                        {
		                        num = 1.1;
		                        if (num2 > 8.0)
		                        {
		                            num *= Math.Pow(8.0 / num2, 4.0);
		                        }
	                        }
	                        break;
	                        
	                    case 3:
	                        {
		                        num = 1.3;
		                        if (num2 > 8.0)
		                        {
		                            num *= Math.Pow(8.0 / num2, 4.0);
		                        }
	                        }
	                        break;
	                        
	                    case 4:
	                        {
		                        num = 1.5;
		                        if (num2 > 6.0)
		                        {
		                            num *= 6.0 / num2;
		                        }
		                        if (num2 > 15.0)
		                        {
		                            num *= Math.Pow(15.0 / num2, 4.0);
		                        }
                    		}
	                        break;
                    }
                    if (this.трамвай.stand_brake) num -= 2.1;
                    if ((позиция_контроллера < 0) || (this.трамвай.stand_brake))
                    {
                        return (num * Math.Sign(трамвай.скорость));
                    }
                    return (num * позиция_реверсора);
                }
            }
            public override int направление
            {
                get
                {
                    return позиция_реверсора;
                }
            }

            public override bool переключение
            {
                get
                {
					if (трамвай.токоприёмник.поднят && (трамвай.передняя_ось.текущий_рельс.следующие_рельсы.Length > 1))
	                {
	                    DoublePoint point = трамвай.токоприёмник.position.XZPoint - трамвай.передняя_ось.текущий_рельс.добавочные_провода.координаты;
	                    return (point.Modulus < 0.5);
	                }
	                return false;
                }
            }
            //TODO: требует доработки, найдены баги с переводом стрелок
            public override void автоматически_управлять(double рекомендуемаяСкорость, double оставшеесяРасстояние, int переключение)
        	{
            	/*if ((трамвай.скорость == 0.0) && ((рекомендуемаяСкорость * позиция_реверсора) < 0.0))
                {
                    позиция_реверсора = -позиция_реверсора;
                }*/
            	var current_speed = трамвай.скорость * позиция_реверсора;
            	var rec_speed = рекомендуемаяСкорость - current_speed;
	            if ((current_speed + rec_speed) < 0.0)
	            {
	                rec_speed -= current_speed + rec_speed;
	            }
	            if ((переключение > 0) && (трамвай.скорость > 0.0))
	            {
	                if ((переключение == 0) != Рельс.стрелки_наоборот)
	                {
	                    if (позиция_контроллера > 0)
	                    {
	                        позиция_контроллера = 0;
	                    }
	                    else if (позиция_контроллера <= -5)
	                    {
	                        позиция_контроллера = -4;
	                    }
	                }
	                else if ((позиция_контроллера <= 0) && (позиция_контроллера > -5))
	                {
	                    позиция_контроллера = 1;
	                }
	            }
	            else if ((Math.Abs(rec_speed) < 0.5) && (трамвай.скорость_abs > 0.5))
	            {
	                if ((rec_speed > 0.1) && (трамвай.скорость_abs < 2.8))
	                {
	                    позиция_контроллера = 1;
	                }
	                else
	                {
	                    позиция_контроллера = 0;
	                }
	            }
	            else if ((трамвай.скорость_abs == 0.0) && (рекомендуемаяСкорость <= 0.0))
	            {
	                позиция_контроллера = 0;
	            }
	            else if (rec_speed > 0.0)
	            {
	                if ((трамвай.скорость_abs > 5.0) && (позиция_контроллера == 0))
	                {
	                    позиция_контроллера = 0;
	                }
	                else if ((трамвай.скорость_abs >= 0.0) && (трамвай.скорость_abs < 2.3))
	                {
	                    позиция_контроллера = 1;
	                }
	                else if ((трамвай.скорость_abs >= 2.3) && (трамвай.скорость_abs < 5.0))
	                {
	                    позиция_контроллера = 2;
	                }
	                else if ((трамвай.скорость_abs >= 5.0) && (трамвай.скорость_abs < 10.0))
	                {
	                    позиция_контроллера = 3;
	                }
	                else if (трамвай.скорость_abs >= 10.0)
	                {
	                    позиция_контроллера = 4;
	                }
	            }
	            else if (rec_speed < 0.0)
	            {
	                позиция_контроллера = -1;
	                if ((оставшеесяРасстояние / ((current_speed + рекомендуемаяСкорость) / 2.0)) < (rec_speed / ускорение))
	                {
	                    позиция_контроллера = -2;
	                    if ((оставшеесяРасстояние / ((current_speed + рекомендуемаяСкорость) / 2.0)) < (rec_speed / ускорение))
	                    {
	                        позиция_контроллера = -3;
	                        if ((оставшеесяРасстояние / ((current_speed + рекомендуемаяСкорость) / 2.0)) < (rec_speed / ускорение))
	                        {
	                            позиция_контроллера = -4;
	                            if ((оставшеесяРасстояние / ((current_speed + рекомендуемаяСкорость) / 2.0)) < (rec_speed / ускорение))
	                            {
	                                позиция_контроллера = -5;
	                            }
	                        }
	                    }
	                }
	            }
        	}
       }
        
        public class РКСУ_Троллейбус : Система_управления
        {
            public double пневматический_тормоз;
            public readonly int позиция_max = 4;
            public readonly int позиция_min = -2;
            public int позиция_контроллера;
            public int позиция_реверсора = 1;
            public int включение_ах = 0;
            public bool двери;
            public double действие_дверей;
            Двери _двери;
            private Троллейбус троллейбус;

            public РКСУ_Троллейбус(Троллейбус троллейбус)
            {
                this.троллейбус = троллейбус;
            }

            public override void CreateSoundBuffers()
            {
            	SoundBuffers = new ISound3D[7];
            	SoundBuffers[0] = MyXAudio2.Device.CreateEmitter(60f, this.троллейбус.основная_папка + "Sound 1.wav");
            	SoundBuffers[1] = MyXAudio2.Device.CreateEmitter(60f, this.троллейбус.основная_папка + "Sound 1.wav");
            	SoundBuffers[2] = MyXAudio2.Device.CreateEmitter(40f, this.троллейбус.основная_папка + "Sound 1.wav");
            	SoundBuffers[3] = MyXAudio2.Device.CreateEmitter(60f, this.троллейбус.основная_папка + "Sound 2.wav");
            	SoundBuffers[4] = MyXAudio2.Device.CreateEmitter(40f, this.троллейбус.основная_папка + "Sound 2.wav");
            	SoundBuffers[5] = MyXAudio2.Device.CreateEmitter(30f, this.троллейбус.основная_папка + "Sound 3.wav");
            	SoundBuffers[6] = MyXAudio2.Device.CreateEmitter(30f, this.троллейбус.основная_папка + "dopen.wav");
            	SoundBuffers[6] = MyXAudio2.Device.CreateEmitter(30f, this.троллейбус.основная_папка + "dclose.wav");
            	SoundBuffers[5].Volume = 0.5f;
            	SoundBuffers[6].Volume = 0.5f;
            	SoundBuffers[5].Frequency = 0.5f;
            }

            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
            	if (!PreUpdateSound(this.троллейбус, игра_активна))
            		return;
            	//...
            	float num = (600.0f * (float)this.троллейбус.скорость_abs) + 1000.0f;
                if (this.троллейбус.скорость_abs < 1.0)
                {
                	num = Math.Max(1600.0f * (float)this.троллейбус.скорость_abs, 1.0f);
                }
                num /= 20000.0f;
                if (!this.троллейбус.обесточен && this.троллейбус.скорость_abs > 0)
                {
                    base.SoundBuffers[0].Frequency = (num * 2.0f);
                    base.SoundBuffers[1].Frequency = (num * 3.0f);
                    base.SoundBuffers[2].Frequency = (num * 5.0f);
                    base.SoundBuffers[3].Frequency = (num * 7.0f);
                    base.SoundBuffers[4].Frequency = (num * 10.0f);
                }
                if (this.троллейбус.обесточен)
                {
                    base.SoundBuffers[0].Frequency = (num * 0.0f);
                    base.SoundBuffers[1].Frequency = (num * 0.0f);
                    base.SoundBuffers[2].Frequency = (num * 0.0f);
                    base.SoundBuffers[3].Frequency = (num * 0.0f);
                    base.SoundBuffers[4].Frequency = (num * 10.0f);
                }
                /*if (_двери)
                {
                    
                   // base.SoundBuffers[6].Frequency = (num * 10.0f);
                }*/
                else
                {
                    for (int k = 0; k < this.frequency.Length; k++)
                    {
                    	this.frequency[k] = Math.Max((int)(this.frequency[k] * 0.99), 100);
                    }
                }
                if (this.троллейбус.скорость == 0.0)
                {
                    base.SoundBuffers[0].Frequency = (num * 0.0f);
                    base.SoundBuffers[1].Frequency = (num * 0.0f);
                    base.SoundBuffers[2].Frequency = (num * 0.0f);
                    base.SoundBuffers[3].Frequency = (num * 0.0f);
                    base.SoundBuffers[4].Frequency = (num * 0.0f);
                }
                else if (this.троллейбус._soundУскоряется || this.троллейбус._soundЗамедляется)
                {
                	base.SoundBuffers[2].Volume = Math.Min(base.SoundBuffers[2].Volume + 0.2f, 1f);
                }
                else
                {
                	base.SoundBuffers[2].Volume = Math.Max(base.SoundBuffers[2].Volume - 0.2f, 0f);
                }
                base.SoundBuffers[0].Volume = 1f;
                base.SoundBuffers[1].Volume = 1f;
                base.SoundBuffers[3].Volume = 0.8f;
                if (this.троллейбус.скорость_abs > 2.5)
                {
                	base.SoundBuffers[3].Volume = 1.0f - (float)Math.Pow(2.0, this.троллейбус.скорость_abs / (3600.0f * 50)) / 1000.0f;
                }
                base.SoundBuffers[4].Volume = base.SoundBuffers[3].Volume;
            	//...
            	if (this.троллейбус.обесточен && this.троллейбус.включен)
                {
                	base.SoundBuffers[5].Play();
                }
            	else
            	{
            		base.SoundBuffers[5].Stop();
            	}
            }
            //TODO: пиздец жопы под названием автоматика троллейбуса переделать ПОЛНОСТЬЮ 
            public override void автоматически_управлять(double рекомендуемаяСкорость, double оставшеесяРасстояние, int переключение)
            {
                if ((троллейбус.скорость == 0.0) && ((рекомендуемаяСкорость * позиция_реверсора) < 0.0))
                {
                    позиция_реверсора = -позиция_реверсора;
                }
                /*if (троллейбус.ах.есть)
                {
                    включение_ах = -включение_ах;
                }*/
                рекомендуемаяСкорость *= позиция_реверсора;
                var current_speed = троллейбус.скорость * позиция_реверсора;
                var dspeed = рекомендуемаяСкорость - current_speed;
                if ((current_speed + dspeed) < 0.0)
                {
                    dspeed -= current_speed + dspeed;
                }
                if (current_speed < 0.0)
                {
                    dspeed = -dspeed;
                }
                пневматический_тормоз = 0.0;
                if ((переключение >= 0) && (current_speed > 0.0))
                {
                    if (переключение != 0)
                    {
                        if ((переключение == 1) && (позиция_контроллера > 0))
                        {
                            позиция_контроллера = 0;
                        }
                    }
                    else if (позиция_контроллера <= 0)
                    {
                        позиция_контроллера = 1;
                    }
                }
                else if ((Math.Abs(dspeed) < 0.5) && (current_speed > 0.5))
                {
                    if ((dspeed > 0.5) && (current_speed < 2.8))
                    {
                        позиция_контроллера = 1;
                    }
                    else
                    {
                        позиция_контроллера = 0;
                    }
                }
                else if ((current_speed == 0.0) && (рекомендуемаяСкорость <= 0.0))
                {
                    позиция_контроллера = 0;
                }
                else if (dspeed > 0.0)
                {
                    if ((current_speed > 2.3) && (позиция_контроллера == 0) && (dspeed < 1.5))
                    {
                        позиция_контроллера = 0;
                    }
                    else if ((current_speed >= 0.0) && (current_speed < 2.3))
                    {
                        this.позиция_контроллера = 1;
                    }
                    else if ((current_speed >= 0.0) && (current_speed < 5.0))
                    {
                        позиция_контроллера = 2;
                    }
                    else if ((current_speed >= 5.0) && (current_speed < 10.0))
                    {
                        позиция_контроллера = 3;
                    }
                    else if (current_speed >= 10.0)
                    {
                        позиция_контроллера = 4;
                    }
                }
                else if (dspeed < 0.0)
                {
                    позиция_контроллера = -1;
                    if ((оставшеесяРасстояние / ((current_speed + рекомендуемаяСкорость) )) < (dspeed / ускорение))
                    {
                        позиция_контроллера = -2;
                        if ((оставшеесяРасстояние / ((current_speed + рекомендуемаяСкорость) )) < (dspeed / ускорение))// /2.0
                        {
                        	пневматический_тормоз = (((dspeed - 2.0) * ((current_speed + рекомендуемаяСкорость) / 2.0)) / Math.Max((double)(оставшеесяРасстояние - 5.0), (double)0.6)) / -1.8;
                            if (пневматический_тормоз < 0.0)
                            {
                                пневматический_тормоз = 0.0;
                            }
                            if (пневматический_тормоз > 1.0)
                            {
                                пневматический_тормоз = 1.0;
                            }
                        }
                    }
                }
            }

            public override int направление
            {
                get
                {
                    return позиция_реверсора;
                }
            }

            public override bool переключение
            {
                get
                {
                    //if (троллейбус.скорость < 2.0);
                    return (позиция_контроллера > 0);
                }
            }

            public override double ускорение
            {
                get
                {
                    double num = 0.0;
                    double num2 = this.троллейбус.скорость * this.позиция_реверсора;
                    double num3 = ((троллейбус.ах != null) && (троллейбус.ах.включён)) ? троллейбус.ах.ускорение : 1.0;
                    if (!this.троллейбус.обесточен)
                    {
                        switch (this.позиция_контроллера)
                        {
                        	case -2:
                        		{
                        			num = -1.1 * num3;
                        		}
                        		break;
                        	case -1:
                        		{
                        			num = -0.7 * num3;
                        		}
                        		break;
                        	case 1:
                        		{
	                        		num = 0.8 * num3;
		                            if (num2 > 2.0 * num3)
		                            {
		                            	num *= Math.Pow((2.0 * num3) / num2, 4.0);
		                            }
                        		}
                        		break;
                        	case 2:
                        		{
	                        		num = 1.1 * num3;
		                            if (num2 > 8.0 * num3)
		                            {
		                            	num *= Math.Pow((8.0 * num3) / num2, 4.0);
		                            }
                        		}
                        		break;
                        	case 3:
                        		{
	                        		num = 1.3 * num3;
		                            if (num2 > 8.0 * num3)
		                            {
		                            	num *= Math.Pow((8.0 * num3) / num2, 4.0);
		                            }
                        		}
                        		break;
                        	case 4:
                        		{
	                        		num = 1.5;
		                            if (num2 > 6.0 * num3)
		                            {
		                            	num *= (6.0 * num3) / num2;
		                            }
		                            if (num2 > 15.0 * num3)
		                            {
		                            	num *= Math.Pow((15.0 * num3) / num2, 4.0);
		                            }
                        		}
                        		break;
                        }
                    }
                    num -= this.пневматический_тормоз * 1.5;
                    if (this.троллейбус.stand_brake) num -= (1.0 - this.пневматический_тормоз) * 1.8;
                    if ((this.позиция_контроллера < 0) || (this.троллейбус.stand_brake))
                    {
                    	return (num * Math.Sign(this.троллейбус.скорость));
                    }
                    return (num * this.позиция_реверсора);
                }
            }

            public override int ход_или_тормоз
            {
                get
                {
                	if (this.троллейбус.stand_brake) return -1;
                    return Math.Sign(this.позиция_контроллера);
                }
            }
        }
    }
}

