using UnityEngine;

namespace CodeBase.Core.Data
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj) => 
            JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string serializedString) =>
            JsonUtility.FromJson<T>(serializedString);
    }
}