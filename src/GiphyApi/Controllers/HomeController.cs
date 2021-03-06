﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GiphyApi.Models;
using System.Xml.Linq;

namespace GiphyApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetGif()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetGif(string search, string limit)
        {
            Gif.listOfGifs = Gif.GetGif(search, limit);
            return RedirectToAction("ViewGifs");
        }
        public IActionResult ViewGifs()
        {
            return View(Gif.listOfGifs);
        }

        public IActionResult ShowOptions()
        {
            return View();
        }
    }
}
