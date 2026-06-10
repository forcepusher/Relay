using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace BananaParty.WebSocketRelay
{
    public static class StateBridge
    {
        private static readonly Dictionary<Type, PropertyInfo> _valuePropertyCache = new();

        public static void Copy(IState source, IState target)
        {
            if (source == null || target == null) return;

            var sourceType = source.GetType();
            var targetType = target.GetType();

            if (sourceType != targetType)
                throw new ArgumentException($"Cannot copy state from {sourceType.Name} to {targetType.Name}");

            // Handle container states first by looking for the '_states' field
            var statesField = sourceType.GetField("_states", BindingFlags.NonPublic | BindingFlags.Instance);
            if (statesField != null)
            {
                CopyContainerStates(source, target, statesField);
                return;
            }

            // For simple states, they have a 'Value' property.
            var valueProp = GetValueProperty(sourceType);
            if (valueProp != null)
            {
                var val = valueProp.GetValue(source);
                valueProp.SetValue(target, val);
            }
        }

        private static void CopyContainerStates(IState source, IState target, FieldInfo statesField)
        {
            var sourceList = statesField.GetValue(source) as IList;
            var targetList = statesField.GetValue(target) as IList;

            if (sourceList == null || targetList == null) return;

            int count = Math.Min(sourceList.Count, targetList.Count);
            for (int i = 0; i < count; i++)
            {
                var s = sourceList[i] as IState;
                var t = targetList[i] as IState;
                if (s != null && t != null)
                    Copy(s, t);
            }
        }

        private static PropertyInfo GetValueProperty(Type type)
        {
            if (_valuePropertyCache.TryGetValue(type, out var prop))
                return prop;

            prop = type.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
                _valuePropertyCache[type] = prop;

            return prop;
        }
    }
}
