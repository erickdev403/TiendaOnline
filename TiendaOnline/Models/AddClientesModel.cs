using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaOnline.Models
{
    public class AddClientesModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ClientesID { get; set; }
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
