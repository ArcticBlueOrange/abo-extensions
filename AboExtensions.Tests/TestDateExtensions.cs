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
}
