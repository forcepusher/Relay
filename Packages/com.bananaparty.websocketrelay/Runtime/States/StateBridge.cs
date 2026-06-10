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

            // Optimization: Check if the type has a CopyFrom method we can call directly
            // (Even if it's not in the interface, we can use reflection once or cache the delegate)
            var copyMethod = sourceType.GetMethod("CopyFrom", new[] { typeof(IState) });
            if (copyMethod != null)
            {
                copyMethod.Invoke(target, new object[] { source });
                return;
            }

            // Handle complex states first
            if (source is ObjectState objSource && target is ObjectState objTarget)
            {
                CopyObjectStates(objSource, objTarget);
                return;
            }

            // For most other states, they have a 'Value' property.
            var valueProp = GetValueProperty(sourceType);
            if (valueProp != null)
            {
                var val = valueProp.GetValue(source);
                valueProp.SetValue(target, val);
            }
        }

        private static void CopyObjectStates(ObjectState source, ObjectState target)
        {
            // Since we can't access the private _states list easily without reflection
            var statesField = typeof(ObjectState).GetField("_states", BindingFlags.NonPublic | BindingFlags.Instance);
            if (statesField == null) return;

            var sourceStates = (List<IState>)statesField.GetValue(source);
            var targetStates = (List<IState>)statesField.GetValue(target);

            if (sourceStates == null || targetStates == null) return;

            for (int i = 0; i < Math.Min(sourceStates.Count, targetStates.Count); i++)
            {
                Copy(sourceStates[i], targetStates[i]);
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
