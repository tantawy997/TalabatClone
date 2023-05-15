using AutoMapper;
using Core.entites.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WepAPIAssignment.Dtos;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> _userManager,
            SignInManager<AppUser> _signInManager,
            ITokenService _tokenService,
            IMapper _mapper)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            tokenService = _tokenService;
            mapper = _mapper;
        }

        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound(new ApiResponse(404));

            return new UserDTO()
            {
                Email = email,
                DisplayName = user.DisplayName,
                Token = tokenService.CreateToken(user),
            };

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user =await userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(404));

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password,false);

            if(!result.Succeeded)
                return Unauthorized(new ApiResponse(404));

            return new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = tokenService.CreateToken(user),
            };
        }

        [HttpGet("checkEmail")]
        public async Task<bool> CheckEmailExistAsync([FromQuery] string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDTO>> Register(RegisterDTO regesterDTO)
        {
            if(await CheckEmailExistAsync(regesterDTO.Email))
            {
                return new BadRequestObjectResult(new ApiValdationErrors
                {
                    errors = new[] {"email address is in use"} 
                });


            }

            var user = new AppUser
            {
                Email = regesterDTO.Email,
                DisplayName = regesterDTO.DisplayName,
                UserName = regesterDTO.Email
            };
            var result = await userManager.CreateAsync(user,regesterDTO.Password);

            if (!result.Succeeded)
                return NotFound(new ApiResponse(404));

            return new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = tokenService.CreateToken(user),
            };

        }


        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(a=> a.Address)
                .SingleOrDefaultAsync(c=> c.Email == email);

            var MappedUser = mapper.Map<AddressDTO>(user.Address);
            
            return Ok(MappedUser);

        }

        [Authorize]
        [HttpPost("UpdateAddress")]

        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(a => a.Address)
                .SingleOrDefaultAsync(c => c.Email == email);

            user.Address = mapper.Map<Address>(addressDTO);

            var result = await userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                return Ok(mapper.Map<AddressDTO>(user.Address));
            }

            return BadRequest(new ApiResponse(400, "Problem Updating the user address"));

        }
    }
}
