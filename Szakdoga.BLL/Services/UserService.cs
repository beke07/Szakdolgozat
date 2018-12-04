using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdoga.BLL.Helpers;
using Szakdoga.BLL.Models;
using Szakdoga.BLL.ServiceInterfaces;
using Szakdoga.Common;
using Szakdoga.DAL;
using Szakdoga.Model;
using Szakdoga.Model.ComplexTypes;

namespace Szakdoga.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public UserService(
            IMapper mapper,
            ApplicationDbContext DbContext)
        {
            this.mapper = mapper;
            this.DbContext = DbContext;
        }

        public async Task<User> CreateAsync(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("A jelszó kitöltése kötelező!");

            if (DbContext.Users.Any(x => x.Username == user.Username))
                throw new AppException("Felhasználónév " + user.Username + " már foglalt!");

            user.PasswordHelper = PasswordHelpers.CreatePasswordHash(password);

            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            User userToDelete = await DbContext.Users
                                        .Include(e => e.Boss)
                                        .Include(e => e.Employees)
                                        .Include(e => e.Projects)
                                        .Include(e => e.Tasks)
                                        .FirstOrDefaultAsync(e => e.Id == id);

            if (userToDelete != null)
            {
                DbContext.Users.Remove(userToDelete);
            }

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                var asd = e;
            }
        }

        public string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars.ToLower(), 10)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public async Task<List<UserData>> GetAllAsync(string name)
        {
            return mapper.Map<List<UserData>>(await DbContext.Users.Include(e => e.Rank).ToListAsync());
        }

        public async Task<List<UserHierarchyDetailsData>> GetUserHierarchyUserList()
        {
            return mapper.Map<List<UserHierarchyDetailsData>>(await DbContext.Users.ToListAsync()).OrderBy(e => e.BossId).ThenBy(e => e.Employees.Count).ToList();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
             return await DbContext.Users.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> SetUserHierarchyAsync(List<UserHierarchyData> userData)
        {
            foreach (var u in userData)
            {
                User user = await DbContext.Users.FirstOrDefaultAsync(e => e.Id == u.Id);
                if (user != null)
                {
                    user.BossId = (u.BossId == Guid.Empty) ? (Guid?)null : u.BossId;
                    user.Employees = DbContext.Users.Where(e => u.EmployeeIds.Contains(e.Id)).ToList();
                    await DbContext.SaveChangesAsync();
                }
            }

            return true;
        }

        public async Task UpdateAsync(UserData user, string password = null)
        {
            User userToUpdate = DbContext.Users.Find(user.Id);
            if (userToUpdate == null)
                throw new AppException("Felhasználó nem található!");

            if (user.Username != user.Username)
            {
                if (DbContext.Users.Any(e => e.Username == user.Username))
                    throw new AppException("Felhasználónév " + user.Username + " már foglalt!");
            }

            userToUpdate.IsAdmin = user.IsAdmin;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Username = user.Username;
            userToUpdate.Email = user.Email;

            if (!string.IsNullOrWhiteSpace(password))
            {
                userToUpdate.PasswordHelper = PasswordHelpers.CreatePasswordHash(password);
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            User user = await DbContext.Users.FirstAsync(e => e.Id == userId);

            if (!PasswordHelpers.VerifyPasswordHash(oldPassword, user.PasswordHelper.PasswordHash, user.PasswordHelper.PasswordSalt))
            {
                return false;
            }

            user.PasswordHelper = PasswordHelpers.CreatePasswordHash(newPassword);
            user.FirstSignIn = false;

            await DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<long> GetHoursToUserAsync(Guid id)
        {
            User user = await DbContext.Users.Include(e => e.Tasks).ThenInclude(e => e.TaskActivities).FirstOrDefaultAsync(e => e.Id == id);
            return user.Tasks.SelectMany(e => e.TaskActivities.Where(t => t.Date.Month == DateTime.Today.Month)).Sum(e => e.Hours);
        }
    }
}
