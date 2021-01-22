using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;
using Engine;

namespace Trancity
{
    public abstract class Transport : IControlledObject, IVector, IОбъектПривязки3D
    {
        private bool faultAlarm;
        private Route route;
        private int fуказатель_поворота;
        private Управление fуправление;
        private bool nosound = false;
        public bool включен = true;
        public bool включены_фары;
        public List<Положение> найденные_положения = new List<Положение>();
        public Order наряд;
        public string основная_папка = "";
        public double осталось_стоять;
        public Парк парк;
        public Trip рейс;
        public int рейс_index;
        public Система_управления система_управления;
        public double скорость;
        protected bool стоим_с_закрытыми_дверями;
        public bool _soundЗамедляется;
        public bool _soundУскоряется;
        public bool _soundДвериОткрываются;
        public bool _soundДвериЗакрываются;
        public УказательНаряда указатель_наряда;
        public ТабличкаВПарк табличка_в_парк = null;
        public МодельТранспорта модель;
        protected BaseStop базоваяОстановка = null;
        public Stop nextStop = null;
        public Stop currentStop = null;
        public int stopIndex = 0;
        protected Двери[] _двери;
        protected int _количествоДверей = 1;
        protected double времяПоворотников;
        protected double времяПоворотниковMax = 1.0;
        protected double времяПоворотниковВыкл = 0.5;
        protected int _бывшийУказательПоворота;
        protected bool _былаАварийнаяСигнализация;
        //
        public bool stand_brake = false;
        public bool ах = false;
        //
        public double length0 = 0.0;
        public double length1 = 0.0;
        public double width = 0.0;
        public MyCamera[] cameras = new MyCamera[0];
        
        public abstract void CreateMesh(World мир);
        public abstract void Render();
        public abstract void UpdateBoundigBoxes(World world);
        public abstract void АвтоматическиУправлять(World мир);
        public abstract Положение[] НайтиВсеПоложения(World мир);
        public abstract void Обновить(World мир, Игрок[] игроки);
        protected abstract void ОбновитьМаршрутныеУказатели();
        public abstract void SetPosition(Road road, double distance, double shift, Double3DPoint pos, DoublePoint rot, World world);

        protected void ОбновитьРейс()
        {
            if (рейс == null)
                return;
            if ((рейс_index != (рейс.pathes.Length - 1)) || (наряд == null))
            {
                while ((рейс_index < рейс.pathes.Length) && (текущее_положение.Дорога != рейс.pathes[рейс_index]))
                {
                    if ((рейс_index > 0) && (текущее_положение.Дорога == рейс.pathes[рейс_index - 1]))
                    {
                        рейс_index--;
                    }
                    else
                    {
                        рейс_index++;
                    }
                }
                if (рейс_index == рейс.pathes.Length)
                {
                    рейс_index = 0;
                    for (var i = 0; i < рейс.pathes.Length; i++)
                    {
                        if (текущее_положение.Дорога != рейс.pathes[i])
                            continue;
                        рейс_index = i;
                        break;
                    }
                }
            }
            if ((наряд == null) || (рейс_index != (рейс.pathes.Length - 1)))
                return;
            // FIXME: баг с переходом к последнему рейсу наряда
            for (var j = 0; j < (наряд.рейсы.Length - 1); j++)
            {
                if (наряд.рейсы[j] != рейс)
                    continue;
                рейс = наряд.рейсы[j + 1];
                nextStop = null;
                currentStop = null;
                stopIndex = 0;
                рейс_index = 0;
                return;
            }
        }

        public abstract void Передвинуть(double расстояние, World мир);

        public bool аварийная_сигнализация
        {
            get
            {
                return this.faultAlarm;
            }
            set
            {
                this.faultAlarm = value;
            }
        }

        public bool в_парк
        {
            get
            {
                return (((this.рейс != null) && this.рейс.inPark) && (this.рейс_index >= this.рейс.inParkIndex));
            }
        }
        
        public enum Тип_дополнения
        {
            фары,
            влево,
            вправо,
            тормоз,
            назад
        }

        public abstract DoublePoint position { get; }
        
        public abstract Double3DPoint Координаты3D { get; }

        public Route маршрут
        {
            get
            {
                return route;
            }
            protected set
            {
                if (route == value) 
                   return;
                route = value;
                ОбновитьМаршрутныеУказатели();
            }
        }

        public abstract double direction { get; }
        
        public abstract double НаправлениеY { get; }

        public double скорость_abs
        {
            get
            {
                return Math.Abs(this.скорость);
            }
            set
            {
                if (value <= 0.0)
                {
                    this.скорость = 0.0;
                }
                else if (this.скорость == 0.0)
                {
                    this.скорость = value;
                }
                else
                {
                    // HACK: зачем здесь try-catch?
                    try 
                    {
                        this.скорость = Math.Sign(this.скорость) * value;
                    }
                    catch {};
                }
            }
        }

