using _2._28_Hw.Data;
using _2._28_Hw.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Diagnostics;

namespace _2._28_Hw.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString=@"Data Source=.\sqlexpress;Initial Catalog=NewDatabase; Integrated Security=true;";      
        public IActionResult Index()
        {
            var mgr = new PeopleManager(_connectionString);
            var vm = new PeopleViewModel
            {
                People = mgr.GetPeople()
            };
            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }
            return View(vm);

        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(List<People>people)
        {
            var mgr = new PeopleManager(_connectionString);
            people = people.FindAll(p => p.FirstName != null && p.LastName != null && p.Age != 0).ToList();
            mgr.AddMany(people);
            TempData["message"] = "Added successfully!";
            return Redirect("/home/index");
        }

    }
}