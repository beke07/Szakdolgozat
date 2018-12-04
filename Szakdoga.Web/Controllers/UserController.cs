using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Szakdoga.BLL.Models;
using Szakdoga.BLL.ServiceInterfaces;
using Szakdoga.Model;

namespace Szakdoga.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList(string searchName)
        {
            List<UserData> users = await userService.GetAllAsync(searchName);
            return Json(new { users = users });
        }

        [HttpGet]
        public async Task<IActionResult> GetHierarchyUserList()
        {
            List<UserHierarchyDetailsData> users = await userService.GetUserHierarchyUserList();
            return Json(new { users = users });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            User user = await userService.GetByIdAsync(id);
            return Json(new { user = user });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserData user)
        {
            try
            {
                await userService.UpdateAsync(user);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordChangeHelper passwordChangeHelper)
        {
            bool result = await userService.UpdatePasswordAsync(passwordChangeHelper.UserId, passwordChangeHelper.OldPassword, passwordChangeHelper.NewPassword);

            if (result) return Ok();
            return NotFound(passwordChangeHelper.OldPassword);
        }

        [HttpPost]
        public async Task<IActionResult> SaveHierarchy(List<UserHierarchyData> userData) {
            await userService.SetUserHierarchyAsync(userData);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetHoursToUser(Guid id)
        {
            return Json(await userService.GetHoursToUserAsync(id));
        }
    }
}
