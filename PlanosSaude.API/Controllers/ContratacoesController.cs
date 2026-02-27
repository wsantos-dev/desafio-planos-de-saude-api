using Microsoft.AspNetCore.Mvc;
using PlanosSaude.API.DTOs.Contratacao;
using PlanosSaude.API.Services;

namespace PlanosSaude.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratacoesController : ControllerBase
    {
        private readonly IContratacaoService _service;

        public ContratacoesController(IContratacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Contratar(
            [FromBody] ContratarRequestDto request,
            CancellationToken cancellationToken)
        {
            var response = await _service.ContratarAsync(
                request.BeneficiarioId,
                request.PlanoId,
                request.DataInicio,
                cancellationToken);

            return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(
           Guid id,
           CancellationToken cancellationToken)
        {
            var contratacao = await _service.ObterPorIdAsync(id, cancellationToken);
            return Ok(contratacao);
        }

        [HttpPatch("{id:guid}/cancelar")]
        public async Task<IActionResult> Cancelar(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _service.CancelarAsync(id, cancellationToken);
            return NoContent();
        }
    }
}