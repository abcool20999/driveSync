using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using driveSync.Models;



namespace ridesnShare.Controllers
{
    public class TripDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Retrieves a list of trips from the database.
        /// </summary>
        /// <returns>
        /// An IEnumerable of TripDTO objects representing the list of trips.
        /// </returns>
        /// <example>
        /// GET: api/TripData/ListTrips
        /// </example>
        [HttpGet]
        [Route("api/RideData/ListRides")]
        public IEnumerable<RideDTO> Rides()
        {
            List<Ride> Rides = db.Rides.ToList();
            List<RideDTO> RideDTOs = new List<RideDTO>();

            Rides.ForEach(r => RideDTOs.Add(new RideDTO()
            {
                RideId = r.RideId,
                StartLocation = r.startLocation,
                EndLocation = r.endLocation,
                Price = r.price,
                Time = r.Time,
                DayOftheweek = r.dayOftheweek
            }));

            return RideDTOs;

        }
        /// <summary>
        /// Adds a new ride to the database.
        /// </summary>
        /// <param name="ride">The ride object containing information about the new ride.</param>
        /// <returns>
        /// An IHttpActionResult indicating the result of the addition operation.
        /// </returns>
        /// <example>
        /// POST: api/RideData/AddRide/5
        /// </example>
        [ResponseType(typeof(Ride))]
        [HttpPost]
        public IHttpActionResult AddRide(Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the driver associated with the ride (assuming RideId is actually DriverId)
            Driver driver = db.Drivers.Find(ride.RideId);

            if (driver == null)
            {
                Debug.WriteLine("Driver doesn't exist");
                return BadRequest("Driver not found");
            }

            // Create an instance of Inventory
            Inventory inventory = new Inventory
            {
                ItemName = ride.ItemName,
                Weight = ride.Weight,
                Size = ride.Size,
                Quantity = ride.Quantity
            };

            // Add the inventory to the database
            db.Inventories.Add(inventory);
            db.SaveChanges();

            // Create an instance of Ride
            Ride newRide = new Ride
            {
                StartLocation = ride.StartLocation,
                EndLocation = ride.EndLocation,
                Price = ride.Price,
                Time = ride.Time,
                DayOfTheWeek = ride.DayOfTheWeek,
            };

            // Add the ride to the database
            db.Rides.Add(newRide);
            db.SaveChanges();

            return Ok("Ride added");
        }

    }
}
