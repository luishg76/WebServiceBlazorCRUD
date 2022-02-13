using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServiceBlazorCRUD.Models;

namespace WebServiceBlazorCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CervezaController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Cerveza>>> Get() 
        {
           using (BlazorCRUDContext db = new BlazorCRUDContext())
           {
              return await db.Cervezas.ToListAsync();
           }
        }

        [HttpGet("{id}", Name ="detalles_persona")]
        public async Task<ActionResult<Cerveza>> Get(int id)
        {
            using(BlazorCRUDContext db=new BlazorCRUDContext())
            {
                return await db.Cervezas.FirstOrDefaultAsync(c => c.Id == id);
            }
        }

        [HttpPost]
        public ActionResult Add(Cerveza model)
        {
           using (BlazorCRUDContext db = new BlazorCRUDContext())
           {
               Cerveza nueva = new Cerveza();
               nueva.Nombre = model.Nombre;
               nueva.Marca = model.Marca;
               db.Cervezas.Add(nueva);
               db.SaveChangesAsync();
                return new CreatedAtRouteResult("detalles_persona", new { id = nueva.Id }, nueva);
           }           
        }

        [HttpPut]
        public ActionResult Edit(Cerveza model)
        {
           using (BlazorCRUDContext db = new BlazorCRUDContext())
           {
              Cerveza modificada = db.Cervezas.FirstOrDefault(c=>c.Id==model.Id);
              modificada.Nombre = model.Nombre;
              modificada.Marca = model.Marca;
              db.Entry(modificada).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
              db.SaveChanges();                    
           }          
           return Ok();            
        }

        [HttpDelete("{Id}")]
        public ActionResult Delet(int Id)
        {
            using (BlazorCRUDContext db = new BlazorCRUDContext())
            {
               Cerveza eliminar = db.Cervezas.FirstOrDefault(c => c.Id == Id);
               db.Cervezas.Remove(eliminar);
               db.SaveChanges();
            }
                     
            return Ok();
        }
    }
}
