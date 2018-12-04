using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Szakdoga.BLL.ServiceInterfaces;
using Szakdoga.DAL;
using Szakdoga.BLL.Models;
using Szakdoga.Model;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Szakdoga.BLL.Helpers;
using Szakdoga.Common;

namespace Szakdoga.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IEmailSender emailSender;

        public LoginService(
            IUserService userService,
            IEmailSender emailSender,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            ApplicationDbContext DbContext)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.emailSender = emailSender;
            this.hostingEnvironment = hostingEnvironment;
            this.DbContext = DbContext;
        }

        public async Task<User> Register(UserData userData, IFormFileCollection files)
        {
            User user = new User();

            if(userData.Id != null && userData.Id != Guid.Empty)
            {
                user = await DbContext.Users.FirstOrDefaultAsync(e => e.Id == userData.Id);
                user.FirstName = userData.FirstName;
                user.LastName = userData.LastName;
                user.Username = userData.Username;
                user.Email = userData.Email;
                user.ProfilePicture = await UploadFiles(files);

                await DbContext.SaveChangesAsync();
            }
            else
            {
                userData.Password = userService.GeneratePassword();
                user = mapper.Map<User>(userData);

                user.Rank = DbContext.Ranks.First(e => e.Id == userData.RankId);
                user.ProfilePicture = await UploadFiles(files);
                user.FirstSignIn = true;

                await userService.CreateAsync(user, userData.Password);
            }

            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await DbContext.Users.FirstOrDefaultAsync(e => e.Username == username);

            if (user == null)
                return null;

            if (!PasswordHelpers.VerifyPasswordHash(password, user.PasswordHelper.PasswordHash, user.PasswordHelper.PasswordSalt))
                return null;

            return user;
        }

        private async Task<string> UploadFiles(IFormFileCollection files)
        {
            var uploadsRootFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            foreach (var file in files)
            {
                string filePath;

                if (file == null || file.Length == 0)
                {
                    filePath = Path.Combine(uploadsRootFolder, "wallpaper-for-facebook-profile-photo.jpg");
                }
                else
                {
                    filePath = Path.Combine(uploadsRootFolder, file.FileName);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream).ConfigureAwait(false);
                    return file.FileName;
                }
            }

            string defaultImg = "wallpaper-for-facebook-profile-photo.jpg";
            return defaultImg;
        }
    }
}
