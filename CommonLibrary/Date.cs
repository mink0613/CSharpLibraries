using System;

namespace CommonLibrary
{
    public class Date
    {
        /// <summary>
        /// Get sunday of selected week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Sunday of selected week</returns>
        public static DateTime GetSundayOfWeek(DateTime date)
        {
            if (date.DayOfWeek > DayOfWeek.Sunday)
            {
                return GetSundayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        /// <summary>
        /// Get monday of selected week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Monday of selected week</returns>
        public static DateTime GetMondayOfWeek(DateTime date)
        {
            if (date.DayOfWeek < DayOfWeek.Monday)
            {
                return GetMondayOfWeek(date.AddDays(1));
            }
            else if (date.DayOfWeek > DayOfWeek.Monday)
            {
                return GetMondayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        /// <summary>
        /// Get tuesday of selected week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Tuesday of selected week</returns>
        public static DateTime GetTuesdayOfWeek(DateTime date)
        {
            if (date.DayOfWeek < DayOfWeek.Tuesday)
            {
                return GetTuesdayOfWeek(date.AddDays(1));
            }
            else if (date.DayOfWeek > DayOfWeek.Tuesday)
            {
                return GetTuesdayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        /// <summary>
        /// Get wednesday of selected week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Wednesday of selected week</returns>
        public static DateTime GetWednesdayOfWeek(DateTime date)
        {
            if (date.DayOfWeek < DayOfWeek.Wednesday)
            {
                return GetWednesdayOfWeek(date.AddDays(1));
            }
            else if (date.DayOfWeek > DayOfWeek.Wednesday)
            {
                return GetWednesdayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        /// <summary>
        /// Get thursday of selected week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Thursday of selected week</returns>
        public static DateTime GetThursdayOfWeek(DateTime date)
        {
            if (date.DayOfWeek < DayOfWeek.Thursday)
            {
                return GetThursdayOfWeek(date.AddDays(1));
            }
            else if (date.DayOfWeek > DayOfWeek.Thursday)
            {
                return GetThursdayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        /// <summary>
        /// Get friday of selected week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Friday of selected week</returns>
        public static DateTime GetFridayOfWeek(DateTime date)
        {
            if (date.DayOfWeek < DayOfWeek.Friday)
            {
                return GetFridayOfWeek(date.AddDays(1));
            }
            else if (date.DayOfWeek > DayOfWeek.Friday)
            {
                return GetFridayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        /// <summary>
        /// Get saturday of selected week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Saturday of selected week</returns>
        public static DateTime GetSaturdayOfWeek(DateTime date)
        {
            if (date.DayOfWeek < DayOfWeek.Saturday)
            {
                return GetSaturdayOfWeek(date.AddDays(1));
            }

            return date;
        }

        /// <summary>
        /// Get last week of selected date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Last week of selected date</returns>
        public static DateTime GetLastWeek(DateTime date)
        {
            return date.AddDays(-7);
        }

        /// <summary>
        /// Get last month of selected date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Last month of selected date</returns>
        public static DateTime GetLastMonth(DateTime date)
        {
            return date.AddMonths(-1);
        }

        /// <summary>
        /// Get last year of selected date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Last year of selected date</returns>
        public static DateTime GetLastYear(DateTime date)
        {
            return date.AddYears(-1);
        }
    }
}
