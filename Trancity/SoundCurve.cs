
using System;

namespace Trancity
{
    /// <summary>
    /// Define the piece-wise linear function to control audio sources
    /// </summary>
    public class SoundCurve
    {
        public struct Point
        {
            public float x;
            public float y;

            public Point(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public enum SoundParameter
        {
            Volume = 0,
            Pitch = 1,
        }

        private Point[] _Points;
        private string _ControlledVariable;
        private string _TargetVariable;
        private SoundParameter _SoundParameter;

        public string ControlledVariable => _ControlledVariable;
        public string TargetVariable => _TargetVariable;
        public SoundParameter SoundParam => _SoundParameter;

        public SoundCurve(string varname, string targetvar, SoundParameter parameter, params Point[] points)
        {
            _ControlledVariable = varname;
            _TargetVariable = targetvar;
            _Points = points;
            _SoundParameter = parameter;
        }

        public float Evaluate(float value)
        {
            float outValue = 0;

            if (value > _Points[_Points.Length - 1].x) return _Points[_Points.Length - 1].y;

            for (int i = 0; i < _Points.Length - 1; i++)
            {
                //If we in our point
                if (value >= _Points[i].x && value < _Points[i + 1].x)
                {
                    outValue = Lerp(_Points[i].y, _Points[i + 1].y, Normalize(value, _Points[i].x, _Points[i + 1].x));

                    if (value > 3)
                    {
                        value = value;
                    }
                    break;
                }
            }

            return outValue;
        }

        private float Lerp(float a, float b, float t)
        {
            t = Math.Min(1f, Math.Max(0, t));
            return a + ((b - a) * t); ;
        }

        private float Normalize(float value, float min, float max)
        {
            return ((value - min) / (max - min));
        }
    }
}
