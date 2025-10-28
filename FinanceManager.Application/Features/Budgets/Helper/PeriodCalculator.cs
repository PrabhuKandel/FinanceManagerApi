

    using System.Globalization;
    using FinanceManager.Domain.Enums;

    namespace FinanceManager.Application.Features.Budgets.Helper
    {
        public static class PeriodCalculator
        {
            public static (DateTime PeriodStart, DateTime PeriodEnd) GetPeriodDates(PeriodType periodType, string selectedPeriod)
            {
                switch (periodType)
                {

                    case PeriodType.Daily:
                        // Format: "YYYY-MM-DD"
                        var date = DateTime.ParseExact(selectedPeriod, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        return (date, date);


                    case PeriodType.Weekly:
                        // Format: "YYYY-Www" (ISO week)
                        // Example: "2025-W42"
                        // selectedPeriod example: "2025-W42" (ISO week)
                        return GetIsoWeekStartEnd(selectedPeriod);

                    case PeriodType.Monthly:
                        // Format: "YYYY-MM"
                        var monthParts = selectedPeriod.Split('-');
                        int y = int.Parse(monthParts[0]);
                        int m = int.Parse(monthParts[1]);
                        var monthStart = new DateTime(y, m, 1);
                        var monthEnd = new DateTime(y, m, DateTime.DaysInMonth(y, m));
                        return (monthStart, monthEnd);


                    case PeriodType.Yearly:
                        // Format: "YYYY"
                        int yearOnly = int.Parse(selectedPeriod);
                        return (new DateTime(yearOnly, 1, 1), new DateTime(yearOnly, 12, 31));
                }

                     return (DateTime.MinValue, DateTime.MinValue);


        }

            private static (DateTime Start, DateTime End) GetIsoWeekStartEnd(string isoWeek)
            {
                // isoWeek example: "2025-W42"
                var parts = isoWeek.Split("-W");
                if (parts.Length != 2)
                    throw new ArgumentException("Invalid weekly period format. Expected YYYY-Www.");

                int year = int.Parse(parts[0]);
                int week = int.Parse(parts[1]);

                // ISO 8601: Week 1 = first week with Thursday in the new year
                var jan1 = new DateTime(year, 1, 1);
                int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
                var firstThursday = jan1.AddDays(daysOffset);
                var cal = CultureInfo.InvariantCulture.Calendar;
                int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                if (firstWeek <= 1)
                    week -= 1;

                var result = firstThursday.AddDays(week * 7);
                var startOfWeek = result.AddDays(-3); // Monday
                var endOfWeek = startOfWeek.AddDays(6); // Sunday

                return (startOfWeek, endOfWeek);
            }
        }
    }
