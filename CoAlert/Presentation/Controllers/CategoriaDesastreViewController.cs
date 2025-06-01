using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.CategoriaDesastre;

namespace CoAlert.Presentation.Controllers
{
    public class CategoriaDesastreViewController : Controller
    {
        private readonly ICategoriaDesastreApplicationService _applicationService;

        public CategoriaDesastreViewController(ICategoriaDesastreApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        public IActionResult Index()
        {
            var categorias = _applicationService.ObterTodasCategorias();
            return View(categorias);
        }
        
        public IActionResult Details(long id)
        {
            var categoria = _applicationService.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaDesastreRequestDto categoria)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.SalvarCategoria(categoria);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoria);
        }
        
        public IActionResult Edit(long id)
        {
            var categoria = _applicationService.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var requestDto = new CategoriaDesastreRequestDto
            {
                NmTitulo = categoria.NmTitulo,
                DsCategoria = categoria.DsCategoria,
                NmTipo = categoria.NmTipo
            };

            return View(requestDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, CategoriaDesastreRequestDto categoria)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.EditarCategoria(id, categoria);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoria);
        }
        
        public IActionResult Delete(long id)
        {
            var categoria = _applicationService.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var result = _applicationService.DeletarCategoria(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 