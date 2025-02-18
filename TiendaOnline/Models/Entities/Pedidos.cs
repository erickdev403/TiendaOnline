using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.Entities
{
    public class Pedidos
    {
        [Key]
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
