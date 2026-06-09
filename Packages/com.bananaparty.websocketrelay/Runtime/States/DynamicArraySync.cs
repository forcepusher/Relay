using System;
using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    internal static class DynamicArraySync
    {
        public static void ReadByIndex<T>(
            int count,
            List<T> states,
            IFactory<T> factory,
            Action<T> readEntry,
            Action<T> disposeEntry)
        {
            while (states.Count > count)
            {
                T removed = states[states.Count - 1];
                states.RemoveAt(states.Count - 1);
                disposeEntry(removed);
            }

            while (states.Count < count)
                states.Add(factory.Create(Guid.Empty));

            for (int i = 0; i < count; i++)
                readEntry(states[i]);
        }

        public static void ReadByKey<T>(
            int count,
            List<T> states,
            IFactory<T> factory,
            Action<T> readEntry,
            Action<IState, IState> copyState,
            Action<T> disposeEntry) where T : IState
        {
            var incoming = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                T staging = factory.Create(Guid.Empty);
                readEntry(staging);
                incoming.Add(staging);
            }

            var incomingKeys = new HashSet<Guid>();
            foreach (T entry in incoming)
                incomingKeys.Add(factory.GetKey(entry));

            for (int i = states.Count - 1; i >= 0; i--)
            {
                if (incomingKeys.Contains(factory.GetKey(states[i])))
                    continue;

                T removed = states[i];
                states.RemoveAt(i);
                disposeEntry(removed);
            }

            var next = new List<T>(incoming.Count);
            foreach (T staging in incoming)
            {
                Guid entryKey = factory.GetKey(staging);
                T existing = FindByKey(states, factory, entryKey);
                if (existing != null)
                {
                    copyState(staging, existing);
                    disposeEntry(staging);
                    next.Add(existing);
                }
                else
                {
                    T entry = factory.Create(entryKey);
                    copyState(staging, entry);
                    disposeEntry(staging);
                    next.Add(entry);
                }
            }

            states.Clear();
            states.AddRange(next);
        }

        private static T FindByKey<T>(List<T> states, IFactory<T> factory, Guid entryKey)
        {
            foreach (T state in states)
            {
                if (factory.GetKey(state) == entryKey)
                    return state;
            }

            return default;
        }
    }
}
