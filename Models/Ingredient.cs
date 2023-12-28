using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Ingredient
    {
        public int IngredientsId { get; set; }
        public string IngredientsName { get; set; } = null!;
    }
}
