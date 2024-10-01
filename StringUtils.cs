using System;
using System.Linq.Expressions;
using System.Text;


namespace Cfsb.Incoming.FedWires.Utils
{
    public static class StringUtils
    {

        public static string PadCenter(this string s, int width, char c = ' ')
        {
            if (string.IsNullOrEmpty(s)) s = " ";
            if (s == null || width <= s.Length) return s;

            int padding = width - s.Length;
            return s.PadLeft(s.Length + padding / 2, c).PadRight(width, c);
        }


        public static string PadRight(this string s, int width, char c = ' ')
        {
            if (string.IsNullOrEmpty(s)) s = " ";
            if (s == null || width <= s.Length) return s;

            int padding = width - s.Length;
            return s.PadRight(padding, c);
        }


        public static string PadLeft(this string s, int width, char c = ' ')
        {
            if (string.IsNullOrEmpty(s)) s = " ";
            if (s == null || width <= s.Length) return s;

            int padding = width - s.Length;
            return s.PadLeft(padding, c);
        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {

            return (T)Enum.Parse(typeof(T), value, true);


        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given int.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }
    }
}
