using System.ComponentModel.DataAnnotations;

namespace ProyectoRefri.Models
{
    public class RecetaModel
    {
        [Required]
        [Display(Name = "Id")]
        public int idReceta { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Imagen")]
        public string imagen { get; set; }

        [Required]
        [Display(Name = "Preparación")]
        public string preparacion { get; set; }
       
    }
}
