using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.ViewModels
{
    public class DishViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string DishName { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int DishCategoryId {  get; set; }

        [Display(Name = "Tiempo de Espera")]
        public int DishFinishTime { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public int DishPrice { get; set; }

        [Display(Name = "TAC")]
        public byte DishTac {  get; set; }
    }
}
