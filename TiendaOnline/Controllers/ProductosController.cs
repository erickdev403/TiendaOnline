using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TiendaOnline.Data;
using TiendaOnline.Models;
using TiendaOnline.Models.Entities;

namespace TiendaOnline.Controllers
{
    public class ProductosController : Controller
    {
        public readonly AplicationDBContext _dBContext;

        public ProductosController(AplicationDBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        public async Task<IActionResult> Add(AddProductosModel model)
        {
            var products = new Productos
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                stock = model.stock
            };
            try
            {
                await _dBContext.Productos.AddAsync(products);
                await _dBContext.SaveChangesAsync();
            }
             catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);  
                throw;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Products = await _dBContext.Productos.ToListAsync();
            return View(Products);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var Products = await _dBContext.Productos.FindAsync(id);
            return View(Products);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Productos productos)
        {
            var products = await _dBContext.Productos.FindAsync(productos.ProductoID);
            if (products is not null)
            {
                products.Nombre = productos.Nombre;
                products.Descripcion = productos.Descripcion;
                products.Precio = productos.Precio;
                products.stock = productos.stock;
                await _dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Productos");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Productos productos)
        {
          
                var products = await _dBContext.Productos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProductoID == productos.ProductoID);
            
            if(products is not null)
            {
                _dBContext.Productos.Remove(products);
                await _dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Productos");
        }
    }
}
