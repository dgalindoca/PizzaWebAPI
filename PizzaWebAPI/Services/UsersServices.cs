using Microsoft.IdentityModel.Tokens;
using PizzaWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PizzaWebAPI.Services
{
    public class UsersServices
    {
        private const string Key = "AppSettings:TokenKey";
        private readonly PizzaDB _pizzaDB;
        private readonly IConfiguration _configuration;

        public UsersServices(PizzaDB pizzaDB, IConfiguration configuration)
        {
            _pizzaDB = pizzaDB;
            _configuration = configuration;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _pizzaDB.Users.ToListAsync();
        }

        public async Task<Users?> GetUserAsync(int id)
        {
            return await _pizzaDB.Users.FindAsync(id);
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            Users? user = await _pizzaDB.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task CreateUserAsync(Users user)
        {
            _pizzaDB.Users.Add(user);
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task SaveUserAsync()
        {
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Users user)
        {
            _pizzaDB.Users.Remove(user);
            await _pizzaDB.SaveChangesAsync();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public bool IsValidEmail(string email)
        {
            // Define the regular expression pattern for a valid email
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Create an instance of the Regex class with the pattern
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to check if the email matches the pattern
            return regex.IsMatch(email);
        }

        public string CreateToken(Users users)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, users.Email),
                new Claim(ClaimTypes.Name, users.FirstName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection(Key).Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
