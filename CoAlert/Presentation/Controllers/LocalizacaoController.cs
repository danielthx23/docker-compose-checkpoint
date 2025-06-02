using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Localizacao;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CoAlert.Presentation.Controllers
{
    [Route("api/localizacao")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly ILocalizacaoApplicationService _applicationService;

        public LocalizacaoController(ILocalizacaoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as localizações", Description = "Retorna todos os registros de localizações cadastradas.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<LocalizacaoResponseDto>))]
        [SwaggerResponse(204, "Nenhuma localização encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodasLocalizacoes();

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém localização por ID", Description = "Retorna os dados de uma localização específica.")]
        [SwaggerResponse(200, "Localização retornada com sucesso", typeof(LocalizacaoResponseDto))]
        [SwaggerResponse(404, "Localização não encontrada")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(long id)
        {
            var result = _applicationService.ObterLocalizacaoPorId(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("cidade/{cidade}")]
        [SwaggerOperation(Summary = "Obtém localizações por cidade", Description = "Retorna todas as localizações de uma determinada cidade.")]
        [SwaggerResponse(200, "Localizações retornadas com sucesso", typeof(IEnumerable<LocalizacaoResponseDto>))]
        [SwaggerResponse(204, "Nenhuma localização encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByCidade(string cidade)
        {
            var result = _applicationService.ObterLocalizacoesPorCidade(cidade);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("estado/{estado}")]
        [SwaggerOperation(Summary = "Obtém localizações por estado", Description = "Retorna todas as localizações de um determinado estado.")]
        [SwaggerResponse(200, "Localizações retornadas com sucesso", typeof(IEnumerable<LocalizacaoResponseDto>))]
        [SwaggerResponse(204, "Nenhuma localização encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByEstado(string estado)
        {
            var result = _applicationService.ObterLocalizacoesPorEstado(estado);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("cep/{cep}")]
        [SwaggerOperation(Summary = "Obtém localização por CEP", Description = "Retorna a localização correspondente ao CEP informado.")]
        [SwaggerResponse(200, "Localização retornada com sucesso", typeof(LocalizacaoResponseDto))]
        [SwaggerResponse(404, "Localização não encontrada")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetByCep(string cep)
        {
            var result = _applicationService.ObterLocalizacaoPorCep(cep);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova localização", Description = "Salva um novo registro de localização.")]
        [SwaggerResponse(201, "Localização salva com sucesso", typeof(LocalizacaoResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] LocalizacaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarLocalizacao(entity);

                if (result != null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdLocalizacao }, result);

                return BadRequest("Não foi possível salvar os dados.");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma localização", Description = "Edita os dados de uma localização existente.")]
        [SwaggerResponse(200, "Localização atualizada com sucesso", typeof(LocalizacaoResponseDto))]
        [SwaggerResponse(404, "Localização não encontrada")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(long id, [FromBody] LocalizacaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarLocalizacao(id, entity);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta uma localização", Description = "Remove uma localização do sistema com base no ID.")]
        [SwaggerResponse(204, "Localização deletada com sucesso")]
        [SwaggerResponse(404, "Localização não encontrada")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(long id)
        {
            var result = _applicationService.DeletarLocalizacao(id);

            if (result != null)
                return NoContent();

            return NotFound();
        }
    }
} 