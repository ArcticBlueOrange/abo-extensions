using AboExtensions.Dates;

namespace AboExtensions.Tests;

public class TestDateExtensions
{
    [Theory]
    [InlineData(2024, 1, 6, true)]   // Saturday
    [InlineData(2024, 1, 7, true)]   // Sunday
    [InlineData(2024, 1, 8, false)]  // Monday
    [InlineData(2024, 1, 5, false)]  // Friday
    public void TestIsWeekend(int y, int m, int d, bool expected) =>
        Assert.Equal(expected, new DateTime(y, m, d).IsWeekend());

    [Theory]
    [InlineData(2024, 1, 6, false)]  // Saturday
    [InlineData(2024, 1, 7, false)]  // Sunday
    [InlineData(2024, 1, 8, true)]   // Monday
    [InlineData(2024, 1, 5, true)]   // Friday
    public void TestIsWeekday(int y, int m, int d, bool expected) =>
        Assert.Equal(expected, new DateTime(y, m, d).IsWeekday());

    [Fact]
    public void TestStartOfDay()
    {
        var d = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = d.StartOfDay();
        Assert.Equal(new DateTime(2024, 6, 15, 0, 0, 0), result);
    }

    [Fact]
    public void TestEndOfDay()
    {
        var d = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = d.EndOfDay();
        Assert.Equal(new DateTime(2024, 6, 15, 23, 59, 59, 999).AddMicroseconds(999).AddTicks(9), result);
    }

    [Theory]
    [InlineData(2024, 1, 10, DayOfWeek.Monday, 2024, 1, 8)]   // Wednesday → Monday
    [InlineData(2024, 1, 8, DayOfWeek.Monday, 2024, 1, 8)]    // Monday → same day
    [InlineData(2024, 1, 14, DayOfWeek.Monday, 2024, 1, 8)]   // Sunday → previous Monday
    [InlineData(2024, 1, 10, DayOfWeek.Sunday, 2024, 1, 7)]   // Wednesday → previous Sunday
    public void TestStartOfWeek(int y, int m, int d, DayOfWeek startDay, int ey, int em, int ed) =>
        Assert.Equal(new DateTime(ey, em, ed), new DateTime(y, m, d).StartOfWeek(startDay));

    [Fact]
    public void TestAge()
    {
        var birthDate = DateTime.Today.AddYears(-30);
        Assert.Equal(30, birthDate.Age());
    }

    [Fact]
    public void TestAgeBeforeBirthday()
    {
        var birthDate = DateTime.Today.AddYears(-30).AddDays(1);
        Assert.Equal(29, birthDate.Age());
    }

    [Fact]
    public void TestAgeOnBirthday()
    {
        var birthDate = DateTime.Today.AddYears(-30);
        Assert.Equal(30, birthDate.Age());
    }

    [Fact]
    public void TestIsInThePast() =>
        Assert.True(DateTime.Now.AddSeconds(-1).IsInThePast());

    [Fact]
    public void TestIsInTheFuture() =>
        Assert.True(DateTime.Now.AddSeconds(1).IsInTheFuture());

    [Fact]
    public void TestIsInThePast_Future_ReturnsFalse() =>
        Assert.False(DateTime.Now.AddSeconds(1).IsInThePast());

    [Fact]
    public void TestIsInTheFuture_Past_ReturnsFalse() =>
        Assert.False(DateTime.Now.AddSeconds(-1).IsInTheFuture());

