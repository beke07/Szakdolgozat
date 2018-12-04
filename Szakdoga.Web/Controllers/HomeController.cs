using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Szakdoga.BLL.ServiceInterfaces;
using Szakdoga.DAL;
using Szakdoga.Model;

namespace Szakdoga.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() {}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
