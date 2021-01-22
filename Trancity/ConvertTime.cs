using System;

namespace Trancity
{
    public class ConvertTime
    {
        public static string TimeFromSeconds(double seconds)
        {
//            const string str = "";
            var num2 = Math.Floor(seconds / 60.0) % 60.0;
            var num3 = Math.Floor(seconds) % 60.0;
            return (((/*str + */Math.Floor((seconds / 60.0) / 60.0).ToString("00")) + ":" + num2.ToString("00")) + ":" + num3.ToString("00"));
        }
    }
}