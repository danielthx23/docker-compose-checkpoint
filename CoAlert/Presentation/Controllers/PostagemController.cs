using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Postagem;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CoAlert.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemApplicationService _applicationService;

        public PostagemController(IPostagemApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as postagens", Description = "Retorna todos os registros de postagens cadastradas.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<PostagemResponseDto>))]
        [SwaggerResponse(204, "Nenhuma postagem encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodasPostagens();

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém postagem por ID", Description = "Retorna os dados de uma postagem específica.")]
        [SwaggerResponse(200, "Postagem retornada com sucesso", typeof(PostagemResponseDto))]
        [SwaggerResponse(404, "Postagem não encontrada")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(long id)
        {
            var result = _applicationService.ObterPostagemPorId(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(Summary = "Obtém postagens por usuário", Description = "Retorna todas as postagens de um determinado usuário.")]
        [SwaggerResponse(200, "Postagens retornadas com sucesso", typeof(IEnumerable<PostagemResponseDto>))]
        [SwaggerResponse(204, "Nenhuma postagem encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByUsuario(long usuarioId)
        {
            var result = _applicationService.ObterPostagensPorUsuario(usuarioId);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("categoria/{categoriaId}")]
        [SwaggerOperation(Summary = "Obtém postagens por categoria", Description = "Retorna todas as postagens de uma determinada categoria.")]
        [SwaggerResponse(200, "Postagens retornadas com sucesso", typeof(IEnumerable<PostagemResponseDto>))]
        [SwaggerResponse(204, "Nenhuma postagem encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByCategoria(long categoriaId)
        {
            var result = _applicationService.ObterPostagensPorCategoria(categoriaId);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("localizacao/{localizacaoId}")]
        [SwaggerOperation(Summary = "Obtém postagens por localização", Description = "Retorna todas as postagens de uma determinada localização.")]
        [SwaggerResponse(200, "Postagens retornadas com sucesso", typeof(IEnumerable<PostagemResponseDto>))]
        [SwaggerResponse(204, "Nenhuma postagem encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByLocalizacao(long localizacaoId)
        {
            var result = _applicationService.ObterPostagensPorLocalizacao(localizacaoId);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova postagem", Description = "Salva um novo registro de postagem.")]
        [SwaggerResponse(201, "Postagem salva com sucesso", typeof(PostagemResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] PostagemRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarPostagem(entity);

                if (result != null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdPostagem }, result);

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
        [SwaggerOperation(Summary = "Atualiza uma postagem", Description = "Edita os dados de uma postagem existente.")]
        [SwaggerResponse(200, "Postagem atualizada com sucesso", typeof(PostagemResponseDto))]
        [SwaggerResponse(404, "Postagem não encontrada")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(long id, [FromBody] PostagemRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarPostagem(id, entity);

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
        [SwaggerOperation(Summary = "Deleta uma postagem", Description = "Remove uma postagem do sistema com base no ID.")]
        [SwaggerResponse(204, "Postagem deletada com sucesso")]
        [SwaggerResponse(404, "Postagem não encontrada")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(long id)
        {
            var result = _applicationService.DeletarPostagem(id);

            if (result != null)
                return NoContent();

            return NotFound();
        }
    }
} 