using System;
using Common;
using SlimDX;
using Engine;

namespace Trancity
{
    public abstract class �����
    {
        private Double3DPoint _pos1;
        private Double3DPoint _pos2;
        private double _������ = 2.557;
        public bool �������������;
        private double _����� = 0.425;
        public int �����;
//        private IMatrixObject _������;
        private MeshObject _������;

        public bool �����������;
        private bool _������;
        private double _���������;
        private ����� _�������;
        private double _������ = 0.05;
        
        public void CreateMesh()
        {
            _�������.CreateMesh();
        }

        public abstract void Render();
        public void ��������()
        {
            _��������� += �������� * World.�������������;
            if (_��������� < 0.0)
            {
                _��������� = 0.0;
            }
            if (_��������� > 1.0)
            {
                _��������� = 1.0;
            }
        }

        public static ����� ���������(������������ ������, MeshObject ������, Double3DPoint p1, Double3DPoint p2, bool ������)
        {
            switch (������.���)
            {
                case ������������.���.�������������:
                    return new �������������(������, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, ������, ������.dir, ������.filename, ������.�����, ������.������, ������.������);

                case ������������.���.������������������:
                    return new ������������������(������, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, ������, ������.dir, ������.filename, ������.�����, ������.������, ������.������);
                    
                case ������������.���.��������:
                    return new ��������(������, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, ������, ������.dir, ������.filename, ������.�����, ������.������, ������.������);
                    
                case ������������.���.Custom:
                    return new CustomDoors(������, p1.x, p1.z, p2.x, p2.z, p1.y, p2.y, ������, ������.dir, ������.filename, ������.�����, ������.������, ������.������);
            }
            throw new ArgumentOutOfRangeException("������." + "���", ������.���, "����������� ��� ������!");
        }

        public string[] ExtraMeshDirs
        {
            set
            {
                _�������.extraMeshDirs = value;
            }
        }

        public bool �������
        {
            get
            {
                return (_��������� <= 0.0);
            }
        }

        public bool �������
        {
            get
            {
                return (_��������� >= 1.0);
            }
        }

        private double ��������
        {
            get
            {
                if (�����������)
                {
                	return 0.8;
                }
                return -0.8;
            }
        }
        
        public void CheckCondition()
        {
        	_�������.IsNear = true;
        }

// ReSharper disable RedundantExtendsListEntry
        private class ����� : MeshObject, MeshObject.IFromFile, IMatrixObject
// ReSharper restore RedundantExtendsListEntry
        {
// ReSharper disable FieldCanBeMadeReadOnly.Local
            private string _filename;
// ReSharper restore FieldCanBeMadeReadOnly.Local
            public Matrix matrix;

            public �����(string filename, string dir)
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

        public class ������������� : �����
        {
            public �������������(MeshObject ������, double x1, double z1, double x2, double z2, double y1, double y2, bool ������, string dir, string filename, double �����, double ������, double ������)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                _pos2 = new Double3DPoint(x2, y2, z2);
                _������ = ������;
                _����� = �����;
                _������ = ������;
                _������ = ������;
                _������ = ������;
                _������� = new �����(filename, dir);
            }

            public override void Render()
            {
                var matrix = _������.last_matrix;//((IMatrixObject)_������).GetMatrix(0);
                float num = _������ ? -1f : 1f;
                var d = _��������� * (Math.PI / 2.0);//(_��������� * Math.PI) / 2.0;
                var matrix1 = Matrix.Translation(((float) _�����) / 2f, 0f, (num * -((float) _������)) / 2f);
                var matrix2 = (matrix1 * Matrix.RotationY(num * -((float) d))) * Matrix.Translation(0f, 0f, num * ((float) (Math.Cos(d) * _������)));
                var matrix3 = ((matrix1 * Matrix.RotationY(num * -((float) (Math.PI - d)))) * Matrix.Translation(-((float) (Math.Sin(d) * _������)), 0f, 0f)) * Matrix.Translation(((float) ((Math.Sin(d) * _������) + (Math.Cos(d) * _�����))) * 2f, 0f, 0f);
                var point = new DoublePoint(_pos2.x - _pos1.x, _pos2.z - _pos1.z);
                float val = (float) ((point.Modulus / 2.0) / _�����);
                var matrix4 = (Matrix.Scaling(val, (float) ((_pos2.y - _pos1.y) / _������), val) * Matrix.RotationY(-((float) point.Angle))) * Matrix.Translation((float) _pos1.x, (float) _pos1.y, (float) _pos1.z);
                if (_������)
                {
                    matrix2 = Matrix.RotationY((float) Math.PI) * matrix2;
                }
                else
                {
                    matrix3 = Matrix.RotationY((float) Math.PI) * matrix3;
                }
                _�������.matrix = (matrix2 * matrix4) * matrix;
                _�������.Render();
                _�������.matrix = (matrix3 * matrix4) * matrix;
                _�������.Render();
            }
        }

