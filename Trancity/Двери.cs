using System;
using Common;
using SlimDX;
using Engine;

namespace Trancity
{
    public abstract class Двери
    {
        private Double3DPoint _pos1;
        private Double3DPoint _pos2;
        private double _высота = 2.557;
        public bool дверьВодителя;
        private double _длина = 0.425;
        public int номер;
//        private IMatrixObject _объект;
        private MeshObject _объект;

        public bool открываются;
        private bool _правые;
        private double _состояние;
        private Дверь _створка;
        private double _ширина = 0.05;
        
        public void CreateMesh()
        {
            _створка.CreateMesh();
        }

        public abstract void Render();
        public void Обновить()
        {
            _состояние += Скорость * World.прошлоВремени;
            if (_состояние < 0.0)
            {
                _состояние = 0.0;
            }
            if (_состояние > 1.0)
            {
                _состояние = 1.0;
            }
        }

        public static Двери Построить(МодельДверей модель, MeshObject объект, Double3DPoint p1, Double3DPoint p2, bool правые)
        {
            switch (модель.тип)
            {
                case МодельДверей.Тип.Двустворчатые:
                    return new Двустворчатые(объект, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, правые, модель.dir, модель.filename, модель.длина, модель.высота, модель.ширина);

                case МодельДверей.Тип.ШарнирноПоворотные:
                    return new ШарнирноПоворотные(объект, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, правые, модель.dir, модель.filename, модель.длина, модель.высота, модель.ширина);
                    
                case МодельДверей.Тип.Сдвижные:
                    return new Сдвижные(объект, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, правые, модель.dir, модель.filename, модель.длина, модель.высота, модель.ширина);
                    
                case МодельДверей.Тип.Custom:
                    return new CustomDoors(объект, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, правые, модель.dir, модель.filename, модель.длина, модель.высота, модель.ширина);
            }
            throw new ArgumentOutOfRangeException("модель." + "тип", модель.тип, "Неизвестный тип дверей!");
        }

        public string[] ExtraMeshDirs
        {
            set
            {
                _створка.extraMeshDirs = value;
            }
        }

        public bool Закрыты
        {
            get
            {
                return (_состояние <= 0.0);
            }
        }

        public bool Открыты
        {
            get
            {
                return (_состояние >= 1.0);
            }
        }

        private double Скорость
        {
            get
            {
                if (открываются)
                {
                	return 0.8;
                }
                return -0.8;
            }
        }
        
