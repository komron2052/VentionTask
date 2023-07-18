using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<UserResultDto>> PostAsync(UserCreationDto dto)
            => Ok(await this.userService.AddAsync(dto));

        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(long id)
            =>Ok(await this.userService.RemoveAsync(id));

        [HttpGet("id")]
        public async ValueTask<ActionResult<UserResultDto>> GetByIdAsync(long id)
            =>Ok(await this.userService.RetrieveByIdAsync(id));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            =>Ok(await this.userService.RetrieveAllAsync());

        [HttpPut]
        public async ValueTask<ActionResult<UserResultDto>> PutAsync(long id, UserUpdateDto dto)
            =>Ok(await this.userService.ModifyAsync(id, dto));  
    }
}