        public class ������������������ : �����
        {
            public ������������������(MeshObject ������, double x1, double z1, double x2, double z2, double y1, double y2, bool ������, string dir, string filename, double �����, double ������, double ������)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                _pos2 = new Double3DPoint(x2, y2, z2);
                _������ = ������;
                _����� = �����;
                _������ = ������;
                _������ = ������;
                _������ = ������;
                _������� = new �����(filename, dir);
            }

            public override void Render()
            {
                var matrix = _������.last_matrix;//((IMatrixObject)_������).GetMatrix(0);
                float num = _������ ? 1f : -1f;
                var d = (_��������� * Math.PI) / 2.0;
                var matrix2 = (Matrix.Translation(((float) _�����) / 2f, 0f, (num * ((float) _������)) / 2f) * Matrix.RotationY(num * ((float) (Math.PI - d)))) * Matrix.Translation((float) (Math.Cos(d) * _�����), 0f, 0f);
                var point = new DoublePoint(_pos2.x - _pos1.x, _pos2.z - _pos1.z);
                float val = (float)(point.Modulus / _�����);
                var matrix3 = (Matrix.Scaling(val, (float) ((_pos2.y - _pos1.y) / _������), val) * Matrix.RotationY(-((float) point.Angle))) * Matrix.Translation((float) _pos1.x, (float) _pos1.y, (float) _pos1.z);
                if (!_������)
                {
                    matrix2 = Matrix.RotationY((float) Math.PI) * matrix2;
                }
                _�������.matrix = (matrix2 * matrix3) * matrix;
                _�������.Render();
            }
        }
        
        public class �������� : �����
        {
            public ��������(MeshObject ������, double x1, double z1, double x2, double z2, double y1, double y2, bool ������, string dir, string filename, double �����, double ������, double ������)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                _pos2 = new Double3DPoint(x2, y2, z2);
                _������ = ������;
                _����� = �����;
                _������ = ������;
                _������ = ������;
                _������ = ������;
                _������� = new �����(filename, dir);
            }

            public override void Render()
            {
            	var matrix = _������.last_matrix;//((IMatrixObject)_������).GetMatrix(0);
                float num = _������ ? -1f : 1f;
                var point = new DoublePoint(_pos2.x - _pos1.x, _pos2.z - _pos1.z);
                var matrix10 = Matrix.RotationY(-((float)point.Angle));
                var matrix12 = Matrix.Translation(((float) _�����) / 2f, 0f, 0f);
                var matrix11 = (Matrix.Translation((float) (_��������� * _�����) * num, 0f, 0f) * matrix10);
                var matrix2 = matrix11 * matrix12;
				float val = (float) (point.Modulus / _�����);
                var matrix3 = (Matrix.Scaling(val, (float) ((_pos2.y - _pos1.y) / _������), val) * Matrix.Translation((float) _pos1.x, (float) _pos1.y, (float) _pos1.z));
                _�������.matrix = (matrix2 * matrix3) * matrix;
                _�������.Render();
            }
        }
        
        public class CustomDoors : �����
        {
        	private Vector3 rotv1;
        	private Vector3 rotv2;
            public CustomDoors(MeshObject ������, double x1, double z1, double x2, double z2, double y1, double y2, bool ������, string dir, string filename, double �����, double ������, double ������)
            {
                _pos1 = new Double3DPoint(x1, y1, z1);
                rotv2 = new Vector3((float)x2, (float)y2, (float)z2);
                rotv1 = new Vector3((float)(x1 + �����), (float)(y1 + ������), (float)(z1 + ������));
                _������ = ������;
                _������ = ������;
                _������� = new �����(filename, dir);
            }

            public override void Render()
            {
                var matrix = _������.last_matrix;//((IMatrixObject)_������).GetMatrix(0);
                var d = (_��������� * Math.PI) / 2.0;
                var matrix2 = Matrix.Translation((float)_pos1.x, (float)_pos1.y, (float)_pos1.z);
                var matrix3 = Matrix.RotationAxis(rotv2, _������ ? (float)(d) : -(float)(d));
                var matrix4 = Matrix.RotationAxis(rotv1, (float)(-(Math.PI - d)));
                var matrix10 = Matrix.RotationY((float)d);
                var matrix11 = Matrix.RotationY((float)-d);
                _�������.matrix = matrix2 * matrix10 * matrix;
                _�������.Render();
            }
        }
    }
}