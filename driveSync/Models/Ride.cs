using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using driveSync.Models;

namespace driveSync.Models
{
    public class Ride
    {

        //what describes a ride
        [Key]
        public int RideId { get; set; }
        public string startLocation { get; set; }
        public string endLocation { get; set; }
        public string price { get; set; }
        public DateTime Time { get; set; }
        public string dayOftheweek { get; set; }

        //a ride has a driver ID
        //a driver has many rides
        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }


       
    }

    public class RideDTO
    {
        public int RideId { get; set; }
        public string startLocation { get; set; }
        public string endLocation { get; set; }
        public string price { get; set; }

        public int DriverId { get; set; }
        public DateTime Time { get; set; }

        public string dayOftheweek { get; set; }

    }
}