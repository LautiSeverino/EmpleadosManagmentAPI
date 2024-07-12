using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DTOs.Empleado;
using BlazorEmpleados.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorEmpleados.API.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration { get; set; }
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IConfiguration configuration, IUserService userService, ILogger<LoginController> logger)
        {
            _configuration = configuration;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Login(LoginRequestDTO login)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint Login");
                var userEntity = await _userService.GetUser(login.UserName, login.UserPassword);
                if (userEntity != null)
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserEmail", userEntity.UserEmail),
                    new Claim("UserName", userEntity.UserName)


                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(5),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                    return BadRequest();
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al Iniciar Sesion");
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserResponseDTO>> Register(UserCreateRequestDTO user)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint Register");
                var existeUser = await _userService.GetUser(user.UserName, user.UserPassword);

                if (existeUser != null)
                    return BadRequest("Ya existe un usuario creado");

                var result = await _userService.Create(user);
                if (result == null)
                    return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al Registrarse");
                return BadRequest(ex.Message);
            }
        }
    }
}
