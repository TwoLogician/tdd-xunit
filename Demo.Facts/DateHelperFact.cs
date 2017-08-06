using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using static Demo.DateHelper;

namespace Demo.Facts
{
    public class DateHelperFact
    {
        public class GroupDays
        {
            private ITestOutputHelper Output { get; }
            public GroupDays(ITestOutputHelper output)
            {
                Output = output;
            }

            [Fact]
            [Trait("Level", "Basic")]
            public void EmptyArray_EmptyString()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(days: new int[] { });
                Output.WriteLine("Hello");
                Assert.Equal(string.Empty, result);
            }

            [Fact]
            public void Null_EmptyString()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(null);
                Assert.Equal(string.Empty, result);
            }

            public static IEnumerable<object[]> DaysInMount(int min, int max)
            {
                for (var i = min; i <= max; i++)
                {
                    yield return new object[] { i, i.ToString() };
                }
            }

            [Theory]
            [MemberData("DaysInMount", 10, 15)]
            [InlineData(10, "10")]
            public void SingleDay(int day, string outputDay)
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(new int[] { day });
                Assert.Equal(outputDay, result);
            }

            [Fact]
            public void ContinouslyDays()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(new int[] { 1, 2, 3 });
                Assert.Equal("1-3", result);
            }

            [Fact]
            public void ContinouslyDaysWithSeparateDay()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(new int[] { 1, 2, 5 });
                Assert.Equal("1-2 and 5", result);
            }

            [Fact]
            public void B()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(new int[] { 1, 2, 5, 6, 7 });
                Assert.Equal("1-2 and 5-7", result);
            }

            [Fact]
            public void C()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(new int[] { 1, 3, 5 });
                Assert.Equal("1, 3 and 5", result);
            }

            [Fact]
            public void D()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(new int[] { 1, 6, 5 });
                Assert.Equal("1 and 5-6", result);
            }

            [Fact]
            public void E()
            {
                var helper = new DateHelper();
                var result = helper.GroupDays(new int[] { 1, 5, 5, 6, 7, 2, 15, 16, 17 });
                Assert.Equal("1-2, 5-7 and 15-17", result);
            }

            [Fact]
            public void DaysOutOfRange_ThrowsException()
            {
                var helper = new DateHelper();
                var ex = Assert.Throws<InvalidDayException>(() => helper.GroupDays(new int[] { 0, 1 }));
                Assert.Equal(0, ex.FirstInvalidDay);
                ex = Assert.Throws<InvalidDayException>(() => helper.GroupDays(new int[] { 29, 30, 31, 32 }));
                Assert.Equal(32, ex.FirstInvalidDay);
            }
        }
    }
}
