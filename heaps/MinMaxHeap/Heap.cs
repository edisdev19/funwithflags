using System;
using System.Collections.Generic;
using System.Text;

namespace heaps.MinMaxHeap
{
    public class MinHeap<T> : Heap<T> where T : IComparable
    {
        protected override bool HeapCondition(int compare) => compare < 0;
    }
    public class MaxHeap<T> : Heap<T> where T : IComparable
    {
        protected override bool HeapCondition(int compare) => compare > 0;
    }

    public abstract class Heap<T> where T : IComparable
    {
        private readonly IList<T> _values;

        public Heap(int initialCapacity = 0)
        {
            _values = new List<T>(initialCapacity);
        }

        private int Heapify(int child)
        {
            while (TryGetParent(child, out var parent) &&
                !HeapCondition(_values[parent].CompareTo(_values[child])))
            {
                var aux = _values[child];
                _values[child] = _values[parent];
                _values[parent] = aux;
                child = parent;
            }
            return child;
        }
        protected abstract bool HeapCondition(int compare);

        public int Length => _values.Count;
        public T this[int index] => _values[index];

        public int Add(T item)
        {
            _values.Add(item);
            return Heapify(_values.Count - 1);
        }

        public bool TryGetParent(int child, out int parent)
        {
            if (child > 0)
            {
                parent = (child - 1) / 2;
                return true;
            }
            parent = -1;
            return false;
        }
    }
}
