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
        public FraudAlertsTests(ITestOutputHelper output)
        {
            FraudAlerts.output = output;
        }


        [Theory]
        [InlineData(@"E:\funwithflags\heaps\FraudAlerts\input01.txt", 633)]
        [InlineData("E:\\funwithflags\\heaps\\FraudAlerts\\input05.txt", 0)]
        [InlineData("E:\\funwithflags\\heaps\\FraudAlerts\\input07.txt", 1)]
        public void TestCase1(string inFile, int expectedResult)
        {
            var lines = File.ReadAllLines(inFile);

            string[] nd = lines[0].Split(' ');
            int n = Convert.ToInt32(nd[0]);

            int d = Convert.ToInt32(nd[1]);

            int[] expenditure = Array.ConvertAll(lines[1].Split(' '), 
                expenditureTemp => Convert.ToInt32(expenditureTemp))
            ;

            int result = FraudAlerts.activityNotifications(expenditure, d);
            Assert.Equal(expectedResult, result);

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
