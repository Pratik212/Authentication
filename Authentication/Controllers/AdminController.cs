using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authentication.Dtos;
using Authentication.Helpers;
using Authentication.Interfaces;
using Authentication.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Controllers
{
    /// <summary>
    /// AdminController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AuthentiContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly Settings _settings;

        /// <summary>
        /// AdminController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="settings"></param>
        public AdminController(AuthentiContext context, IUserRepository userRepository, IMapper mapper,IOptions<Settings> settings)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
            _settings = settings.Value;
        }

        #region POSTAPI
        
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                
                Utils.CreatePasswordHash(userDto.Password, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userRepository.Register(user);
                
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.Login(loginDto.Email, loginDto.Password);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Address,
                user.MobileNumber,
                Token = tokenString,
                Expiration = token.ValidTo,
            });
        }
        
          /// <summary>
        /// Validate Admin Token
        /// </summary>
        /// <param name="tokenDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateToken")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ValidateToken([FromBody] TokenDto tokenDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(tokenDto.Token) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;


            var userId = Convert.ToInt64(jti);
            

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);
            // generate new token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            return Ok(new
            {
                user.Id,
                user.Email,
                Token = tokenString,
                Expiration = token.ValidTo,
                user.FirstName,
                user.LastName,
            });
        }

        
        #endregion
        
        #region GETAPI

        /// <summary>
        /// GetAllUser
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _context.Users.ToListAsync();

            var result = user.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Address,
            });

            return Ok(result);
        }
        
        #endregion
    }
}