﻿using Newtonsoft.Json;

namespace BlogProject.Web.Extensions
{
    public static class CustomSessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonData);
        }

        public static T GetObject<T>(this ISession session, string key) where T : class, new()
        {
            var data = session.GetString(key);
            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
