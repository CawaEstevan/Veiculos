using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaCamisaGamer.Application.Interfaces;
using LojaCamisaGamer.Application.ViewModels;

namespace LojaCamisaGamer.Web.Controllers
{
    public class CamisaController : Controller
    {
        private readonly ICamisaService _camisaService;
        private readonly ICategoriaService _categoriaService;

        public CamisaController(ICamisaService camisaService, ICategoriaService categoriaService)
        {
            _camisaService = camisaService;
            _categoriaService = categoriaService;
        }

        // GET: Camisa
        public async Task<IActionResult> Index()
        {
            var camisas = await _camisaService.GetAllAsync();
            return View(camisas);
        }

        // GET: Camisa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camisa = await _camisaService.GetByIdAsync(id.Value);
            if (camisa == null)
            {
                return NotFound();
            }

            return View(camisa);
        }

        // GET: Camisa/Create
        public async Task<IActionResult> Create()
        {
            await LoadCategoriasSelectList();
            return View();
        }

        // POST: Camisa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CamisaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _camisaService.CreateAsync(viewModel);
                TempData["Success"] = "Camisa cadastrada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await LoadCategoriasSelectList(viewModel.CategoriaId);
            return View(viewModel);
        }

        // GET: Camisa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camisa = await _camisaService.GetByIdAsync(id.Value);
            if (camisa == null)
            {
                return NotFound();
            }

            await LoadCategoriasSelectList(camisa.CategoriaId);
            return View(camisa);
        }

        // POST: Camisa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CamisaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _camisaService.UpdateAsync(viewModel);
                TempData["Success"] = "Camisa atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await LoadCategoriasSelectList(viewModel.CategoriaId);
            return View(viewModel);
        }

        // GET: Camisa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camisa = await _camisaService.GetByIdAsync(id.Value);
            if (camisa == null)
            {
                return NotFound();
            }

            return View(camisa);
        }

        // POST: Camisa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _camisaService.DeleteAsync(id);
            TempData["Success"] = "Camisa excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Camisa/Search?termo=valorant
        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                return Json(new List<CamisaViewModel>());
            }

            var camisas = await _camisaService.SearchAsync(termo);
            return Json(camisas);
        }

        private async Task LoadCategoriasSelectList(int? selectedId = null)
        {
            var categorias = await _categoriaService.GetAllAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", selectedId);
        }
    }
}