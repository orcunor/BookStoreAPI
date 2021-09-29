using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public UserManager<ApplicationUser> _userManager { get; }
        public BinaryReader JwtRegisteredClaimName { get; private set; }


        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email
            };

            return await _userManager.CreateAsync(user,signUpModel.Password);
        }

        public async Task<string> LoginAsync(SigInModel sigInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(sigInModel.Email,sigInModel.Password,false,true);

            if (!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,sigInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

           return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