    [Theory]
    [InlineData(1, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(6, 2)]
    [InlineData(7, 3)]
    [InlineData(9, 3)]
    [InlineData(10, 4)]
    [InlineData(12, 4)]
    public void TestQuarter(int month, int expected) =>
        Assert.Equal(expected, new DateTime(2024, month, 1).Quarter());

    [Fact]
    public void TestAddWorkdays_Forward()
    {
        var friday = new DateTime(2024, 1, 5);   // Friday
        var result = friday.AddWorkdays(3);
        Assert.Equal(new DateTime(2024, 1, 10), result);  // skips Sat+Sun → Wed
    }

    [Fact]
    public void TestAddWorkdays_Backward()
    {
        var wednesday = new DateTime(2024, 1, 10);
        var result = wednesday.AddWorkdays(-3);
        Assert.Equal(new DateTime(2024, 1, 5), result);   // skips Sat+Sun → Fri
    }

    [Fact]
    public void TestAddWorkdays_Zero() =>
        Assert.Equal(new DateTime(2024, 1, 5), new DateTime(2024, 1, 5).AddWorkdays(0));

    [Theory]
    [InlineData(2024, 1, 8, DayOfWeek.Monday, 2024, 1, 15)]   // Monday → next Monday
    [InlineData(2024, 1, 10, DayOfWeek.Friday, 2024, 1, 12)]  // Wednesday → Friday
    [InlineData(2024, 1, 12, DayOfWeek.Monday, 2024, 1, 15)]  // Friday → next Monday
    public void TestNextWeekday(int y, int m, int d, DayOfWeek day, int ey, int em, int ed) =>
        Assert.Equal(new DateTime(ey, em, ed), new DateTime(y, m, d).NextWeekday(day));

    // ToUnixTimestamp

    [Fact]
    public void ToUnixTimestamp_Epoch_IsZero() =>
        Assert.Equal(0L, DateTime.UnixEpoch.ToUnixTimestamp());

    [Fact]
    public void ToUnixTimestamp_KnownUtcDate()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(1704067200L, dt.ToUnixTimestamp());
    }

    [Fact]
    public void ToUnixTimestamp_LocalConvertedToUtc()
    {
        var utc = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var local = utc.ToLocalTime();
        Assert.Equal(utc.ToUnixTimestamp(), local.ToUnixTimestamp());
    }

    [Fact]
    public void ToUnixTimestamp_UnspecifiedTreatedAsUtc()
    {
        var unspecified = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
        var utc = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(utc.ToUnixTimestamp(), unspecified.ToUnixTimestamp());
    }

    [Fact]
    public void ToUnixTimestamp_ThrowOnUnspecified_Throws() =>
        Assert.Throws<InvalidOperationException>(() =>
            new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)
                .ToUnixTimestamp(throwOnUnspecified: true));

    // ToUnixTimestampMs

    [Fact]
    public void ToUnixTimestampMs_Epoch_IsZero() =>
        Assert.Equal(0L, DateTime.UnixEpoch.ToUnixTimestampMs());

    [Fact]
    public void ToUnixTimestampMs_IsSecondsTimesThousand()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(dt.ToUnixTimestamp() * 1000L, dt.ToUnixTimestampMs());
    }

    [Fact]
    public void ToUnixTimestampMs_CapturesMilliseconds()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc);
        Assert.Equal(dt.ToUnixTimestamp() * 1000L + 500L, dt.ToUnixTimestampMs());
    }

    // FromUnixTimestamp

    [Fact]
    public void FromUnixTimestamp_Zero_IsEpoch() =>
        Assert.Equal(DateTime.UnixEpoch, 0L.FromUnixTimestamp());

    [Fact]
    public void FromUnixTimestamp_KnownValue() =>
        Assert.Equal(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), 1704067200L.FromUnixTimestamp());

    [Fact]
    public void FromUnixTimestamp_ReturnsUtc() =>
        Assert.Equal(DateTimeKind.Utc, 0L.FromUnixTimestamp().Kind);

    [Fact]
    public void FromUnixTimestamp_RoundTrip()
    {
        var dt = new DateTime(2024, 6, 15, 12, 30, 0, DateTimeKind.Utc);
        Assert.Equal(dt, dt.ToUnixTimestamp().FromUnixTimestamp());
    }

    // FromoUnixTimestampMs

    [Fact]
    public void FromUnixTimestampMs_Zero_IsEpoch() =>
        Assert.Equal(DateTime.UnixEpoch, 0L.FromoUnixTimestampMs());

    [Fact]
    public void FromUnixTimestampMs_ReturnsUtc() =>
        Assert.Equal(DateTimeKind.Utc, 0L.FromoUnixTimestampMs().Kind);

    [Fact]
    public void FromUnixTimestampMs_RoundTrip()
    {
        var dt = new DateTime(2024, 6, 15, 12, 30, 0, 500, DateTimeKind.Utc);
        Assert.Equal(dt, dt.ToUnixTimestampMs().FromoUnixTimestampMs());
    }
}
