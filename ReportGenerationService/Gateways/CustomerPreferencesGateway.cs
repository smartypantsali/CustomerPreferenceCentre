using ReportGenerationService.Enums;
using ReportGenerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerationService.Gateways
{
    public class CustomerPreferencesGateway : ICustomerPreferencesGateway
    {
        private readonly byte _numberOfDays;

        public CustomerPreferencesGateway()
        {
            // TODO: Can be configured as a config variable and mocked for unit tests
            _numberOfDays = 90;
        }

        /// <summary>
        /// Processes request body and produces "report"
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Dictionary<string, string[]> GenerateCustomerMarketInfoReport(CustomerPreferencesForm form)
        {
            var customerPreferences = new Tuple<DateTime, Stack<string>>[_numberOfDays];

            for (int i = 0; i < _numberOfDays; i++)
            {
                customerPreferences[i] = Tuple.Create(DateTime.Now.AddDays(i), new Stack<string>());
            }

            foreach (var preference in form.CustomerPreferences)
            {
                // Finds preferenceType and adds to number of days applicable too
                switch (preference.Type)
                {
                    case DayPreferenceType.Never:
                        break;
                    case DayPreferenceType.EveryDay:
                        foreach (var day in customerPreferences)
                        {
                            day.Item2.Push(preference.Customer);
                        }
                        break;
                    case DayPreferenceType.DaysOfWeek:
                        foreach (var dayOfTheWeek in preference.SpecificDaysOfWeek)
                        {
                            var listDays = customerPreferences.Where(t => t.Item1.DayOfWeek == (System.DayOfWeek)dayOfTheWeek);

                            foreach (var day in listDays)
                            {
                                day.Item2.Push(preference.Customer);
                            }
                        }
                        break;
                    case DayPreferenceType.SpecificDayOfMonth:
                        var specificDates = customerPreferences.Where(t => t.Item1.Day == preference.SpecificMonthDay);
                        foreach (var date in specificDates)
                        {
                            date.Item2.Push(preference.Customer);
                        }
                        break;
                }
            }

            var customerPreferencesFormatted = new Dictionary<string, string[]>();

            foreach (var tuple in customerPreferences)
            {
                customerPreferencesFormatted[tuple.Item1.ToString("ddd dd-MMM-yyyy")] = tuple.Item2.ToArray();
            }

            return customerPreferencesFormatted;
        }
    }
}
