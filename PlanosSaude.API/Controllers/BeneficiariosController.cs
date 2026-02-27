using Microsoft.AspNetCore.Mvc;
using PlanosSaude.API.DTOs;
using PlanosSaude.API.Services;

namespace PlanosSaude.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeneficiariosController : ControllerBase
    {
        private readonly IBeneficiarioService _service;

        public BeneficiariosController(IBeneficiarioService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(
            Guid id,
            CancellationToken cancellationToken)
        {
            var beneficiario = await _service.ObterPorIdAsync(id, cancellationToken);
            return Ok(beneficiario);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos(
            CancellationToken cancellationToken)
        {
            var lista = await _service.ObterTodosAsync(cancellationToken);
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(
            [FromBody] CriarBeneficiarioDto dto,
            CancellationToken cancellationToken)
        {

            var beneficiario = await _service.CriarAsync(dto, cancellationToken);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = beneficiario.Id },
                beneficiario);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] AtualizarBeneficiarioDto dto,
            CancellationToken cancellationToken)
        {

            await _service.AtualizarAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(
        Guid id,
        CancellationToken cancellationToken)
        {
            await _service.RemoverAsync(id, cancellationToken);
            return NoContent();
        }
    }
}