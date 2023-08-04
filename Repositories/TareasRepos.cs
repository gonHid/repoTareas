using repoTareas.Models;
using System.Collections;
using System.Linq;

namespace repoTareas.Repositories
{
    public class TareasRepos
    {
        List<Tarea> FakeBdd = new List<Tarea>(); 

        public TareasRepos()
        {
            Tarea t1 = new Tarea();
            t1.id = 1;
            t1.name = "Tarea 1";
            t1.description = "Descripción de la tarea 1";
            t1.status = "TODO";
            t1.user = "usuario1";
            t1.importance = 1;
            Tarea t2 = new Tarea();
            t2.id = 2;
            t2.name = "Tarea 2";
            t2.description = "Descripción de la tarea 2";
            t2.status = "INPROGRESS";
            t2.user = "usuario2";
            t2.importance = 2;
            Tarea t3 = new Tarea();
            t3.id = 3;
            t3.name = "Tarea 3";
            t3.description = "Descripción de la tarea 3";
            t3.status = "REVIEWING";
            t3.user = "usuario3";
            t3.importance = 3;
            Tarea t4 = new Tarea();
            t4.id = 4;
            t4.name = "Tarea 4";
            t4.description = "Descripción de la tarea 4";
            t4.status = "DONE";
            t4.user = "usuario2";
            t4.importance = 1;
            FakeBdd.Add(t1);
            FakeBdd.Add(t2);
            FakeBdd.Add(t3);
            FakeBdd.Add(t4);
        }

        public async Task<List<Tarea>> GetTareas()
        {

            return FakeBdd;
        }

        public async Task<Tarea> GetTareaByID(int id)
        {
            var tareaLINQ = FakeBdd.FirstOrDefault(x => x.id == id);
            return tareaLINQ;
        }

        public async Task<bool> PostNuevaTarea(Tarea tarea)
        {
            tarea.id = FakeBdd.Count();
            FakeBdd.Add(tarea);
            return true;
        }

        public async Task<bool> UpdateTarea(Tarea tarea)
        {
            Tarea tareaGuardada = await GetTareaByID( tarea.id);
            if (tareaGuardada != null)
            {
                tareaGuardada.status = tarea.status;
                tareaGuardada.user = tarea.user;
                tareaGuardada.description = tarea.description;  
                tareaGuardada.name = tarea.name;

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTarea(int id)
        {
            FakeBdd.RemoveAll(t => t.id == id);
            return true;
        }
    }
}
