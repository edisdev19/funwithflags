using heaps.MinMaxHeap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;

namespace heaps.FraudAlerts
{
    public class FraudAlerts
    {
        public static int MedianFinder(int[] items)
        {
            var minHeap = new MinHeap<int>();
            var maxHeap = new MaxHeap<int>();

            foreach (int item in items)
            {
                maxHeap.Add(item);
                if (maxHeap.Length > items.Length / 2)
                {
                    var maxItem = maxHeap.Pop(0);
                    minHeap.Add(maxItem);
                }
            }

            return items.Length % 2 == 1 ?
                minHeap[0] : ((minHeap[0] + maxHeap[0]) / 2);
        }

        public static int activityNotificationsTrivial(int[] expenditure, int d)
        {
            int n = expenditure.Length;
            int counter = 0;
            for (int i = d; i < n; i++)
            {
                var past = expenditure.Skip(i - d).Take(d).OrderBy(x => x).ToArray();
                var median = d % 2 == 1 ? past[d / 2] * 0.1 : ((past[d / 2] * 0.1 + past[d / 2] * 0.1 - 1) * 0.1 / 2);
                if (expenditure[i] >= median)
                    counter++;
            }
            return counter;
        }

        public static int activityNotifications(int[] expenditure, int d)
        {
            var minHeap = new MinHeap<int>();
            var maxHeap = new MaxHeap<int>();

            for (int i = 0; i < d; i++)
            {
                maxHeap.Add(expenditure[i]);
                if (maxHeap.Length > (d + 1) / 2)
                {
                    var maxItem = maxHeap.Pop(0);
                    minHeap.Add(maxItem);
                }
            }

            int counter = 0;
            for (int i = d; i < expenditure.Length; i++)
            {
                var mid = d % 2 == 1 ? (2 * maxHeap[0]) : (minHeap[0] + maxHeap[0]);
                if (expenditure[i] >= mid) counter++;

                var oldExpenditure = expenditure[i - d];
                if (oldExpenditure <= maxHeap[0])
                    maxHeap.PopItem(oldExpenditure);
                else
                    minHeap.PopItem(oldExpenditure);

                maxHeap.Add(expenditure[i]);
                if (maxHeap.Length > (d + 1) / 2)
                {
                    var maxItem = maxHeap.Pop(0);
                    minHeap.Add(maxItem);
                }
                else if (maxHeap[0] > minHeap[0])
                {
                    var maxMax = maxHeap.Pop(0);
                    var minMin = minHeap.Pop(0);
                    maxHeap.Add(minMin);
                    minHeap.Add(maxMax);
                }

            }

            return counter;
        }
    }
}