        public void CheckCondition()
        {
        	_створка.IsNear = true;
        }

// ReSharper disable RedundantExtendsListEntry
        private class Дверь : MeshObject, MeshObject.IFromFile, IMatrixObject
// ReSharper restore RedundantExtendsListEntry
        {
// ReSharper disable FieldCanBeMadeReadOnly.Local
            private string _filename;
// ReSharper restore FieldCanBeMadeReadOnly.Local
            public Matrix matrix;

            public Дверь(string filename, string dir)
            {
                meshDir = dir;
                _filename = filename;
            }

            public Matrix GetMatrix(int index)
            {
                return matrix;
            }

            public string Filename
            {
                get
                {
                    return _filename;
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

        public class Двустворчатые : Двери
        {
            public Двустворчатые(MeshObject объект, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, string dir, string filename, double длина, double высота, double ширина)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                _pos2 = new Double3DPoint(x2, y2, z2);
                _правые = правые;
                _длина = длина;
                _высота = высота;
                _ширина = ширина;
                _объект = объект;
                _створка = new Дверь(filename, dir);
            }

            public override void Render()
            {
                var matrix = _объект.last_matrix;//((IMatrixObject)_объект).GetMatrix(0);
                float num = _правые ? -1f : 1f;
                var d = _состояние * (Math.PI / 2.0);//(_состояние * Math.PI) / 2.0;
                var matrix1 = Matrix.Translation(((float) _длина) / 2f, 0f, (num * -((float) _ширина)) / 2f);
                var matrix2 = (matrix1 * Matrix.RotationY(num * -((float) d))) * Matrix.Translation(0f, 0f, num * ((float) (Math.Cos(d) * _ширина)));
                var matrix3 = ((matrix1 * Matrix.RotationY(num * -((float) (Math.PI - d)))) * Matrix.Translation(-((float) (Math.Sin(d) * _ширина)), 0f, 0f)) * Matrix.Translation(((float) ((Math.Sin(d) * _ширина) + (Math.Cos(d) * _длина))) * 2f, 0f, 0f);
                var point = new DoublePoint(_pos2.x - _pos1.x, _pos2.z - _pos1.z);
                float val = (float) ((point.Modulus / 2.0) / _длина);
                var matrix4 = (Matrix.Scaling(val, (float) ((_pos2.y - _pos1.y) / _высота), val) * Matrix.RotationY(-((float) point.Angle))) * Matrix.Translation((float) _pos1.x, (float) _pos1.y, (float) _pos1.z);
                if (_правые)
                {
                    matrix2 = Matrix.RotationY((float) Math.PI) * matrix2;
                }
                else
                {
                    matrix3 = Matrix.RotationY((float) Math.PI) * matrix3;
                }
                _створка.matrix = (matrix2 * matrix4) * matrix;
                _створка.Render();
                _створка.matrix = (matrix3 * matrix4) * matrix;
                _створка.Render();
            }
        }

        public class ШарнирноПоворотные : Двери
        {
            public ШарнирноПоворотные(MeshObject объект, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, string dir, string filename, double длина, double высота, double ширина)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                _pos2 = new Double3DPoint(x2, y2, z2);
                _правые = правые;
                _длина = длина;
                _высота = высота;
                _ширина = ширина;
                _объект = объект;
                _створка = new Дверь(filename, dir);
            }

            public override void Render()
            {
                var matrix = _объект.last_matrix;//((IMatrixObject)_объект).GetMatrix(0);
                float num = _правые ? 1f : -1f;
                var d = (_состояние * Math.PI) / 2.0;
                var matrix2 = (Matrix.Translation(((float) _длина) / 2f, 0f, (num * ((float) _ширина)) / 2f) * Matrix.RotationY(num * ((float) (Math.PI - d)))) * Matrix.Translation((float) (Math.Cos(d) * _длина), 0f, 0f);
                var point = new DoublePoint(_pos2.x - _pos1.x, _pos2.z - _pos1.z);
                float val = (float)(point.Modulus / _длина);
                var matrix3 = (Matrix.Scaling(val, (float) ((_pos2.y - _pos1.y) / _высота), val) * Matrix.RotationY(-((float) point.Angle))) * Matrix.Translation((float) _pos1.x, (float) _pos1.y, (float) _pos1.z);
                if (!_правые)
                {
                    matrix2 = Matrix.RotationY((float) Math.PI) * matrix2;
                }
                _створка.matrix = (matrix2 * matrix3) * matrix;
                _створка.Render();
            }
        }
        
        public class Сдвижные : Двери
        {
            public Сдвижные(MeshObject объект, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, string dir, string filename, double длина, double высота, double ширина)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                _pos2 = new Double3DPoint(x2, y2, z2);
                _правые = правые;
                _длина = длина;
                _высота = высота;
                _ширина = ширина;
                _объект = объект;
                _створка = new Дверь(filename, dir);
            }

            public override void Render()
            {
            	var matrix = _объект.last_matrix;//((IMatrixObject)_объект).GetMatrix(0);
                float num = _правые ? -1f : 1f;
                var point = new DoublePoint(_pos2.x - _pos1.x, _pos2.z - _pos1.z);
                var matrix10 = Matrix.RotationY(-((float)point.Angle));
                var matrix12 = Matrix.Translation(((float) _длина) / 2f, 0f, 0f);
                var matrix11 = (Matrix.Translation((float) (_состояние * _длина) * num, 0f, 0f) * matrix10);
                var matrix2 = matrix11 * matrix12;
				float val = (float) (point.Modulus / _длина);
                var matrix3 = (Matrix.Scaling(val, (float) ((_pos2.y - _pos1.y) / _высота), val) * Matrix.Translation((float) _pos1.x, (float) _pos1.y, (float) _pos1.z));
                _створка.matrix = (matrix2 * matrix3) * matrix;
                _створка.Render();
            }
        }
        
        public class CustomDoors : Двери
        {
        	private Vector3 rotv1;
        	private Vector3 rotv2;
            public CustomDoors(MeshObject объект, double x1, double z1, double x2, double z2, double y1, double y2, bool правые, string dir, string filename, double длина, double высота, double ширина)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                rotv2 = new Vector3((float)x2, (float)y2, (float)z2);
                rotv1 = new Vector3((float)(x1 + длина), (float)(y1 + высота), (float)(z1 + ширина));
                _правые = правые;
                _объект = объект;
                _створка = new Дверь(filename, dir);
            }

            public override void Render()
            {
                var matrix = _объект.last_matrix;//((IMatrixObject)_объект).GetMatrix(0);
                var d = (_состояние * Math.PI) / 2.0;
                var matrix2 = Matrix.Translation((float)_pos1.x, (float)_pos1.y, (float)_pos1.z);
                var matrix3 = Matrix.RotationAxis(rotv2, _правые ? (float)(d) : -(float)(d));
                var matrix4 = Matrix.RotationAxis(rotv1, (float)(-(Math.PI - d)));
                var matrix10 = Matrix.RotationY((float)d);
                var matrix11 = Matrix.RotationY((float)-d);
                _створка.matrix = matrix2 * matrix10 * matrix;
                _створка.Render();
            }
        }
    }
}