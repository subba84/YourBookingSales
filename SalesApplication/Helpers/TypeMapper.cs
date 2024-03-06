using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SalesApplication
{
    public static class TypeMapper
    {
        public static T Map<S, T>(S obj, Dictionary<string, string> customMapping = null)
        {
            if (obj == null)
                return default(T);
            Dictionary<string, PropertyInfo> propMap = typeof(S).GetProperties().ToDictionary(x => x.Name, x => x);
            PropertyInfo[] props = typeof(T).GetProperties();
            T returnObj = ApplyMap<S, T>(obj, customMapping, propMap, props);
            return returnObj;
        }

        private static T ApplyMap<S, T>(S obj, Dictionary<string, string> customMapping, Dictionary<string, PropertyInfo> propMap, PropertyInfo[] props)
        {
            T returnObj = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in props)
            {
                string key = prop.Name;
                if (customMapping != null && customMapping.Keys.Contains(key))
                {
                    key = customMapping[key];
                }
                if (propMap.Keys.Contains(key))
                {
                    if (prop.SetMethod != null)
                        prop.SetValue(returnObj, propMap[key].GetValue(obj));
                }
            }

            return returnObj;
        }

        public static List<T> Map<S, T>(IEnumerable<S> obj, Dictionary<string, string> customMapping = null)
        {
            //This below Block is newly added to handle null data.
            if (obj == null)
                return null;


            List<T> returnCollection = new List<T>();
            Dictionary<string, PropertyInfo> propMap = typeof(S).GetProperties().ToDictionary(x => x.Name, x => x);
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (S item in obj)
            {
                returnCollection.Add(ApplyMap<S, T>(item, customMapping, propMap, props));
            }
            return returnCollection;
        }
    }
}