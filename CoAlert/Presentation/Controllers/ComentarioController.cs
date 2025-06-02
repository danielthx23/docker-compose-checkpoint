using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Comentario;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CoAlert.Presentation.Controllers
{
    [Route("api/comentario")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioApplicationService _applicationService;

        public ComentarioController(IComentarioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os comentários", Description = "Retorna todos os registros de comentários cadastrados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<ComentarioResponseDto>))]
        [SwaggerResponse(204, "Nenhum comentário encontrado")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosComentarios();

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém comentário por ID", Description = "Retorna os dados de um comentário específico.")]
        [SwaggerResponse(200, "Comentário retornado com sucesso", typeof(ComentarioResponseDto))]
        [SwaggerResponse(404, "Comentário não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(long id)
        {
            var result = _applicationService.ObterComentarioPorId(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("postagem/{postagemId}")]
        [SwaggerOperation(Summary = "Obtém comentários por postagem", Description = "Retorna todos os comentários de uma determinada postagem.")]
        [SwaggerResponse(200, "Comentários retornados com sucesso", typeof(IEnumerable<ComentarioResponseDto>))]
        [SwaggerResponse(204, "Nenhum comentário encontrado")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByPostagem(long postagemId)
        {
            var result = _applicationService.ObterComentariosPorPostagem(postagemId);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(Summary = "Obtém comentários por usuário", Description = "Retorna todos os comentários de um determinado usuário.")]
        [SwaggerResponse(200, "Comentários retornados com sucesso", typeof(IEnumerable<ComentarioResponseDto>))]
        [SwaggerResponse(204, "Nenhum comentário encontrado")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByUsuario(long usuarioId)
        {
            var result = _applicationService.ObterComentariosPorUsuario(usuarioId);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("respostas/{comentarioId}")]
        [SwaggerOperation(Summary = "Obtém respostas de um comentário", Description = "Retorna todas as respostas de um determinado comentário.")]
        [SwaggerResponse(200, "Respostas retornadas com sucesso", typeof(IEnumerable<ComentarioResponseDto>))]
        [SwaggerResponse(204, "Nenhuma resposta encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetRespostas(long comentarioId)
        {
            var result = _applicationService.ObterRespostasComentario(comentarioId);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo comentário", Description = "Salva um novo registro de comentário.")]
        [SwaggerResponse(201, "Comentário salvo com sucesso", typeof(ComentarioResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] ComentarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarComentario(entity);

                if (result != null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdComentario }, result);

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
        [SwaggerOperation(Summary = "Atualiza um comentário", Description = "Edita os dados de um comentário existente.")]
        [SwaggerResponse(200, "Comentário atualizado com sucesso", typeof(ComentarioResponseDto))]
        [SwaggerResponse(404, "Comentário não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(long id, [FromBody] ComentarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarComentario(id, entity);

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
        [SwaggerOperation(Summary = "Deleta um comentário", Description = "Remove um comentário do sistema com base no ID.")]
        [SwaggerResponse(204, "Comentário deletado com sucesso")]
        [SwaggerResponse(404, "Comentário não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(long id)
        {
            var result = _applicationService.DeletarComentario(id);

            if (result != null)
                return NoContent();

            return NotFound();
        }
    }
} 