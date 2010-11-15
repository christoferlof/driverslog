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
            var builder = new StringBuilder();
            FormatHeader(builder);

            foreach (var trip in trips) {
                FormatLineItem(builder, trip);
            }
            return builder.ToString();
        }

        private static void FormatLineItem(StringBuilder builder, Trip trip) {
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
            builder.AppendLine(string.Join(Delimiter,line));
        }

        private static void FormatHeader(StringBuilder builder) {
            var headers = new[] { "Date", "Car", "To", "From", "Start", "Stop", "Distance", "Notes" };
            builder.AppendLine(string.Join(Delimiter, headers));
        }
    }
}