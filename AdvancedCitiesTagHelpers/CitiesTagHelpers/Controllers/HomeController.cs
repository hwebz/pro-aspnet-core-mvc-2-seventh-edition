﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesTagHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitiesTagHelpers.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo) => repository = repo;

        public ViewResult Index() => View(repository.Cities);

        public ViewResult Create()
        {
            ViewBag.Countries = new SelectList(repository.Cities.Select(c => c.Country).Distinct());
            return View();
        }

        public ViewResult Edit()
        {
            ViewBag.Countries = new SelectList(repository.Cities.Select(c => c.Country).Distinct());
            return View("Create", repository.Cities.First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City city)
        {
            repository.AddCity(city);
            return RedirectToAction("Index");
        }
    }
}
