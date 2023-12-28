using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class DishCategory
    {
        public DishCategory()
        {
            Dishes = new HashSet<Dish>();
        }

        public int DishCategoryId { get; set; }
        public string DishCategoryName { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
