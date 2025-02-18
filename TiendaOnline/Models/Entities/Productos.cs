using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.Entities
{
    public class Productos
    {
        [Key]
        public int ProductoID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio {  get; set; }
        public int stock { get; set; }
    }
}
