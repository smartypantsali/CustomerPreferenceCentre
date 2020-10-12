using Framework.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportGenerationService.Api.v1.Models
{
    public class CustomerPreferenceReport : BaseReport<CustomerPreferenceReport, Customer>
    {
        /// <summary>
        /// Processes Customer preference to produce report
        /// </summary>
        /// <returns></returns>
        public override CustomerPreferenceReport GenerateReport(Customer customer)
        {
            var subscriptionDays = getSubscriptionDays();

            // Finds preferenceType and adds to number of days applicable to
            switch (customer.CustomerPreference.Type)
            {
                case DayPreferenceType.Never:
                    break;
                case DayPreferenceType.EveryDay:
                    foreach (var day in subscriptionDays)
                    {
                        day.Item2.Push(customer.Name);
                    }
                    break;
                case DayPreferenceType.DaysOfWeek:
                    foreach (var dayOfTheWeek in customer.CustomerPreference.SpecificDaysOfWeek)
                    {
                        var listDays = subscriptionDays.Where(t => t.Item1.DayOfWeek == (System.DayOfWeek)dayOfTheWeek);

                        foreach (var day in listDays)
                        {
                            day.Item2.Push(customer.Name);
                        }
                    }
                    break;
                case DayPreferenceType.SpecificDayOfMonth:
                    var specificDates = subscriptionDays.Where(t => t.Item1.Day == customer.CustomerPreference.SpecificMonthDay);
                    foreach (var date in specificDates)
                    {
                        date.Item2.Push(customer.Name);
                    }
                    break;
            }

            if (Report == null)
            {
                Report = new Dictionary<string, Stack<string>>();
            }

            foreach (var tuple in subscriptionDays)
            {
                var key = tuple.Item1.ToString("ddd dd-MMM-yyyy");
                if (!Report.ContainsKey(key))
                {
                    Report[key] = tuple.Item2;
                }
                else
                {
                    if (Report[key].Count == 0)
                    {
                        Report[key] = tuple.Item2;
                    }
                    else
                    {
                        foreach (var @string in tuple.Item2)
                        {
                            if (!Report[key].Contains(@string))
                            {
                                Report[key].Push(@string);
                            }                            
                        }
                    }
                    
                }
            }

            return this;
        }

        /// <summary>
        /// Get days to subscribe to
        /// </summary>
        /// <returns></returns>
        private Tuple<DateTime, Stack<string>>[] getSubscriptionDays()
        {
            var subscriptionDays = new Tuple<DateTime, Stack<string>>[NumberOfDays];

            for (int i = 0; i < NumberOfDays; i++)
            {
                subscriptionDays[i] = Tuple.Create(DateTime.Now.AddDays(i), new Stack<string>());
            }

            return subscriptionDays;
        }
    }
}
