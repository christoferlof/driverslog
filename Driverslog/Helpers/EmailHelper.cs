using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Driverslog.Models;

namespace Driverslog.Helpers {
    public class EmailHelper {

        private const string Delimiter = ",";

        public static string Format(IEnumerable<Trip> trips) {
            return FormatInternal(trips, FormatTripHeader, FormatTripLineItem);
        }

        public static string Format(IEnumerable<Expense> expenses) {
            return FormatInternal(expenses,FormatExpenseHeader, FormatExpenseLineItem);
        }

        private static string FormatInternal<T>(IEnumerable<T> items, Action<StringBuilder> headerFormatter, Action<StringBuilder,T> lineFormatter) {
            var builder = new StringBuilder();
            headerFormatter(builder);

            foreach (var item in items) {
                lineFormatter(builder, item);
            }
            return builder.ToString();
        }

        private static void FormatTripLineItem(StringBuilder builder, Trip trip) {
            var line = new[] {
                trip.Date.ToShortDateString(),
                trip.Car,
                trip.To,
                trip.From,
                trip.OdometerStart.ToString(CultureInfo.InvariantCulture),
                trip.OdometerStop.ToString(CultureInfo.InvariantCulture),
                trip.Distance,
                trip.Notes
            };
            Join(builder,line);
        }

        private static void FormatExpenseLineItem(StringBuilder builder, Expense expense) {
            var line = new[] {
                expense.Date.ToShortDateString(),
                expense.Car,
                expense.Title,
                expense.AmountWithCurrency,
                expense.Notes
            };
            Join(builder, line);
        }

        private static void FormatTripHeader(StringBuilder builder) {
            var headers = new[] { "Date", "Car", "To", "From", "Start", "Stop", "Distance", "Notes" };
            Join(builder,headers);
        }

        private static void FormatExpenseHeader(StringBuilder builder) {
            var headers = new[]{"Date", "Car", "Title", "Amount", "Notes"};
            Join(builder,headers);
        }

        private static void Join(StringBuilder builder, string[] line) {
            builder.AppendLine(string.Join(Delimiter, line));
        }
    }
}