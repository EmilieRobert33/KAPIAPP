using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KAPIAPP_App.Models;
using KAPIAPP.Services.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace KAPIAPP_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger,
                                ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
      
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boutique>>> GetBoutiques()
        {
            var boutiques = await _context.Boutiques.ToListAsync();
            return View(boutiques);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
