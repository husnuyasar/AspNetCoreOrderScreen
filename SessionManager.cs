using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OrderScreen.Data;

namespace OrderScreen
{
    public static class SessionManager
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void Clear(this ISession session, string key)
        {
            session.Remove(key);
        }
    }
}