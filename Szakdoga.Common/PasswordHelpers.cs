using System;
using System.Collections.Generic;
using System.Text;
using Szakdoga.Model.ComplexTypes;

namespace Szakdoga.Common
{
    public class PasswordHelpers
    {
        public static PasswordHelper CreatePasswordHash(string password)
        {
            PasswordHelper passwordHelper = new PasswordHelper();

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("A jelszó nem lehet üres!", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHelper.PasswordSalt = hmac.Key;
                passwordHelper.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return passwordHelper;
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("A jelszó nem lehet üres!", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Helytelen hash méret (64 byte várt)!.", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
