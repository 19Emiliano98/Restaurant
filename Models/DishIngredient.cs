using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class DishIngredient
    {
        public int DishIngredientsId { get; set; }
        public int IngredientsId { get; set; }

        public virtual Ingredient Ingredients { get; set; } = null!;
    }
}
