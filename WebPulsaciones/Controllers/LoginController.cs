using Datos;
using Entity;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPulsaciones.Models;

namespace WebPulsaciones.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        PulsacionesContext _context;
        UserService _userService;
        IJwtService _jwtService;
        public LoginController(PulsacionesContext context, IJwtService jwtService)
        {
            _context = context;

            var admin = _context.Users.Find("admin");
            if (admin == null) 
            {
                _context.Users.Add(new Entity.User() {  UserName="admin", Password="admin", Email="admin@gmail.com", Estado="AC", FirstName="Adminitrador", LastName="", MobilePhone="31800000000"});
                var i=_context.SaveChanges();
            }

            _userService = new UserService(context);
            _jwtService = jwtService;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = _userService.Validate(model.Username, model.Password);

            if (user == null) 
            {
                ModelState.AddModelError("Acceso Denegado", "Username or password is incorrect");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }

            var response= _jwtService.GenerateToken(user);

            return Ok(response);
        }

        
       
    }
}
