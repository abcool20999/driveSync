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

namespace driveSync.Controllers
{
    public class DriverDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Retrieves a list of passengers from the database.
        /// </summary>
        /// <returns>
        /// An IEnumerable of PassengerDTO objects representing the list of passengers.
        /// </returns>
        /// <example>
        /// GET: api/PassengerData/ListPassengers
        /// </example>
        [HttpGet]
        [Route("api/PassengerData/ListPassengers")]
        public IEnumerable<PassengerDTO> Passengers()
        {
            List<Passenger> Passengers = db.Passengers.ToList();
            List<PassengerDTO> PassengerDTOs = new List<PassengerDTO>();

            Passengers.ForEach(p => PassengerDTOs.Add(new PassengerDTO()
            {
                PassengerId = p.PassengerId,
                firstName = p.firstName,
                lastName = p.lastName,
                username = p.username,
                email = p.email
            }));

            return PassengerDTOs;

        }
        /// <summary>
        /// Validates a passenger's credentials by checking if the user exists in the database and if the provided password matches.
        /// </summary>
        /// <param name="passenger">The Passenger object containing username and password for validation.</param>
        /// <returns>
        /// IHttpActionResult representing the result of the validation process:
        ///   - If the user exists and the password matches, returns Ok with the validated Passenger object.
        ///   - If the user exists but the password does not match, returns BadRequest with a message indicating incorrect password.
        ///   - If the user does not exist, returns BadRequest with a message indicating that the user was not found.
        /// </returns>
        /// <example></example>

        [HttpPost]
        [Route("api/DriverData/Validate")]
        public IHttpActionResult Validate(Driver driver)
        {
            Debug.WriteLine(driver.username);
            Debug.WriteLine(driver.password);

            // returns true or false if user exist or not

            bool isUserExist = (db.Drivers.Where(p => p.username == driver.username)
                                   .FirstOrDefault() == null) ? false : true;

            // Debug.WriteLine(isUserExist + "isUserExists");

            if (isUserExist)
            {
                // validate user
                Driver validatedDriver = db.Drivers.Where(d => d.username == d.username)
                                               .Where(d => d.password == d.password).FirstOrDefault();
                if (validatedDriver != null)
                {
                    return Ok(validatedDriver);

                }

                return BadRequest("Wrong password");

            }
            else
            {
                // return with a message
                return BadRequest("User not found");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassengerExists(int id)
        {
            return db.Passengers.Count(e => e.PassengerId == id) > 0;
        }
    }
}

