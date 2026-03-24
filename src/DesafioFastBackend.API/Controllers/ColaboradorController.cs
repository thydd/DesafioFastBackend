using Microsoft.AspNetCore.Mvc;
using DesafioFastBackend.Application.UseCases.Colaboradores.Create;
using DesafioFastBackend.Application.UseCases.Colaboradores.Delete;
using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Application.UseCases.Colaboradores.GetById;
using DesafioFastBackend.Application.UseCases.Colaboradores.List;
using DesafioFastBackend.Application.UseCases.Colaboradores.Update;

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
        public async Task<ActionResult<IEnumerable<ColaboradorOutputDto>>> GetAllAsync()
        {
            var output = await listColaboradoresUseCase.ExecuteAsync(new ListColaboradoresInputDto());
            return Ok(output);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ColaboradorOutputDto>> GetByIdAsync([FromRoute] int id)
        {
            var output = await getColaboradorByIdUseCase.ExecuteAsync(new GetColaboradorByIdInputDto { Id = id });
            if (output is null)
            {
                return NotFound();
            }

            return Ok(output);
        }

        [HttpPost]
        public async Task<ActionResult<ColaboradorOutputDto>> CreateAsync([FromBody] CreateColaboradorInputDto input)
        {
            var output = await createColaboradorUseCase.ExecuteAsync(input);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = output.Id }, output);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ColaboradorOutputDto>> UpdateAsync([FromRoute] int id, [FromBody] UpdateColaboradorInputDto input)
        {
            var output = await updateColaboradorUseCase.ExecuteAsync(new UpdateColaboradorInputDto
            {
                Id = id,
                Nome = input.Nome
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
            var deleted = await deleteColaboradorUseCase.ExecuteAsync(new DeleteColaboradorInputDto { Id = id });
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
