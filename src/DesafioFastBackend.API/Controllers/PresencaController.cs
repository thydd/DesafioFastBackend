using DesafioFastBackend.Application.UseCases.Presencas.Create;
using DesafioFastBackend.Application.UseCases.Presencas.Delete;
using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Application.UseCases.Presencas.GetById;
using DesafioFastBackend.Application.UseCases.Presencas.List;
using DesafioFastBackend.Application.UseCases.Presencas.Update;
using DesafioFastBackend.API.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "ReadAccess")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<PresencaOutputDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PresencaOutputDto>>>> GetAllAsync()
        {
            var output = await listPresencasUseCase.ExecuteAsync(new ListPresencasInputDto());
            if (!output.Any())
            {
                return NoContent();
            }

            return Ok(ApiResponse<IEnumerable<PresencaOutputDto>>.Ok(output, "Presenças listadas com sucesso."));
        }

        [HttpGet("{workshopId:int}/{colaboradorId:int}")]
        [Authorize(Policy = "ReadAccess")]
        [ProducesResponseType(typeof(ApiResponse<PresencaOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PresencaOutputDto>>> GetByIdAsync([FromRoute] int workshopId, [FromRoute] int colaboradorId)
        {
            var output = await getPresencaByIdUseCase.ExecuteAsync(new GetPresencaByIdInputDto
            {
                WorkshopId = workshopId,
                ColaboradorId = colaboradorId
            });

            if (output is null)
            {
                return NoContent();
            }

            return Ok(ApiResponse<PresencaOutputDto>.Ok(output, "Presença encontrada com sucesso."));
        }

        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
        [ProducesResponseType(typeof(ApiResponse<PresencaOutputDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PresencaOutputDto>>> CreateAsync([FromBody] CreatePresencaInputDto input)
        {
            var output = await createPresencaUseCase.ExecuteAsync(input);
            return StatusCode(
                StatusCodes.Status201Created,
                ApiResponse<PresencaOutputDto>.Ok(output, "Presença criada com sucesso."));
        }

        [HttpPut("{workshopId:int}/{colaboradorId:int}")]
        [Authorize(Policy = "WriteAccess")]
        [ProducesResponseType(typeof(ApiResponse<PresencaOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PresencaOutputDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PresencaOutputDto>>> UpdateAsync([FromRoute] int workshopId, [FromRoute] int colaboradorId, [FromBody] UpdatePresencaInputDto input)
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
                return NotFound(ApiResponse<PresencaOutputDto>.Fail("Presença não encontrada."));
            }

            return Ok(ApiResponse<PresencaOutputDto>.Ok(output, "Presença atualizada com sucesso."));
        }

        [HttpDelete("{workshopId:int}/{colaboradorId:int}")]
        [Authorize(Policy = "WriteAccess")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAsync([FromRoute] int workshopId, [FromRoute] int colaboradorId)
        {
            var deleted = await deletePresencaUseCase.ExecuteAsync(new DeletePresencaInputDto
            {
                WorkshopId = workshopId,
                ColaboradorId = colaboradorId
            });

            if (!deleted)
            {
                return NotFound(ApiResponse<object>.Fail("Presença não encontrada."));
            }

            return Ok(ApiResponse<object>.Ok(null, "Presença removida com sucesso."));
        }
    }
}
