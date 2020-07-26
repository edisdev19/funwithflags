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
            RollingMedianFinder medianFinder = new RollingMedianFinder(d);
            for (int i = 0; i < d; i++)
                medianFinder.Add(expenditure[i]);

            int counter = 0;
            for (int i = d; i < expenditure.Length; i++)
            {
                if (expenditure[i] >= medianFinder.DoubleMid)
                    counter++;
                medianFinder.Remove(expenditure[i - d]);
                medianFinder.Add(expenditure[i]);
            }

            return counter;
        }

        public class RollingMedianFinder
        {
            private int _d;
            MaxHeap<int> _maxHeap = new MaxHeap<int>();
            MinHeap<int> _minHeap = new MinHeap<int>();

            public RollingMedianFinder(int d)
            {
                _d = d;
            }
            public void Add(int i)
            {
                _maxHeap.Add(i);
                if (_maxHeap.Length > (_d + 1) / 2)
                {
                    var maxItem = _maxHeap.Pop(0);
                    _minHeap.Add(maxItem);
                }
                else if (_maxHeap.Length > 0 && _minHeap.Length > 0 && 
                    _maxHeap[0] > _minHeap[0])
                {
                    var maxMax = _maxHeap.Pop(0);
                    var minMin = _minHeap.Pop(0);
                    _maxHeap.Add(minMin);
                    _minHeap.Add(maxMax);
                }
            }
            public void Remove(int i)
            {
                if (i <= _maxHeap[0])
                    _maxHeap.PopItem(i);
                else
                    _minHeap.PopItem(i);

            }
            public int DoubleMid =>
                _d % 2 == 1
                    ? (2 * _maxHeap[0])
                    : (_minHeap[0] + _maxHeap[0])
                ;
        }
    }
}
