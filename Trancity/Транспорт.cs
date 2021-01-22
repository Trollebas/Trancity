using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;
using Engine;

namespace Trancity
{
    public abstract class Transport : IControlledObject, IVector, I��������������3D
    {
        private bool faultAlarm;
        private Route route;
        private int f���������_��������;
        private ���������� f����������;
        private bool nosound = false;
        public bool ������� = true;
        public bool ��������_����;
        public List<���������> ���������_��������� = new List<���������>();
        public Order �����;
        public string ��������_����� = "";
        public double ��������_������;
        public ���� ����;
        public Trip ����;
        public int ����_index;
        public �������_���������� �������_����������;
        public double ��������;
        protected bool �����_�_���������_�������;
        public bool _sound�����������;
        public bool _sound����������;
        public bool _sound����������������;
        public bool _sound����������������;
        public ��������������� ���������_������;
        public ������������� ��������_�_���� = null;
        public ���������������� ������;
        protected BaseStop ���������������� = null;
        public Stop nextStop = null;
        public Stop currentStop = null;
        public int stopIndex = 0;
        protected �����[] _�����;
        protected int _���������������� = 1;
        protected double �����������������;
        protected double �����������������Max = 1.0;
        protected double ��������������������� = 0.5;
        protected int _�����������������������;
        protected bool _�������������������������;
        //
        public bool stand_brake = false;
        public bool �� = false;
        //
        public double length0 = 0.0;
        public double length1 = 0.0;
        public double width = 0.0;
        public MyCamera[] cameras = new MyCamera[0];
        
        public abstract void CreateMesh(World ���);
        public abstract void Render();
        public abstract void UpdateBoundigBoxes(World world);
        public abstract void ����������������������(World ���);
        public abstract ���������[] �����������������(World ���);
        public abstract void ��������(World ���, �����[] ������);
        protected abstract void ���������������������������();
        public abstract void SetPosition(Road road, double distance, double shift, Double3DPoint pos, DoublePoint rot, World world);

        protected void ������������()
        {
            if (���� == null)
                return;
            if ((����_index != (����.pathes.Length - 1)) || (����� == null))
            {
                while ((����_index < ����.pathes.Length) && (�������_���������.������ != ����.pathes[����_index]))
                {
                    if ((����_index > 0) && (�������_���������.������ == ����.pathes[����_index - 1]))
                    {
                        ����_index--;
                    }
                    else
                    {
                        ����_index++;
                    }
                }
                if (����_index == ����.pathes.Length)
                {
                    ����_index = 0;
                    for (var i = 0; i < ����.pathes.Length; i++)
                    {
                        if (�������_���������.������ != ����.pathes[i])
                            continue;
                        ����_index = i;
                        break;
                    }
                }
            }
            if ((����� == null) || (����_index != (����.pathes.Length - 1)))
                return;
            // FIXME: ��� � ��������� � ���������� ����� ������
            for (var j = 0; j < (�����.�����.Length - 1); j++)
            {
                if (�����.�����[j] != ����)
                    continue;
                ���� = �����.�����[j + 1];
                nextStop = null;
                currentStop = null;
                stopIndex = 0;
                ����_index = 0;
                return;
            }
        }

        public abstract void �����������(double ����������, World ���);

        public bool ���������_������������
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

        public bool �_����
        {
            get
            {
                return (((this.���� != null) && this.����.inPark) && (this.����_index >= this.����.inParkIndex));
            }
        }
        
        public enum ���_����������
        {
            ����,
            �����,
            ������,
            ������,
            �����
        }

        public abstract DoublePoint position { get; }
        
        public abstract Double3DPoint ����������3D { get; }

