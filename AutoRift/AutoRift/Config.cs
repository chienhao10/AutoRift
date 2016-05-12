using System;
using System.Collections.Generic;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AutoRift
{
    public static class Config
    {
        private static Dictionary<ConfigKey, object> Values { get; set; }

        public static void Init()
        {
            Values = new Dictionary<ConfigKey, object>();
            MenuManager.LoadMenu();
        }

        public static bool Bool(this ConfigKey key)
        {
            return Get<bool>(key);
        }

        public static int Int(this ConfigKey key)
        {
            return Get<int>(key);
        }
        public static T Get<T>(this ConfigKey key)
        {
            if (!Values.ContainsKey(key))
            {
                return default(T);
            }

            var valueBase = Values[key] as ValueBase<T>;
            if (valueBase != null)
            {
                return valueBase.CurrentValue;
            }

            if (Values[key] is T)
            {
                return (T) Values[key];
            }
            return default(T);
        }

        public static T Set<T>(ConfigKey key, T value)
        {
            if (Values.ContainsKey(key)) throw new InvalidOperationException("Key already exists");
            Values.Add(key, value);
            return value;
        }

        public static T Set<T>(Menu menu, ConfigKey key, T value) where T : ValueBase
        {
            return Set(key, menu.Add(key.ToString().ToLower(), value));
        }
    }
}