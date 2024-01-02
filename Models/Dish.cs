using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public partial class Dish
    {
        public int DishId { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string DishName { get; set; } = null!;

        [Required]
        [Display(Name = "Categoria")]
        public int DishCategory { get; set; }

        [Display(Name = "Tiempo de Espera")]
        public int? DishFinishTime { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public int DishPrice { get; set; }

        [Display(Name = "TAC")]
        public byte? DishTac { get; set; }

        public virtual DishCategory DishCategoryNavigation { get; set; } = null!;
    }
}
