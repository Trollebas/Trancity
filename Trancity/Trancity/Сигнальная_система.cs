namespace Trancity
{
    using Common;
    using SlimDX;
    using SlimDX.Direct3D9;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class Сигнальная_система
    {
        private int fсостояние;
        public int граница_переключения;
        public List<Контакт> элементы = new List<Контакт>();
        public List<Visual_Signal> vsignals = new List<Visual_Signal>();

        public Сигнальная_система(int граница_переключения, int состояние)
        {
            this.граница_переключения = граница_переключения;
            this.состояние = состояние;
        }

        public void CreateMesh()
        {
            foreach (Visual_Signal vs in vsignals)
        	{
        		vs.CreateMesh();
        	}
//            this.Обновить_материалы();
        }
        
        public void Render()
        {
        	foreach (Visual_Signal vs in vsignals)
        	{
        		vs.Render();
        	}
        }

        public void Добавить_элемент(Контакт элемент)
        {
            this.элементы.Add(элемент);
        }
        
        public void Добавить_сигнал(Visual_Signal элемент)
        {
            this.vsignals.Add(элемент);
        }
        
        public void Убрать_сигнал(Visual_Signal элемент)
        {
        	элемент.road = null;
            this.vsignals.Remove(элемент);
        }
        
        public void Убрать_элемент(Контакт элемент)
        {
        	элемент.дорога = null;
            this.элементы.Remove(элемент);
        }

        /*private void Обновить_материалы()
        {
        	foreach (Visual_Signal vs in vsignals)
        	{
        		vs.Обновить_материалы();
        	}
        }*/

        public Сигналы сигнал
        {
            get
            {
                this.состояние = Math.Max(0, this.состояние);
                if (this.состояние < this.граница_переключения)
                {
                    return Сигналы.Зелёный;
                }
                return Сигналы.Красный;
            }
        }

        public int состояние
        {
            get
            {
                return this.fсостояние;
            }
            set
            {
                value = Math.Max(0, value);
//                bool flag = this.fсостояние < this.граница_переключения;
//                bool flag2 = value < this.граница_переключения;
                this.fсостояние = value;
                /*if (flag != flag2)
                {
                    this.Обновить_материалы();
                }*/
            }
        }

        public class Контакт
        {
        	private Road _дорога;
            public double расстояние;
            public Сигнальная_система система;
            public bool минус;

            public Контакт(Сигнальная_система система, Road дорога, double расстояние, bool минус)
            {
                this.система = система;
                this.система.Добавить_элемент(this);
                this.дорога = дорога;
                this.расстояние = расстояние;
                this.минус = минус;
            }
            
            public Road дорога
	        {
	        	get
	        	{
	        		return this._дорога;
	        	}
	        	set
	        	{
	        		if (this._дорога != null)
	        		{
	        			this._дорога.objects.Remove(this);
	        		}
	        		this._дорога = value;
	        		if (value != null)
	        		{
	        			this._дорога.objects.Add(this);
	        		}
	        	}
	        }
        }
    }
}

