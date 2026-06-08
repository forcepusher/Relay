using System;
using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class DynamicArrayNode<T> : IStateNode where T : IStateNode
    {
        public string Name { get; }
        private readonly List<T> _nodes;
        private readonly List<T> _entriesToDelete = new();
        private readonly List<T> _entriesToAdd = new();
        private readonly List<(T Current, T Desired)> _entriesToWrite = new();

        public DynamicArrayNode(string name, List<T> nodes)
        {
            Name = name;
            _nodes = nodes;
        }

        public IReadOnlyList<T> Items => _nodes;

        public void Write(IWriteGraph writeGraph)
        {
            writeGraph.StartArray(Name);
            writeGraph.WriteEntry(_nodes.Count);

            foreach (T node in _nodes)
                node.Write(writeGraph);

            writeGraph.EndArray();
        }

        public void Read(IReadGraph readGraph)
        {
            throw new NotSupportedException(
                $"Use {nameof(Read)}({nameof(IReadGraph)}, {nameof(Func<T>)}) on {nameof(DynamicArrayNode<T>)}.");
        }

        public void Read(IReadGraph readGraph, Func<T> createEntry)
        {
            readGraph.StartArray(Name);
            int count = readGraph.ReadIntArrayEntry();
            var desired = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                T entry = createEntry();
                entry.Read(readGraph);
                desired.Add(entry);
            }

            readGraph.EndArray();
            ReconcileAgainst(desired);
        }

        public void ReconcileAgainst(IReadOnlyList<T> desired) =>
            ReconcileAgainst(desired, _nodes);

        public void ReconcileAgainst(IReadOnlyList<T> desired, IReadOnlyList<T> current)
        {
            _entriesToDelete.Clear();
            _entriesToAdd.Clear();
            _entriesToWrite.Clear();

            var matchedDesiredIndices = new HashSet<int>();

            foreach (T currentEntry in current)
            {
                int matchIndex = FindMatchIndex(currentEntry, desired, matchedDesiredIndices);

                if (matchIndex >= 0)
                {
                    _entriesToWrite.Add((currentEntry, desired[matchIndex]));
                    matchedDesiredIndices.Add(matchIndex);
                }
                else
                {
                    _entriesToDelete.Add(currentEntry);
                }
            }

            for (int i = 0; i < desired.Count; i++)
            {
                if (!matchedDesiredIndices.Contains(i))
                    _entriesToAdd.Add(desired[i]);
            }
        }

        public IReadOnlyList<T> GetEntriesToDelete() => _entriesToDelete;

        public IReadOnlyList<T> GetEntriesToAdd() => _entriesToAdd;

        public IReadOnlyList<(T Current, T Desired)> GetEntriesToWrite() => _entriesToWrite;

        private static int FindMatchIndex(T currentEntry, IReadOnlyList<T> desired, HashSet<int> matchedDesiredIndices)
        {
            if (!string.IsNullOrEmpty(currentEntry.Name))
            {
                for (int i = 0; i < desired.Count; i++)
                {
                    if (matchedDesiredIndices.Contains(i))
                        continue;

                    if (desired[i].Name == currentEntry.Name)
                        return i;
                }

                return -1;
            }

            for (int i = 0; i < desired.Count; i++)
            {
                if (!matchedDesiredIndices.Contains(i))
                    return i;
            }

            return -1;
        }
    }
}
