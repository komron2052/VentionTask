using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<SubjectResultDto>> PostAsync(SubjectCreationDto dto)
            => Ok(await this.subjectService.AddAsync(dto));

        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(long id)
            => Ok(await this.subjectService.RemoveAsync(id));

        [HttpGet("id")]
        public async ValueTask<ActionResult<SubjectResultDto>> GetByIdAsync(long id)
            => Ok(await this.subjectService.RetrieveByIdAsync(id));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await this.subjectService.RetrieveAllAsync());

        [HttpPut]
        public async ValueTask<ActionResult<SubjectResultDto>> PutAsync(long id, SubjectCreationDto dto)
            => Ok(await this.subjectService.ModifyAsync(id, dto));
    }
}
