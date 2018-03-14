using System;
using System.Globalization;

namespace CoreCodeCamper.Core.Model
{
    public class TimeSlot // ValueObject
    {
        private static string[] dayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
        private static string GetAbbreviatedDayName(DateTime date) => dayNames[(int)date.DayOfWeek];

        public static int DefaultDurationMinutes = 45;

        public DateTime Start { get; }
        public int DurationMinutes { get; }
        public override string ToString() => $"{GetAbbreviatedDayName(Start)} {Start.ToString("t")}";

        public TimeSlot(DateTime start, int? durationMinutes = null) => (Start, DurationMinutes) = (start, durationMinutes ?? DefaultDurationMinutes);        
    }
}
