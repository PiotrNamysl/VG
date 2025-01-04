using VirtualGardener.Shared.Models.Enums;

namespace VirtualGardener.Client.Utilities;

public static class Helpers
{
    public static int GetDaysByFrequency(Frequency frequency)
    {
        return frequency switch
        {
            Frequency.Daily => 1,
            Frequency.EveryOtherDay => 2,
            Frequency.OnceAWeek => 7,
            Frequency.TwiceAMonth => 14,
            Frequency.Monthly => 30,
            _ => throw new ArgumentOutOfRangeException(nameof(frequency), frequency, null)
        };
    }
}