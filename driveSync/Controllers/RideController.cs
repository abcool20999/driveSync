using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using driveSync.Models;
using System.Web.Script.Serialization;
using System.Net.NetworkInformation;
using System.Data.Entity.Migrations.Model;

namespace driveSync.Controllers
{
    public class RideController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static RideController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44332/api/RideData/");
        }
        // GET: Ride/List
        public ActionResult List()
        {
            try
            {
                // Establish url connection endpoint
                string url = "ListRides";

                // Send request to API to retrieve list of rides
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Check if response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Parse JSON response into a list of RideDTO objects
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    IEnumerable<RideDTO> rides = jss.Deserialize<IEnumerable<RideDTO>>(responseData);

                    // Debug info
                    Debug.WriteLine("Number of trips received: " + rides.Count());

                    // Return the view with the list of rides
                    return View(rides);
                }
                else
                {
                    Debug.WriteLine("API request failed with status code: " + response.StatusCode);
                    // Handle unsuccessful response (e.g., return an error view)
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Handle any exceptions (e.g., return an error view)
                return View("Error");
            }
        }
    


// GET: Ride/Details/5
public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ride/Add
        public ActionResult Add()
        {
            return View("Add");
        }

        // POST: Trip/AddTrip
        [HttpPost]
        public ActionResult AddRide(Ride ride)
        {
            Debug.WriteLine("the inputted trip name is :");
            Debug.WriteLine(ride.price);
            //objective: add a new trip into our system using the API
            //curl -H "Content-Type:application/json" -d @trip.json  https://localhost:44354/api/RideData/AddRide
            string url = "AddTrip";

            //convert trip object into a json format to then send to our api
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(ride);

            Debug.WriteLine(jsonpayload);

            //send the json payload to the url through the use of our client
            //setup the postdata as HttpContent variable content
            HttpContent content = new StringContent(jsonpayload);

            //configure a header for our client to specify the content type of app for post 
            content.Headers.ContentType.MediaType = "application/json";

            //check if you can access information from our postasync request, get an httpresponse request and result of the request

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }
        }

        // GET: Ride/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ride/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ride/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ride/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
