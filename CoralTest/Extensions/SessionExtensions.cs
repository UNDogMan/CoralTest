using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoralTest.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value) where T : new()
        {
            session.SetString(key, JsonSerializer.Serialize<T>(value));
        }

        public static T Get<T>(this ISession session, string key) where T : new()
        {
            var value = session.GetString(key);
            return value is null ? new T() : JsonSerializer.Deserialize<T>(value);
        }
    }
}
