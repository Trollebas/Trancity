/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 01.10.2011
 * Время: 21:46
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;

namespace Engine
{
	public abstract class BoundingFigure
	{
		//public Double3DPoint position;
		public int LODnum = 0;
		public abstract void Update(Double3DPoint _position, DoublePoint _направление);
	}
	
	public class Sphere : BoundingFigure
	{
		public Double3DPoint position;
		private Double3DPoint fposition;
		public double radius;
		public Sphere(Double3DPoint position, double radius)
		{
			this.fposition = position;
			this.radius = radius;
			Update(Double3DPoint.Zero, DoublePoint.Zero);
		}
		
		public override void Update(Double3DPoint _position, DoublePoint _направление)
		{
			/*Double3DPoint point = new Double3DPoint(_направление);
			Double3DPoint point1 = new Double3DPoint(_направление);
			Double3DPoint point2 = Double3DPoint.Поворот(_направление, Math.PI / 2);
			point1.угол_y += Math.PI / 2;
			position = (Double3DPoint)(_position + (((point * fposition.x) + (point2 * fposition.z)) + point1 * fposition.y));*/
			position = Double3DPoint.Multiply(fposition, _position, _направление);
		}
	}
	
	/// <summary>
	/// в текущем виде это чистый AABB
	/// </summary>
	public class AABB : BoundingFigure
	{
		public Double3DPoint min;
		public Double3DPoint max;
		private Double3DPoint min2;
		private Double3DPoint max2;
		
		public AABB(Double3DPoint min, Double3DPoint max)
		{
			this.min2 = min;
			this.max2 = max;
		}
		
		// TODO:переделать в OBB
		public override void Update(Double3DPoint _position, DoublePoint _направление)
		{
			Double3DPoint point = new Double3DPoint(_направление);
			Double3DPoint point1 = new Double3DPoint(_направление);
			Double3DPoint point2 = Double3DPoint.Rotate(_направление, (Math.PI / 2.0));
			point1.AngleY += Math.PI / 2;
			
			min = (Double3DPoint)(_position + (((point * min2.x) + (point2 * min2.z)) + point1 * min2.y));

			max = (Double3DPoint)(_position + (((point * max2.x) + (point2 * max2.z)) + point1 * max2.y));
		}
	}
}