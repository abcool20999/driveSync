﻿using System;
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
    public class PassengerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PassengerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44332/api/PassengerData");
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

        public ActionResult PassengerProfile()
        {


            // Pass user object to the view
            return View();
        }
        //GET: Passenger
        public ActionResult Index()
        {
            return View();
        }

        // GET: Passenger/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Passenger/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Passenger/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
