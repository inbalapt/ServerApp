﻿using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    public class MyRatesController : Controller
    {
        private static List<Rate> rates = new List<Rate>();

        public MyRatesController()
        {
            if (rates.Count == 0)
            {
                rates.Add(new Rate() { Id = 1, Name = "Noa", Description = "good", Rating = 5 });
                rates.Add(new Rate() { Id = 2, Name = "Inbal", Description = " ho god", Rating = 4 });
                rates.Add(new Rate() { Id = 3, Name = "Amit", Description = "terrible", Rating = 3 });
            }

        }
           
           
        
        public IActionResult Index()
        {
            return View(rates);
        }

        public IActionResult Details(int id)
        {
            Rate rate = rates.Find(x => x.Id == id);
            return View(rate);
        }
       
        public IActionResult Edit(int id)
        {
            Rate rate = rates.Find(rate => rate.Id == id);
            return View(rate);
        }

        [HttpPost]
        public IActionResult Edit(int id, string name, int rating, string description)
        {
            Rate rate = rates.Find(rate => rate.Id == id);
            rate.Name = name;
            rate.Rating = rating;
            rate.Description = description;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Add()
        {
            return View(rates);
        }
        [HttpPost]
        public IActionResult Add(string name,int rating, string description)
        {
            int id = rates.Max(x => x.Id)+1;
            rates.Add(new Rate() { Rating = rating, Description = description, Name = name, Id = id});
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Rate rate = rates.Find(rate => rate.Id == id);
            return View(rate);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteRate(int id)
        {
            Rate rate = rates.Find(rate => rate.Id == id);
            if (rate != null)
            {
                rates.Remove(rate); 
            }
            return RedirectToAction(nameof(Index));
        }
    }
}