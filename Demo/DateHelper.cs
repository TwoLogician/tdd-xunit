using System;
using System.Linq;

namespace Demo
{
    public class DateHelper
    {
        public string GroupDays(int[] days)
        {
            var daysString = string.Empty;
            var prev = 0;
            if (days != null)
            {
                if (days.Length == 1)
                {
                    daysString = days[0].ToString();
                }
                else
                {
                    var invalidDays = days.Where(x => x < 1 || x > 31).ToList();
                    if (invalidDays.Count == 0)
                    {
                        Array.Sort(days);
                        days = days.Distinct().ToArray();
                        for (var i = 0; i < days.Length; i++)
                        {
                            var day = days[i];
                            var next = prev + 1;
                            if (i == 0)
                            {
                                daysString += day.ToString();
                            }
                            else if (next != day)
                            {
                                daysString = daysString.Replace(" and ", ", ");
                                if (prev != days[0] && !daysString.Contains($", {prev}"))
                                {
                                    daysString += $"-{prev} and {day}";
                                }
                                else
                                {
                                    daysString += $" and {day}";
                                }
                            }
                            else if (next == day && day == days[days.Length - 1])
                            {
                                daysString += $"-{day}";
                            }
                            prev = day;
                        }
                    }
                    else
                    {
                        throw new InvalidDayException(invalidDays[0]);
                    }
                }
            }
            return daysString;
        }

        public class InvalidDayException : Exception
        {
            public int FirstInvalidDay { get; set; }
            public InvalidDayException(int firstInvalidDay)
            {
                FirstInvalidDay = firstInvalidDay;
            }

            public InvalidDayException(string message) : base(message)
            {
            }

            public InvalidDayException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}
