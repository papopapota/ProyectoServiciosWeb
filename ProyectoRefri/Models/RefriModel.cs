using System.ComponentModel.DataAnnotations;

namespace ProyectoRefri.Models
{
    public class RefriModel
    {
        [Required] public int idRefri { get; set; }
        [Required] public string? idProducto { get; set; }
        [Required] public string? nombre { get; set; }
        [Required] public int cantidad { get; set; }
        

    }
}
