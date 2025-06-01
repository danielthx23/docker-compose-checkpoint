using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Usuario;

namespace CoAlert.Presentation.Controllers
{
    public class UsuarioViewController : Controller
    {
        private readonly IUsuarioApplicationService _applicationService;

        public UsuarioViewController(IUsuarioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IActionResult Index()
        {
            var usuarios = _applicationService.ObterTodosUsuarios();
            return View(usuarios);
        }

        public IActionResult Details(long id)
        {
            var usuario = _applicationService.ObterUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioRequestDto usuario)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.SalvarDadosUsuario(usuario);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(usuario);
        }

        public IActionResult Edit(long id)
        {
            var usuario = _applicationService.ObterUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var requestDto = new UsuarioRequestDto
            {
                NmUsuario = usuario.NmUsuario,
                NrSenha = "**********",
                NmEmail = usuario.NmEmail
            };

            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, UsuarioRequestDto usuario)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.EditarDadosUsuario(id, usuario);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(usuario);
        }

        public IActionResult Delete(long id)
        {
            var usuario = _applicationService.ObterUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var result = _applicationService.DeletarDadosUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 