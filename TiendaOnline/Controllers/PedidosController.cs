using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TiendaOnline.Data;
using TiendaOnline.Models.Entities;
using TiendaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Controllers
{
    public class PedidosController : Controller
    {
        public readonly AplicationDBContext _dbContext;

        public PedidosController(AplicationDBContext dBContext)
        {
            this._dbContext = dBContext;
        }
        [HttpGet]
        public IActionResult add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPedidosModel model)
        {

            var request = new Pedidos
            {
                ClienteID = Convert.ToInt32(model.ClienteID),
                ProductoID = Convert.ToInt32(model.ProductoID),
                Cantidad = Convert.ToInt32(model.Cantidad),
                Fecha = Convert.ToDateTime(model.Fecha),
            };
            try
            {
                await _dbContext.Pedidos.AddAsync(request);
                await _dbContext.SaveChangesAsync();

            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
                throw;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var request = await _dbContext.Pedidos.ToListAsync();
            return View(request);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var request = await _dbContext.Pedidos.FindAsync(id);
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Pedidos model)
        {
            var request = await _dbContext.Pedidos.FindAsync(model.PedidoID);
            if (request is not null)
            {
                request.ClienteID = Convert.ToInt32(model.ClienteID);
                request.ProductoID = Convert.ToInt32(model.ProductoID);
                request.Cantidad = Convert.ToInt32(model.Cantidad);
                request.Fecha = Convert.ToDateTime(model.Fecha);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Pedidos");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Pedidos pedidos)
        {
            var request = await _dbContext.Pedidos
                .FirstOrDefaultAsync(x => x.PedidoID == pedidos.PedidoID); 
          if(request is not null)
            {
                _dbContext.Pedidos.Remove(request);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Pedidos");
        }
    }
}
