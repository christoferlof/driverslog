namespace Driverslog.Models
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Victoria.Data;

    [DataContract]
    public class Location : ActiveRecord<Location>
    {
        [DataMember]
        public string Name { get; set; }

        public static void AddIfNew(Trip trip)
        {
            if (!Exists(trip.From))
            {
                Add(new Location { Name = trip.From });
            }

            if (!Exists(trip.To))
            {
                Add(new Location { Name = trip.To});
            }
        }

        public static bool HasRecords
        {
            get
            {
                return All != null && All.Count > 0;
            }
        }

        private static bool Exists(string location)
        {
            return String.IsNullOrWhiteSpace(location) || 
                All.Any(x => x.Name.Equals(location, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}