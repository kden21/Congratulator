using Congratulator.Data.Service.Interfaces;
using Congratulator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Congratulator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IPersonService _personService;

        public HomeController(ILogger<HomeController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public async Task<IActionResult> Index(string date)
        {
            DateTime dateTime;
            try
            {
                dateTime = DateTime.Parse(date);
            }
            catch (Exception ex)
            {      
                dateTime = DateTime.Today;
            }
            
            
            var response = await _personService.GetPersonsByDate(dateTime);
            if (response.StatusCode == Data.Models.Enums.StatusCode.OK)
                return View(response.Data.ToList());
            return RedirectToAction("Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}