using System;

namespace ApplicationCore.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsEarlierThan(this DateTime first, DateTime second) 
            => DateTime.Compare(first, second) <= 0;
    }
}