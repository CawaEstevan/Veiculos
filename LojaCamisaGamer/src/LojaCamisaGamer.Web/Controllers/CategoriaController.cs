using Microsoft.AspNetCore.Mvc;
using LojaCamisaGamer.Application.Interfaces;
using LojaCamisaGamer.Application.ViewModels;

namespace LojaCamisaGamer.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return View(categorias);
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaService.GetByIdWithCamisasAsync(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _categoriaService.CreateAsync(viewModel);
                TempData["Success"] = "Categoria criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaService.GetByIdAsync(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoriaService.UpdateAsync(viewModel);
                TempData["Success"] = "Categoria atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaService.GetByIdAsync(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriaService.DeleteAsync(id);
            TempData["Success"] = "Categoria excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Categoria/Search?termo=fps
        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                return Json(new List<CategoriaViewModel>());
            }

            var categorias = await _categoriaService.SearchAsync(termo);
            return Json(categorias);
        }
    }
}