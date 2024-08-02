namespace Trancity
{
    using Common;
//    using Microsoft.DirectX.DirectSound;
    using SlimDX.DirectSound;
    using SlimDX.Multimedia;
    using System;
    using System.Reflection;

    public abstract class Система_управления
    {
//        public SlimDX.DirectSound.PrimarySoundBuffer[] SoundBuffers;
        public SlimDX.DirectSound.SecondarySoundBuffer[] SoundBuffers;
//        public SoundBuffer3D[] buffers3d;
        protected int[] SoundBuffers_Volume;
        protected int[] frequency = new int[6];
        protected int[] last_frequency = new int[6];
        protected int[] volume = new int[6];
        protected int volume_muting;

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
            		          	
            	   case "РКСУ_Троллейбус":
            		return new РКСУ_Троллейбус((Троллейбус)transport);

                   case "КП_Авто":
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
//                BufferDescription desc = new BufferDescription();
                SoundBufferDescription desc = new SoundBufferDescription();
                var loader = new SoundLoader(this.автобус.основная_папка + "engine.wav");
//                desc.ControlFrequency = true;
//                desc.ControlVolume = true;
				desc.SizeInBytes = loader.OutBytes.Length;
				desc.Format = loader.Format;
                desc.Flags = BufferFlags.ControlVolume | BufferFlags.ControlFrequency;
//                base.SoundBuffers = new Microsoft.DirectX.DirectSound.Buffer[6];
                base.SoundBuffers = new SlimDX.DirectSound.SecondarySoundBuffer[5];
                base.SoundBuffers_Volume = new int[5];
                for (int i = 0; i < SoundBuffers.Length; i++)
                {
                	base.SoundBuffers[i] = new SlimDX.DirectSound.SecondarySoundBuffer(MyDirectSound.device, desc);//this.автобус.основная_папка + "engine.wav", desc, MyDirectSound.device);
                	base.SoundBuffers[i].Frequency = 100;
                	base.SoundBuffers[i].Volume = -10000;
                	base.SoundBuffers[i].Write(loader.OutBytes, 0, LockFlags.None);
                	base.SoundBuffers[i].Play(0, PlayFlags.Looping);
                }
            }

            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
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
                	num5 = ((this.положение_педалей > 0.0) || (this.автобус.скорость_abs >= 2.0)) ? ((1800.0 * num3) / 2.0) : 0.0;
                }
                if (this.передача == 0)
                {
                    num5 = 0.0;
                }
                if (this.автобус.включен)
                {
                    this.frequency[0] = (int)Math.Max(num4 * 2.8, 100.0);
                    this.frequency[1] = (int)Math.Max(num4 * 4.2, 100.0);
                    this.frequency[2] = (this.автобус._soundУскоряется || this.автобус._soundЗамедляется) ? Math.Max((int)(num5 * 6.0), 0x1f40) : 0x1f40;
                    this.frequency[3] = (int)Math.Max(num5 * 10.5, 100.0);
                    this.frequency[4] = (int)Math.Max(num5 * 15.0, 100.0);
                }
                else
                {
                    for (int m = 0; m < this.frequency.Length; m++)
                    {
                    	this.frequency[m] = Math.Max((int)(this.frequency[m] * 0.99), 100);
                    }
                }
                for (int j = 0; j < this.frequency.Length; j++)
                {
                	this.frequency[j] = Math.Min(Math.Max(this.frequency[j], (int)Math.Max(this.last_frequency[j] * 0.985, (double)(this.last_frequency[j] - 500))), (int)Math.Max(((double)this.last_frequency[j]) / 0.985, (double)(this.last_frequency[j] + 500)));
                    this.last_frequency[j] = this.frequency[j];
                }
                this.volume[0] = -10000;
                this.volume[1] = -10000;
                this.volume[2] = -10000;
                this.volume[3] = -10000;
                this.volume[4] = -10000;
                this.volume[5] = -10000;
                this.frequency[5] = 0x1388;
                base.SoundBuffers_Volume[0] = 0;
                base.SoundBuffers_Volume[1] = 0;
                base.SoundBuffers_Volume[2] = 0;
                base.SoundBuffers_Volume[3] = 0;
                base.SoundBuffers_Volume[4] = base.SoundBuffers_Volume[3];
                if (игра_активна && (this.volume_muting < 0))
                {
                    this.volume_muting = -((int)Math.Pow(Math.Sqrt((double)-this.volume_muting) - 10.0, 2.0));
                }
                else if (!игра_активна && (this.volume_muting > -10000))
                {
                    this.volume_muting = -((int)Math.Pow(Math.Sqrt((double)-this.volume_muting) + 10.0, 2.0));
                }
                for (int k = 0; k < игроки.Length; k++)
                {
                    Double3DPoint point = new Double3DPoint(this.автобус.position.x, 1.0, this.автобус.position.y) - игроки[k].cameraPosition;
                    double num10 = point.модуль;
                    if (num10 < 120.0)
                    {
                        for (int n = 0; n < base.SoundBuffers.Length; n++)
                        {
                            int num12 = base.SoundBuffers_Volume[n];
                            if (num10 > 15.0)
                            {
                                int num13 = num12 - ((int)((num10 - 15.0) * 20.0));
                                num12 = Math.Max(num13, -10000);//(num13 > -10000) ? num13 : -10000;
                                if (num10 > 100.0)
                                {
                                    num13 = num12 - ((int)((num10 - 100.0) * 100.0));
                                    num12 = Math.Max(num13, -10000);//(num13 > -10000) ? num13 : -10000;
                                }
                            }
                            if (this.frequency[n] < 0x1388)
                            {
                                int num14 = num12 - (0x2625a0 / this.frequency[n]);
                                num12 = Math.Max(num14, -10000);//(num14 > -10000) ? num14 : -10000;
                            }
                            this.volume[n] = Math.Max(this.volume[n], (num12 + this.volume_muting));
                        }
                    }
                }
                try
                {
                    for (int num15 = 0; num15 < 5; num15++)
                    {
                    	if ((this.volume[num15] > -10000) && (base.SoundBuffers[num15].Status != BufferStatus.Playing))
                        {
                            base.SoundBuffers[num15].Play(0, PlayFlags.Looping);
                        }
                    	else if ((this.volume[num15] <= -10000) && (base.SoundBuffers[num15].Status == BufferStatus.Playing))
                        {
                            base.SoundBuffers[num15].Stop();
                        }
                    }
                    if (base.SoundBuffers[0].Volume != this.volume[0])
                    {
                        base.SoundBuffers[0].Volume = this.volume[0];
                    }
                    if (base.SoundBuffers[1].Volume != this.volume[1])
                    {
                        base.SoundBuffers[1].Volume = this.volume[1];
                    }
                    if (base.SoundBuffers[2].Volume != this.volume[2])
                    {
                        base.SoundBuffers[2].Volume = this.volume[2];
                    }
                    if (base.SoundBuffers[3].Volume != this.volume[3])
                    {
                        base.SoundBuffers[3].Volume = this.volume[3];
                    }
                    if (base.SoundBuffers[4].Volume != this.volume[4])
                    {
                        base.SoundBuffers[4].Volume = this.volume[4];
                    }
                    if (base.SoundBuffers[0].Frequency != this.frequency[0])
                    {
                        base.SoundBuffers[0].Frequency = this.frequency[0];
                    }
                    if (base.SoundBuffers[1].Frequency != this.frequency[1])
                    {
                        base.SoundBuffers[1].Frequency = this.frequency[1];
                    }
                    if (base.SoundBuffers[2].Frequency != this.frequency[2])
                    {
                        base.SoundBuffers[2].Frequency = this.frequency[2];
                    }
                    if (base.SoundBuffers[3].Frequency != this.frequency[3])
                    {
                        base.SoundBuffers[3].Frequency = this.frequency[3];
                    }
                    if (base.SoundBuffers[4].Frequency != this.frequency[4])
                    {
                        base.SoundBuffers[4].Frequency = this.frequency[4];
                    }
                }
                catch
                {
                }
            }

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
                                if ((this.текущий_режим == "D") && (this.передача < 5))
                                {
                                    this.передача++;
                                }
                                else if ((this.текущий_режим == "2") && (this.передача < 2))
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

        
        /// <summary>
        ///new
        /// </summary>
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
            	/*BufferDescription desc = new BufferDescription();
                desc.ControlFrequency = true;
                desc.ControlVolume = true;
                SoundBuffers = new Microsoft.DirectX.DirectSound.Buffer[3];
                SoundBuffers_Volume = new int[3];
                SoundBuffers[0] = new Microsoft.DirectX.DirectSound.Buffer(this.трамвай.основная_папка + "Sound 1.wav", desc, MyDirectSound.device);
                SoundBuffers[0].Frequency = 100;//100
                SoundBuffers[0].Volume = -10000;
                SoundBuffers[0].Play(0, BufferPlayFlags.Looping);
                SoundBuffers[1] = new Microsoft.DirectX.DirectSound.Buffer(this.трамвай.основная_папка+ "Sound 1.wav", desc, MyDirectSound.device);
                SoundBuffers[1].Frequency = 100;
                SoundBuffers[1].Volume = -10000;
                SoundBuffers[1].Play(0, BufferPlayFlags.Looping);
                SoundBuffers[2] = new Microsoft.DirectX.DirectSound.Buffer(this.трамвай.основная_папка + "Sound 1.wav", desc, MyDirectSound.device);
                SoundBuffers[2].Frequency = 100;
                SoundBuffers[2].Volume = -10000;
                SoundBuffers[2].Play(0, BufferPlayFlags.Looping);*/
            }
            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
            	frequency[0] = (((int)(3000.0 * трамвай.скорость_abs)) + 0x3e8);//(трамвай.скорость_abs > 1.0) ? (((int)(3000.0 * трамвай.скорость_abs)) + 0x3e8) : 5000;
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
	                    DoublePoint point = трамвай.токоприёмник.position.xz_point - трамвай.передняя_ось.текущий_рельс.добавочные_провода.координаты;
	                    return (point.модуль < 0.5);
	                }
	                return false;
                }
            }
            public override void автоматически_управлять(double рекомендуемаяСкорость, double оставшеесяРасстояние, int num0)
        	{
            	/*if ((трамвай.скорость == 0.0) && ((рекомендуемаяСкорость * позиция_реверсора) < 0.0))
                {
                    позиция_реверсора = -позиция_реверсора;
                }*/
            	var num8 = трамвай.скорость * позиция_реверсора;
            	var num9 = рекомендуемаяСкорость - num8;
	            if ((num8 + num9) < 0.0)
	            {
	                num9 -= num8 + num9;
	            }
	            if (переключение && (трамвай.скорость > 0.0))
	            {
	                if ((num0 == 0) != Рельс.стрелки_наоборот)
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
	            else if ((Math.Abs(num9) < 0.5) && (трамвай.скорость_abs > 0.5))
	            {
	                if ((num9 > 0.1) && (трамвай.скорость_abs < 2.8))
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
	            else if (num9 > 0.0)
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
	            else if (num9 < 0.0)
	            {
	                позиция_контроллера = -1;
	                if ((оставшеесяРасстояние / ((num8 + рекомендуемаяСкорость) / 2.0)) < (num9 / ускорение))
	                {
	                    позиция_контроллера = -2;
	                    if ((оставшеесяРасстояние / ((num8 + рекомендуемаяСкорость) / 2.0)) < (num9 / ускорение))
	                    {
	                        позиция_контроллера = -3;
	                        if ((оставшеесяРасстояние / ((num8 + рекомендуемаяСкорость) / 2.0)) < (num9 / ускорение))
	                        {
	                            позиция_контроллера = -4;
	                            if ((оставшеесяРасстояние / ((num8 + рекомендуемаяСкорость) / 2.0)) < (num9 / ускорение))
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
            private Троллейбус троллейбус;

            public РКСУ_Троллейбус(Троллейбус троллейбус)
            {
                this.троллейбус = троллейбус;
            }

            public override void CreateSoundBuffers()
            {
            	SoundBufferDescription[] desc = new SoundBufferDescription[6];
            	SoundLoader[] loader = new SoundLoader[6];
            	base.SoundBuffers = new SlimDX.DirectSound.SecondarySoundBuffer[6];
//            	base.buffers3d = new SoundBuffer3D[6];
                base.SoundBuffers_Volume = new int[6];
                loader[0] = new SoundLoader(this.троллейбус.основная_папка + "Sound 1.wav");
                loader[1] = loader[0];
                loader[2] = loader[0];
                loader[3] = new SoundLoader(this.троллейбус.основная_папка + "Sound 2.wav");
                loader[4] = loader[3];
                loader[5] = new SoundLoader(this.троллейбус.основная_папка + "Sound 3.wav");
            	for (int i = 0; i < SoundBuffers.Length; i++)
                {
            		desc[i] = new SoundBufferDescription();
            		desc[i].SizeInBytes = loader[i].OutBytes.Length;
					desc[i].Format = loader[i].Format;
                	desc[i].Flags = BufferFlags.ControlVolume | BufferFlags.ControlFrequency;// | BufferFlags.Control3D;// | BufferFlags.GlobalFocus;
                	base.SoundBuffers[i] = new SlimDX.DirectSound.SecondarySoundBuffer(MyDirectSound.device, desc[i]);//this.автобус.основная_папка + "engine.wav", desc, MyDirectSound.device);
                	if (i != 5) base.SoundBuffers[i].Frequency = 100;
                	base.SoundBuffers[i].Volume = -10000;
                	base.SoundBuffers[i].Write(loader[i].OutBytes, 0, LockFlags.None);
                	if (i != 5) base.SoundBuffers[i].Play(0, PlayFlags.Looping);
//                	buffers3d[i] = new SoundBuffer3D(SoundBuffers[i]);
//                	buffers3d[i].Position = new SlimDX.Vector3(0.0f, 0.0f, 0.0f);
            	}
            }

            public override void UpdateSound(Игрок[] игроки, bool игра_активна)
            {
                double num = (600.0 * this.троллейбус.скорость_abs) + 1000.0;
                if (this.троллейбус.скорость_abs < 1.0)
                {
                	num = Math.Max(1600.0 * this.троллейбус.скорость_abs, 1.0);
                }
                if (!this.троллейбус.обесточен)
                {
                    this.frequency[0] = (int)(num * 2.0);
                    this.frequency[1] = (int)(num * 3.0);
                    this.frequency[2] = (int)(num * 5.0);
                    this.frequency[3] = (int)(num * 7.0);
                    this.frequency[4] = (int)(num * 10.0);
                }
                else
                {
                    for (int k = 0; k < this.frequency.Length; k++)
                    {
                    	this.frequency[k] = Math.Max((int)(this.frequency[k] * 0.99), 100);
                    }
                }
                for (int i = 0; i < this.frequency.Length; i++)
                {
                    if (this.frequency[i] > Math.Max(((double)this.last_frequency[i]) / 0.99, (double)(this.last_frequency[i] + 500)))
                    {
                        this.frequency[i] = (int)Math.Max(((double)this.last_frequency[i]) / 0.99, (double)(this.last_frequency[i] + 500));
                    }
                    this.last_frequency[i] = this.frequency[i];
                }
                this.volume[0] = -10000;
                this.volume[1] = -10000;
                this.volume[2] = -10000;
                this.volume[3] = -10000;
                this.volume[4] = -10000;
                this.volume[5] = -10000;
                this.frequency[5] = 0x1388;
                if (this.троллейбус.скорость == 0.0)
                {
                    for (int m = 0; m < base.SoundBuffers_Volume.Length; m++)
                    {
                        base.SoundBuffers_Volume[m] = -10000;
                    }
                }
                else if (base.SoundBuffers_Volume[2] == -10000)
                {
                    base.SoundBuffers_Volume[2] = -2000;
                }
                if (this.троллейбус._soundУскоряется || this.троллейбус._soundЗамедляется)
                {
                	base.SoundBuffers_Volume[2] = Math.Min(base.SoundBuffers_Volume[2] + 200, -200);
                }
                else
                {
                    base.SoundBuffers_Volume[2] -= 200;
                    if (base.SoundBuffers_Volume[2] < -1800)
                    {
                        base.SoundBuffers_Volume[2] = -1800;
                    }
                }
                base.SoundBuffers_Volume[0] = 0;
                base.SoundBuffers_Volume[1] = 0;
                base.SoundBuffers_Volume[3] = 0;
                if (this.троллейбус.скорость_abs > 2.5)
                {
                    base.SoundBuffers_Volume[3] = -100 * ((int)Math.Pow(2.0, this.троллейбус.скорость_abs / 2.5));
                }
                base.SoundBuffers_Volume[4] = base.SoundBuffers_Volume[3];
                base.SoundBuffers_Volume[5] = -1800;
                if (игра_активна && (this.volume_muting < 0))
                {
                    this.volume_muting = -((int)Math.Pow(Math.Sqrt((double)-this.volume_muting) - 10.0, 2.0));
                }
                else if (!игра_активна && (this.volume_muting > -10000))
                {
                    this.volume_muting = -((int)Math.Pow(Math.Sqrt((double)-this.volume_muting) + 10.0, 2.0));
                }
                for (int j = 0; j < игроки.Length; j++)
                {
                    Double3DPoint point = new Double3DPoint(this.троллейбус.position.x, 1.0, this.троллейбус.position.y) - игроки[j].cameraPosition;
                    double num6 = point.модуль;
                    if (num6 < 120.0)
                    {
                        for (int n = 0; n < base.SoundBuffers.Length; n++)
                        {
                            int num8 = base.SoundBuffers_Volume[n];
                            if (num6 > 15.0)
                            {
                                int num9 = num8 - ((int)((num6 - 15.0) * 20.0));
                                num8 = (num9 > -10000) ? num9 : -10000;
                                if (num6 > 100.0)
                                {
                                    num9 = num8 - ((int)((num6 - 100.0) * 100.0));
                                    num8 = (num9 > -10000) ? num9 : -10000;
                                }
                            }
                            if (this.frequency[n] < 0x1388)
                            {
                                int num10 = num8 - (0x2625a0 / this.frequency[n]);
                                num8 = (num10 > -10000) ? num10 : -10000;
                            }
                            this.volume[n] = Math.Max(this.volume[n], (num8 + this.volume_muting));
                            /*if (this.volume[n] < (num8 + this.volume_muting))
                            {
                                this.volume[n] = num8 + this.volume_muting;
                            }*/
                        }
                    }
                }
                try
                {
                    for (int num11 = 0; num11 < 5; num11++)
                    {
                    	if ((this.volume[num11] > -10000) && (base.SoundBuffers[num11].Status != BufferStatus.Playing))
                        {
                            base.SoundBuffers[num11].Play(0, PlayFlags.Looping);
                        }
                        else if ((this.volume[num11] <= -10000) && (base.SoundBuffers[num11].Status == BufferStatus.Playing))
                        {
                            base.SoundBuffers[num11].Stop();
                        }
                    }
                    if ((this.троллейбус.обесточен && this.троллейбус.включен) && ((base.SoundBuffers[5].Status != BufferStatus.Playing) && (this.volume[5] > -10000)))
                    {
                        base.SoundBuffers[5].Play(0, PlayFlags.Looping);
                    }
                    else if ((!this.троллейбус.обесточен || !this.троллейбус.включен)/* && (base.SoundBuffers[5].Status == BufferStatus.Playing)*/)
                    {
                        base.SoundBuffers[5].Stop();
                        base.SoundBuffers[5].CurrentPlayPosition = 0;//.SetCurrentPosition(0);
                    }
                    if (base.SoundBuffers[0].Volume != this.volume[0])
                    {
                        base.SoundBuffers[0].Volume = this.volume[0];
                    }
                    if (base.SoundBuffers[1].Volume != this.volume[1])
                    {
                        base.SoundBuffers[1].Volume = this.volume[1];
                    }
                    if (base.SoundBuffers[2].Volume != this.volume[2])
                    {
                        base.SoundBuffers[2].Volume = this.volume[2];
                    }
                    if (base.SoundBuffers[3].Volume != this.volume[3])
                    {
                        base.SoundBuffers[3].Volume = this.volume[3];
                    }
                    if (base.SoundBuffers[4].Volume != this.volume[4])
                    {
                        base.SoundBuffers[4].Volume = this.volume[4];
                    }
                    if (base.SoundBuffers[5].Volume != this.volume[5])
                    {
                        base.SoundBuffers[5].Volume = this.volume[5];
                    }
                    if (base.SoundBuffers[0].Frequency != this.frequency[0])
                    {
                        base.SoundBuffers[0].Frequency = this.frequency[0];
                    }
                    if (base.SoundBuffers[1].Frequency != this.frequency[1])
                    {
                        base.SoundBuffers[1].Frequency = this.frequency[1];
                    }
                    if (base.SoundBuffers[2].Frequency != this.frequency[2])
                    {
                        base.SoundBuffers[2].Frequency = this.frequency[2];
                    }
                    if (base.SoundBuffers[3].Frequency != this.frequency[3])
                    {
                        base.SoundBuffers[3].Frequency = this.frequency[3];
                    }
                    if (base.SoundBuffers[4].Frequency != this.frequency[4])
                    {
                        base.SoundBuffers[4].Frequency = this.frequency[4];
                    }
                }
                catch
                {
                }
            }

            public override void автоматически_управлять(double рекомендуемаяСкорость, double оставшеесяРасстояние, int переключение)
            {
                if ((троллейбус.скорость == 0.0) && ((рекомендуемаяСкорость * позиция_реверсора) < 0.0))
                {
                    позиция_реверсора = -позиция_реверсора;
                }
                рекомендуемаяСкорость *= позиция_реверсора;
                //Console.Write(string.Format("рекомендуемаяСкорость {0} ", рекомендуемаяСкорость));
                var current_speed = троллейбус.скорость * позиция_реверсора;
                //Console.Write(string.Format("num {0} ", num));
                var dspeed = рекомендуемаяСкорость - current_speed;
                //Console.Write(string.Format("num2 {0} ", num2));
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
                        	//пневматический_тормоз = оставшеесяРасстояние / (num * 2.0);//((num2 * ((num + рекомендуемаяСкорость) / 2.0)) / оставшеесяРасстояние) / 1.5;//1.5
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
                //Console.Write(string.Format("позиция_контроллера {0}\t",позиция_контроллера));
                //Console.WriteLine();
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