        public Route �������
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
                ���������������������������();
            }
        }

        public abstract double direction { get; }
        
        public abstract double �����������Y { get; }

        public double ��������_abs
        {
            get
            {
                return Math.Abs(this.��������);
            }
            set
            {
                if (value <= 0.0)
                {
                    this.�������� = 0.0;
                }
                else if (this.�������� == 0.0)
                {
                    this.�������� = value;
                }
                else
                {
                    // HACK: ����� ����� try-catch?
                    try 
                    {
                        this.�������� = Math.Sign(this.��������) * value;
                    }
                    catch {};
                }
            }
        }

        public abstract ��������� �������_��������� { get; }

        public int ���������_��������
        {
            get
            {
                return this.f���������_��������;
            }
            set
            {
                this.f���������_�������� = value;
            }
        }

        public ���������� ����������
        {
            get
            {
                return this.f����������;
            }
            set
            {
                this.f���������� = value;
            }
        }

        public abstract double ��������� { get; }
        
        public bool ������������(int �����)
        {
            foreach (var ����� in _�����)
            {
                if ((�����.����� == �����) && !�����.�������)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ������������(int �����)
        {
            foreach (����� ����� in this._�����)
            {
                if ((�����.����� == �����) && !�����.�������)
                {
                    return false;
                }
            }
            return true;
        }

        public void ������������(bool �������)
        {
            for (int i = 0; i < this._����������������; i++)
            {
                this.������������(i, �������);
            }
        }

        public void ������������(int �����, bool �������)
        {
            foreach (����� ����� in this._�����)
            {
                if (�����.����� == �����)
                {
                    �����.����������� = �������;
                }
            }
        }

        public void ��������������������(bool �������)
        {
            foreach (����� ����� in this._�����)
            {
                if (�����.�������������)
                {
                    �����.����������� = �������;
                }
            }
        }

        public bool �����_��������_�������
        {
            get
            {
                foreach (����� ����� in this._�����)
                {
                    if (�����.������������� && !�����.�������)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool �����_��������_�������
        {
            get
            {
                foreach (����� ����� in this._�����)
                {
                    if (�����.������������� && !�����.�������)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool �����_�������
        {
            get
            {
                foreach (����� ����� in this._�����)
                {
                    if ((�����.����� >= 0) && !�����.�������)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool �����_�������
        {
            get
            {
                foreach (����� ����� in this._�����)
                {
                    if ((�����.����� >= 0) && !�����.�������)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        
        protected void UpdateTripStops()
        {
            if ((���� == null) || (����.tripStopList == null) || (����.tripStopList.Count == 0))
                return;
            if (stopIndex < ����.tripStopList.Count)
            {
                while ((nextStop == null) && (stopIndex < ����.tripStopList.Count))
                {
                    if (!����.tripStopList[stopIndex].flag)
                    {
                        stopIndex = Math.Min(stopIndex + 1, ����.tripStopList.Count);
                    }
                    else
                    {
                        nextStop = ����.tripStopList[stopIndex].stop;
                    }
                }
            }
            else
            {
                stopIndex = ����.tripStopList.Count;
                nextStop = null;
            }
        }
        
        protected void SearchForCurrentStop(Stop stop)
        {
            if ((���� == null) || (����.tripStopList == null)
               || (nextStop == null) || (stop == nextStop)
               || (stopIndex != 0))
                return;
            for (int i = 0; i < ����.tripStopList.Count; i++)
            {
                if ((����.tripStopList[i].stop != stop) || (!����.tripStopList[i].flag))
                    continue;
                stopIndex = i;
                nextStop = ����.tripStopList[i].stop;
                return;
            }
        }
        
        public void CreateSoundBuffers()
        {
            �������_����������.CreateSoundBuffers();
        }
        
        public void UpdateSound(�����[] ������, bool ����_�������)
        {
            if (!nosound)
                �������_����������.UpdateSound(������, ����_�������);
        }
        
        public void LoadCameras()
        {
            if (������.cameras == null)
                return;
            cameras = new MyCamera[������.cameras.Length];
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i] = new MyCamera();
                cameras[i].position = ������.cameras[i].pos;
                cameras[i].rotation = ������.cameras[i].rot;
            }
        }
        
        public bool SetCamera(int index, ����� player)
        {
            if (index >= cameras.Length)
                return false;
            player.cameraPositionChange = Double3DPoint.Zero;
            player.cameraRotationChange = DoublePoint.Zero;
            var dir = new DoublePoint(direction, �����������Y);
            player.cameraPosition = Double3DPoint.Multiply(cameras[index].position, ����������3D, dir);
            player.cameraRotation.x = direction + cameras[index].rotation.x;
            player.cameraRotation.y = �����������Y + cameras[index].rotation.y;
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