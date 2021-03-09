using Engine;

namespace Common
{
    using System;
    using System.Globalization;
    using System.Xml;

    public class Xml
    {
        public static XmlDocument document;

        public static XmlDocument Connect(string filename)
        {
            document = new XmlDocument();
            document.Load(filename);
            return document;
        }

        public static void Disconnect()
        {
            document = null;
        }

        public static XmlElement AddElement(XmlNode parent, string name)
        {
            return AddElement(parent, name, string.Empty);//"");
        }

        public static XmlElement AddElement(XmlNode parent, string name, double value)
        {
            return AddElement(parent, name, value.ToString(DoubleFormat));
        }

        public static XmlElement AddElement(XmlNode parent, string name, string text)
        {
            var newChild = document.CreateElement(name);
            if (!string.IsNullOrEmpty(text))
            {
                newChild.InnerText = text;
            }
            parent.AppendChild(newChild);
            return newChild;
        }

        public static double GetDouble(XmlNode element)
        {
            return GetDouble(element, 0.0);
        }

        public static double GetDouble(XmlNode element, double default_value)
        {
            try
            {
                return double.Parse(element.InnerText, DoubleFormat);
            }
            catch
            {
                return default_value;
            }
        }

        public static float GetFloat(XmlNode element, float default_value)
        {
            if (float.TryParse(element.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
            {
                return value;
            }

            return default_value;
        }

        public static float GetFloatAttrib(XmlNode element, float default_value)
        {
            if (float.TryParse(element.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
            {
                return value;
            }

            return default_value;
        }

        public static int GetIntAttrib(XmlNode element, int default_value)
        {
            if (int.TryParse(element.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
            {
                return value;
            }

            return default_value;
        }

        //TODO: Maybe add some checks
        public static Double3DPoint GetPosition(XmlNode element)
        {
            Double3DPoint point = Double3DPoint.Zero;

            point.x = GetFloat(element["x"], 0f);
            point.y = GetFloat(element["y"], 0f);
            point.z = GetFloat(element["z"], 0f);

            return point;
        }

        public static string GetString(XmlNode element)
        {
            return GetString(element, string.Empty);
        }

        public static string GetString(XmlNode element, string default_value)
        {
            if (element != null)
            {
                return element.InnerText;
            }
            return default_value;
        }

        public static IFormatProvider DoubleFormat
        {
            get
            {
                var info = new NumberFormatInfo { NumberDecimalSeparator = "." };
                return info;
            }
        }

        /*public static bool GetSimpleBool(XmlNode element)
        {
        	if (element != null)
            {
        		return true;
        	}
            return false;
        }*/
    }
}

