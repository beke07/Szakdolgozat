using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RazorLight;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Szakdoga.BLL.Models;
using Szakdoga.BLL.ServiceInterfaces;
using Szakdoga.DAL;
using Szakdoga.Model;

namespace Szakdoga.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        private readonly IEmailSender emailSender;

        public LoginController(
            ILoginService loginService,
            IEmailSender emailSender
            )
        {
            this.emailSender = emailSender;
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserData userData)
        {
            User user = await loginService.Register(userData, Request.Form.Files);

            if (userData.Id == null || userData.Id == Guid.Empty)
            {
                var engine = new RazorLightEngineBuilder()
                  .UseFilesystemProject(Directory.GetCurrentDirectory() + "\\Views\\Shared")
                  .UseMemoryCachingProvider()
                  .Build();

                string emailString = await engine.CompileRenderAsync("EmailTemplate.cshtml", userData);

                await emailSender.SendEmailAsync(
                    user.Email,
                    "no-reply: Jelszó",
                    emailString);
            }

            return Json(new { user = user });
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(UserData userData)
        {
            User user = await loginService.AuthenticateAsync(userData.Username, userData.Password);
            if (user != null && user.IsAdmin)
            {
                return Json(new { auth_user = user, role = "admin" });
            }
            else if (user != null && !user.IsAdmin)
            {
                return Json(new { auth_user = user, role = "user" });
            }

            return NotFound(userData);
        }
    }
}
