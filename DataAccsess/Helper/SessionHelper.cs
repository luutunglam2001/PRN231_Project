
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace DataAccsess.Helper
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession sesson, string key, object value)
        {
            sesson.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectFormJson<T> (this ISession sesson, string key)
        {
            var value = sesson.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
