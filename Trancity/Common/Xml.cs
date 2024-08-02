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
                var info = new NumberFormatInfo {NumberDecimalSeparator = "."};
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

