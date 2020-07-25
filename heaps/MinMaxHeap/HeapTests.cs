using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace heaps.MinMaxHeap
{
    public class HeapTests
    {
        [Fact]
        public void A00_LengthIsInitializedToZero()
        {
            Heap<int> heap = new MaxHeap<int>();
            Assert.Equal(0, heap.Length);
        }

        [Fact]
        public void A01_FirstNodeIsInserted()
        {
            int dummyItem = 27349;
            Heap<int> heap = new MaxHeap<int>();
            var index = heap.Add(27349);
            Assert.Equal(1, heap.Length);
            Assert.Equal(0, index);
            int firstItem = heap[index];
            Assert.Equal(dummyItem, firstItem);
        }

        [Fact]
        public void A02_Parents()
        {
            var heap = new MaxHeap<int>();
            Assert.False(heap.TryGetParent(0, out var _));
            Assert.True(heap.TryGetParent(1, out var parentOf1));
            Assert.Equal(0, parentOf1);
            Assert.True(heap.TryGetParent(2, out var parentOf2));
            Assert.Equal(0, parentOf2);
            Assert.True(heap.TryGetParent(3, out var parentOf3));
            Assert.Equal(1, parentOf3);
            Assert.True(heap.TryGetParent(4, out var parentOf4));
            Assert.Equal(1, parentOf4);
            Assert.True(heap.TryGetParent(5, out var parentOf5));
            Assert.Equal(2, parentOf5);
            Assert.True(heap.TryGetParent(6, out var parentOf6));
            Assert.Equal(2, parentOf6);
        }

        [Fact]
        public void A03_SecondInsertHeapify()
        {
            var smallerItem = 3;
            var largerItem = 71;

            var heap1 = new MaxHeap<int>();
            var heap2 = new MaxHeap<int>();

            heap1.Add(smallerItem);
            heap1.Add(largerItem);
            heap2.Add(largerItem);
            heap2.Add(smallerItem);

            Assert.Equal(2, heap1.Length);
            Assert.Equal(heap2.Length, heap1.Length);
            Assert.Equal(largerItem, heap1[0]);
            Assert.Equal(smallerItem, heap1[1]);
            Assert.Equal(largerItem, heap2[0]);
            Assert.Equal(smallerItem, heap2[1]);
        }

        [Fact]
        public void A04_Heapified_Max()
        {
            var heap = new MaxHeap<int>();
            heap.Add(3);
            heap.Add(3);
            heap.Add(3);
            heap.Add(3);
            heap.Add(3);
            heap.Add(5);
            heap.Add(4);
            heap.Add(4);
            Assert.Equal(5, heap[0]);
        }

        [Fact]
        public void A05_Heapified_Min()
        {
            var heap = new MinHeap<int>();
            heap.Add(3);
            heap.Add(3);
            heap.Add(3);
            heap.Add(3);
            heap.Add(3);
            heap.Add(5);
            heap.Add(4);
            heap.Add(4);
            Assert.Equal(3, heap[0]);
        }

    }
}
