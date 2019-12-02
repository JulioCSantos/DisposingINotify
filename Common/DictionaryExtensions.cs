using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DictionaryExtensions
    {
        public static bool TryAdd<TKey, TValue>
            (this Dictionary<TKey, Dictionary<TValue, TKey>> outerDict, TKey key, TValue value)
        {
            if (outerDict.ContainsKey(key) == true)
            {
                var innerDict = outerDict[key];
                if (innerDict.ContainsKey(value)) return false;
                else {
                    innerDict.Add(value, key);
                    return true;
                }
            }
            else
            {
                var innerDict = new Dictionary<TValue, TKey> {{value, key}};
                outerDict.Add(key, innerDict);
            }
            return true;
        }

        public static bool TryRemove<TKey, TValue>
            (this Dictionary<TKey, Dictionary<TValue, TKey>> outerDict, TKey key, TValue value)
        {
            if (outerDict.ContainsKey(key) == false) return false;
            var innerDict = outerDict[key];
            if (innerDict.ContainsKey(value) == false) return false;
            else {
                innerDict.Remove(value);
                if (innerDict.Count == 0) outerDict.Remove(key);
                return true;
            }
        }
    }
}
