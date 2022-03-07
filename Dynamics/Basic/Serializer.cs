using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Basic
{
    public static class Serializer
    {
        public static void AddIfProvided<T>(this JObject jobject, string attribute, T value)
        {
            if (value == null)
                return;

            var type = GetType(typeof(T));

            if (type == typeof(string))
                jobject.AddString(attribute, value);

            else if (type == typeof(int))
                jobject.AddInteger(attribute, value);

            else if (type == typeof(double))
                jobject.AddDouble(attribute, value);

            else if (type == typeof(bool))
                jobject.AddBool(attribute, value);
        }

        public static void AddInteger<T>(this JObject jobject, string attribute, T value)
        {
            jobject[attribute] = Convert.ToInt32(value);
        }

        public static void AddDouble<T>(this JObject jobject, string attribute, T value)
        {
            jobject[attribute] = Convert.ToDouble(value);
        }

        public static void AddString<T>(this JObject jobject, string attribute, T value)
        {
            jobject[attribute] = value.ToString();
        }

        public static void AddBool<T>(this JObject jobject, string attribute, T value)
        {
            jobject[attribute] = Convert.ToBoolean(value);
        }

        public static Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type; 
        }
    }
}
