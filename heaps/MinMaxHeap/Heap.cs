using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace heaps.MinMaxHeap
{
    public interface IHeap<T> where T : IComparable, IComparable<T>
    {
        T RootValue { get; }

        int Add(T item);
    }

    public class MinHeap<T> : Heap<T> where T : IComparable, IComparable<T>
    {
        protected override bool HeapCondition(int compare) => compare < 0;
    }
    public class MaxHeap<T> : Heap<T> where T : IComparable, IComparable<T>
    {
        protected override bool HeapCondition(int compare) => compare > 0;
    }

    public abstract class Heap<T> : IHeap<T> where T : IComparable, IComparable<T>
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

        internal int Length => _values.Count;
        internal T this[int index] => _values[index];

        public int Add(T item)
        {
            _values.Add(item);
            return Heapify(_values.Count - 1);
        }

        public T Pop(int index)
        {
            return default(T);
        }

        internal bool TryGetParent(int child, out int parent)
        {
            if (child > 0)
            {
                parent = (child - 1) / 2;
                return true;
            }
            parent = -1;
            return false;
        }
        internal bool TryGetLeft(int index, out int left)
        {
            left = index * 2 + 1;
            return left < Length;
        }
        internal bool TryGetRight(int index, out int right)
        {
            right = index * 2 + 2;
            return right < Length;
        }

        public T RootValue => _values.First();
    }
}
