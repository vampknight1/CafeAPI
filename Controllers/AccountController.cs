using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CafeAPI.Models;

namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]UserData userParam)
        {
            // validasi sederhana, autentikasi berhasil ketika username dan password yang dimasukkan sama.
            if (userParam.Username == userParam.Password)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Kunci.Aman);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userParam.Username));

                // jika username=Admin, maka kasih role Administrator
                if (userParam.Username == "Admin")
                    claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                else
                    claims.Add(new Claim(ClaimTypes.Role, "Regular"));

                var claimsID = new ClaimsIdentity(claims);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsID,

                    // Set waktu expire token = 7 menit kedepan.
                    Expires = DateTime.UtcNow.AddMinutes(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                userParam.SetToken(tokenHandler.WriteToken(token));

                // remove password before returning
                userParam.Password = null;
            }
            else
            {
                userParam = null;
            }
            if (userParam == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(userParam);
        }
    }
}