using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Comentario;

namespace CoAlert.Presentation.Controllers
{
    public class ComentarioViewController : Controller
    {
        private readonly IComentarioApplicationService _applicationService;
        private readonly IUsuarioApplicationService _usuarioService;
        private readonly IPostagemApplicationService _postagemService;

        public ComentarioViewController(
            IComentarioApplicationService applicationService,
            IUsuarioApplicationService usuarioService,
            IPostagemApplicationService postagemService)
        {
            _applicationService = applicationService;
            _usuarioService = usuarioService;
            _postagemService = postagemService;
        }

        public IActionResult Index()
        {
            var comentarios = _applicationService.ObterTodosComentarios();
            return View(comentarios);
        }

        public IActionResult Details(long id)
        {
            var comentario = _applicationService.ObterComentarioPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return View(comentario);
        }

        public IActionResult Create()
        {
            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Postagens = _postagemService.ObterTodasPostagens();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ComentarioRequestDto comentario)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.SalvarComentario(comentario);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Postagens = _postagemService.ObterTodasPostagens();
            return View(comentario);
        }

        public IActionResult Edit(long id)
        {
            var comentario = _applicationService.ObterComentarioPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }

            var requestDto = new ComentarioRequestDto
            {
                NmConteudo = comentario.NmConteudo,
                UsuarioId = comentario.Usuario.IdUsuario,
                PostagemId = comentario.Postagem.IdPostagem,
                IdComentarioParente = comentario.IdComentarioParente
            };

            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Postagens = _postagemService.ObterTodasPostagens();
            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, ComentarioRequestDto comentario)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.EditarComentario(id, comentario);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Postagens = _postagemService.ObterTodasPostagens();
            return View(comentario);
        }

        public IActionResult Delete(long id)
        {
            var comentario = _applicationService.ObterComentarioPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return View(comentario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var result = _applicationService.DeletarComentario(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 