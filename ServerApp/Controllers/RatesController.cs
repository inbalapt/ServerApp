﻿using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    public class RatesController : Controller
    {
        private static List<Rate> rates = new List<Rate>();

        public RatesController()
        {
            if (rates.Count == 0)
            {
                rates.Add(new Rate() { Id = 1, Name = "Noa", Feedback = "good", Rating = 5 });
                rates.Add(new Rate() { Id = 2, Name = "Inbal", Feedback = " ho god", Rating = 4 });
                rates.Add(new Rate() { Id = 3, Name = "Amit", Feedback = "terrible", Rating = 3 });
            }

        }
            rates = new List<Rate>();
            rates.Add(new Rate() { Id = 1, Name = "Noa", Description = "good", Rating = 5 });
            rates.Add(new Rate() { Id = 2, Name = "Inbal", Description = " ho god", Rating = 4 });
            rates.Add(new Rate() { Id = 3, Name = "Amit", Description = "terrible", Rating = 3 });
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

        public IActionResult Edit(int id, string name, int rate, string feedback)
        {
            return View(rates);
        }
        public IActionResult Add()
        {
            return View(rates);
        }
        public IActionResult Delete()
        {
            return View(rates);
        }

    }
}
