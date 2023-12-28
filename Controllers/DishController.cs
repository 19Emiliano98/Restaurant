using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

namespace Restaurant.Controllers
{
    public class DishController : Controller
    {
        private readonly RestaurantContext _context;

        public DishController(RestaurantContext context) => _context = context;

        public async Task<IActionResult> GetDishes()
        {
            var dishes = _context.Dishes.Include(x => x.DishCategoryNavigation );

            return View( await dishes.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["PlatoNuevo"] = new SelectList(_context.DishCategories, "DishCategoryId", "DishCategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishViewModel model)
        {

            if(ModelState.IsValid)
            {
                var dish = new Dish()
                {
                    DishName = model.DishName,
                    DishCategory = model.DishCategoryId,
                    DishFinishTime = model.DishFinishTime,
                    DishPrice = model.DishPrice,
                    DishTac = model.DishTac
                };

                _context.Add(dish);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetDishes));
            }

            ViewData["PlatoNuevo"] = new SelectList(_context.DishCategories, "DishCategoryId", "DishCategoryName", model.DishCategoryId);

            return View();
        }
    }
}
