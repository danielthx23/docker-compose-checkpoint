using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Localizacao;

namespace CoAlert.Presentation.Controllers
{
    public class LocalizacaoViewController : Controller
    {
        private readonly ILocalizacaoApplicationService _applicationService;

        public LocalizacaoViewController(ILocalizacaoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IActionResult Index()
        {
            var localizacoes = _applicationService.ObterTodasLocalizacoes();
            return View(localizacoes);
        }

        public IActionResult Details(long id)
        {
            var localizacao = _applicationService.ObterLocalizacaoPorId(id);
            if (localizacao == null)
            {
                return NotFound();
            }
            return View(localizacao);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LocalizacaoRequestDto localizacao)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.SalvarLocalizacao(localizacao);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(localizacao);
        }

        public IActionResult Edit(long id)
        {
            var localizacao = _applicationService.ObterLocalizacaoPorId(id);
            if (localizacao == null)
            {
                return NotFound();
            }

            var requestDto = new LocalizacaoRequestDto
            {
                NmBairro = localizacao.NmBairro,
                NmLogradouro = localizacao.NmLogradouro,
                NrNumero = localizacao.NrNumero,
                NmCidade = localizacao.NmCidade,
                NmEstado = localizacao.NmEstado,
                NrCep = localizacao.NrCep,
                NmPais = localizacao.NmPais,
                DsComplemento = localizacao.DsComplemento
            };

            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, LocalizacaoRequestDto localizacao)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.EditarLocalizacao(id, localizacao);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(localizacao);
        }

        public IActionResult Delete(long id)
        {
            var localizacao = _applicationService.ObterLocalizacaoPorId(id);
            if (localizacao == null)
            {
                return NotFound();
            }
            return View(localizacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var result = _applicationService.DeletarLocalizacao(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 