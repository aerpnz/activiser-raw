using System;
using System.Runtime.InteropServices;

namespace activiser.Library.Gps
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SystemTime
    {
        public readonly short year;
        public readonly short month;
        public readonly short dayOfWeek;
        public readonly short day;
        public readonly short hour;
        public readonly short minute;
        public readonly short second;
        public readonly short millisecond;

        //public SystemTime(short year, short month, short day, short hour, short minute, short second, short millisecond)
        //{
        //    this.year = year;
        //    this.month = month;
        //    this.day = day;
        //    this.hour = hour;
        //    this.minute = minute;
        //    this.second = second;
        //    this.millisecond = millisecond;
        //    DateTime dt = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Unspecified);
        //    this.dayOfWeek = (short)dt.DayOfWeek;
        //}

        //public SystemTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
        //{
        //    this.year = (short)year;
        //    this.month = (short)month;
        //    this.day = (short)day;
        //    this.hour = (short)hour;
        //    this.minute = (short)minute;
        //    this.second = (short)second;
        //    this.millisecond = (short)millisecond;
        //    DateTime dt = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Unspecified);
        //    this.dayOfWeek = (short)dt.DayOfWeek;
        //}

        public SystemTime(System.DateTime dt)
        {
            this.year = (short)dt.Year;
            this.month = (short)dt.Month;
            this.dayOfWeek = (short)dt.DayOfWeek;
            this.day = (short)dt.Day;
            this.hour = (short)dt.Hour;
            this.minute = (short)dt.Minute;
            this.second = (short)dt.Second;
            this.millisecond = (short)dt.Millisecond;
        }

        public System.DateTime ToDateTime()
        {
            return new System.DateTime(this.year, this.month, this.day, this.hour, this.minute, this.second, this.millisecond, DateTimeKind.Utc);
        }
    }
}