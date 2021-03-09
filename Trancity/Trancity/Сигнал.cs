/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 29.12.2013
 * Time: 17:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using SlimDX;

namespace Trancity
{
    public class Visual_Signal : BaseSignal
    {
        public Сигнальная_система система = null;
        private int green_mtrl;
        private int red_mtrl;

        public Visual_Signal(Сигнальная_система _signal_system, string _name) : base(_name, 3)
        {
            this.система = _signal_system;
            this.система.Добавить_сигнал(this);
            green_mtrl = model.FindNumericArg("green_mtrl", -1);
            red_mtrl = model.FindNumericArg("red_mtrl", -1);
        }

        public override void CreateMesh()
        {
            if (model == null) return;
            base.meshDir = model.dir;
            base.CreateMesh();
            if (green_mtrl >= base._meshMaterials.Length) green_mtrl = -1;
            if (red_mtrl >= base._meshMaterials.Length) red_mtrl = -1;
            this.Обновить_материалы();
        }

        public Road road
        {
            get
            {
                return this.положение.Дорога;
            }
            set
            {
                if (this.положение.Дорога != null)
                {
                    this.положение.Дорога.objects.Remove(this);
                }
                this.положение.Дорога = value;
                if (value != null)
                {
                    this.положение.Дорога.objects.Add(this);
                }
            }
        }

        public override int MatricesCount
        {
            get
            {
                int matricies = base.MatricesCount;
                if (matricies > 0) Обновить_материалы();
                return matricies;
            }
        }

        public void Обновить_материалы()
        {
            if (base._meshMaterials == null) return;
            bool flag = система.сигнал == Сигналы.Зелёный;
            if (green_mtrl >= 0)
            {
                Color4 emissiveColor = base._meshMaterials[green_mtrl].Emissive;
                emissiveColor.Green = flag ? ((float)1) : ((float)0);
                base._meshMaterials[green_mtrl].Emissive = emissiveColor;
            }
            if (red_mtrl >= 0)
            {
                Color4 value3 = base._meshMaterials[red_mtrl].Emissive;
                value3.Red = flag ? ((float)0) : ((float)1);
                base._meshMaterials[red_mtrl].Emissive = value3;
            }
        }
    }
}
