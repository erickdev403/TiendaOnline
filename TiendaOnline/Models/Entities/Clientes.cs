using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.Entities
{
    public class Clientes
    {
        [Key]
        public int ClientesID { get; set; }
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
