using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyNetCoreMVC.Models;
using ViewModels;

namespace MyNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [ViewData]
        public string Title { get; set; }

        public IActionResult Index()
        {
            Title = "关于我们";
            ViewData["Description"] = "页面用于描述应用的相关信息";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "contact page.";

            var viewModel = new Person()
            {
                Name = "张三",
                Province = "XX省",
                City = "XX市",
                Address = "XX区XX街道",
                Phone = "13XXXXXXXXX"
            };

            return View(viewModel);
        }
        public IActionResult DemoAction()
        {
            ViewData["Greeting"] = "Hello";
            ViewData["Person"] = new Person()
            {
                Name = "张三",
                Province = "XX省",
                City = "XX市",
                Address = "XX区XX街道",
                Phone = "13XXXXXXXXX"
            };

            ViewBag.Greeting = "Hello";
            ViewBag.Person = new Person()
            {
                Name = "张三",
                Province = "XX省",
                City = "XX市",
                Address = "XX区XX街道",
                Phone = "13XXXXXXXXX"
            };

            return View();
        }

        public IActionResult OnGetPartial() =>
            new PartialViewResult
            {
                ViewName = "_AuthorPartialRP",
                ViewData = ViewData,
            };

        public IActionResult About()
        {
            var url = Url.Action("AddUser", "Users", new { Area = "Zebra" });
            return Content($"URL: {url}");
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