        public abstract Положение текущее_положение { get; }

        public int указатель_поворота
        {
            get
            {
                return this.fуказатель_поворота;
            }
            set
            {
                this.fуказатель_поворота = value;
            }
        }

        public Управление управление
        {
            get
            {
                return this.fуправление;
            }
            set
            {
                this.fуправление = value;
            }
        }

        public abstract double ускорение { get; }
        
        public bool ДверьЗакрыта(int номер)
        {
            foreach (var двери in _двери)
            {
                if ((двери.номер == номер) && !двери.Закрыты)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ДверьОткрыта(int номер)
        {
            foreach (Двери двери in this._двери)
            {
                if ((двери.номер == номер) && !двери.Открыты)
                {
                    return false;
                }
            }
            return true;
        }

        public void ОткрытьДвери(bool открыть)
        {
            for (int i = 0; i < this._количествоДверей; i++)
            {
                this.ОткрытьДвери(i, открыть);
            }
        }

        public void ОткрытьДвери(int номер, bool открыть)
        {
            foreach (Двери двери in this._двери)
            {
                if (двери.номер == номер)
                {
                    двери.открываются = открыть;
                }
            }
        }

        public void ОткрытьДвериВодителя(bool открыть)
        {
            foreach (Двери двери in this._двери)
            {
                if (двери.дверьВодителя)
                {
                    двери.открываются = открыть;
                }
            }
        }

        public bool двери_водителя_закрыты
        {
            get
            {
                foreach (Двери двери in this._двери)
                {
                    if (двери.дверьВодителя && !двери.Закрыты)
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
                foreach (Двери двери in this._двери)
                {
                    if (двери.дверьВодителя && !двери.Открыты)
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
                foreach (Двери двери in this._двери)
                {
                    if ((двери.номер >= 0) && !двери.Закрыты)
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
                foreach (Двери двери in this._двери)
                {
                    if ((двери.номер >= 0) && !двери.Открыты)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        
        protected void UpdateTripStops()
        {
            if ((рейс == null) || (рейс.tripStopList == null) || (рейс.tripStopList.Count == 0))
                return;
            if (stopIndex < рейс.tripStopList.Count)
            {
                while ((nextStop == null) && (stopIndex < рейс.tripStopList.Count))
                {
                    if (!рейс.tripStopList[stopIndex].flag)
                    {
                        stopIndex = Math.Min(stopIndex + 1, рейс.tripStopList.Count);
                    }
                    else
                    {
                        nextStop = рейс.tripStopList[stopIndex].stop;
                    }
                }
            }
            else
            {
                stopIndex = рейс.tripStopList.Count;
                nextStop = null;
            }
        }
        
        protected void SearchForCurrentStop(Stop stop)
        {
            if ((рейс == null) || (рейс.tripStopList == null)
               || (nextStop == null) || (stop == nextStop)
               || (stopIndex != 0))
                return;
            for (int i = 0; i < рейс.tripStopList.Count; i++)
            {
                if ((рейс.tripStopList[i].stop != stop) || (!рейс.tripStopList[i].flag))
                    continue;
                stopIndex = i;
                nextStop = рейс.tripStopList[i].stop;
                return;
            }
        }
        
        public void CreateSoundBuffers()
        {
            система_управления.CreateSoundBuffers();
        }
        
        public void UpdateSound(Игрок[] игроки, bool игра_активна)
        {
            if (!nosound)
                система_управления.UpdateSound(игроки, игра_активна);
        }
        
        public void LoadCameras()
        {
            if (модель.cameras == null)
                return;
            cameras = new MyCamera[модель.cameras.Length];
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i] = new MyCamera();
                cameras[i].position = модель.cameras[i].pos;
                cameras[i].rotation = модель.cameras[i].rot;
            }
        }
        
        public bool SetCamera(int index, Игрок player)
        {
            if (index >= cameras.Length)
                return false;
            player.cameraPositionChange = Double3DPoint.Zero;
            player.cameraRotationChange = DoublePoint.Zero;
            var dir = new DoublePoint(direction, НаправлениеY);
            player.cameraPosition = Double3DPoint.Multiply(cameras[index].position, Координаты3D, dir);
            player.cameraRotation.x = direction + cameras[index].rotation.x;
            player.cameraRotation.y = НаправлениеY + cameras[index].rotation.y;
            return true;
        }
        
        public bool condition
        {
            get
            {
                return ((Math.Abs(Math.Floor(this.position.x / (double)Ground.grid_size) - Game.col) > 1.0) || (Math.Abs(Math.Floor(this.position.y / (double)Ground.grid_size) - Game.row) > 1.0));
            }
        }
        
        protected abstract void CheckCondition();
    }
}