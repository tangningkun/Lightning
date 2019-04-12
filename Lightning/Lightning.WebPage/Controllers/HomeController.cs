using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lightning.WebPage.Models;
using Lightning.WebPage.Application;
using Lightning.Application.Users;
using Microsoft.Extensions.Configuration;

namespace Lightning.WebPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly IConfiguration _configuration;
        public HomeController(IUserAppService userAppService, IConfiguration configuration)
        {
            _userAppService = userAppService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var name = _configuration["Appsettings:name"].ToString();
            return View();
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
