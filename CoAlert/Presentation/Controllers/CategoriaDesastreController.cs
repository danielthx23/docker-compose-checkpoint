using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.CategoriaDesastre;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CoAlert.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaDesastreController : ControllerBase
    {
        private readonly ICategoriaDesastreApplicationService _applicationService;

        public CategoriaDesastreController(ICategoriaDesastreApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as categorias de desastre", Description = "Retorna todos os registros de categorias de desastre cadastradas.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<CategoriaDesastreResponseDto>))]
        [SwaggerResponse(204, "Nenhuma categoria encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodasCategorias();

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém categoria por ID", Description = "Retorna os dados de uma categoria específica.")]
        [SwaggerResponse(200, "Categoria retornada com sucesso", typeof(CategoriaDesastreResponseDto))]
        [SwaggerResponse(404, "Categoria não encontrada")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(long id)
        {
            var result = _applicationService.ObterCategoriaPorId(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("tipo/{tipo}")]
        [SwaggerOperation(Summary = "Obtém categorias por tipo", Description = "Retorna todas as categorias de um determinado tipo.")]
        [SwaggerResponse(200, "Categorias retornadas com sucesso", typeof(IEnumerable<CategoriaDesastreResponseDto>))]
        [SwaggerResponse(204, "Nenhuma categoria encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByTipo(string tipo)
        {
            var result = _applicationService.ObterCategoriasPorTipo(tipo);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("titulo/{titulo}")]
        [SwaggerOperation(Summary = "Obtém categorias por título", Description = "Retorna todas as categorias com o título especificado.")]
        [SwaggerResponse(200, "Categorias retornadas com sucesso", typeof(IEnumerable<CategoriaDesastreResponseDto>))]
        [SwaggerResponse(204, "Nenhuma categoria encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult GetByTitulo(string titulo)
        {
            var result = _applicationService.ObterCategoriasPorTitulo(titulo);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova categoria", Description = "Salva um novo registro de categoria de desastre.")]
        [SwaggerResponse(201, "Categoria salva com sucesso", typeof(CategoriaDesastreResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] CategoriaDesastreRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarCategoria(entity);

                if (result != null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdCategoriaDesastre }, result);

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
        [SwaggerOperation(Summary = "Atualiza uma categoria", Description = "Edita os dados de uma categoria existente.")]
        [SwaggerResponse(200, "Categoria atualizada com sucesso", typeof(CategoriaDesastreResponseDto))]
        [SwaggerResponse(404, "Categoria não encontrada")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(long id, [FromBody] CategoriaDesastreRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarCategoria(id, entity);

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
        [SwaggerOperation(Summary = "Deleta uma categoria", Description = "Remove uma categoria do sistema com base no ID.")]
        [SwaggerResponse(204, "Categoria deletada com sucesso")]
        [SwaggerResponse(404, "Categoria não encontrada")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(long id)
        {
            var result = _applicationService.DeletarCategoria(id);

            if (result != null)
                return NoContent();

            return NotFound();
        }
    }
} 