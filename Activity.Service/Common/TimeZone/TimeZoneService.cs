using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WaitLess.Service.Common.TimeZone
{
    public static class TimeZoneService
    {
        public static DateTime ConvertDateTimeToUtc(this DateTime aptStartDatetime,string timeZone)
        {
            DateTime returnConvertedDatetime = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(timeZone))
            {
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeToUtc(aptDateTime, UserTimeZone);
            }
            else
            {
                bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                timeZone = isWindows ? "Coordinated Universal Time" : "Africa/Abidjan";
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeToUtc(aptDateTime, UserTimeZone);
            }
            return returnConvertedDatetime;
        }

        public static DateTime ConvertDateTimeFromUtc(this DateTime aptStartDatetime, string timeZone)
        {
            DateTime returnConvertedDatetime = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(timeZone))
            {
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeFromUtc(aptDateTime, UserTimeZone);
            }
            else
            {
                bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                timeZone = isWindows ? "Coordinated Universal Time" : "Africa/Abidjan";
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeFromUtc(aptDateTime, UserTimeZone);
            }
            return returnConvertedDatetime;
        }
        public static DateTime ConvertDateTimeToUtc(this DateTime aptStartDatetime,TimeSpan timeSpan, string timeZone)
        {
            DateTime returnConvertedDatetime = DateTime.UtcNow;
            aptStartDatetime = aptStartDatetime.AddHours(aptStartDatetime.Hour * -1).AddMinutes(aptStartDatetime.Minute * -1).AddSeconds(aptStartDatetime.Second * -1);
            aptStartDatetime = aptStartDatetime.AddHours(timeSpan.Hours).AddMinutes(timeSpan.Minutes).AddSeconds(timeSpan.Seconds);
            if (!string.IsNullOrEmpty(timeZone))
            {
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeToUtc(aptDateTime, UserTimeZone);
            }
            else
            {
                bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                timeZone = isWindows ? "Coordinated Universal Time" : "Africa/Abidjan";
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeToUtc(aptDateTime, UserTimeZone);
            }
            return returnConvertedDatetime;
        }

        public static DateTime ConvertDateTimeFromUtc(this DateTime aptStartDatetime, TimeSpan timeSpan, string timeZone)
        {
            DateTime returnConvertedDatetime = DateTime.UtcNow;
            aptStartDatetime = aptStartDatetime.AddHours(aptStartDatetime.Hour * -1).AddMinutes(aptStartDatetime.Minute * -1).AddSeconds(aptStartDatetime.Second * -1);
            aptStartDatetime = aptStartDatetime.AddHours(timeSpan.Hours).AddMinutes(timeSpan.Minutes).AddSeconds(timeSpan.Seconds);
            if (!string.IsNullOrEmpty(timeZone))
            {
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeFromUtc(aptDateTime, UserTimeZone);
            }
            else
            {
                bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                timeZone = isWindows ? "Coordinated Universal Time" : "Africa/Abidjan";
                TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime aptDateTime = DateTime.SpecifyKind(aptStartDatetime, DateTimeKind.Unspecified);
                returnConvertedDatetime = TimeZoneInfo.ConvertTimeFromUtc(aptDateTime, UserTimeZone);
            }
            return returnConvertedDatetime;
        }
        public static TimeSpan ConvertTimeSpanToUtc(this TimeSpan tsUtc, string zone)
        {
            DateTime dtUtc = DateTime.Parse(tsUtc.ToString());
            dtUtc = TimeZoneService.ConvertDateTimeToUtc(dtUtc, zone);
            TimeSpan ts = dtUtc.TimeOfDay;
            return ts;
        }
        public static TimeSpan ConvertTimeSpanFromUtc(this TimeSpan tsUtc, string zone)
        {
            DateTime dtUtc = DateTime.Parse(tsUtc.ToString());
            dtUtc = TimeZoneService.ConvertDateTimeFromUtc(dtUtc, zone);
            TimeSpan ts = dtUtc.TimeOfDay;
            return ts;
        }
    }
}
