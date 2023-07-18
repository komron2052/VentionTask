using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<GroupResultDto>> PostAsync(GroupCreationDto dto)
            => Ok(await this.groupService.AddAsync(dto));

        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(long id)
            => Ok(await this.groupService.RemoveAsync(id));

        [HttpGet("id")]
        public async ValueTask<ActionResult<GroupResultDto>> GetByIdAsync(long id)
            => Ok(await this.groupService.RetrieveByIdAsync(id));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await this.groupService.RetrieveAllAsync());

        [HttpPut]
        public async ValueTask<ActionResult<GroupResultDto>> PutAsync(long id, GroupCreationDto dto)
            => Ok(await this.groupService.ModifyAsync(id, dto));
    }
}
