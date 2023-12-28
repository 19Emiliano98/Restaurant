using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

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
    }
}
