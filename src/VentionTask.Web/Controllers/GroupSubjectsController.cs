using Microsoft.AspNetCore.Mvc;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupSubjectsController : ControllerBase
    {
        private readonly IGroupSubjectService gsService;

        public GroupSubjectsController(IGroupSubjectService gsService)
        {
            this.gsService = gsService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<GroupSubjectResultDto>> PostAsync(GroupSubjectCreationDto dto)
            => Ok(await this.gsService.AddAsync(dto));

        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(long id)
            => Ok(await this.gsService.RemoveAsync(id));

        [HttpGet("id")]
        public async ValueTask<ActionResult<GroupSubjectResultDto>> GetByIdAsync(long id)
            => Ok(await this.gsService.RetrieveByIdAsync(id));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await this.gsService.RetrieveAllAsync());

        [HttpPut]
        public async ValueTask<ActionResult<GroupSubjectResultDto>> PutAsync(long id, GroupSubjectCreationDto dto)
            => Ok(await this.gsService.ModifyAsync(id, dto));
    }
}
