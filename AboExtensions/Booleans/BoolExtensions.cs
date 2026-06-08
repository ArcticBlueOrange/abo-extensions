namespace AboExtensions.Booleans;

public static class BoolExtensions
{
    /// <summary>
    /// bool => !bool
    /// </summary>
    /// <param name="b"></param>
    /// <returns>bool</returns>
    public static bool Toggle(this bool b) => !b;
}
