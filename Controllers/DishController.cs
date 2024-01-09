using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.Models.ViewModels;
using System.Data;
using System.Net;
using System.Reflection;

namespace Restaurant.Controllers
{
    public class DishController : Controller
    {
        private readonly RestaurantContext _context;

        public DishController(RestaurantContext context) => _context = context;

        public async Task<IActionResult> GetDishes()
        {
            var dishes = _context.Dishes.Include(x => x.DishCategoryNavigation);

            return View(await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dishes == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FirstOrDefaultAsync(m => m.DishId == id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        //
        // Post
        //

        public IActionResult Create()
        {
            ViewData["PlatoNuevo"] = new SelectList(_context.DishCategories, "DishCategoryId", "DishCategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishViewModel model)
        {

            if (ModelState.IsValid)
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
            // Este viewdata lo dejo comentado porque yo pienso que no es necesario, voy a hacer testing antes de borrarlo definitivamente

            return View();
        }

        //
        // Update
        //

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dishes == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);
            
            if (dish == null)
            {
                return NotFound();
            }

            ViewData["EditPlato"] = new SelectList(_context.DishCategories, "DishCategoryId", "DishCategoryName");

            

            return View(dish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("DishId,DishName,DishCategory,DishFinishTime,DishPrice,DishTac")] Dish dish)
        {

            if (id != dish.DishId)
            {
                return NotFound();
            }


            //if (ModelState.IsValid)
            //{

            //    try
            //    {
            //        _context.Update(dish);

            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!UsuarioExists(dish.DishId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(GetDishes));
            //}
            
            ViewData["EditPlato"] = new SelectList(_context.DishCategories, "DishCategoryId", "DishCategoryName");

            try
            {
                _context.Update(dish);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(dish.DishId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(GetDishes));


            //return View(dish);
        }

        //
        //Delete
        //

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dishes == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FirstOrDefaultAsync(m => m.DishId == id);
            
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dishes == null)
            {
                return Problem("Entity set 'RestaurantContext.Dish'  is null.");
            }
            
            var dish = await _context.Dishes.FindAsync(id);
            
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(GetDishes));
        }


        private bool UsuarioExists(int dishId)
        {
            return (_context.Dishes?.Any(e => e.DishId == dishId)).GetValueOrDefault();
        }
    }
}
