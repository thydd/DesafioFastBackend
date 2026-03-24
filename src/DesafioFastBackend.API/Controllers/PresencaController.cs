using DesafioFastBackend.Application.UseCases.Presencas.Create;
using DesafioFastBackend.Application.UseCases.Presencas.Delete;
using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Application.UseCases.Presencas.GetById;
using DesafioFastBackend.Application.UseCases.Presencas.List;
using DesafioFastBackend.Application.UseCases.Presencas.Update;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFastBackend.API.Controllers
{
    [Route("api/presencas")]
    [ApiController]
    public class PresencaController(
        ICreatePresencaUseCase createPresencaUseCase,
        IListPresencasUseCase listPresencasUseCase,
        IGetPresencaByIdUseCase getPresencaByIdUseCase,
        IUpdatePresencaUseCase updatePresencaUseCase,
        IDeletePresencaUseCase deletePresencaUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresencaOutputDto>>> GetAllAsync()
        {
            var output = await listPresencasUseCase.ExecuteAsync(new ListPresencasInputDto());
            return Ok(output);
        }

        [HttpGet("{workshopId:int}/{colaboradorId:int}")]
        public async Task<ActionResult<PresencaOutputDto>> GetByIdAsync([FromRoute] int workshopId, [FromRoute] int colaboradorId)
        {
            var output = await getPresencaByIdUseCase.ExecuteAsync(new GetPresencaByIdInputDto
            {
                WorkshopId = workshopId,
                ColaboradorId = colaboradorId
            });

            if (output is null)
            {
                return NotFound();
            }

            return Ok(output);
        }

        [HttpPost]
        public async Task<ActionResult<PresencaOutputDto>> CreateAsync([FromBody] CreatePresencaInputDto input)
        {
            var output = await createPresencaUseCase.ExecuteAsync(input);
            return CreatedAtAction(nameof(GetByIdAsync), new { workshopId = output.WorkshopId, colaboradorId = output.ColaboradorId }, output);
        }

        [HttpPut("{workshopId:int}/{colaboradorId:int}")]
        public async Task<ActionResult<PresencaOutputDto>> UpdateAsync([FromRoute] int workshopId, [FromRoute] int colaboradorId, [FromBody] UpdatePresencaInputDto input)
        {
            var output = await updatePresencaUseCase.ExecuteAsync(new UpdatePresencaInputDto
            {
                WorkshopId = workshopId,
                ColaboradorId = colaboradorId,
                DataHoraCheckIn = input.DataHoraCheckIn,
                Status = input.Status
            });

            if (output is null)
            {
                return NotFound();
            }

            return Ok(output);
        }

        [HttpDelete("{workshopId:int}/{colaboradorId:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int workshopId, [FromRoute] int colaboradorId)
        {
            var deleted = await deletePresencaUseCase.ExecuteAsync(new DeletePresencaInputDto
            {
                WorkshopId = workshopId,
                ColaboradorId = colaboradorId
            });

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
