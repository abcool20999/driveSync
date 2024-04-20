using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace driveSync
{
    public class AvailableRidesDTO
    {
        public int RideId { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastname { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public string Price { get; set; }
        public int SpotsLeft { get; set; }
        public int DriverAge { get; set; }
        public string CarType { get; set; }
        public DateTime Time { get; set; }
        public string WeekDay { get; set; }

        public string BagQuantity { get; set; }
        public string BagWeight { get; set; }
        public string BagSize { get; set; }

        public string LuggageQuantity { get; set; }
        public string LuggageWeight { get; set; }
        public string LuggageSize { get; set; }
    }
}