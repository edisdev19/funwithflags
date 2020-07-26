using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace heaps.FraudAlerts
{
    public class FraudAlertsTests
    {
        [Fact]
        public void TestCase1()
        {
            Assert.Equal(
                2,
                FraudAlerts.activityNotifications(new[] { 2, 3, 4, 2, 3, 6, 8, 4, 5 }, 5)
                );
        }

        public static object[][] MidFinderTests = new object[][]
        {
            new object[] { new int[] { 0 }, 0 },
            new object[] { new int[] { 1 }, 1 },
            new object[] { new int[] { 1, 3 }, 2 },
            new object[] { new int[] { 1, 3, 4 }, 3 },
            new object[] { new int[] { 1, 3, 4, 5, 1, 1 }, 2 },
            new object[] { new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 2 },
            new object[] { new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 3 }, 2 },
            new object[] { new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3 }, 2 },
            new object[] { new int[] { 3, 3, 3, 2, 2, 2, 3, 3, 3 }, 3 },
            new object[] { new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 2 }
        };
        [Theory]
        [MemberData(nameof(MidFinderTests))]
        public void MidFinder(int[] input, int mid)
        {
            int foundMid = FraudAlerts.MedianFinder(input);
            Assert.Equal(mid, foundMid);
        }

    }
}
