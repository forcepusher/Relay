using System.Collections.Generic;

namespace BananaParty.WebSocketRelay
{
    public class Journal<T>
    {
        private readonly List<T> _list;
        private List<T> _snapshot = new();

        public Journal(List<T> list)
        {
            _list = list;
        }

        public void Snapshot()
        {
            _snapshot = new List<T>(_list);
        }

        public IReadOnlyList<T> GetDeletes()
        {
            var deletes = new List<T>();

            foreach (T item in _snapshot)
            {
                if (!_list.Contains(item))
                    deletes.Add(item);
            }

            return deletes;
        }

        public IReadOnlyList<T> GetWrites()
        {
            var writes = new List<T>();

            foreach (T item in _list)
            {
                if (!_snapshot.Contains(item))
                    writes.Add(item);
            }

            return writes;
        }

        public IReadOnlyList<T> GetUpdates()
        {
            var updates = new List<T>();

            foreach (T item in _list)
            {
                if (_snapshot.Contains(item))
                    updates.Add(item);
            }

            return updates;
        }
    }
}
