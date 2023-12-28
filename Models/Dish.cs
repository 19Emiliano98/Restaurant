using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Dish
    {
        public int DishId { get; set; }
        public int DishCategory { get; set; }
        public string DishName { get; set; } = null!;
        public int? DishFinishTime { get; set; }
        public int DishPrice { get; set; }
        public byte? DishTac { get; set; }

        public virtual DishCategory DishCategoryNavigation { get; set; } = null!;
    }
}
