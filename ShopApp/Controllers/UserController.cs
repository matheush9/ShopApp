using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.User;
using ShopApp.Domain.DTOs.User;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _userService.GetUserById(id);

            if (user is null)
                return NotFound(user);

            return Ok(user);
        }

        
        [HttpPost]
        public async Task<ActionResult<GetUserResponseDto>> Add([FromBody] AddUserRequestDto newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var authenticatedUserId = int.Parse(User.FindFirst("UserId").Value);

            if (authenticatedUserId != id)
                return Unauthorized();

            var user = await _userService.DeleteUser(id);

            if (user is null)
                return NotFound(user);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, EditUserRequestDto newUser)
        {
            var authenticatedUserId = int.Parse(User.FindFirst("UserId").Value);

            if (authenticatedUserId != id)
                return Unauthorized();  

                var user = await _userService.UpdateUser(id, newUser);

            if (user is null)
                return NotFound(user);

            return Ok(user);
        }

        [HttpPost("auth")]
        public async Task<ActionResult> Authenticate([FromBody] AddUserRequestDto userRequest)
        {
            var token = await _userService.Authenticate(userRequest);

            if (string.IsNullOrEmpty(token.Token))
                return Unauthorized();

            return Ok(token);
        }
    }
}
