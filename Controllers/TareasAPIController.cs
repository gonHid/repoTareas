using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using repoTareas.Models;
using repoTareas.Services;
using System.Threading;

namespace repoTareas.Controllers
{
    public class TareasAPIController : Controller
    {
        private readonly TareasServices _tareasServices = new TareasServices();

        [HttpGet]
        public async Task<ActionResult> GetTareas()
        {
            var tareas = await _tareasServices.GetTareas();
            return Json(new { tareas });
        }

        [HttpGet]
        public async Task<ActionResult> GetTareaById(int id)
        {
            var tarea = await _tareasServices.GetTareaByID(id);
            return Json(new { tarea });
        }

        // POST: TareasAPIController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Tarea tarea)
        {
            var result = await _tareasServices.PostNuevaTarea(tarea);
            return Json(result);
        }

        // POST: TareasAPIController/Edit/5
        [HttpPost]
        public async Task<ActionResult> AvanzarTarea(int id)
        {
           
            Console.WriteLine("id desde front: " + id);
            var result = await _tareasServices.AvanzarTarea(id);
            return Json(result);
        }

        // POST: TareasAPIController/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteTarea(int id)
        {
            var result = await _tareasServices.DeleteTarea(id);
            return Json(result);
        }
    }
}
