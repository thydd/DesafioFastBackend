using Microsoft.AspNetCore.Mvc;
using DesafioFastBackend.Application.UseCases.Colaboradores.Create;
using DesafioFastBackend.Application.UseCases.Colaboradores.Delete;
using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Application.UseCases.Colaboradores.GetById;
using DesafioFastBackend.Application.UseCases.Colaboradores.List;
using DesafioFastBackend.Application.UseCases.Colaboradores.Update;
using DesafioFastBackend.API.Contracts;

namespace DesafioFastBackend.API.Controllers
{
    [Route("api/colaboradores")]
    [ApiController]
    public class ColaboradorController(
        ICreateColaboradorUseCase createColaboradorUseCase,
        IListColaboradoresUseCase listColaboradoresUseCase,
        IGetColaboradorByIdUseCase getColaboradorByIdUseCase,
        IUpdateColaboradorUseCase updateColaboradorUseCase,
        IDeleteColaboradorUseCase deleteColaboradorUseCase) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ColaboradorOutputDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<ColaboradorOutputDto>>>> GetAllAsync()
        {
            var output = await listColaboradoresUseCase.ExecuteAsync(new ListColaboradoresInputDto());
            if (!output.Any())
            {
                return NoContent();
            }

            return Ok(ApiResponse<IEnumerable<ColaboradorOutputDto>>.Ok(output, "Colaboradores listados com sucesso."));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<ColaboradorOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ColaboradorOutputDto>>> GetByIdAsync([FromRoute] int id)
        {
            var output = await getColaboradorByIdUseCase.ExecuteAsync(new GetColaboradorByIdInputDto { Id = id });
            if (output is null)
            {
                return NoContent();
            }

            return Ok(ApiResponse<ColaboradorOutputDto>.Ok(output, "Colaborador encontrado com sucesso."));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ColaboradorOutputDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ColaboradorOutputDto>>> CreateAsync([FromBody] CreateColaboradorInputDto input)
        {
            var output = await createColaboradorUseCase.ExecuteAsync(input);
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = output.Id },
                ApiResponse<ColaboradorOutputDto>.Ok(output, "Colaborador criado com sucesso."));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<ColaboradorOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ColaboradorOutputDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ColaboradorOutputDto>>> UpdateAsync([FromRoute] int id, [FromBody] UpdateColaboradorInputDto input)
        {
            var output = await updateColaboradorUseCase.ExecuteAsync(new UpdateColaboradorInputDto
            {
                Id = id,
                Nome = input.Nome
            });

            if (output is null)
            {
                return NotFound(ApiResponse<ColaboradorOutputDto>.Fail("Colaborador não encontrado."));
            }

            return Ok(ApiResponse<ColaboradorOutputDto>.Ok(output, "Colaborador atualizado com sucesso."));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAsync([FromRoute] int id)
        {
            var deleted = await deleteColaboradorUseCase.ExecuteAsync(new DeleteColaboradorInputDto { Id = id });
            if (!deleted)
            {
                return NotFound(ApiResponse<object>.Fail("Colaborador não encontrado."));
            }

            return Ok(ApiResponse<object>.Ok(null, "Colaborador removido com sucesso."));
        }
    }
}
