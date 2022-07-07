using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanPhucShopApi.DTO;
using TanPhucShopApi.Models.DTO.UserDto;
using TanPhucShopApi.Services.UserService;

namespace TanPhucShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private IConfiguration configuration;
        private string BASE_URL;
        public UsersController(IUserService _userService, IConfiguration _configuration)
        {
            userService = _userService;
            configuration = _configuration;
            BASE_URL = configuration["BASE_URL"] + "users";
        }

        [HttpGet]
        //[Authorize(Roles ="Admin")]
        public IActionResult GetAllUserDto()
        {
            var users =  userService.GetAll();
            return Ok(users);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] RegisterUserDto user)
        {
            var createdUser = await userService.Create(user);

            return Created(BASE_URL + "/" + createdUser.Id, createdUser);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,UpdateUserDto updateUserDto)
        {
            var user = await userService.FindUserById(id);
            if (user == null) return NotFound();
            else
            {
                var Result = await userService.Update(id,updateUserDto);
                if (Result == true)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet("status/{id}")]
        public async Task<IActionResult> ChangeStatusUser(int id)
        {
            var user = await userService.FindUserById(id);
            if (user == null) return NotFound();
            else
            {
                var Result = await userService.ChangeStatusUser(id);
                if (Result == true)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userService.FindUserById(id);
            if (user == null) return NotFound();
            else
            {
                var Result = await userService.Delete(id);
                if (Result == true)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userDTO = await userService.FindUserById(id);
            if(userDTO == null) return NotFound();
            return Ok(userDTO); 
        }

        [HttpPost("RemoveRole")]
        public async Task<IActionResult> RemoveRolesUser([FromBody] UserRoleDto removeUserRoleDTO)
        {
            var result = await userService.RemoveRoleUser(removeUserRoleDTO);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRolesUser([FromBody] UserRoleDto AddUserRoleDTO)
        {
            var result = await userService.AddRoleUser(AddUserRoleDTO);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto lgoinUserDto)
        {
            var user = await userService.Login(lgoinUserDto);
            if (user!=null) return Ok(user);
            return BadRequest();
        }
    }
}
