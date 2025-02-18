using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Data;
using TiendaOnline.Models;
using TiendaOnline.Models.Entities;

namespace TiendaOnline.Controllers
{
    public class ClientesController : Controller
    {
        public readonly AplicationDBContext AplicationDBContext;
        public ClientesController(AplicationDBContext dBContext)
        {
            this.AplicationDBContext = dBContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddClientesModel model)
        {

            var clients = new Clientes
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Email = model.Email,
                Telefono = model.Telefono
            };
            try
            {
                await AplicationDBContext.Clientes.AddAsync(clients);
                await AplicationDBContext.SaveChangesAsync();
              
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
            var clients = await AplicationDBContext.Clientes.ToListAsync();
            return View(clients);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var clients = await AplicationDBContext.Clientes.FindAsync(id);
            return View(clients);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Clientes clientes)
        {
           var clients = await AplicationDBContext.Clientes.FindAsync(clientes.ClientesID);
            if (clients is not null)
            {
                clients.Nombre= clientes.Nombre;
                clients.Apellido= clientes.Apellido;
                clients.Email= clientes.Email;
                clients.Telefono= clientes.Telefono;

                await AplicationDBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Clientes");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Clientes clientes)
        {
            var clients = await AplicationDBContext.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClientesID== clientes.ClientesID);
            if (clients is not null)
            {
                AplicationDBContext.Clientes.Remove(clients);
                await AplicationDBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Clientes");
        }
    }
}
