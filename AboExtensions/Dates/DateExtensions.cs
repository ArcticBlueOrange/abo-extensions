using System.Globalization;

namespace AboExtensions.Dates;

public static class DateExtensions
{
    public static bool IsWeekend(this DateTime d) =>
        d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday;
    public static bool IsWeekday(this DateTime d) => !d.IsWeekend();
    public static DateTime StartOfDay(this DateTime d) => d.Date;
    public static DateTime EndOfDay(this DateTime d) => d.Date.AddDays(1).AddTicks(-1);
    public static DateTime StartOfWeek(this DateTime d, DayOfWeek startDay = DayOfWeek.Monday)
    {
        int diff = ((int)d.DayOfWeek - (int)startDay + 7) % 7;
        return d.Date.AddDays(-diff);
    }
    public static int Age(this DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
    public static bool IsInThePast(this DateTime d) => d < DateTime.Now;
    public static bool IsInTheFuture(this DateTime d) => d > DateTime.Now;
    public static int Quarter(this DateTime d) => (d.Month - 1) / 3 + 1;
    public static DateTime AddWorkdays(this DateTime d, int days)
    {
        int step = days < 0 ? -1 : 1;
        int remaining = Math.Abs(days);
        while (remaining > 0)
        {
            d = d.AddDays(step);
            if (d.IsWeekday()) remaining--;
        }
        return d;
    }
    public static DateTime NextWeekday(this DateTime d, DayOfWeek day)
    {
        int diff = ((int)day - (int)d.DayOfWeek + 7) % 7;
        return d.Date.AddDays(diff == 0 ? 7 : diff);
    }

    public static bool IsToday(this DateTime d) => d.Date == DateTime.Today;

    public static bool IsYesterday(this DateTime d) => d.Date == Yesterday;

    public static bool IsTomorrow(this DateTime d) => d.Date == Tomorrow;

    public static bool IsSameDay(this DateTime d, DateTime o) => d.Date == o.Date;

    /// <summary>
    ///   Descrizione: restituisce il tempo trascorso da d a DateTime.Now.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static TimeSpan Elapsed(this DateTime d) => DateTime.Now - d.Date;

    public static long ToUnixTimestamp(this DateTime d, bool throwOnUnspecified = false)
    {
        if (d.Kind == DateTimeKind.Local)
            d = d.ToUniversalTime();
        if (d.Kind == DateTimeKind.Unspecified && throwOnUnspecified)
            throw new InvalidOperationException("Unspecified DateTime Kind");

        return Convert.ToInt64((d - DateTime.UnixEpoch).TotalSeconds);
    }
    public static long ToUnixTimestampMs(this DateTime d, bool throwOnUnspecified = false)
    {
        if (d.Kind == DateTimeKind.Local)
            d = d.ToUniversalTime();
        if (d.Kind != DateTimeKind.Unspecified && throwOnUnspecified)
            throw new InvalidOperationException("Unspecified DateTime Kind");

        return Convert.ToInt64((d - DateTime.UnixEpoch).TotalMilliseconds);
    }

    public static DateTime FromUnixTimestamp(this long d)
        => DateTime.UnixEpoch + TimeSpan.FromSeconds(d);
    public static DateTime FromoUnixTimestampMs(this long d)
        => DateTime.UnixEpoch + TimeSpan.FromMilliseconds(d);

    public static DateTime Yesterday => DateTime.Today.AddDays(-1);

    public static DateTime Tomorrow => DateTime.Today.AddDays(1);

    /// <summary>
    /// Returns the week number (ISO 8601)
    /// ISO Weeks starts on Monday; W1 is the one containing a Thursday
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static int WeekNumber(this DateTime d) => ISOWeek.GetWeekOfYear(d);
}
