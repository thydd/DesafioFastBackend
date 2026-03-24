using Microsoft.AspNetCore.Mvc;
using DesafioFastBackend.Application.UseCases.Workshops.Create;
using DesafioFastBackend.Application.UseCases.Workshops.Delete;
using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Application.UseCases.Workshops.GetById;
using DesafioFastBackend.Application.UseCases.Workshops.List;
using DesafioFastBackend.Application.UseCases.Workshops.Update;

namespace DesafioFastBackend.API.Controllers
{
    [Route("api/workshops")]
    [ApiController]
    public class WorkshopController(
        ICreateWorkshopUseCase createWorkshopUseCase,
        IListWorkshopsUseCase listWorkshopsUseCase,
        IGetWorkshopByIdUseCase getWorkshopByIdUseCase,
        IUpdateWorkshopUseCase updateWorkshopUseCase,
        IDeleteWorkshopUseCase deleteWorkshopUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkshopOutputDto>>> GetAllAsync()
        {
            var output = await listWorkshopsUseCase.ExecuteAsync(new ListWorkshopsInputDto());
            return Ok(output);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<WorkshopOutputDto>> GetByIdAsync([FromRoute] int id)
        {
            var output = await getWorkshopByIdUseCase.ExecuteAsync(new GetWorkshopByIdInputDto { Id = id });
            if (output is null)
            {
                return NotFound();
            }

            return Ok(output);
        }

        [HttpPost]
        public async Task<ActionResult<WorkshopOutputDto>> CreateAsync([FromBody] CreateWorkshopInputDto input)
        {
            var output = await createWorkshopUseCase.ExecuteAsync(input);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = output.Id }, output);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<WorkshopOutputDto>> UpdateAsync([FromRoute] int id, [FromBody] UpdateWorkshopInputDto input)
        {
            var output = await updateWorkshopUseCase.ExecuteAsync(new UpdateWorkshopInputDto
            {
                Id = id,
                Nome = input.Nome,
                Descricao = input.Descricao,
                DataRealizacao = input.DataRealizacao
            });

            if (output is null)
            {
                return NotFound();
            }

            return Ok(output);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var deleted = await deleteWorkshopUseCase.ExecuteAsync(new DeleteWorkshopInputDto { Id = id });
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
