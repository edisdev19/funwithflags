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

    public abstract class Heap<T> : IHeap<T> where T : IComparable<T>, IComparable
    {
        private readonly IList<T> _values;

        public Heap(int initialCapacity = 0)
        {
            _values = new List<T>(initialCapacity);
        }

        private int FlowDown(int index)
        {
            while (true)
            {
                T largest = _values[index];
                int largestIndex = index;
                if (TryGetLeft(index, out var leftIndex))
                {
                    if (!HeapCondition(largest.CompareTo(_values[leftIndex])))
                    {
                        largest = _values[leftIndex];
                        largestIndex = leftIndex;
                    }
                }
                if (TryGetRight(index, out var rightIndex))
                {
                    if (!HeapCondition(largest.CompareTo(_values[rightIndex])))
                    {
                        largest = _values[rightIndex];
                        largestIndex = rightIndex;
                    }
                }
                if (largestIndex == index)
                    break;

                Exchange(index, largestIndex);
                index = largestIndex;
            }
            return index;
        }

        private int FlowUp(int index)
        {
            while (TryGetParent(index, out var parent) &&
                !HeapCondition(_values[parent].CompareTo(_values[index])))
            {
                Exchange(index, parent);
                index = parent;
            }
            return index;
        }

        private void Exchange(int index, int parent)
        {
            var aux = _values[index];
            _values[index] = _values[parent];
            _values[parent] = aux;
        }

        protected abstract bool HeapCondition(int compare);

        internal int Length => _values.Count;
        internal T this[int index] => _values[index];

        public int Add(T item)
        {
            _values.Add(item);
            return FlowUp(_values.Count - 1);
        }

        public T Pop(int index)
        {
            Exchange(index, Length - 1);
            T value = _values[Length - 1];
            _values.RemoveAt(Length - 1);
            if (_values.Any() && index < Length)
                FlowUp(FlowDown(index));
            return value;
        }

        public void PopItem(T item)
        {
            Pop(_values.IndexOf(item));
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
