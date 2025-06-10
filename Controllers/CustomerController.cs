using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ModefyEcommerce.Models;
using ModefyEcommerce.Business;
using ModefyEcommerce.Helpers;

namespace ModefyEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBusiness _customerBusiness ;
        private readonly IConfiguration _configuration;
        private readonly HashHelper _hashHelper;

        public CustomerController(CustomerBusiness customerBusiness, IConfiguration configuration, HashHelper hashHelper)
        {
            _customerBusiness = customerBusiness;
            _configuration = configuration;
            _hashHelper = hashHelper;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Customer newCustomer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = _customerBusiness.GetCustomerByEmail(newCustomer.Email);
            if (existing != null) return Conflict("Email already in use.");

            newCustomer.Password = _hashHelper.ComputeSha256Hash(newCustomer.Password);
            newCustomer.CreatedAt = DateTime.Now;
            newCustomer.Status = "active";

            var created = _customerBusiness.Create(newCustomer);

            if (created == null) return StatusCode(500, "Could not create customer.");

            return Ok(created);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Customer loginData)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _customerBusiness.GetCustomerByEmail(loginData.Email);

            if (customer == null) return Unauthorized("Invalid email or password.");

            string hashedInput = _hashHelper.ComputeSha256Hash(loginData.Password);
            if (customer.Password != hashedInput) return Unauthorized("Invalid email or password.");

            string token = GenerateJwtToken(customer);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Customer customer)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Name, customer.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
