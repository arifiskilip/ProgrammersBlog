using System;

namespace ProgrammersBlog.Services.Extensions
{
    public static class DatetimeExtension
    {
        public static string FullDateAndTimeStringWithUnderscore(this DateTime dateTime)
        {
            return
               $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";
        }
    }
}
