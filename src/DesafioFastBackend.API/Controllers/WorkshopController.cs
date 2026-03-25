using Microsoft.AspNetCore.Mvc;
using DesafioFastBackend.Application.UseCases.Workshops.Create;
using DesafioFastBackend.Application.UseCases.Workshops.Delete;
using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Application.UseCases.Workshops.GetById;
using DesafioFastBackend.Application.UseCases.Workshops.List;
using DesafioFastBackend.Application.UseCases.Workshops.Update;
using DesafioFastBackend.API.Contracts;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Policy = "ReadAccess")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WorkshopOutputDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<WorkshopOutputDto>>>> GetAllAsync()
        {
            var output = await listWorkshopsUseCase.ExecuteAsync(new ListWorkshopsInputDto());
            if (!output.Any())
            {
                return NoContent();
            }

            return Ok(ApiResponse<IEnumerable<WorkshopOutputDto>>.Ok(output, "Workshops listados com sucesso."));
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "ReadAccess")]
        [ProducesResponseType(typeof(ApiResponse<WorkshopOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<WorkshopOutputDto>>> GetByIdAsync([FromRoute] int id)
        {
            var output = await getWorkshopByIdUseCase.ExecuteAsync(new GetWorkshopByIdInputDto { Id = id });
            if (output is null)
            {
                return NoContent();
            }

            return Ok(ApiResponse<WorkshopOutputDto>.Ok(output, "Workshop encontrado com sucesso."));
        }

        [HttpPost]
        [Authorize(Policy = "WriteAccess")]
        [ProducesResponseType(typeof(ApiResponse<WorkshopOutputDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<WorkshopOutputDto>>> CreateAsync([FromBody] CreateWorkshopInputDto input)
        {
            var output = await createWorkshopUseCase.ExecuteAsync(input);
            return StatusCode(
                StatusCodes.Status201Created,
                ApiResponse<WorkshopOutputDto>.Ok(output, "Workshop criado com sucesso."));
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "WriteAccess")]
        [ProducesResponseType(typeof(ApiResponse<WorkshopOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<WorkshopOutputDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<WorkshopOutputDto>>> UpdateAsync([FromRoute] int id, [FromBody] UpdateWorkshopInputDto input)
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
                return NotFound(ApiResponse<WorkshopOutputDto>.Fail("Workshop não encontrado."));
            }

            return Ok(ApiResponse<WorkshopOutputDto>.Ok(output, "Workshop atualizado com sucesso."));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "WriteAccess")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAsync([FromRoute] int id)
        {
            var deleted = await deleteWorkshopUseCase.ExecuteAsync(new DeleteWorkshopInputDto { Id = id });
            if (!deleted)
            {
                return NotFound(ApiResponse<object>.Fail("Workshop não encontrado."));
            }

            return Ok(ApiResponse<object>.Ok(null, "Workshop removido com sucesso."));
        }
    }
}
