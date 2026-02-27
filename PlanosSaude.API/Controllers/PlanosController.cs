using Microsoft.AspNetCore.Mvc;
using PlanosSaude.API.DTOs.Planos;
using PlanosSaude.API.Services;

namespace PlanosSaude.API.Controllers
{
    [ApiController]
    [Route("api/planos")]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanoService _service;

        public PlanosController(IPlanoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PlanoRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await _service.CriarAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            var response = await _service.ObterPorIdAsync(id, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var response = await _service.ListarAsync(cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] PlanoRequestDto dto, CancellationToken cancellationToken)
        {
            await _service.AtualizarAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
        {
            await _service.RemoverAsync(id, cancellationToken);
            return NoContent();
        }
    }
}