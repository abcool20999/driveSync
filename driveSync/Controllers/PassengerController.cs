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
using Newtonsoft.Json;

namespace driveSync.Controllers
{
    public class PassengerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PassengerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44332/api/PassengerData/");
        }

        public ActionResult PassengerLoginSubmit(Passenger passenger)
        {
            Debug.WriteLine(passenger.username);
            Debug.WriteLine(passenger.password);

            string url = "Validate";
            string jsonpayload = jss.Serialize(passenger);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            try
            {
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                //if (response.IsSuccessStatusCode)
                {
                    Passenger resUser = response.Content.ReadAsAsync<Passenger>().Result;

                    //Session.Clear();
                    //Session["userId"] = resUser;
                    //var action = $"PassengerProfile";
                    //return RedirectToAction(action, "Passenger");

                    return RedirectToAction("PassengerProfile", "Passenger");

                }
                //else
                //{
                //    Debug.WriteLine("Unsuccessful login attempt.");
                //    return RedirectToAction("Index", "Home"); // Redirect to home page if login fails
                //}
            }
            catch (Exception ex)
            {
                // Log the exception details
                Debug.WriteLine("An error occurred during login: " + ex.Message);
                // Redirect to an error page or handle the error appropriately
                return RedirectToAction("Error", "Passenger");
            }
        }

        public ActionResult List()
        {
            try
            {
                // Establish url connection endpoint
                string url = "ListPassengers";

                // Send request to API to retrieve list of passengers
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Check if response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Parse JSON response into a list of PassengerDTO objects
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    IEnumerable<PassengerDTO> passengers = jss.Deserialize<IEnumerable<PassengerDTO>>(responseData);

                    // Debug info
                    Debug.WriteLine("Number of rides received: " + passengers.Count());

                    // Return the view with the list of rides
                    return View(passengers);
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

        public ActionResult PassengerProfile()
        {


            // Pass user object to the view
            return View();
        }
    

        // GET: Passenger/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Passenger/Add
        public ActionResult Add()
        {
              return View();     
           
        }

        // POST: Passenger/Create
        [HttpPost]

        public ActionResult AddPassenger(Passenger passenger)
        {
            Debug.WriteLine("the inputted passenger name is :");
            Debug.WriteLine(passenger.firstName);
            //objective: add a new passenger into our system using the API
            //curl -H "Content-Type:application/json" -d @trip.json  https://localhost:44354/api/PassengerData/AddPassendger

            string url = "AddPassenger";

            //convert passenger object into a json format to then send to our api
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(passenger);

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
           

        // GET: Passenger/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Passenger/Edit/5
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

        // GET: Passenger/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Passenger/Login
        [HttpGet]
        public ActionResult PassengerLogin()
        {
            
           return View("PassengerLogin");
          
        }

  
    }
}
