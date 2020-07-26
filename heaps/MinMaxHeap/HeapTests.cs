using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.Equal(5, heap.RootValue);
            AssertMaxHeapified(heap);
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
            Assert.Equal(3, heap.RootValue);
            AssertMinHeapified(heap);
        }

        [Fact]
        public void A06_LeftRight()
        {
            IDictionary<int, (int? left, int? right)> expectedChildren =
                Enumerable.Range(0, 10)
                .ToDictionary(i => i, i => ((int?)null, (int?)null));

            var heap = new MaxHeap<int>();
            AssertChildren(heap, expectedChildren);

            heap.Add(1234);
            AssertChildren(heap, expectedChildren);

            heap.Add(1234);
            expectedChildren[0] = (1, null);
            AssertChildren(heap, expectedChildren);

            heap.Add(1234);
            expectedChildren[0] = (1, 2);
            AssertChildren(heap, expectedChildren);

            heap.Add(12345);
            expectedChildren[1] = (3, null);
            AssertChildren(heap, expectedChildren);

            heap.Add(12345);
            expectedChildren[1] = (3, 4);
            AssertChildren(heap, expectedChildren);
        }

        private void AssertChildren(MaxHeap<int> heap, IDictionary<int, (int? left, int? right)> expectedChildren)
        {
            foreach (var i in expectedChildren.Keys)
            {
                AssertChildren(heap, i, expectedChildren[i]);
            }
        }

        private void AssertChildren<T>(Heap<T> heap, int parent, (int? left, int? right) expected)
            where T : IComparable, IComparable<T>
        {
            var isLeftAvailable = heap.TryGetLeft(parent, out var left);
            if (expected.left.HasValue)
            {
                Assert.True(isLeftAvailable);
                Assert.Equal(expected.left.Value, left);
            }
            else
            {
                Assert.False(isLeftAvailable);
            }
            var isRightAvailable = heap.TryGetRight(parent, out var right);
            if (expected.right.HasValue)
            {
                Assert.True(isRightAvailable);
                Assert.Equal(expected.right.Value, right);
            }
            else
            {
                Assert.False(isRightAvailable);
            }
        }

        public static object[][] A07_A08_BuildMaxHeap_Data = new object[][]
        {
            new object[] { new [] { 1, 2, 3, 4, 5, 6, 7 } },
            new object[] { new [] { 4, 5, 5, 6, 3, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 7, 7, 7, 7, 100, 1, 2, 3, 4, 4, 4 } },
            new object[] { new [] { 7, 5, 1, 8, 2, 1, 3, 3, 2, 54, 6, 10 } }
        };

        [Theory]
        [MemberData(nameof(A07_A08_BuildMaxHeap_Data))]
        public void A07_BuildMaxHeap(int[] input)
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            for (int i = 0; i < input.Length; i++)
            {
                maxHeap.Add(input[i]);
                AssertMaxHeapified(maxHeap);
            }
            AssertHeapData(input, maxHeap);
        }

        private void AssertHeapData(IEnumerable<int> input, MaxHeap<int> maxHeap)
        {
            var inputList = new List<int>(input);
            AssertMaxHeapified(maxHeap);
            for (int i = 0; i < maxHeap.Length; i++)
            {
                Assert.Contains(maxHeap[i], inputList);
                inputList.Remove(maxHeap[i]);
            }
            Assert.Empty(inputList);
        }

        private void AssertMaxHeapified(MaxHeap<int> maxHeap, int i = 0)
        {
            if (maxHeap.TryGetLeft(i, out int iLeft))
            {
                Assert.True(maxHeap[i] >= maxHeap[iLeft]);
                AssertMaxHeapified(maxHeap, iLeft);
            }
            if (maxHeap.TryGetRight(i, out int iRight))
            {
                Assert.True(maxHeap[i] >= maxHeap[iRight]);
                AssertMaxHeapified(maxHeap, iRight);
            }
        }
        private void AssertMinHeapified(MinHeap<int> minHeap, int i = 0)
        {
            if (minHeap.TryGetLeft(i, out int iLeft))
            {
                Assert.True(minHeap[i] <= minHeap[iLeft]);
                AssertMinHeapified(minHeap, iLeft);
            }
            if (minHeap.TryGetRight(i, out int iRight))
            {
                Assert.True(minHeap[i] <= minHeap[iRight]);
                AssertMinHeapified(minHeap, iRight);
            }
        }

        [Theory]
        [MemberData(nameof(A07_A08_BuildMaxHeap_Data))]
        public void A08_PopRoot(int[] input)
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            for (int i = 0; i < input.Length; i++)
            {
                maxHeap.Add(input[i]);
            }
            AssertHeapData(input, maxHeap);
            var inputList = input.OrderByDescending(p => p).ToArray();
            for (int i = 0; i < input.Length; i++)
            {
                Assert.Equal(inputList[i], maxHeap.Pop(0));
                AssertHeapData(inputList.Skip(i + 1), maxHeap);
            }
        }

        [Theory]
        [MemberData(nameof(A07_A08_BuildMaxHeap_Data))]
        public void A08_PopMiddle(int[] input)
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            for (int i = 0; i < input.Length; i++)
            {
                maxHeap.Add(input[i]);
            }
            AssertHeapData(input, maxHeap);
            var midIndex = input.Length / 2;
            var inputList = new List<int>(input);
            var midItem = maxHeap[midIndex];
            inputList.Remove(midItem);

            maxHeap.Pop(midIndex);
            AssertHeapData(inputList, maxHeap);
        }

        [Fact]
        public void A09_PopLast()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            maxHeap.Add(1);
            maxHeap.Add(2);
            maxHeap.PopItem(2);
            Assert.Equal(1, maxHeap[0]);
            Assert.Equal(1, maxHeap.Length);
        }
    }
}
