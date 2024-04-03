﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedulist.App.Services;
using Schedulist.App.Services.Interfaces;
using Schedulist.App.ViewModels;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
namespace Schedulist.App.Controllers
{
    public class StatisticsController : Controller
    {
        public readonly SchedulistDbContext _context;

        public StatisticsController(SchedulistDbContext context)
        {
            _context = context;
        }
      
        public ActionResult Index()
        {
            var workModes = _context.WorkModes
                .GroupBy(wm => wm.Name)
                .Select(group => new { Name = group.Key, Count = group.Count() })
                .ToList();

            var labels = workModes.Select(wm => wm.Name).ToArray();
            var data = workModes.Select(wm => wm.Count).ToArray();

            ViewBag.Labels = labels;
            ViewBag.Data = data;

            return View();
        }
        public IActionResult DataFromDatabase()
        {
       
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Privacy()
        {
            return View(); 
        }
    }
}
